using BikeCommunitySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeCommunitySite.Services
{
    public interface IPlaceService
    {
        Task<IQueryable<GooglePlaceModel.Result>> GetTopDestinations();
        Task<DestinationModel.Place> GetPlaceDetailsAsync(double lat, double lon, string city);
        Task<GooglePlaceModel.RootObject> GetAccommodations();
        Task<GooglePlaceModel.RootObject> GetRentalStores();
        Task<GooglePlaceDetailModel.Result> GetGooglePlaceDetails(string placeId);

        Task<string> GetCsvFormat();
    }
}
