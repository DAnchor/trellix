namespace Trellix.WebApp.Configuration.Compression;

using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

public static class WebCompressionInstaller
{
    public static void AddWebContentCompression(this IServiceCollection services)
    {
        services.AddResponseCompression(opt =>
        {
            opt.Providers.Add<GzipCompressionProvider>();
            opt.EnableForHttps = true;
            opt.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat
            ([
                "application/json", "image/jpg", "image/jpeg",
                "image/jpe", "image/svg+xml", "	image/gif",
                "text/css", "text/html", "text/javascript"
            ]);
        });
        services.Configure<GzipCompressionProviderOptions>(opt =>
        {
            opt.Level = CompressionLevel.Optimal;
        });
    }
}

