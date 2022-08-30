using PersonalSite.Infrastructure.EF;
using PersonalSite.Infrastructure.SimpleInject;
using PersonalSite.Infrastructure.Swagger;
using PersonalSite.Services.Auth;
using PersonalSite.Services.FullTextSearch;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);
var container = new Container();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddHostedService<SearchIndexHostedService>();
builder.AddSimpleInjectorDi(container);

Console.WriteLine("Trying to create a db ");
using (var context = new ApplicationContext(builder.Configuration))
{
    if (context.Database.EnsureCreated())
    {
        Console.WriteLine("Creating db...");
    }
}

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

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();