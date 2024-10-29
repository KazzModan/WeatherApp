using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Autofac;
using WeatherApp.Infrastructure;
using WeatherApp.ViewModels.MainPage;
using WeatherApp.Views;

namespace WeatherApp.Bootstrapper
{
    public class Bootstrapper
    {
        private readonly IContainer _container;

        public Bootstrapper()
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule<ViewModels.RegistrationModule>()
                .RegisterModule<RegistrationModule>();

            _container = containerBuilder.Build();
        }

        public Page Run()
        {
            var mainPageViewModel = _container.Resolve<IMainPageViewModel>();

            var mainPage = new MainPage(mainPageViewModel);

            if (mainPage == null)
                throw new NotImplementedException();

            return mainPage;
        }
    }
}