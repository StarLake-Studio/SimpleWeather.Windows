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
            
            // 设置默认城市
            LocationTextBox.Text = "北京";
            
            // 加载默认天气数据
            LoadWeatherData();
        }
        
        private async void LoadWeatherData()
        {
            try
            {
                StatusText.Text = "正在加载天气数据...";
                var weatherData = await _weatherService.GetWeatherAsync(LocationTextBox.Text);
                UpdateUI(weatherData);
                StatusText.Text = $"天气数据已更新 - {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"加载失败: {ex.Message}";
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
                StatusText.Text = "请输入城市名称";
                return;
            }
            
            try
            {
                StatusText.Text = $"正在搜索 {location} 的天气...";
                var weatherData = await _weatherService.GetWeatherAsync(location);
                UpdateUI(weatherData);
                StatusText.Text = $"天气已更新 - {DateTime.Now:HH:mm:ss}";
            }
            catch (Exception ex)
            {
                StatusText.Text = $"搜索失败: {ex.Message}";
            }
        }
        
        private void UpdateUI(WeatherData weatherData)
        {
            LocationText.Text = weatherData.Location;
            DateText.Text = weatherData.Date.ToString("yyyy年M月d日 dddd");
            DescriptionText.Text = weatherData.Description;
            TemperatureText.Text = $"{weatherData.Temperature:F0}°C";
            FeelsLikeText.Text = $"{weatherData.FeelsLike:F0}°C";
            HumidityText.Text = $"{weatherData.Humidity}%";
            PressureText.Text = $"{weatherData.Pressure} hPa";
            WindText.Text = $"{weatherData.WindSpeed} m/s";
            VisibilityText.Text = $"{weatherData.Visibility} km";
            SunriseText.Text = weatherData.Sunrise.ToString("HH:mm");
            
            ForecastListView.ItemsSource = weatherData.Forecast;
        }
    }
}