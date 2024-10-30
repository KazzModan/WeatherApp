using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using WeatherApp.Domain.REST;

namespace WeatherApp.ViewModels.MainPage
{
    public class MainPageViewModel : IMainPageViewModel, INotifyPropertyChanged
    {
        private readonly IApiRequestExecutor _apiRequestExecutor;
        private string _сity;
        private string _inputText;

        public MainPageViewModel(IApiRequestExecutor apiRequestExecutor)
        {
            _apiRequestExecutor = apiRequestExecutor;
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
        }

        public ICommand SubmitCommand { get; }

        public string City
        {
            get => _сity;
            set
            {
                if (_сity != value)
                {
                    _сity = value;
                    OnPropertyChanged(nameof(City));
                }
            }
        }

        public string InputText
        {
            get => _inputText;
            set
            {
                if (_inputText != value)
                {
                    _inputText = value;
                    OnPropertyChanged(nameof(InputText));
                    (SubmitCommand as RelayCommand)?.RaiseCanExecuteChanged();
                }
            }
        }

        public WeatherResponse Item { get; private set; }
        public string Precipitation { get; set; }
        public Brush PrecipitationColor { get; set; }

        public async Task LoadDataAsync()
        {
            Item = await _apiRequestExecutor.GetAsync<WeatherResponse>(
                "/forecasts/v1/daily/1day/323030?apikey=mw67UsQIG9Ifq6qiVGl15dqiyzRGhxbR&metric=true");
            if (Item.DailyForecasts[0].Day.HasPrecipitation || Item.DailyForecasts[0].Night.HasPrecipitation)
            {
                Precipitation = "Rain!";
                PrecipitationColor = new SolidColorBrush(Colors.Red);
            }
            else
            {
                Precipitation = "Clear";
                PrecipitationColor = new SolidColorBrush(Colors.LimeGreen);
            }
        }


        private async void OnSubmit(object parameter)
        {
            City = InputText;
            InputText = string.Empty;
        }
        private bool CanSubmit(object parameter)
        {
            return !string.IsNullOrEmpty(InputText); // Команда активна, если есть текст
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}