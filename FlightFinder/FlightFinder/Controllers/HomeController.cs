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

            return View();
        }

        public IActionResult Results()
        {

            var jResponseObj = doGet();

            ViewData["Agents"] = jResponseObj.Agents;
            ViewData["Itineraries"] = jResponseObj.Itineraries;
            ViewData["Legs"] = jResponseObj.Legs;
            ViewData["Places"] = jResponseObj.Places;
            ViewData["Segments"] = jResponseObj.Segments;
            ViewData["Flight"] = jResponseObj;

            List<Journey> journeys = new List<Journey>();
            iterateThroughItineraries(jResponseObj, journeys);
            ViewData["Journeys"] = journeys;
            return View();
        }


        private string doPost()
        {
            var outboundDate = Request.Form["departing"];
            var inboundDate = Request.Form["returning"];
            var originPlace = Request.Form["airportFrom"];
            var destinationPlace = Request.Form["airportTo"];
            var numOfAdults = Request.Form["adults"];
            var numOfChildren = Request.Form["children"];
            var cabinClass = Request.Form["cabinClass"].ToString().ToLower();

            var postClient = new RestClient("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/v1.0");
            var postRequest = new RestRequest(Method.POST);
            postRequest.AddHeader("X-RapidAPI-Host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            postRequest.AddHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
            postRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            postRequest.AddParameter("application/x-www-form-urlencoded",
                                     "inboundDate=" + inboundDate +
                                     "&cabinClass=" + cabinClass +
                                     "&children=" + numOfChildren +
                                     "&infants=0" +
                                     "&country=HU" +
                                     "&currency=HUF" +
                                     "&locale=hu-HU" +
                                     "&originPlace=" + originPlace +
                                     "&destinationPlace=" + destinationPlace +
                                     "&outboundDate=" + outboundDate +
                                     "&adults=" + numOfAdults,
                                     ParameterType.RequestBody);


            IRestResponse response = postClient.Execute(postRequest);

            var status = response.StatusCode;
            while (status.Equals(HttpStatusCode.TooManyRequests)){
                response = postClient.Execute(postRequest);
            }

            var location = response.Headers.ToList()
                .Where(x => x.Name == "Location")
                .Select(x => x.Value)
                .FirstOrDefault()
                .ToString();

            var sessionKey = location.Substring(location.Length - 36);

            return sessionKey;
        }

        private JsonResponseData doGet()
        {
            var sessionKey = doPost();
            while (string.IsNullOrEmpty(sessionKey))
            {
                sessionKey = doPost();
            }
            var getClient = new RestClient("https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/{sessionkey}?pageIndex=0&pageSize=10");
            var getRequest = new RestRequest(Method.GET);
            getRequest.AddUrlSegment("sessionkey", sessionKey);
            getRequest.AddHeader("X-RapidAPI-Host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
            getRequest.AddHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
            var restResponse = getClient.Execute(getRequest);
            var jsonResponse = JsonConvert.DeserializeObject(restResponse.Content);
            JsonResponseData jResponseObj = new JsonResponseData(jsonResponse);

            return jResponseObj;
            

        }

        private void iterateThroughItineraries(JsonResponseData jsonResponseObj, List<Journey> journeys)
        {
            foreach (Itinerary itinerary in jsonResponseObj.Itineraries)
            {
                Journey journey = new Journey();
                journey.price = itinerary.Price;
                journey.link = itinerary.Link;
                iterateTroughLegs(jsonResponseObj, itinerary, journey);
                journeys.Add(journey);
            }
        }
        private void iterateTroughLegs(JsonResponseData jsonResponseObj, Itinerary itinerary, Journey journey)
        {
            foreach (var leg in jsonResponseObj.Legs)
            {
                getOutboundFlight(jsonResponseObj, leg, itinerary, journey);
            }
            
            foreach (var leg in jsonResponseObj.Legs)
            {
                getInboundFlight(jsonResponseObj, leg, itinerary, journey);
            }
        }

        private void getOutboundFlight(JsonResponseData jsonResponseObj, Leg leg, Itinerary itinerary, Journey journey)
        {
            if (leg.Id == itinerary.OutboundLegId)
            {
               journey.outboundFlight = fillFlightData(jsonResponseObj, leg);
            }
        }

        private void getInboundFlight(JsonResponseData jsonResponseObj, Leg leg, Itinerary itinerary, Journey journey)
        {
            if (leg.Id == itinerary.InboundLegId)
            {
               journey.inboundFlight = fillFlightData(jsonResponseObj, leg);
            }
        }

        private Flight fillFlightData(JsonResponseData jsonResponseObj, Leg leg)
        {
            Flight flight = new Flight();

            foreach (Place place in jsonResponseObj.Places)
            {
                if (leg.OriginStation == place.Id)
                {
                    flight.originStation = place.Name;
                }
                if (leg.DestinationStation == place.Id)
                {
                    flight.destinationStation = place.Name;
                }
            }

            foreach (Carrier carrier in jsonResponseObj.Carriers)
            {
                if (leg.Carriers == carrier.Id)
                {
                   flight.imgUrl = carrier.ImageUrl;
                }
            }

            flight.duration = leg.Duration;
            flight.arrival = leg.Arrival;
            flight.departure = leg.Departure;

            return flight;
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
