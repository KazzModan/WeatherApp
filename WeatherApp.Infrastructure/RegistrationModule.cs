using Autofac;
using WeatherApp.Domain.REST;
using WeatherApp.Infrastructure.REST;

namespace WeatherApp.Infrastructure
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApiRequestExecutor>().As<IApiRequestExecutor>().SingleInstance();
        }
    }
}