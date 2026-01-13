using System.Net;
using System.Net.NetworkInformation;
using System.Text.Json;
using Geo_radio.Models;

namespace Geo_radio.Services;

public class RadioBrowserService {
    
    private readonly HttpClient http;

    public RadioBrowserService(HttpClient http) {
        this.http = http;
    }

    private static String GetRadioBrowserApiUrl() {
        
        //Get fastest ip of dns
        String baseUrl = @"all.api.radio-browser.info";
        IPAddress[] ips = Dns.GetHostAddresses(baseUrl);
        long lastRoundTripTime = long.MaxValue;
        string searchUrl = @"de2.api.radio-browser.info"; //Fallback

        foreach (IPAddress ip in ips) {
            PingReply reply = new Ping().Send(ip);

            if (reply != null && reply.RoundtripTime < lastRoundTripTime) {
                lastRoundTripTime = reply.RoundtripTime;
                searchUrl = ip.ToString();
            }
        }

        IPHostEntry hostEntry = Dns.GetHostEntry(searchUrl);

        if (!String.IsNullOrEmpty(hostEntry.HostName)) {
            searchUrl = hostEntry.HostName;
        }

        return searchUrl;
    }

    public async Task<List<Station>> GetStationsAsync() {
        List<Station> stations = new List<Station>();

        Random random = new Random();

        http.BaseAddress = new System.Uri("https://" + "de1.api.radio-browser.info" + "/"); //Fallback
        //http.BaseAddress = new System.Uri("https://" + GetRadioBrowserApiUrl() + "/"); (broken as of 11/01/26 need to remake this function)

        //Generate 3 random stations per game
        for (int i = 0; i < 3; i++) {
            int offset = random.Next(0, 1000);

            HttpResponseMessage response = await http.GetAsync($"/json/stations/search?order=random&limit=1&offset={offset}");
            response.EnsureSuccessStatusCode();

            String json = await response.Content.ReadAsStringAsync();
            JsonSerializerOptions options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var station = JsonSerializer.Deserialize<List<Station>>(json, options);

            stations.Add(station?.FirstOrDefault() ?? new Station());
        }

        return stations;
    }
}