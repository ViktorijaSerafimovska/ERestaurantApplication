using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ERestaurant.Domain.DTO;
using ERestaurant.Service.Interface;

namespace ERestaurant.Service.Implementation
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "a61b1e6474267831ab03e5441cecf5a4"; //this is my key, generated when i registred

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherResponseDto> GetWeatherAsync(string city = "Skopje")
        {
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={_apiKey}&units=metric";
            var response = await _httpClient.GetStringAsync(url);

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            return JsonSerializer.Deserialize<WeatherResponseDto>(response, options);
        }
    }
}
