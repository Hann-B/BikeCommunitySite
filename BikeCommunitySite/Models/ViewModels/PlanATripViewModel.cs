using Sakura.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeCommunitySite.Models.ViewModels
{
    public class PlanATripViewModel
    {
        public IQueryable<DestinationModel.Place> Destination { get; set; }
        public IQueryable<GooglePlaceModel.Result> Accomadation { get; set; }
        public IQueryable<GooglePlaceModel.Result> BikeShops { get; set; }
    }
}
