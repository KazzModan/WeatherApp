using System;
using System.Threading.Tasks;
using Autofac;
using WeatherApp.Infrastructure;
using WeatherApp.ViewModels.MainPage;
using WeatherApp.Views;

namespace WeatherApp.Bootstrapper
{
    public class Bootstrapper : IDisposable
    {
        private readonly IContainer _container;
        private IMainPageViewModel _mainPageViewModel;

        public Bootstrapper()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<ViewModels.RegistrationModule>()
                .RegisterModule<RegistrationModule>();

            _container = containerBuilder.Build();
            _container.Resolve<IMainPageViewModel>();
        }

        public async Task<MainPage> Run()
        {
            _mainPageViewModel = _container.Resolve<IMainPageViewModel>();

            await _mainPageViewModel.InitializeAsync();
            var mainPage = new MainPage(_mainPageViewModel);

            if (mainPage == null)
                throw new NotImplementedException();

            return mainPage;
        }

        public void Dispose()
        {
            _container?.Dispose();
        }
    }
}