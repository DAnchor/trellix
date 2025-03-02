namespace Trellix.WebApp.Configuration.Security;

public static class HeadersInstaller
{
    public static IApplicationBuilder UseHeaderInstaller(this IApplicationBuilder app)
    {
        return app.Use(async (ctx, next) =>
        {
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Strict-Transport-Security
            ctx.Response.Headers.Append("Strict-Transport-Security", "max-age=5184000; includeSubDomains; preload");
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Frame-Options
            ctx.Response.Headers.Append("X-Frame-Options", "DENY");
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-XSS-Protection
            ctx.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/X-Content-Type-Options
            ctx.Response.Headers.Append("X-Content-Type-Options", "nosniff");
            // https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Cache-Control
            ctx.Response.Headers.Append("Cache-Control", "no-cache");

            await next();
        });
    }
}