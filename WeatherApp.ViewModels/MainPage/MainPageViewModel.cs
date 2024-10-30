using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private string _precipitation;
        private Brush _precipitationBrush;
        private WeatherResponse _item = new WeatherResponse();
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

        public WeatherResponse Item
        {
            get => _item;
            set
            {
                if (!_item.Equals(value))
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                }
            }
        }

        public string Precipitation
        {
            get => _precipitation;
            set
            {
                if (_precipitation != value)
                {
                    if (Item.DailyForecasts[0].Day.HasPrecipitation || Item.DailyForecasts[0].Night.HasPrecipitation)
                    {
                        _precipitation = "Rain!";
                        PrecipitationColor = new SolidColorBrush(Colors.Red);
                        OnPropertyChanged(nameof(Precipitation));
                        OnPropertyChanged(nameof(PrecipitationColor));
                    }
                }
            }
        }
        public Brush PrecipitationColor { get; set; }

        public async Task LoadDataAsync(string key)
        {
            Item = await _apiRequestExecutor.GetForecastAsync<WeatherResponse>(key);
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
            var list = await _apiRequestExecutor.GetLocationAsync<IEnumerable<LocationResponse>>(InputText);
            City = InputText;
            InputText = string.Empty;
            await LoadDataAsync(list.FirstOrDefault().Key);
        }
        private bool CanSubmit(object parameter)
        {
            return !string.IsNullOrEmpty(InputText);
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}