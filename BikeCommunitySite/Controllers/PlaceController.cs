using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeSite.Services;
using BikeCommunitySite.Services;
using Sakura.AspNetCore;
using BikeCommunitySite.Models;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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

        [HttpGet("Place/Destination")]
        public async Task<ActionResult> DestinationDetails(double lat, double lon, string city)
        {
            var SelectedPlace = await _placeService.GetPlaceDetailsAsync(lat, lon, city);
            return View(SelectedPlace);
        }

        [HttpGet("Place/Accommodations")]
        public async Task<ActionResult> Accommodations(int? page, int? pageSize)
        {
            int no = page ?? 1;
            int size = pageSize ?? 5;
            var ListofAccomodations = await _placeService.GetAccommodations();
            IPagedList<GooglePlaceModel.Result> lst = null;
            lst = ListofAccomodations.results             
                .ToPagedList<GooglePlaceModel.Result>(size, no);
            return View(lst);
        }

        [HttpGet("Place/RentalStores")]
        public async Task<ActionResult> RentalStores(int? page, int? pageSize)
        {
            int no = page ?? 1;
            int size = pageSize ?? 5;
            var ListofRentalStores = await _placeService.GetRentalStores();
            IPagedList<GooglePlaceModel.Result> lst = null;
            lst = ListofRentalStores.results
                .ToPagedList<GooglePlaceModel.Result>(size, no);
            return View(lst);
        }

        [HttpGet("Place/Details")]
        public async Task<ActionResult> PlaceDetails(string placeId)
        {
            var rv = await _placeService.GetGooglePlaceDetails(placeId);
            return View(rv);
        }

        [HttpPost("Place/destinations.csv")]
        public async Task<ActionResult> CsvDestination()
        {
            var content = await _placeService.GetCsvFormat();

            string csv = TypeConversions.jsonToCSV(content, ",");

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(csv);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "destinations.csv" };

            return View(result);
        }
    }
}