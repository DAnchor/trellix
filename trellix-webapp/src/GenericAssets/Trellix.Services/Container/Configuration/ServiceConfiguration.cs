namespace Trellix.Services.Container.Configuration;

using Trellix.Services.Container;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;

public static class ServiceConfiguration
{
    public static void AddServicesConfiguration
    (
        this IServiceCollection service,
        IConfiguration configuration,
        string httpClientName,
        string ApiUri
    )
    {
        service.AddHttpClient(configuration[httpClientName] ?? string.Empty, client =>
        {
            client.BaseAddress = new Uri(configuration[ApiUri] ?? string.Empty);
            client.Timeout = new TimeSpan(0, 0, 60);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        });

        service.AddScoped<IAttachmentService, AttachmentService>();
        service.AddScoped<IResponseHeaderService, ResponseHeaderService>();
        service.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    }
}

