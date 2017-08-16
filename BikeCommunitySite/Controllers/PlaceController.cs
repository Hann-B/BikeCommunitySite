using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeSite.Services;
using BikeCommunitySite.Services;

namespace BikeCommunitySite.Controllers
{
    public class PlaceController : Controller
    {
        private readonly IPlaceService _placeService;
        public PlaceController(IPlaceService placeService)
        {
            _placeService = placeService;
        }


        // GET: Places
        public ActionResult PlacesIndex()
        {
            return View();
        }

        // GET: Places/Details/5
        public async Task<ActionResult> Details(double lat, double lon, string city)
        {
            var SelectedPlace = await _placeService.GetPlaceDetailsAsync(lat, lon, city);
            return View(SelectedPlace);
        }

        [HttpGet("Place/Accommodations")]
        public async Task<ActionResult> Accommodations()
        {
            var ListofAccomodations = await _placeService.GetAccommodations();
            return View(ListofAccomodations);
        }
    }
}