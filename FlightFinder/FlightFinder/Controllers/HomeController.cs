using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlightFinder.Models;
using System.Net;
using System.IO;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightFinder.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var client = new RestClient("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/54eb1bc5-68ff-4c2a-b57c-f3ece6fbce04?pageIndex=0&pageSize=10");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            //request.AddHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
            //IRestResponse response = client.Execute(request);

            //dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content);
            //dynamic api = JObject.Parse(jsonResponse);

            //// grab the values and do your assertions :-)
            //var season = api.season;
            //var circuits = api.Circuits;
            //var circuitId = api.Circuits[0].circuitId;
            //var circuitName = api.Circuits[0].circuitName;

            //ViewData["MyJSON"] = jsonResponse;


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
