using System;
using System.Collections.Generic;

namespace SimpleWeather.Windows.Models
{
    public class WeatherData
    {
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Temperature { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public double WindSpeed { get; set; }
        public double Visibility { get; set; }
        public DateTime Sunrise { get; set; }
        public List<ForecastItem> Forecast { get; set; }
    }
    
    public class ForecastItem
    {
        public string Day { get; set; }
        public string Description { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
    }
}