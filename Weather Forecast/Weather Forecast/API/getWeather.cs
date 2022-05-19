using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Weather_Forecast.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Weather_Forecast.API
{
    internal class getWeather
    {
        private String _apiHostName = "https://api.openweathermap.org/data/2.5/weather?";
        private String _lat;
        private String _lon;
        private String _key = "a222901b348fcf5bbe477bf108c1061e";
        //Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
        //Kelvin chuyen ve Celsius = -273.15 
        //Fahrenheit chuyen ve Celsius = (F - 32) * 5/9 = C
        private String _unit = "Metric";

        HttpClient _http = new HttpClient();
        
        public getWeather(string lat, string lon)
        {
            _lat = lat;
            _lon = lon;
        }

        public async Task<weather.Root> getWeatherForeCast()
        {
            var response = await _http.GetAsync($"{_apiHostName}lat={_lat}&lon={_lon}&appid={_key}&units={_unit}&lang=vi");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<weather.Root>(content);
            }
            else
            {
                throw new Exception();
            }
        }
        public async Task<dailyWeather.Root> getDailyWeather()
        {
            var apiHostName = "https://api.openweathermap.org/data/2.5/forecast?";
            var response = await _http.GetAsync($"{apiHostName}lat={_lat}&lon={_lon}&cnt=5&appid={_key}&units={_unit}&lang=vi");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<dailyWeather.Root>(content);
            }
            else
            {
                throw new Exception();
            }
        }
    }
}
