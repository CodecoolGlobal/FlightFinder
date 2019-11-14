// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code.

//get the fields(the code sets this fields)
var postString;
var sessionKey;

//get your date from the input fields
function getData() {
    var departure = document.getElementById("Departure").value;
    var destination = document.getElementById("Destination").value;
    var startDate = document.getElementById("start").value;
    var endDate = document.getElementById("end").value;
    var cabinClass = document.getElementById("cabinClass").value.toLowerCase();

    postString =
        `inboundDate=${endDate}&` +
        `cabinClass=${cabinClass}&` +
        "children=0&" +
        "infants=0&" +
        "country=HU&" +
        "currency=HUF&" +
        "locale=hun-HU&" +
        `originPlace=${departure}&` +
        `destinationPlace=${destination}&` +
        `outboundDate=${startDate}&` +
        "adults=1";
}

//DO POST Method
function doPOST(callback) {
    var xhr = new XMLHttpRequest();
    var data = postString;
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === this.DONE) {
            var contentType = this.getResponseHeader("location");
            // ez a callback felelős azért hogy a get request pontosak akkor fusson le amikor már a post készen van
            //itt ez a callback a doGET methodot hívja meg
            sessionKey = contentType.substr(contentType.length - 36);
            callback();
        }
    });

    xhr.open("POST", "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/v1.0");
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
    xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded");
    xhr.send(data);
}

// DO GET Method
function doGET() {

    var data = null;
    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === this.DONE) {
            //here gives the site your info back(display post and get request result)
            var respObj = JSON.parse(this.responseText);
            console.log(respObj);
            iterateThroughItineraries(respObj);
        }
    });

    var data = "https://" + `skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/${sessionKey}?pageIndex=0&pageSize=10`

    xhr.open("GET", data);
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
    xhr.send(data);

}

function iterateThroughItineraries(responseObject) {

    var itineraries = responseObject.Itineraries;
    var listOfItineraries = document.createElement("ul");
    listOfItineraries.setAttribute("id", "itineraries");
    document.getElementById("results").appendChild(listOfItineraries);

    for (let itinerary of itineraries) {
        var itineraryDOM = addItinerary();

        console.log(itineraryDOM);
        itineraryDOM.querySelector(".price").innerHTML = `${itinerary.PricingOptions[0].Price} Ft`;
        itineraryDOM.querySelector(".link").innerHTML = `<a href="${itinerary.PricingOptions[0].DeeplinkUrl}"><button>Purchase</button></a>`;
        iterateThroughLegs(responseObject, itinerary, itineraryDOM);
        document.getElementById("results").appendChild(itineraryDOM);

    }
}

function iterateThroughLegs(responseObject, itinerary, itineraryDOM) {

    var legs = responseObject.Legs;

    for (let leg of legs) {
        getOutboundFlight(responseObject, leg, itinerary, itineraryDOM);
    }
    for (let leg of legs) {
        getInboundFlight(responseObject, leg, itinerary, itineraryDOM);
    }
}

function getOutboundFlight(responseObject, leg, itinerary, itineraryDOM) {

    if (leg.Id == itinerary.OutboundLegId) {
        var flight = itineraryDOM.querySelector(".outboundFlight");
        fillFlightData(responseObject, leg, flight);
    }
}

function getInboundFlight(responseObject, leg, itinerary, itineraryDOM) {

    if (leg.Id == itinerary.InboundLegId) {
        var flight = itineraryDOM.querySelector(".inboundFlight");
        fillFlightData(responseObject, leg, flight);
    }
}

function fillFlightData(responseObject, leg, flight) {

    var carriers = responseObject.Carriers;
    var places = responseObject.Places;
    var originStation;
    var destinationStation;
    var imgUrl;
    var departure = leg.Departure.replace("T", " ");
    var arrival = leg.Arrival.replace("T", " ");

    for (let place of places) {
        if (leg.OriginStation == place.Id) {
            originStation = place.Name;
        }
        if (leg.DestinationStation == place.Id) {
            destinationStation = place.Name;
        }
    }

    for (let carrier of carriers) {
        if (leg.Carriers[0] == carrier.Id) {
            imgUrl = carrier.ImageUrl;
        }
    }
    flight.querySelector(".carrierIMG").innerHTML = `<img src="${imgUrl}" />`;
    flight.querySelector(".departure").innerHTML = `${originStation}` + " " + `${departure}`;
    flight.querySelector(".duration").innerHTML = `${leg.Duration} min`;
    flight.querySelector(".arrival").innerHTML = `${destinationStation}` + " " + `${arrival}`;


    //document.getElementById("results").innerHTML += `<ul>
    //                                                                <li><img src="${imgUrl}"/></li>
    //                                                                <li>${leg.Departure}</li>
    //                                                                <li>${leg.Duration} min</li>
    //                                                                <li>${leg.Arrival}</li>
    //                                                               </ul>`
}

function addItinerary() {
    var temp = document.querySelector(".itineraryTemplate");
    var clone = document.importNode(temp.content, true);
    return clone;
}

// After the button pressed get your api response with the chosen criteria
document.getElementById("submitButton").addEventListener("click", function () {
    document.getElementById("results").innerHTML = "";
    getData();
    doPOST(doGET);
});