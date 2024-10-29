using System.Threading.Tasks;

namespace WeatherApp.Domain.REST
{
    public interface IApiRequestExecutor
    {
        Task<TResponse> GetAsync<TResponse>(string request);
    }
}