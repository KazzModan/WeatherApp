using WeatherApp.ViewModels.MainPage;

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
