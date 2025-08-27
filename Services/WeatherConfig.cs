namespace SimpleWeather.Windows.Services
{
    /// <summary>
    /// 天气服务配置类
    /// Weather service configuration class
    /// </summary>
    public static class WeatherConfig
    {
        /// <summary>
        /// 和风天气API Key
        /// QWeather API Key
        /// 请在 https://dev.qweather.com/ 注册并获取您的API Key
        /// Please register at https://dev.qweather.com/ to get your API Key
        /// </summary>
        public const string API_KEY = "1599925e989c40ea8104b2fc5058b4b4";
        
        /// <summary>
        /// 和风天气API基础URL
        /// QWeather API base URL
        /// </summary>
        public const string API_HOST = "https://devapi.qweather.com";
        
        /// <summary>
        /// GeoAPI基础URL（用于城市搜索）
        /// GeoAPI base URL (for city lookup)
        /// </summary>
        public const string GEO_API_HOST = "https://geoapi.qweather.com";
    }
}