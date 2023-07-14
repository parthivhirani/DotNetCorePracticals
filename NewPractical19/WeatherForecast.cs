using System.ComponentModel.DataAnnotations;

namespace NewPractical19
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [Display(Name = "Atmosphere")]
        public string? Summary { get; set; }
    }
}