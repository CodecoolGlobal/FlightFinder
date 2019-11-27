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

        public async Task<IActionResult> IndexAsync()
        {
            var postClient = new RestClient("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/v1.0");
            var postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("X-RapidAPI-Host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            postRequest.AddHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
            postRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            postRequest.AddParameter("application/x-www-form-urlencoded",
                                     "inboundDate=2019-11-28&cabinClass=business&children=0&infants=0&country=US&currency=USD&locale=en-US&originPlace=SFO-sky&destinationPlace=LHR-sky&outboundDate=2019-11-26&adults=1",
                                     ParameterType.RequestBody);
            //postRequest.AddParameter("inboundDate", "2019-11-24", ParameterType.RequestBody);
            //postRequest.AddParameter("cabinClass", "business", ParameterType.RequestBody);
            //postRequest.AddParameter("children", 0, ParameterType.RequestBody);
            //postRequest.AddParameter("infants", 0, ParameterType.RequestBody);
            //postRequest.AddParameter("country", "US", ParameterType.RequestBody);
            //postRequest.AddParameter("currency", "USD", ParameterType.RequestBody);
            //postRequest.AddParameter("locale", "en-US", ParameterType.RequestBody);
            //postRequest.AddParameter("originPlace", "BUD-sky", ParameterType.RequestBody);
            //postRequest.AddParameter("destinationPlace", "LOND-sky", ParameterType.RequestBody);
            //postRequest.AddParameter("outboundDate", "2019-11-25", ParameterType.RequestBody);
            //postRequest.AddParameter("adults", 1, ParameterType.RequestBody);


            IRestResponse response = await postClient.ExecuteTaskAsync(postRequest);
            //Task<IRestResponse> t = postClient.ExecuteTaskAsync(postRequest);
            //t.Wait();
            //var Response = await t;

            var location =  response.Headers.ToList()
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault()
                .ToString();

            var sessionKey = location.Substring(location.Length - 36);


            var getClient = new RestClient("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/{sessionkey}?pageIndex=0&pageSize=10");
            var getRequest = new RestRequest(Method.GET);
            getRequest.AddUrlSegment("sessionkey", sessionKey);
            getRequest.AddHeader("X-RapidAPI-Host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            getRequest.AddHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
            var restResponse = getClient.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject(restResponse.Content);
            Flight flight = new Flight(jsonResponse);


            ViewData["SessionKey"] = flight.SessionKey;
            ViewData["Agents"] = flight.Agents;
            ViewData["Itineraries"] = flight.Itineraries;
            ViewData["Legs"] = flight.Legs;
            ViewData["Places"] = flight.Places;
            ViewData["Segments"] = flight.Segments;

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
