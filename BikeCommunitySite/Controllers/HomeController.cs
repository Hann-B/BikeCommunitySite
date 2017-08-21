using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BikeCommunitySite.Models;
using BikeCommunitySite.Services;
using Sakura.AspNetCore;
using BikeCommunitySite.Models.ViewModels;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

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

        public IActionResult Trips()
        {
            return View();
        }

        public async Task<IActionResult> Destinations(int? page, int? pageSize)
        {
            int no = page ?? 1;
            int size = pageSize ?? 5;
            var listOfPlaces = await _placeService.GetTopDestinations();
            IPagedList<DestinationModel.Place> lst = null;
            lst = listOfPlaces
                .OrderBy(o => o.description)
                .Reverse()
                .ToPagedList<DestinationModel.Place>(size, no);

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
