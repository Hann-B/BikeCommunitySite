using BikeCommunitySite.Models;
using BikeCommunitySite.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeSite.Services
{
    public class PlaceService:IPlaceService
    {
        readonly private SingleTracksAPI _singleTracksApi;
        readonly private GoogleApis _googleApis;
        public PlaceService(IOptions<SingleTracksAPI> SingleTracksOptionsAccessor, IOptions<GoogleApis>googleOptionsAccessor)
        {
            _singleTracksApi = SingleTracksOptionsAccessor.Value;
            _googleApis = googleOptionsAccessor.Value;
        }

        public async Task<IQueryable<PlaceModel.Place>> GetTopDestinations()
        {
            var singleTracksApi = _singleTracksApi;

            var request = (HttpWebRequest)WebRequest.Create(singleTracksApi.AllUsaPlaces.ToString());
            request.Accept = "application/json";
            request.Headers["X-Mashape-Key"] = singleTracksApi.X_Mashape_Key.ToString();

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var allresults = JsonConvert.DeserializeObject<PlaceModel.RootObject>(raw);
            //var filteredResults = allresults.places.RemoveAll(r => r.description.ToString() == string.Empty);
            return allresults.places.AsQueryable();

        }

        public async Task<PlaceModel.Place> GetPlaceDetailsAsync(double lat, double lon, string city)
        {
            var singleTracksApi = _singleTracksApi;
            var DetailStr = string.Format(singleTracksApi.PlaceDetails, lat, lon, city);

            var request = (HttpWebRequest)WebRequest.Create(DetailStr);
            request.Accept = "application/json";
            request.Headers["X-Mashape-Key"] = singleTracksApi.X_Mashape_Key.ToString();

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var selectedPlace = JsonConvert.DeserializeObject<PlaceModel.RootObject>(raw);
            return selectedPlace.places.FirstOrDefault();
        }

        public async Task<AccomodationModel.RootObject> GetAccommodations()
        {
            var googleApis = _googleApis;
            var Accommodations = string.Format(googleApis.PlacesApi, _googleApis.PlacesApiKey);

            var request = (HttpWebRequest)WebRequest.Create(Accommodations);
            request.Accept = "application/json";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var ListOfAccommodations = JsonConvert.DeserializeObject<AccomodationModel.RootObject>(raw);
            return ListOfAccommodations;
        }
    }
}
