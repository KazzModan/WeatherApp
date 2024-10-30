using System.Threading.Tasks;

namespace WeatherApp.ViewModels.MainPage
{
    public interface IMainPageViewModel
    {
        Task LoadDataAsync();
    }
}