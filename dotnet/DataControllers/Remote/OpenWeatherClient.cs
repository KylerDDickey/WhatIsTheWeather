using DataControllers.Abstract;

namespace DataControllers.Remote;

public sealed class OpenWeatherClient : IOpenWeatherClient
{
    private readonly HttpClient _httpClient;

    public OpenWeatherClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}
