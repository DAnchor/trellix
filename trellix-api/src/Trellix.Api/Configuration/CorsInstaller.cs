namespace Trellix.Api.Configuration;

public static class CorsInstaller
{
    public static void AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: "CorsPolicy", corsPolicyBuilder => 
                corsPolicyBuilder
                    .WithOrigins("http://localhost:5033")
                    .AllowAnyHeader()
                    .AllowAnyMethod());
        });
    }
} 