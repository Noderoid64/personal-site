
using System.Diagnostics;
using Serilog;

namespace PersonalSite.Services.FullTextSearch;

public class SearchIndexHostedService : IHostedService, IDisposable
{
    private Timer _timer;

    private IConfiguration _configuration;

    public SearchIndexHostedService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMinutes(5));
        
        Log.Information("SearchIndexService scheduled");
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        
        Log.Information("SearchIndexService stopped");

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        Log.Information("SearchIndex updating started...");
        try
        {
            var sw = new Stopwatch();
            sw.Start();
            var service = TextSearchIndex.GetInstance();
            await service.FillNewIndexAsync(_configuration);
            sw.Stop();
            Log.Information("SearchIndex updating finished in {sw:000}ms", sw.ElapsedMilliseconds);
        }
        catch (Exception e)
        {
            Log.Error("SearchIndex failed {Error}", e);
        }
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}