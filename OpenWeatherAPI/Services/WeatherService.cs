using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using OpenWeatherAPI.Models;
using System.Diagnostics;

namespace OpenWeatherAPI.Services
{
    public class WeatherService
    {
        private string apiKey = "85a135c30fe7be89e6ed22ff28886bfc";

        private WeatherResponse weatherResponse;

        private HttpClient httpClient;

        private JsonSerializerOptions JsonSerializerOptions;

        public WeatherService()
        {
            httpClient = new HttpClient();
            JsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<WeatherResponse> GetWeatherResponse(string cityName)
        {
            Uri uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}");
            //Lógica de fazer requisição
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content, JsonSerializerOptions);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine($"Erro ao obter dados da API: {e.Message}");
                return new WeatherResponse(); //Retorna um objeto inicializado em caso de falha.
                //Debug.WriteLine(e.Message);
            }
            return weatherResponse;
        }

        public async Task<WeatherResponse> GetWeatherResponseByLocation()
        {
            try
            {
                Location location = await Geolocation.Default.GetLastKnownLocationAsync();

                if (location != null)
                {
                    
                    Uri uri = new Uri($"https://api.openweathermap.org/data/2.5/weather?lat={location.Latitude}&lon={location.Longitude}&appid={apiKey}");

                    HttpResponseMessage response = await httpClient.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        WeatherResponse weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content, JsonSerializerOptions);
                        return weatherResponse;
                    }
                }
                else
                {
                    Debug.WriteLine("Localização não encontrada.");
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Debug.WriteLine($"Geolocalização não suportada: {fnsEx.Message}");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Debug.WriteLine($"Geolocalização não ativada: {fneEx.Message}");
            }
            catch (PermissionException pEx)
            {
                Debug.WriteLine($"Permissões de localização negadas: {pEx.Message}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter dados da API: {ex.Message}");
            }

            //Retorna um objeto padrão em caso de falha
            return new WeatherResponse();
        }

    }
}
