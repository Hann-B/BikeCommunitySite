using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeCommunitySite.Models;
using BikeCommunitySite.Services;
using Sakura.AspNetCore;
using BikeCommunitySite.Models.ViewModels;

namespace BikeCommunitySite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPlaceService _placeService;
        public HomeController(IPlaceService placeService)
        {
            _placeService = placeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Trips()
        {
            //Destinations
            var listOfDestinations = await _placeService.GetTopDestinations();
            IQueryable<GooglePlaceModel.Result> desintations = null;
            desintations = listOfDestinations
                .AsQueryable();

            //Accommodations
            var listofAccommodations = await _placeService.GetAccommodations();
            IQueryable<GooglePlaceModel.Result> accommodations = null;
            accommodations = listofAccommodations.results
                .OrderBy(a=>a.name)
                .AsQueryable();

            //Bike Shops
            var listofShops = await _placeService.GetRentalStores();
            IQueryable<GooglePlaceModel.Result> bikeShops = null;
            bikeShops = listofShops.results
                .AsQueryable();
               
            var rv = new PlanATripViewModel
            {
                Destination = desintations,
                Accomadation = accommodations,
                BikeShops = bikeShops
            };
            return View(rv);
        }

        public async Task<IActionResult> Destinations(int? page, int? pageSize)
        {
            int no = page ?? 1;
            int size = pageSize ?? 5;
            var listOfPlaces = await _placeService.GetDestinations();
            IPagedList<GooglePlaceModel.Result> lst = null;
            lst = listOfPlaces.results
                .ToPagedList<GooglePlaceModel.Result>(size, no);

            return View(lst);
        }

        public IActionResult Map()
        {
            //var content = _placeService.GetTopDestinations().Result.ToString();
            //string csv = JsonToCsvConversion.jsonToCSV(content, ",");

            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //result.Content = new StringContent(csv);
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("text/csv");
            //result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = "export.csv" };
            return View();
        }

        public IActionResult About()
        {

            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
