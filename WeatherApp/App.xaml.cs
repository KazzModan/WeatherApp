using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WeatherApp.Views;
using Windows.UI.ViewManagement;

namespace WeatherApp
{
    sealed partial class App : Application
    {
        private Bootstrapper.Bootstrapper _bootstrapper; 
        public App()
        {
            this.InitializeComponent();
            _bootstrapper = new Bootstrapper.Bootstrapper();
        }
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            var mainPage = await _bootstrapper.Run();
            Window.Current.Content = new Frame { Content = mainPage, DataContext = mainPage.DataContext};
            Window.Current.Activate();
        } 
    }
}
