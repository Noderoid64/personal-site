using System.Diagnostics;
using Serilog;

namespace PersonalSite.Infrastructure.Serilog;

public class LogMiddleware
{
    private readonly RequestDelegate _next;

    public LogMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        
        var sw = new Stopwatch();
        sw.Start();
        await _next(context);
        sw.Stop();
        Log.Information("{method} request '{path}' finished in {elapsed:000} ms", context.Request.Method, context.Request.Path, sw.ElapsedMilliseconds);
    }
}