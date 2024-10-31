using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Domain.REST;
using Windows.Media.Protection.PlayReady;

namespace WeatherApp.Infrastructure.REST
{
    public class ApiRequestExecutor : IApiRequestExecutor
    {
        private readonly Uri  _baseAddress = new Uri("http://dataservice.accuweather.com");
        private readonly string apiKey = "OS5ZwDKBjXGs9qD0RA8KrYdH4X0pIXAP";

        public async Task<TResponse> GetForecastAsync<TResponse>(string cityKey)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var httpResponseMessage = await client.GetAsync($"/forecasts/v1/daily/1day/{cityKey}?apikey={apiKey}&metric=true");
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(content);
                return response;
            };
        }

        public async Task<TResponse> GetLocationAsync<TResponse>(string cityName)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var httpResponseMessage = await client.GetAsync($"/locations/v1/cities/search?apikey={apiKey}&q={cityName}");
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(content);
                return response;
            };
        }
    }
}