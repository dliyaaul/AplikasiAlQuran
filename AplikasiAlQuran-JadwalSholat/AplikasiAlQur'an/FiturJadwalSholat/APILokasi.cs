using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AplikasiAlQur_an.FiturJadwalSholat
{
    public class APILokasi
    {
        public static async Task<LocationResponse> GetCoordinatesFromIp()
        {
            string apiUrl = "http://ipinfo.io/json";

            LocationResponse fallbackData = new LocationResponse
            {
                loc = "-",
                region = "-",
                city = "-"
            };

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    var locationData = JsonConvert.DeserializeObject<LocationResponse>(responseBody);

                    var coordinates = locationData.loc.Split(',');
                    var kotaResponse = locationData.city;
                    var provinsiResponse = locationData.region;

                    LocationResponse locationResponse = JsonConvert.DeserializeObject<LocationResponse>(responseBody);

                    double latitude = Convert.ToDouble(coordinates[0]);
                    double longitude = Convert.ToDouble(coordinates[1]);
                    string kota = Convert.ToString(kotaResponse);
                    string region = Convert.ToString(provinsiResponse);

                    return (locationResponse);
                }

                catch (Exception ex)
                {
                    return fallbackData;
                }
            }
        }

        public class LocationResponse
        {
            public string loc { get; set; }
            public string region { get; set; }
            public string city { get; set; }

        }
    }
}
