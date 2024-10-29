using Autofac;
using WeatherApp.ViewModels.MainPage;

namespace WeatherApp.ViewModels
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<MainPageViewModel>().As<IMainPageViewModel>().InstancePerDependency();
        }
    }
}