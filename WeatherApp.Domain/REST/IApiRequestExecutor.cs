using System.Threading.Tasks;

namespace WeatherApp.Domain.REST
{
    public interface IApiRequestExecutor
    {
        Task<TResponse> GetForecastAsync<TResponse>(string cityKey);
        Task<TResponse> GetLocationAsync<TResponse>(string request);
    }
}