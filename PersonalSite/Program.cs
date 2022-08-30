using PersonalSite.Infrastructure.Common;
using PersonalSite.Infrastructure.EF;
using PersonalSite.Infrastructure.Serilog;
using PersonalSite.Infrastructure.SimpleInject;
using PersonalSite.Infrastructure.Swagger;
using PersonalSite.Services.Auth;
using PersonalSite.Services.FullTextSearch;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);
var container = new Container();

builder.AddSerilog();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddHostedService<SearchIndexHostedService>();
builder.Services.AddHttpsRedirection(options =>
{
    options.HttpsPort = 5000;
});
builder.AddSimpleInjectorDi(container);
builder.AddEF();

var app = builder.Build();
app.Services.AddSimpleInjectorDi(container);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
    app.UseCors(x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
    });
}

app.UseRequestLoggingViaSerilog();
app.ConfigureExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();