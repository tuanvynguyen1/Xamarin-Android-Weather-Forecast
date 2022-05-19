using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Weather_Forecast.API;
using Weather_Forecast.Models;
using Xamarin.Essentials;
using System.Diagnostics;

namespace Weather_Forecast
{
    public partial class MainPage : ContentPage
    {
        private weather.Root _weather;
        private dailyWeather.Root _dailyWeather;
        DateTime _today;

        public class dataWeather
        {
            public string Temp { get; set; }
            public string Date { get; set; }
            public string Icon { get; set; }
            public dataWeather(string temp, string date, string icon)
            {
                Temp = temp;
                Date = date;
                Icon = icon;
            }
        }
        public MainPage()
        {

            InitializeComponent();
            doBackground();

        }
        public string getDayString(DateTime s)
        {
            
            return "Ngày " + s.Day.ToString() + ", Tháng " + s.Month.ToString() + ", Năm " + s.Year.ToString();
        }
        private async Task taskToDo()
        {
            while (true)
            {
                await getLocationName();
                //Function được gọi mỗi 1 tiếng 
                //Vì không có tiền trả API Call
                await Task.Delay(TimeSpan.FromHours(1));
                Debug.WriteLine("One Task has finished");
            }
        }
        private async void doBackground()
        {
            await taskToDo();
        }
        string getShortDay(DateTime s)
        {
            Debug.WriteLine(s.DayOfWeek.ToString());
            switch (s.DayOfWeek.ToString())
            {
                case "Monday":
                    {
                        return "Thứ hai " + s.Day.ToString();
                    }
                case "Tuesday":
                    {
                        return "Thứ ba " + s.Day.ToString();
                    }
                case "Webnesday":
                    {
                        return "Thứ tư " + s.Day.ToString();
                    }
                case "Thursday":
                    {
                        return "Thứ năm " + s.Day.ToString();
                    }
                case "Friday":
                    {
                        return "Thứ sáu " + s.Day.ToString();
                    }
                case "Saturday":
                    {
                        return "Thứ bảy " + s.Day.ToString();
                    }
                default:
                    {
                        return "Chủ nhật " + s.Day.ToString();
                    }
            }
        }
        public async Task getLocationName()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Default,
                        Timeout = TimeSpan.FromMinutes(30)
                    });
                }
                
                if (location == null) return;
                _today = DateTime.Today;
                currentDay.Text = getDayString(_today);
                string lat = location.Latitude.ToString();
                string lon = location.Longitude.ToString();
                var a = new getCityNameAPI(lat, lon);
                var b = new getWeather(lat, lon);
                _weather = await b.getWeatherForeCast();
                _dailyWeather = await b.getDailyWeather(); 
                cityNameLabel.Text = await a.getCityName();
                commentLabel.Text = _weather.weather[0].description.ToString();
                weatherIMG.Source = "https://openweathermap.org/img/wn/" + _weather.weather[0].icon.ToString() + ".png";
                temperatureLabel.Text = _weather.main.temp.ToString().Split('.')[0];
                humidityLabel.Text = _weather.main.humidity.ToString().Split('.')[0] + "%";
                windSpeedLabel.Text = _weather.wind.speed.ToString() + " m/s";
                pressureLabel.Text = _weather.main.pressure.ToString() + " hpa";
                cloudLabel.Text = _weather.clouds.all.ToString() + "%";
                List<dataWeather> list = new List<dataWeather>();
                DateTime tempdate = _today;
                foreach (dailyWeather.List d in _dailyWeather.list)
                {
                    tempdate = tempdate.AddDays(1);
                    list.Add(new dataWeather(d.main.temp.ToString().Split('.')[0], getShortDay(tempdate), "https://openweathermap.org/img/wn/" + d.weather.First().icon.ToString() + ".png"));
                }
                WeatherForecastList.ItemsSource = list;

            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.ToString());
                await Application.Current.MainPage.DisplayAlert("Error has occur", "You Must Enable Location in your device to use this app", "OK");
                
            }
            
        }

        private async void RefreshView_Refreshing(object sender, EventArgs e)
        {
            await getLocationName();
            refreshData.IsRefreshing = false;
        }
    }
}
