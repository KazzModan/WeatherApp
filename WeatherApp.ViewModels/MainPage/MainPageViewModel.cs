using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml.Media;
using WeatherApp.Domain.Common;
using WeatherApp.Domain.REST;
using Windows.UI.Xaml.Controls;

namespace WeatherApp.ViewModels.MainPage
{
    public class MainPageViewModel : IMainPageViewModel, INotifyPropertyChanged
    {
        private readonly IApiRequestExecutor _apiRequestExecutor;
        private readonly IPathService _pathService;
        private string _city;
        private Brush _cityColor;
        private string _inputText;
        private WeatherResponse _item = new WeatherResponse();
        private string _precipitation;
        private readonly HashSet<string> _shownCities = new HashSet<string>();

        public MainPageViewModel(IApiRequestExecutor apiRequestExecutor, IPathService pathService)
        {
            _apiRequestExecutor = apiRequestExecutor;
            _pathService = pathService;
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
        }

        public ICommand SubmitCommand { get; }

        public string City
        {
            get => _city;
            set
            {
                if (_city != value)
                {
                    _city = value;
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
                if (!_item.Equals(value) || _item == null)
                {
                    _item = value;
                    OnPropertyChanged(nameof(Item));
                    if (value != null)
                        UpdatePrecipitation();
                }
            }
        }

        public string Precipitation
        {
            get => _precipitation;
            private set
            {
                if (_precipitation != value)
                {
                    _precipitation = value;
                    OnPropertyChanged(nameof(Precipitation));
                }
            }
        }

        public Brush CityColor
        {
            get => _cityColor;
            private set
            {
                if (_cityColor != value)
                {
                    _cityColor = value;
                    OnPropertyChanged(nameof(CityColor));
                }
            }
        }

        public Brush PrecipitationColor { get; set; }

        public async Task InitializeAsync()
        {
            City = await _pathService.InitializeAsync();
            if (City != "")
                await LoadDataAsync(City);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public async Task LoadDataAsync(string cityName)
        {
            var list = await _apiRequestExecutor.GetLocationAsync<IEnumerable<LocationResponse>>(cityName);
            Item = await _apiRequestExecutor.GetForecastAsync<WeatherResponse>(list.FirstOrDefault().Key);
        }


        private async void OnSubmit(object parameter)
        {
            var list = await _apiRequestExecutor.GetLocationAsync<IEnumerable<LocationResponse>>(InputText);
            if (list != null)
            {
                City = InputText;
                InputText = string.Empty;
                Item = await _apiRequestExecutor.GetForecastAsync<WeatherResponse>(list.FirstOrDefault().Key);
                CityColor = new SolidColorBrush(Colors.Black);
            }
            else
            {
                City = "Wrong Input!";
                CityColor = new SolidColorBrush(Colors.Red);
                InputText = string.Empty;
                Item = new WeatherResponse();
                Precipitation = "";
            }
        }

        private bool CanSubmit(object parameter)
        {
            return !string.IsNullOrEmpty(InputText);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private async void UpdatePrecipitation()
        {
            if (Item.DailyForecasts.FirstOrDefault()?.Day.HasPrecipitation == true ||
                Item.DailyForecasts.FirstOrDefault()?.Night.HasPrecipitation == true)
            {
                Precipitation = "Rain!";
                PrecipitationColor = new SolidColorBrush(Colors.Red);

                if (!_shownCities.Contains(City))
                {
                    _shownCities.Add(City);

                    var dialog = new ContentDialog
                    {
                        Title = "Rain forecast",
                        Content = $"In the {City} will rain.",
                        CloseButtonText = "OK"
                    };

                    await dialog.ShowAsync();
                }
            }
            else
            {
                Precipitation = "Clear!";
                PrecipitationColor = new SolidColorBrush(Colors.LimeGreen);
            }

            OnPropertyChanged(nameof(Precipitation));
            OnPropertyChanged(nameof(PrecipitationColor));
        }
        public void Dispose()
        {
            if (City != "Wrong Input!") _pathService.SaveAsync(City);
        }
    }
}