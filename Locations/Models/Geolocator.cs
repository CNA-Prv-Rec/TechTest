using System.Net.Http;
using System.Net;
using RestSharp;

public class Geolocator
{
    public string GeolocateIPAddress(string ipAddress)
    {
        // we are better getting location from the browser, but in the absence we can generate from ipaddress
        string strIpLocation = "";
    
            var client = new RestClient("https://ipapi.co/" + ipAddress + "/json/");
            var request = new RestRequest()
            {
                Method = Method.GET
            };

            var response = client.Execute(request);

            var dictionary = JsonConvert.DeserializeObject<IDictionary>(response.Content);
            foreach (var key in dictionary.Keys)
            {
                strIpLocation += key.ToString() + ": " + dictionary[key] + "\r\n";
            }
            return strIpLocation;
    
    }

public void AddGeoLocationToUser(HttpContext context, User u)
{
    string ipAddress= "";
    string location = "";
    if ((u.Latitude == null)||(u.Latitude == "")) //no location supplied by front end so generate it from ip address
    {
        ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        if (ipAddress!="")
        {
            string location = geo.GeolocateIPAddress(ipAddress());
            user.Latitude = location.split(",")[0];
            user.Longitude = location.split(",")[1].Trim();

        }

    }
}
}

