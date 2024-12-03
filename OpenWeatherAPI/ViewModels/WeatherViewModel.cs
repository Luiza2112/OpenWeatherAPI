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
//using AuthenticationServices;
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
        public string weatherIcon = "nuvem.png";

        [ObservableProperty]
        public string degreesCelsius;

        [ObservableProperty]
        public string thermalSensation;

        [ObservableProperty]
        public string humidity;
        
        [ObservableProperty]
        public string wind;

        WeatherService weatherService;

        public ICommand GetCityWeather { get; }
        //if (weatherIcon == null){WeatherIcon = "nuvem.jpg";}
        public WeatherViewModel()
        {
            GetCityWeather = new Command(GetWeather);
            weatherService = new WeatherService();
        }

        public async void GetWeather()
        {
            WeatherResponse = await weatherService.GetWeatherResponse(cityName);

            CityName = cityName;

            if (MainTranslations.TryGetValue(WeatherResponse.weather[0].main, out string translation))
            {
                WeatherForecast = translation; //Tradução encontrada no dicionário
            }
            else
            {
                WeatherForecast = "Previsão desconhecida"; //Tradução não encontrada
            }

            WeatherIcon = GetWeatherIcon(WeatherResponse.weather[0].icon);

            DegreesCelsius = Math.Round(WeatherResponse.main.temp - 273).ToString() + "ºC";
            //Converter de Kelvin para Celcius
            ThermalSensation = Math.Round(WeatherResponse.main.feels_like - 273).ToString() + "ºC";

            Humidity = WeatherResponse.main.humidity.ToString() + "%";
            //Converter m/s para km/h
            Wind = Math.Round(WeatherResponse.wind.speed * 3.6).ToString() + "km/h";

        }

        private static readonly Dictionary<string, string> MainTranslations = new()
        {
            {"Thunderstorm", "Trovoadas"},
            {"Drizzle", "Garoa"},
            {"Rain", "Chuva"},
            {"Snow", "Neve" },
            {"Mist", "Névoa"},
            {"Smoke", "Fumaça"},
            {"Haze", "Neblina"},
            {"Dust", "Nuvens ou Redemoinhos de Poeira"},
            {"Fog", "Nevoeiro"},
            {"Sand", "Tempestade de Areia"},
            {"Ash", "Cinzas Vulcânicas"},
            {"Squall", "Rajadas de Vento"},
            {"Tornado", "Tornado"},
            {"Clear", "Céu limpo"},
            {"Clouds", "Com Nuvens"},
        };

        public string GetWeatherIcon(string iconCode)
        {
            return $"https://openweathermap.org/img/wn/{iconCode}@2x.png";
        }

    }
}
