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
        
            // I need to test/debug this to make sure I get what I want here. I'm going to get a whole load of bumpf in addition to geolocation, 
            // and so will need to get the correct dictionary key for the geolocation
            strIPLocation = dictionary["latitude"] + "," + dictionary["longitude"];

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

