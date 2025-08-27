# SimpleWeather for Windows

A simple weather application for Windows built with WinUI 3 and UWP style.

![Build Status](https://github.com/StarLake-Studio/SimpleWeather.Windows/workflows/Build%20and%20Package%20SimpleWeather/badge.svg)

## Features

- Current weather display
- 5-day forecast
- Location search
- Clean, modern UWP-style interface
- Chinese language interface
- Integration with QWeather (和风天气) API for real weather data

## Getting Started

### Prerequisites

- Windows 10 version 1809 or higher
- Visual Studio 2022 or higher
- .NET 6 SDK
- Windows App SDK 1.4

### Installation

1. Clone the repository
   ```bash
   git clone https://github.com/StarLake-Studio/SimpleWeather.Windows.git
   ```
2. Open `SimpleWeather.Windows.sln` in Visual Studio
3. Configure the QWeather API Key (see [SETUP.md](SETUP.md) for detailed instructions)
4. Build and run the project

Alternatively, you can use the provided batch scripts:
- Run `build.bat` to build the project
- Run `run.bat` to run the project

### Project Structure

```
SimpleWeather.Windows/
├── App.xaml/.cs              # Application entry point
├── MainWindow.xaml/.cs       # Main window UI and logic
├── Models/                   # Data models
│   └── WeatherData.cs        # Weather data structures
├── Services/                 # Business logic
│   ├── WeatherService.cs     # Weather data service
│   └── WeatherConfig.cs      # Weather API configuration
├── Assets/                   # Application assets (icons, images)
├── .github/workflows/        # GitHub Actions workflows
├── build.bat                 # Build script for Windows
├── run.bat                   # Run script for Windows
├── package.bat               # Packaging script for Windows
├── SETUP.md                  # Setup guide for QWeather API
├── .gitignore                # Git ignore file
└── README.md                 # This file
```

## Usage

1. Enter a city name in the search box
2. Click "Search" to get weather information
3. View current weather and 5-day forecast

## API Integration

This application is integrated with QWeather (和风天气) API to provide real weather data:

1. Get an API key from [QWeather Developer Platform](https://dev.qweather.com/)
2. Update the `API_KEY` constant in `Services/WeatherConfig.cs`
3. The application will automatically use the API to fetch real weather data

## Troubleshooting

If you encounter build issues, try the following:

1. Install required workloads:
   ```
   dotnet workload install windowsappsdk
   dotnet workload install wasm-tools
   ```

2. Clean and rebuild the project:
   - Delete `bin` and `obj` folders
   - Rebuild the solution

3. Ensure you have the latest Windows App SDK installed

4. If you encounter XAML compiler errors:
   - Make sure you're using the correct .NET 6 SDK
   - Check that the Windows App SDK is properly installed
   - Try building with `msbuild` instead of `dotnet build`
   - Ensure XAML syntax is correct, especially binding expressions

5. If you encounter target framework errors:
   - Ensure that restore has run successfully with the correct runtime identifier
   - Check that the RuntimeIdentifiers in the project file include the target platform
   - Try running `dotnet restore -r win-x64` before building
   - Make sure the RuntimeIdentifier is explicitly set in the build command

## GitHub Actions

This repository includes GitHub Actions for automated building and packaging:

- **Build and Package SimpleWeather**: Automatically builds the application on every push or pull request to main/master branches
- **Release**: Creates release packages when a new GitHub release is created

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a pull request

## License

This project is licensed under the MIT License.

## Download

You can download the latest release from the [Releases](https://github.com/StarLake-Studio/SimpleWeather.Windows/releases) page.