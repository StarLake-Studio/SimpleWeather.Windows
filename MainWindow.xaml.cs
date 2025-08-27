using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using SimpleWeather.Windows.Services;
using SimpleWeather.Windows.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace SimpleWeather.Windows
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private WeatherService _weatherService;
        
        public MainWindow()
        {
            this.InitializeComponent();
            InitializeAppTitleBar();
            
            _weatherService = new WeatherService();
            
            // Load default weather data
            LoadWeatherData();
        }
        
        private async void LoadWeatherData()
        {
            try
            {
                StatusText.Text = "Loading weather data...";
                var weatherData = await _weatherService.GetWeatherAsync("New York");
                UpdateUI(weatherData);
                StatusText.Text = "Weather data loaded";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error loading weather: {ex.Message}";
            }
        }
        
        private void InitializeAppTitleBar()
        {
            // Set the title bar height
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
        }
        
        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string location = LocationTextBox.Text;
            if (string.IsNullOrWhiteSpace(location))
            {
                StatusText.Text = "Please enter a location";
                return;
            }
            
            try
            {
                StatusText.Text = $"Searching for weather in {location}...";
                var weatherData = await _weatherService.GetWeatherAsync(location);
                UpdateUI(weatherData);
                StatusText.Text = $"Weather updated for {location}";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"Error: {ex.Message}";
            }
        }
        
        private void UpdateUI(WeatherData weatherData)
        {
            LocationText.Text = weatherData.Location;
            DateText.Text = weatherData.Date.ToString("MMMM dd, yyyy");
            DescriptionText.Text = weatherData.Description;
            TemperatureText.Text = $"{weatherData.Temperature:F1}°C";
            FeelsLikeText.Text = $"{weatherData.FeelsLike:F1}°C";
            HumidityText.Text = $"{weatherData.Humidity}%";
            PressureText.Text = $"{weatherData.Pressure} hPa";
            WindText.Text = $"{weatherData.WindSpeed} m/s";
            VisibilityText.Text = $"{weatherData.Visibility} km";
            SunriseText.Text = weatherData.Sunrise.ToString("HH:mm");
            
            ForecastListView.ItemsSource = weatherData.Forecast;
        }
    }
}