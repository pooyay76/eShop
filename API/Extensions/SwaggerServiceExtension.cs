using Microsoft.OpenApi.Models;

namespace API.Extensions
{
    public static class SwaggerServiceExtension
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorisation",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement()
                {
                    {
                        securityScheme,new[]{"Bearer"}
                    }
                };
                c.AddSecurityRequirement(securityRequirements);

            });
            return services;
        }
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
