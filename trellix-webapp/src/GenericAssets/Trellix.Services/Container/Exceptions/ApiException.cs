namespace Trellix.Services.Exceptions;

using System.Net;

public class ApiException : Exception
{
    public HttpStatusCode StatusCode { get; init; }
    public string BadRequestMessage { get; init; }

    public ApiException(HttpStatusCode statusCode, string message) :base(message)
    {
        StatusCode = statusCode;
        BadRequestMessage = statusCode == HttpStatusCode.BadRequest ? BadRequestMessage = message : "";
    }
}