using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Domain.REST;

namespace WeatherApp.Infrastructure.REST
{
    public class ApiRequestExecutor : IApiRequestExecutor
    {
        private readonly Uri  _baseAddress = new Uri("http://dataservice.accuweather.com");
        private readonly string apiKey = "mw67UsQIG9Ifq6qiVGl15dqiyzRGhxbR";

        public async Task<TResponse>  GetAsync<TResponse>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = _baseAddress;
                var httpResponseMessage = await client.GetAsync(request);
                var content = await httpResponseMessage.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<TResponse>(content);
                return response;
            };
        }
    }
}