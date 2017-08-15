using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeCommunitySite.Models;
using BikeCommunitySite.Services;

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

        public async Task<IActionResult> Destinations(int page = 1)
        {
            var listOfPlaces = await _placeService.GetTopDestinations();
            return View(listOfPlaces.OrderBy(o => o.description).Reverse());
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
