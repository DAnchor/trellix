namespace Trellix.Services.Container;

using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;
using System.Text;

public interface IResponseHeaderService
{
    Task SetCspHeader(dynamic viewBag, HttpResponse response);
}

public class ResponseHeaderService : IResponseHeaderService
{
    public Task SetCspHeader(dynamic viewBag, HttpResponse response)
    {
        var sha256NonceValue = string
            .Concat(Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(
                $"Hello World!{new Random().Next(0, 9999)}Hello World!"
            ))).Take(24));

        viewBag.Sha256NonceValue = sha256NonceValue;

        response.Headers.Add(
            "Content-Security-Policy", $"script-src 'self' 'nonce-{sha256NonceValue}', " +
            $"script-src-elem 'self' 'nonce-{sha256NonceValue}'");

        return Task.CompletedTask;
    }
}
