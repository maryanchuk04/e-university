using System.Net.Http;
using EUniversity.Shared.Extensions;
using Microsoft.Extensions.Logging;

namespace EUniversity.Schedule.Manager.Client.Factories;

/// <summary>
/// Factory that`s create instance of IScheduleManagerClient.
/// </summary>
public interface IScheduleManagerClientFactory
{
    IScheduleManagerClient Create(string baseUrl, string apiKey);
}

/// <inheritdoc/>
public class ScheduleManagerClientFactory(IHttpClientFactory httpClientFactory, ILogger<ScheduleManagerClient> logger) : IScheduleManagerClientFactory
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory.ThrowIfNull();
    private readonly ILogger<ScheduleManagerClient> _logger = logger.ThrowIfNull();

    public IScheduleManagerClient Create(string baseAddress, string apiKey)
    {
        return new ScheduleManagerClient(baseAddress, apiKey, _httpClientFactory, _logger);
    }
}
