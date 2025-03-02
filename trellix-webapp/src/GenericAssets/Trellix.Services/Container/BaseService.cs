namespace Trellix.Services.Container;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using Trellix.Services.Exceptions;

public class BaseService
(
    IHttpClientFactory httpClientFactory,
    IConfiguration configuration,
    ILogger<BaseService> logger
)
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    protected readonly IConfiguration _configuration = configuration;
    protected readonly ILogger<BaseService> _logger = logger;
    
    protected HttpClient GetHttpClient() => 
        _httpClientFactory.CreateClient(_configuration["HttpClientName"] ?? string.Empty);

    protected async Task<TResponse> GetRequest<TResponse>(string uri)
    {
        var httpClient = GetHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            await ThrowWebApiException(uri, response.StatusCode, await response.Content.ReadAsStringAsync() ?? string.Empty);
        }

        var responseCallback = JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync());

        return responseCallback;
    }

    protected async Task<MemoryStream> DownloadRequest<TResponse>(string uri)
    {
        var httpClient = GetHttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            await ThrowWebApiException(uri, response.StatusCode, await response.Content.ReadAsStringAsync() ?? string.Empty);
        }

        MemoryStream responseCallback = (MemoryStream)await response.Content.ReadAsStreamAsync();

        return responseCallback;
    }

    protected async Task<TResponse> PostFromForm<TResponse>(string uri, HttpContent content)
    {
        var httpClient = GetHttpClient();
        var response = await httpClient.PostAsync(uri, content);

        if (!response.IsSuccessStatusCode)
        {
            await ThrowWebApiException(uri, response.StatusCode, await response.Content.ReadAsStringAsync() ?? string.Empty);
        }

        var responseCallback = await response.Content.ReadAsAsync<TResponse>();

        return responseCallback;
    }

    private Task ThrowWebApiException(string uri, HttpStatusCode responseStatusCode, string? responseAsString = null)
    {
        if (!string.IsNullOrEmpty(responseAsString) && responseStatusCode.ToString().Contains("BadRequest"))
        {
            _logger.LogError($"{responseStatusCode} returned from {uri} with message {responseAsString}");

            throw new ApiException(responseStatusCode, responseAsString);
        }
        
        _logger.LogError($"{responseStatusCode} returned from {uri} with message {responseAsString}");

        throw new Exception(responseAsString);
    }
}
