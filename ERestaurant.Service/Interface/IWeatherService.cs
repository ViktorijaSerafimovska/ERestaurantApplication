using System.Threading.Tasks;
using ERestaurant.Domain.DTO;

namespace ERestaurant.Service.Interface
{
    public interface IWeatherService
    {
        Task<WeatherResponseDto> GetWeatherAsync(string city = "Skopje");
    }
}
