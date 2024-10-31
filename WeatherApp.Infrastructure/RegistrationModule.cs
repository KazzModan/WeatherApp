using Autofac;
using WeatherApp.Domain.Common;
using WeatherApp.Domain.REST;
using WeatherApp.Infrastructure.Common;
using WeatherApp.Infrastructure.REST;

namespace WeatherApp.Infrastructure
{
    public class RegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ApiRequestExecutor>().As<IApiRequestExecutor>().SingleInstance();
            builder.RegisterType<PathService>().As<IPathService>().SingleInstance();
        }
    }
}