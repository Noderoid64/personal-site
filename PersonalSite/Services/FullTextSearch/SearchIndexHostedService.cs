
using System.Diagnostics;

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
        
        Console.WriteLine("SearchIndex job started...");
        
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        Console.WriteLine("DoWork started...");
        var sw = new Stopwatch();
        sw.Start();
        var service = TextSearchIndex.GetInstance();
        await service.FillNewIndexAsync(_configuration);
        sw.Stop();
        Console.WriteLine("DoWork end in " + sw.ElapsedMilliseconds + " ms.");
    }

    public void Dispose()
    {
        _timer.Dispose();
    }
}