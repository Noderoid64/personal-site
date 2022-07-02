using Microsoft.OpenApi.Models;

namespace PersonalSite.Infrastructure.Swagger;

public static class SwaggerExtension
{
    public static void AddSwagger(this IServiceCollection sc)
    {
        sc.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo() { 
                Title = "Personal site Api", 
                Version = "v1" 
            });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
                In = ParameterLocation.Header, 
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey 
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme 
                    { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" 
                        } 
                    },
                    new string[] { } 
                } 
            });
        });
    }
}