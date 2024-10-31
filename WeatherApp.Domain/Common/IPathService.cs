using System.Threading.Tasks;

namespace WeatherApp.Domain.Common
{
    public interface IPathService
    {
        Task<string> InitializeAsync();
        Task SaveAsync(string settings);
    }
}