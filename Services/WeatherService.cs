using SimpleWeather.Windows.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleWeather.Windows.Services
{
    public class WeatherService
    {
        // In a real application, you would use an API key and call a weather API
        // For this example, we'll simulate data
        private const string API_KEY = "YOUR_API_KEY_HERE";
        private const string BASE_URL = "https://api.openweathermap.org/data/2.5";
        
        public async Task<WeatherData> GetWeatherAsync(string location)
        {
            // Simulate API call delay
            await Task.Delay(1000);
            
            // Return sample data
            return new WeatherData
            {
                Location = location,
                Date = DateTime.Now,
                Description = "Partly Cloudy",
                Temperature = 24.5,
                FeelsLike = 26.3,
                Humidity = 68,
                Pressure = 1013,
                WindSpeed = 3.7,
                Visibility = 10.0,
                Sunrise = DateTime.Today.AddHours(6).AddMinutes(30),
                Forecast = new List<ForecastItem>
                {
                    new ForecastItem { Day = "Today", Description = "Partly Cloudy", MinTemperature = 19, MaxTemperature = 25 },
                    new ForecastItem { Day = "Tomorrow", Description = "Sunny", MinTemperature = 21, MaxTemperature = 27 },
                    new ForecastItem { Day = "Wednesday", Description = "Rain", MinTemperature = 17, MaxTemperature = 22 },
                    new ForecastItem { Day = "Thursday", Description = "Cloudy", MinTemperature = 18, MaxTemperature = 23 },
                    new ForecastItem { Day = "Friday", Description = "Sunny", MinTemperature = 20, MaxTemperature = 26 }
                }
            };
        }
        
        // Method to get weather by coordinates (if needed)
        public async Task<WeatherData> GetWeatherByCoordinatesAsync(double latitude, double longitude)
        {
            // Simulate API call delay
            await Task.Delay(1000);
            
            // Return sample data
            return new WeatherData
            {
                Location = "Current Location",
                Date = DateTime.Now,
                Description = "Clear Sky",
                Temperature = 22.1,
                FeelsLike = 23.8,
                Humidity = 65,
                Pressure = 1015,
                WindSpeed = 2.5,
                Visibility = 16.0,
                Sunrise = DateTime.Today.AddHours(6).AddMinutes(15),
                Forecast = new List<ForecastItem>
                {
                    new ForecastItem { Day = "Today", Description = "Clear Sky", MinTemperature = 18, MaxTemperature = 24 },
                    new ForecastItem { Day = "Tomorrow", Description = "Partly Cloudy", MinTemperature = 20, MaxTemperature = 26 },
                    new ForecastItem { Day = "Wednesday", Description = "Sunny", MinTemperature = 22, MaxTemperature = 28 },
                    new ForecastItem { Day = "Thursday", Description = "Sunny", MinTemperature = 23, MaxTemperature = 29 },
                    new ForecastItem { Day = "Friday", Description = "Partly Cloudy", MinTemperature = 21, MaxTemperature = 27 }
                }
            };
        }
    }
}