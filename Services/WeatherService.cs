using SimpleWeather.Windows.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SimpleWeather.Windows.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        
        public WeatherService()
        {
            _httpClient = new HttpClient();
        }
        
        public async Task<WeatherData> GetWeatherAsync(string location)
        {
            try
            {
                // 1. 通过GeoAPI获取城市ID
                var locationId = await GetLocationIdAsync(location);
                if (string.IsNullOrEmpty(locationId))
                {
                    throw new Exception($"无法找到城市: {location}");
                }
                
                // 2. 获取实时天气数据
                var currentWeather = await GetCurrentWeatherAsync(locationId);
                
                // 3. 获取天气预报数据
                var forecast = await GetForecastAsync(locationId);
                
                // 4. 组合数据
                return new WeatherData
                {
                    Location = currentWeather.Location,
                    Date = DateTime.Now,
                    Description = currentWeather.Description,
                    Temperature = currentWeather.Temperature,
                    FeelsLike = currentWeather.FeelsLike,
                    Humidity = currentWeather.Humidity,
                    Pressure = currentWeather.Pressure,
                    WindSpeed = currentWeather.WindSpeed,
                    Visibility = currentWeather.Visibility,
                    Sunrise = currentWeather.Sunrise,
                    Forecast = forecast
                };
            }
            catch (Exception ex)
            {
                throw new Exception($"获取天气数据失败: {ex.Message}", ex);
            }
        }
        
        // 通过GeoAPI获取城市ID
        private async Task<string> GetLocationIdAsync(string locationName)
        {
            try
            {
                var url = $"{WeatherConfig.GEO_API_HOST}/v2/city/lookup?location={locationName}&key={WeatherConfig.API_KEY}";
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                var json = await response.Content.ReadAsStringAsync();
                var geoResponse = JsonSerializer.Deserialize<GeoApiResponse>(json);
                
                if (geoResponse?.Code == "200" && geoResponse.Location?.Length > 0)
                {
                    return geoResponse.Location[0].Id;
                }
                
                return null;
            }
            catch
            {
                return null;
            }
        }
        
        // 获取实时天气数据
        private async Task<CurrentWeather> GetCurrentWeatherAsync(string locationId)
        {
            var url = $"{WeatherConfig.API_HOST}/v7/weather/now?location={locationId}&key={WeatherConfig.API_KEY}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var weatherResponse = JsonSerializer.Deserialize<WeatherNowResponse>(json);
            
            if (weatherResponse?.Code == "200")
            {
                var now = weatherResponse.Now;
                return new CurrentWeather
                {
                    Location = weatherResponse.FxLink.Contains("beijing") ? "北京" : "未知位置", // 简化处理
                    Description = now.Text,
                    Temperature = double.Parse(now.Temp),
                    FeelsLike = double.Parse(now.FeelsLike),
                    Humidity = int.Parse(now.Humidity),
                    Pressure = int.Parse(now.Pressure),
                    WindSpeed = double.Parse(now.WindSpeed),
                    Visibility = double.Parse(now.Vis),
                    Sunrise = DateTime.Today.AddHours(6) // 简化处理
                };
            }
            
            throw new Exception("无法获取实时天气数据");
        }
        
        // 获取天气预报数据
        private async Task<List<ForecastItem>> GetForecastAsync(string locationId)
        {
            var url = $"{WeatherConfig.API_HOST}/v7/weather/7d?location={locationId}&key={WeatherConfig.API_KEY}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            
            var json = await response.Content.ReadAsStringAsync();
            var forecastResponse = JsonSerializer.Deserialize<WeatherDailyResponse>(json);
            
            var forecast = new List<ForecastItem>();
            
            if (forecastResponse?.Code == "200")
            {
                foreach (var day in forecastResponse.Daily)
                {
                    forecast.Add(new ForecastItem
                    {
                        Day = GetDayOfWeek(day.FxDate),
                        Description = day.TextDay,
                        MinTemperature = double.Parse(day.TempMin),
                        MaxTemperature = double.Parse(day.TempMax)
                    });
                }
            }
            
            return forecast;
        }
        
        // 将日期转换为星期几
        private string GetDayOfWeek(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out var date))
            {
                if (date.Date == DateTime.Today)
                    return "今天";
                else if (date.Date == DateTime.Today.AddDays(1))
                    return "明天";
                else if (date.Date == DateTime.Today.AddDays(2))
                    return "后天";
                else
                    return date.ToString("dddd");
            }
            return dateStr;
        }
        
        // GeoAPI响应模型
        private class GeoApiResponse
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            
            [JsonPropertyName("location")]
            public LocationInfo[] Location { get; set; }
        }
        
        private class LocationInfo
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            
            [JsonPropertyName("id")]
            public string Id { get; set; }
        }
        
        // 实时天气响应模型
        private class WeatherNowResponse
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            
            [JsonPropertyName("fxLink")]
            public string FxLink { get; set; }
            
            [JsonPropertyName("now")]
            public NowInfo Now { get; set; }
        }
        
        private class NowInfo
        {
            [JsonPropertyName("temp")]
            public string Temp { get; set; }
            
            [JsonPropertyName("feelsLike")]
            public string FeelsLike { get; set; }
            
            [JsonPropertyName("text")]
            public string Text { get; set; }
            
            [JsonPropertyName("windSpeed")]
            public string WindSpeed { get; set; }
            
            [JsonPropertyName("humidity")]
            public string Humidity { get; set; }
            
            [JsonPropertyName("pressure")]
            public string Pressure { get; set; }
            
            [JsonPropertyName("vis")]
            public string Vis { get; set; }
        }
        
        // 当前天气数据模型
        private class CurrentWeather
        {
            public string Location { get; set; }
            public string Description { get; set; }
            public double Temperature { get; set; }
            public double FeelsLike { get; set; }
            public int Humidity { get; set; }
            public int Pressure { get; set; }
            public double WindSpeed { get; set; }
            public double Visibility { get; set; }
            public DateTime Sunrise { get; set; }
        }
        
        // 天气预报响应模型
        private class WeatherDailyResponse
        {
            [JsonPropertyName("code")]
            public string Code { get; set; }
            
            [JsonPropertyName("daily")]
            public DailyInfo[] Daily { get; set; }
        }
        
        private class DailyInfo
        {
            [JsonPropertyName("fxDate")]
            public string FxDate { get; set; }
            
            [JsonPropertyName("tempMax")]
            public string TempMax { get; set; }
            
            [JsonPropertyName("tempMin")]
            public string TempMin { get; set; }
            
            [JsonPropertyName("textDay")]
            public string TextDay { get; set; }
        }
    }
}