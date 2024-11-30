using OpenWeatherAPI.Models;
using OpenWeatherAPI.Services;
using OpenWeatherAPI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
//using AndroidX.Core.Util;

namespace OpenWeatherAPI.ViewModels
{
    public partial class WeatherViewModel : ObservableObject
    {
        [ObservableProperty]
        WeatherResponse weatherResponse;

        [ObservableProperty]
        public string cityName;

        [ObservableProperty]
        public string weatherForecast;

        [ObservableProperty]
        public string degreesCelsius;

        [ObservableProperty]
        public string humidity;

        [ObservableProperty]
        public string wind;

        WeatherService weatherService;

        public ICommand getCityWeather { get; }

        public WeatherViewModel()
        {
            getCityWeather = new Command(getWeather);
            weatherService = new WeatherService();
        }

        public async void getWeather()
        {
            WeatherResponse = await weatherService.GetWeatherResponse(cityName);

            CityName = cityName;

            WeatherForecast = WeatherResponse.Weather[0].Description;

            DegreesCelsius = Math.Round(WeatherResponse.Main.Temp).ToString() + "ºC";

            Humidity = WeatherResponse.Main.Humidity.ToString() + "%";

            Wind = WeatherResponse.Wind.Speed.ToString() + "km/h";

        }

    }
}
