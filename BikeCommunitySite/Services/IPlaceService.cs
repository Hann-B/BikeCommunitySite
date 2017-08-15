using BikeCommunitySite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeCommunitySite.Services
{
    public interface IPlaceService
    {
        Task<IQueryable<PlaceModel.Place>> GetTopDestinations();
        Task<PlaceModel.Place> GetPlaceDetailsAsync(double lat, double lon, string city);
    }
}
