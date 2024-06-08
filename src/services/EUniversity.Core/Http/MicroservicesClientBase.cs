using System.Net;
using System.Text;
using EUniversity.Core.Exceptions;
using EUniversity.Shared.Constants;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EUniversity.Core.Http;

public abstract class MicroservicesClientBase<T>
    where T : MicroservicesClientBase<T>
{
    private readonly Uri _endpointUri;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _apiKey;
    private readonly TimeSpan _defaultClientTimeout;

    protected ILogger<T> Logger { get; private set; }

    protected static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    #region ctors

    protected MicroservicesClientBase(
        string endpoint,
        string apiKey,
        IHttpClientFactory httpClientFactory,
        ILogger<T> logger,
        TimeSpan? timeout = null)
    {
        if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var endpointUri))
        {
            throw new ArgumentException("The endpoint for the backend API should be a well-formed URI", nameof(endpoint));
        }

        Logger = logger.ThrowIfNull();
        _httpClientFactory = httpClientFactory.ThrowIfNull();
        _endpointUri = endpointUri;
        _apiKey = apiKey.ThrowIfNull();
        _defaultClientTimeout = timeout ?? TimeSpan.FromSeconds(120);

        if (timeout?.TotalSeconds == 0)
        {
            Logger.LogWarning("Client HTTP timeout was configured as 0 seconds. Defaulting to 120 seconds. Please fix the Azure app configuration for this client timeout setting.");
        }
    }

    #endregion

    #region Http methods

    protected async Task<TResponse> GetAsync<TResponse>(string relativeUri, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null,
            CancellationToken cancellationToken = default)
    {
        return await PerformCallAsync<object, TResponse>(relativeUri, HttpMethod.Get, default, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, cancellationToken: cancellationToken);
    }

    protected async Task<TResponse> PostAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null, TimeSpan? timeout = null, CancellationToken cancellationToken = default)
    {
        return await PerformCallAsync<TPayload, TResponse>(relativeUri, HttpMethod.Post, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, timeout, cancellationToken);
    }

    protected async Task<TResponse> PutAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null, CancellationToken cancellationToken = default)
    {
        return await PerformCallAsync<TPayload, TResponse>(relativeUri, HttpMethod.Put, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, cancellationToken: cancellationToken);
    }

    protected Task<TResponse> DeleteJsonAsync<TResponse>(string relativeUri, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null)
    {
        return PerformCallAsync<object, TResponse>(relativeUri, HttpMethod.Delete, default, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders);
    }

    protected async Task<TResponse> PatchJsonAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null)
    {
        return await PerformCallAsync<TPayload, TResponse>(relativeUri, HttpMethod.Patch, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders);
    }

    #endregion

    #region Internal functions

    private HttpClient GetHttpClient(TimeSpan? timeout = null)
    {
        var http = _httpClientFactory.CreateClient();

        http.DefaultRequestHeaders.Clear();
        http.BaseAddress = _endpointUri;
        http.Timeout = timeout ?? _defaultClientTimeout;
        http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        if (!string.IsNullOrEmpty(_apiKey))
            http.DefaultRequestHeaders.TryAddWithoutValidation(SharedApiKeyContants.HeaderName, _apiKey);

        return http;
    }

    private async Task<TResponse> PerformCallAsync<TPayload, TResponse>(
        string relativeUri,
        HttpMethod httpMethod,
        TPayload? payload,
        IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null,
        Dictionary<string, string> oneTimeCustomHeaders = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default)
    {
        var httpClient = GetHttpClient(timeout);
        var request = new HttpRequestMessage(httpMethod, relativeUri);

        if (!EqualityComparer<TPayload>.Default.Equals(payload, default))
        {
            request.Content = GetJsonStringContent(payload);
        }

        AddCustomHeaders(request, oneTimeCustomHeaders);

        var responseMessage = await httpClient.SendAsync(request, cancellationToken);
        return await ParseResponse<TResponse>(responseMessage, relativeUri, httpMethod, acceptableNonSuccessStatusCodes, cancellationToken);
    }

    private static void AddCustomHeaders(HttpRequestMessage request, Dictionary<string, string>? headers)
    {
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }
        }
    }

    private static StringContent GetJsonStringContent<TPayloadType>(TPayloadType payload)
    {
        var json = JsonConvert.SerializeObject(payload, JsonSerializerSettings);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<TResponse> ParseResponse<TResponse>(HttpResponseMessage response, string relativeUri, HttpMethod httpMethod, IEnumerable<HttpStatusCode>? acceptableNonSuccessStatusCodes = null, CancellationToken cancellationToken = default)
    {
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        acceptableNonSuccessStatusCodes ??= [];

        if (!response.IsSuccessStatusCode && !acceptableNonSuccessStatusCodes.Contains(response.StatusCode))
        {
            Logger.LogWarning("Unexpected response code of {statusCode} from {endpointUri}{relativeUri} ({httpMethod}): {responseContent}. {@acceptableNonSuccessStatusCodes}", response.StatusCode, _endpointUri, relativeUri, httpMethod, responseContent, acceptableNonSuccessStatusCodes);

            throw new ServiceClientUnexpectedResponseCodeException(responseContent)
            {
                Method = httpMethod.ToString(),
                RootUri = _endpointUri,
                RelativeUri = relativeUri,
                StatusCode = (int)response.StatusCode,
                ExpectedStatusCodes = acceptableNonSuccessStatusCodes.Select(c => (int)c).ToArray()
            };
        }

        return DeserializeResponse<TResponse>(responseContent, relativeUri, httpMethod, response.StatusCode);
    }

    private TResponse? DeserializeResponse<TResponse>(string responseContent, string relativeUri, HttpMethod httpMethod, HttpStatusCode statusCode)
    {
        if (string.IsNullOrEmpty(responseContent))
            return default;

        try
        {
            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Unexpected response content from {endpointUri}{relativeUri} ({httpMethod}): {responseContent}. {expectedResponseType}", _endpointUri, relativeUri, httpMethod, responseContent, typeof(TResponse));
            throw new ServiceClientUnexpectedResponseContentException(ex.Message)
            {
                Method = httpMethod.ToString(),
                RootUri = _endpointUri,
                RelativeUri = relativeUri,
                StatusCode = (int)statusCode,
                Content = responseContent
            };
        }
    }

    #endregion
}
