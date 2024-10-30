using WeatherApp.ViewModels.MainPage;
using Windows.UI.ViewManagement;

namespace WeatherApp.Views
{

    public sealed partial class MainPage 
    {
        public MainPage(IMainPageViewModel mainPageViewModel)
        {
            this.InitializeComponent();

            this.DataContext = mainPageViewModel;
        }
    }
}
