namespace ERestaurant.Domain.DTO
{
    public class WeatherResponseDto
    {
        public List<WeatherInfo> Weather { get; set; }
        public MainInfo Main { get; set; }
        public WindInfo Wind { get; set; }
        public string? Name { get; set; } 
    }

    public class WeatherInfo
    {
        public string? Main { get; set; } 
        public string? Description { get; set; }
        public string? Icon { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
    }

    public class WindInfo
    {
        public double Speed { get; set; }
    }
}
