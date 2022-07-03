using PersonalSite.Infrastructure.SimpleInject;
using PersonalSite.Infrastructure.Swagger;
using PersonalSite.Services.Auth;
using SimpleInjector;

var builder = WebApplication.CreateBuilder(args);
var container = new Container();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddAuth(builder.Configuration);
builder.AddSimpleInjectorDi(container);

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