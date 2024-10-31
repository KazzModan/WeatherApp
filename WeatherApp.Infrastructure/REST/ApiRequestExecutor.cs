using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WeatherApp.Domain.REST;

namespace WeatherApp.Infrastructure.REST
{
    public class ApiRequestExecutor : IApiRequestExecutor
    {
        private readonly Uri _baseAddress = new Uri("http://dataservice.accuweather.com");
        private readonly string apiKey = "f7FdfGGFxAsGnXtUKXh6mSGg4cZZi3AJ";

        public async Task<TResponse> GetForecastAsync<TResponse>(string cityKey)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var httpResponseMessage =
                    await client.GetAsync($"/forecasts/v1/daily/1day/{cityKey}?apikey={apiKey}&metric=true");

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(content);
                return response;
            }

            ;
        }

        public async Task<TResponse> GetLocationAsync<TResponse>(string cityName)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var httpResponseMessage =
                    await client.GetAsync($"/locations/v1/cities/search?apikey={apiKey}&q={cityName}");

                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                if (content != "[]")
                {
                    var response = JsonConvert.DeserializeObject<TResponse>(content);
                    return response;
                }

                return default;
            }

            ;
        }
    }
}