using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace WeatherApp
{
    internal sealed partial class App : Application, IDisposable
    {
        private readonly Bootstrapper.Bootstrapper _bootstrapper;

        public App()
        {
            InitializeComponent();
            _bootstrapper = new Bootstrapper.Bootstrapper();
            Suspending += OnSuspending;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {
            var mainPage = await _bootstrapper.Run();
            Window.Current.Content = new Frame { Content = mainPage, DataContext = mainPage.DataContext };
            Window.Current.Activate();
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            Dispose();
            deferral.Complete();
        }

        public void Dispose()
        {
            _bootstrapper.Dispose();
        }
    }
}