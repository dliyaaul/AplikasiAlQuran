using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using static AplikasiAlQur_an.FiturJadwalSholat.APILokasi;

namespace AplikasiAlQur_an.FiturJadwalSholat
{
    public class PrayerTimesResponse
    {
        public Data data { get; set; }

        public class Data
        {
            public Timings timings { get; set; }
        }

        public class Timings
        {
            public string Fajr { get; set; }
            public string Sunrise { get; set; }
            public string Dhuhr { get; set; }
            public string Asr { get; set; }
            public string Maghrib { get; set; }
            public string Isha { get; set; }
        }
    }

    public class APIJadwalSholat
    {

        public static async Task<PrayerTimesResponse> GetJadwalSholat(DateTime selectedDate)
        {
            LocationResponse locationResponse = await APILokasi.GetCoordinatesFromIp();

            PrayerTimesResponse fallbackPrayerTimes = new PrayerTimesResponse
            {
                data = new PrayerTimesResponse.Data
                {
                    timings = new PrayerTimesResponse.Timings
                    {
                        Fajr = "00:00",
                        Sunrise = "00:00",
                        Dhuhr = "00:00",
                        Asr = "00:00",
                        Maghrib = "00:00",
                        Isha = "00:00"
                    }
                }
            };

            string latitude;
            string longitude;
            string country = "ID";

            if (locationResponse.loc != "-")
            {
                string[] Coordinate = locationResponse.loc.Split(',');

                latitude = Coordinate[0];
                longitude = Coordinate[1];
            }
            else
            {
                latitude = "0";
                longitude = "0";
            }

            string date = selectedDate.ToString("yyyy-MM-dd");
            string apiUrl = "http://ipinfo.io/json";
            string urlAPI = $"https://api.aladhan.com/v1/timingsByCity/{date}?city=Surabaya&country=Indonesia&state=Jawa+Timur&method=20&shafaq=general&tune=5%2C3%2C5%2C7%2C9%2C-1%2C0%2C8%2C-6&timezonestring=Asia%2FJakarta&calendarMethod=UAQ";

            string urlAPI2 = $"https://api.aladhan.com/v1/timings?latitude={latitude}&longitude={longitude}&method=20&shafaq=general&tune=5%2C3%2C5%2C7%2C9%2C-1%2C0%2C8%2C-6&timezonestring=Asia%2FJakarta&calendarMethod=UAQ";
            string urlAPI3 = $"https://api.aladhan.com/v1/timings/{date}?latitude={latitude}&longitude={longitude}&method=20&shafaq=general&tune=5%2C3%2C5%2C7%2C9%2C-1%2C0%2C8%2C-6&timezonestring=Asia%2FJakarta&calendarMethod=UAQ";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(urlAPI3);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    PrayerTimesResponse prayerTimes = JsonConvert.DeserializeObject<PrayerTimesResponse>(responseBody);

                    //Console.WriteLine("RESPONSEHEADER " + response);
                    //Console.WriteLine("RESPONSEBODY " + responseBody);
                    //Console.WriteLine("PRAYERTIMES " + prayerTimes);
                    //Console.WriteLine("PRAYERTIMES " + latitude);
                    //Console.WriteLine("Tanggal " + date);

                    return prayerTimes;

                }
                catch (Exception ex)
                {
                    return fallbackPrayerTimes;
                }
            }
        }
    }
}
