using BikeCommunitySite.Models;
using BikeCommunitySite.Services;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeSite.Services
{
    public class PlaceService : IPlaceService
    {
        readonly private SingleTracksAPI _singleTracksApi;
        readonly private GoogleApis _googleApis;
        public PlaceService(IOptions<SingleTracksAPI> SingleTracksOptionsAccessor, IOptions<GoogleApis> googleOptionsAccessor)
        {
            _singleTracksApi = SingleTracksOptionsAccessor.Value;
            _googleApis = googleOptionsAccessor.Value;
        }

        public async Task<IQueryable<GooglePlaceModel.Result>> GetTopDestinations()
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
            var allresults = JsonConvert.DeserializeObject<DestinationModel.RootObject>(raw);

            var googlePlaces = new List<GooglePlaceModel.Result>();
            googlePlaces = await TypeConversions.ToGooglePlaceFormat(allresults.places);

            return googlePlaces.AsQueryable();
        }

        public async Task<GooglePlaceModel.RootObject> GetDestinations()
        {
            var textsearch = "https://maps.googleapis.com/maps/api/place/textsearch/json?query=mountain+biking+trails+usa&key=AIzaSyBgSD6hkVYX4myK7K7Od7UUMPA3i0ijS6E";

            var request = (HttpWebRequest)WebRequest.Create(textsearch);
            request.Accept = "application/json";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var destinations = JsonConvert.DeserializeObject<GooglePlaceModel.RootObject>(raw);
            return destinations;
        }

        public async Task<string> GetCsvFormat()
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
            return raw;
        }

        public async Task<DestinationModel.Place> GetPlaceDetailsAsync(double lat, double lon, string city)
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
            var selectedPlace = JsonConvert.DeserializeObject<DestinationModel.RootObject>(raw);
            return selectedPlace.places.FirstOrDefault();
        }

        public async Task<GooglePlaceModel.RootObject> GetAccommodations()
        {
            var googleApis = _googleApis;
            var type = "lodging";
            var Accommodations = string.Format(googleApis.PlacesApi, type, _googleApis.PlacesApiKey);

            var request = (HttpWebRequest)WebRequest.Create(Accommodations);
            request.Accept = "application/json";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var ListOfAccommodations = JsonConvert.DeserializeObject<GooglePlaceModel.RootObject>(raw);
            return ListOfAccommodations;
        }

        public async Task<GooglePlaceModel.RootObject> GetRentalStores()
        {
            var googleApis = _googleApis;
            var type = "bicycle_store";
            var RentalStores = string.Format(googleApis.PlacesApi, type, _googleApis.PlacesApiKey);

            var request = (HttpWebRequest)WebRequest.Create(RentalStores);
            request.Accept = "application/json";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var ListOfRentalStores = JsonConvert.DeserializeObject<GooglePlaceModel.RootObject>(raw);
            return ListOfRentalStores;
        }

        public async Task<GooglePlaceDetailModel.Result> GetGooglePlaceDetails(string place_id)
        {
            var googleApis = _googleApis;

            var PlaceDetails = string.Format(googleApis.PlaceDetails, place_id, googleApis.PlacesApiKey);

            var request = (HttpWebRequest)WebRequest.Create(PlaceDetails);
            request.Accept = "application/json";

            WebResponse response = await request.GetResponseAsync();

            var raw = String.Empty;
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8, true, 1024, true))
            {
                raw = reader.ReadToEnd();
            }
            var rv = JsonConvert.DeserializeObject<GooglePlaceDetailModel.RootObject>(raw);
            return rv.result;

        }
    }
}
