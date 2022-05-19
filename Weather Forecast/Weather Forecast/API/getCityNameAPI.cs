using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using Weather_Forecast.Models;

namespace Weather_Forecast.API
{
    internal class getCityNameAPI
    {
        private String _apiHostName = "https://api.bigdatacloud.net/data/reverse-geocode-client?";
        private String _lat;
        private String _lng;
        private String _lang = "vn";

        HttpClient _httpClient = new HttpClient();

        public getCityNameAPI(string lat, string lng)
        {
            _lat = lat;
            _lng = lng;
        }

        public async Task<String> getCityName()
        {
            var response = await _httpClient.GetAsync($"{_apiHostName}/latitude={_lat}&longitude={_lng}&localityLanguage={_lang}");

            if (response.IsSuccessStatusCode)
            {
                var byteArray = response.Content.ReadAsByteArrayAsync().Result;
                var content = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);
                var data = JsonConvert.DeserializeObject<Geo.Root>(content);
                return data.principalSubdivision.ToString();
            }
            else
            {
                throw new Exception();
            }
        }
    }

}
