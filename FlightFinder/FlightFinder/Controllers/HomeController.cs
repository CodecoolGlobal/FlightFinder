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
                                     "inboundDate=2019-12-28&cabinClass=business&children=0&infants=0&country=US&currency=USD&locale=en-US&originPlace=SFO-sky&destinationPlace=LHR-sky&outboundDate=2019-12-04&adults=1",
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

            var location = response.Headers.ToList()
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
            JsonResponseData jResponseObj = new JsonResponseData(jsonResponse);




            //foreach (var leg in jResponseObj.Legs)
            //{
            //    Flight flight = new Flight();
            //    flight.destinationStation = leg.DestinationStation; 
            //    flight.destinationTime = leg.Arrival; 
            //    flight.Id = leg.Id; 
            //    flight.departureTime = leg.Departure;
            //    flight.departureStation = leg.OriginStation;
            //}
            //foreach (var itinerary in jResponseObj.Itineraries)
            //{
            //    Journey journey = new Journey();
            //    //journey.inboundFlight = itinerary.InboundLegId;
            //}


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
