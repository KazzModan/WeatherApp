using System;
using System.Threading.Tasks;

namespace WeatherApp.ViewModels.MainPage
{
    public interface IMainPageViewModel : IDisposable
    {
        Task InitializeAsync();
    }
}