using Serilog;
using Serilog.Events;

namespace PersonalSite.Infrastructure.Serilog;

public static class SerilogExtensions
{
    public static void AddSerilog(this WebApplicationBuilder builder)
    {
        var isDebug = string.Equals((string) builder.Configuration.GetSection("Logging:LogLevel:Serilog").Value, "Debug");
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(isDebug ? LogEventLevel.Debug : LogEventLevel.Information)
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Serilog has been initialized in {Debug} mode", isDebug ? "Debug" : "Information");

    }
    
    public static IApplicationBuilder UseRequestLoggingViaSerilog(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<LogMiddleware>();
    }
}