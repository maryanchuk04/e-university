using System.Net;
using System.Text;
using EUniversity.Core.Exceptions;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EUniversity.Core.Http;

public abstract class MicroservicesClientBase<T>
    where T : MicroservicesClientBase<T>
{
    private readonly Uri _endpointUri;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly TimeSpan _defaultClientTimeout = TimeSpan.FromSeconds(100);

    protected ILogger<T> _logger { get; private set; }

    protected static readonly JsonSerializerSettings _settings = new()
    {
        Formatting = Formatting.None,
        NullValueHandling = NullValueHandling.Ignore,
        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
    };

    #region ctors

    protected MicroservicesClientBase(
        string endpoint,
        IHttpClientFactory httpClientFactory,
        ILogger<T> logger,
        TimeSpan? timeout = null)
    {
        if (!Uri.TryCreate(endpoint, UriKind.Absolute, out var endpointUri))
        {
            throw new ArgumentException("The endpoint for the backend API should be a well formed URI", nameof(endpoint));
        }

        _logger = logger.ThrowIfNull();
        _httpClientFactory = httpClientFactory.ThrowIfNull();

        _endpointUri = endpointUri;

        if (timeout?.TotalSeconds == 0)
        {
            _logger.LogWarning("Client http timeout was configured as 0 seconds. This would result in complete breakage. Defaulting to 120 seconds. Please fix the azure app configuration for this client timeout setting.");
            timeout = TimeSpan.FromSeconds(120);
        }

        _defaultClientTimeout = timeout ?? TimeSpan.FromSeconds(120);
    }

    #endregion

    #region Http methods

    protected async Task<TResponse> GetAsync<TResponse>(string relativeUri, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null,
            CancellationToken cancellationToken = default)
    {
        return await PerformCallAsync<object, TResponse>(relativeUri, HttpMethod.Get, default, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, cancellationToken: cancellationToken);
    }

    protected async Task<TResponse> PostAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null, TimeSpan? timeout = null)
    {
        return await PerformCallAsync<TPayload, TResponse>(relativeUri, HttpMethod.Post, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, timeout);
    }

    protected async Task<TResponse> PutAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null, CancellationToken cancellationToken = default)
    {
        return await PerformCallAsync<TPayload, TResponse>(relativeUri, HttpMethod.Put, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders, cancellationToken: cancellationToken);
    }

    protected Task<TResponse> DeleteJsonAsync<TResponse>(string relativeUri, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null)
    {
        return DeleteJsonAsync<object, TResponse>(relativeUri, default, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders);
    }

    protected async Task<TResponse> DeleteJsonAsync<TPayload, TResponse>(string relativeUri, TPayload payload, IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null, Dictionary<string, string> oneTimeCustomHeaders = null)
    {
        return await PerformCallAsync<object, TResponse>(relativeUri, HttpMethod.Delete, payload, acceptableNonSuccessStatusCodes, oneTimeCustomHeaders);
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

        return http;
    }

    private async Task<TResponse?> PerformCallAsync<TPayload, TResponse>(
        string relativeUri,
        HttpMethod httpMethod,
        TPayload? payload,
        IEnumerable<HttpStatusCode> acceptableNonSuccessStatusCodes = null,
        Dictionary<string, string> oneTimeCustomHeaders = null,
        TimeSpan? timeout = null,
        CancellationToken cancellationToken = default)
    {
        var http = GetHttpClient(timeout);
        var request = new HttpRequestMessage(httpMethod, relativeUri);

        if (!EqualityComparer<TPayload>.Default.Equals(payload, default))
        {
            request.Content = GetJsonStringContent(payload);
        }

        if (oneTimeCustomHeaders != null)
        {
            foreach (var customHeader in oneTimeCustomHeaders)
            {
                request.Headers.TryAddWithoutValidation(customHeader.Key, customHeader.Value);
            }
        }

        var responseMessage = await http.SendAsync(request, cancellationToken);
        return await ParseResponse<TResponse>(responseMessage, relativeUri, httpMethod, acceptableNonSuccessStatusCodes, cancellationToken);
    }

    private static StringContent GetJsonStringContent<TPayloadType>(TPayloadType payload)
    {
        var json = JsonConvert.SerializeObject(payload, _settings);
        return new StringContent(json, Encoding.UTF8, "application/json");
    }

    private async Task<TResponse?> ParseResponse<TResponse>(HttpResponseMessage response, string relativeUri, HttpMethod httpMethod, IEnumerable<HttpStatusCode>? acceptableNonSuccessStatusCodes = null, CancellationToken cancellationToken = default)
    {
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        acceptableNonSuccessStatusCodes ??= [];

        if (!response.IsSuccessStatusCode && !acceptableNonSuccessStatusCodes.Contains(response.StatusCode))
        {
            _logger.LogWarning("Unexpected response code of {statusCode} from {endpointUri}{relativeUri} ({httpMethod}): {responseContent}. {@acceptableNonSuccessStatusCodes}", response.StatusCode, _endpointUri, relativeUri, httpMethod, responseContent, acceptableNonSuccessStatusCodes);

            throw new ServiceClientUnexpectedResponseCodeException(responseContent)
            {
                Method = httpMethod.ToString(),
                RootUri = _endpointUri,
                RelativeUri = relativeUri,
                StatusCode = (int)response.StatusCode,
                ExpectedStatusCodes = acceptableNonSuccessStatusCodes.Select(c => (int)c).ToArray()
            };
        }

        try
        {
            if (string.IsNullOrEmpty(responseContent))
                return default;

            return JsonConvert.DeserializeObject<TResponse>(responseContent);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Unexpected response content from {endpointUri}{relativeUri} ({httpMethod}): {responseContent}. {expectedReponseType}", _endpointUri, relativeUri, httpMethod, responseContent, typeof(TResponse));
            throw new ServiceClientUnexpectedResponseContentException(ex.Message)
            {
                Method = httpMethod.ToString(),
                RootUri = _endpointUri,
                RelativeUri = relativeUri,
                StatusCode = (int)response.StatusCode,
                Content = responseContent
            };
        }
    }

    #endregion
}
