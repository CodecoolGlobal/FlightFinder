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

    postString =
        `inboundDate=${endDate}&` +
        "cabinClass=economy&" +
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

            getCategories(respObj);


        }
    });

    var data = "https://" + `skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/${sessionKey}?pageIndex=0&pageSize=10`

    xhr.open("GET", data);
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");

    xhr.send(data);

}

function getCategories(responseObject) {

    var itineraries = responseObject.Itineraries;
    var itinerary;
    for (let i = 0; i < itineraries.length; i++) {
        itinerary = itineraries[i];
        iterateThroughLegs(responseObject, itinerary);

    }

}

function iterateThroughLegs(responseObject, itinerary) {

    //var itineraries = responseObject.Itineraries;

    var legs = responseObject.Legs;

    var leg;

    for (let i = 0; i < legs.length; i++) {
        leg = legs[i]
        getOutboundFlight(responseObject, leg, itinerary);
        getInboundFlight(responseObject, leg, itinerary);
    }
}

function getOutboundFlight(responseObject, leg, itinerary) {

    if (leg.Id == itinerary.OutboundLegId) {
        fillFlightData(responseObject, leg);
    }
}

function getInboundFlight(responseObject, leg, itinerary) {

    if (leg.Id == itinerary.InboundLegId) {
        fillFlightData(responseObject, leg);
    }
}

function fillFlightData(responseObject, leg) {

    var carriers = responseObject.Carriers;
    var carrier;
    var imgUrl;

    for (let i = 0; i < carriers.length; i++) {
        carrier = carriers[i];
        if (leg.Carriers[0] == carrier.Id) {
            imgUrl = carrier.ImageUrl;
        }
    }
    document.getElementById("results").innerHTML += `<ul>
                                                                    <li><img src="${imgUrl}"/></li>
                                                                    <li>${leg.Departure}</li>
                                                                    <li>${leg.Duration} min</li>
                                                                    <li>${leg.Arrival}</li>
                                                                   </ul>`
}

// After the button pressed get your api response with the chosen criteria
document.getElementById("submitButton").addEventListener("click", function () {
    document.getElementById("results").innerHTML = "";
    getData();
    doPOST(doGET);
});



            //var agents = respObj.Agents;

            //var itineraries = respObj.Itineraries;

            //var segments = respObj.Segments;

            //var carriers = respObj.Carriers;

            //var legs = respObj.Legs;

            //for (let i = 0; i <= itineraries.length; i++) {
            //    for (let j = 0; j < legs.length; j++) {
            //        if (legs[j].Id == itineraries[i].OutboundLegId) {
            //            for (let k = 0; k < carriers.length; k++) {
            //                if (legs[j].Carriers[0] == carriers[k].Id) {
            //                    var imgUrl = carriers[k].imageUrl;
            //                }
            //            }
            //            document.getElementById("results").innerHTML += `<ul>
            //                                                        <li>${legs[j].Departure}</li>
            //                                                        <li>${legs[j].Duration} min</li>
            //                                                        <li>${legs[j].Arrival}</li>
            //                                                       </ul>`
            //        }
            //        if (legs[j].Id == itineraries[i].InboundLegId) {
            //            for (let k = 0; k < carriers.length; k++) {
            //                if (legs[j].Carriers[0] == carriers[k].Id) {
            //                    imgUrl = carriers[k].imageUrl;
            //                }
            //                document.getElementById("results").innerHTML += `<ul>
            //                                                        <li>${legs[j].Departure}</li>
            //                                                        <li>${legs[j].Duration} min</li>
            //                                                        <li>${legs[j].Arrival}</li>
            //                                                       </ul>`
            //            }
            //        }
            //    }
            //}
            //for (let i = 0; i < carriers.length; i++) {
            //    for (let j = 0; j < segments.length; j++) {
            //        if (carriers[i].Id == segments[j].Carrier) {
            //            document.getElementById("results").innerHTML += `<ul>
            //                                                        <li>${carriers[i].Name}<img src="${carriers[i].ImageUrl}"/></li>
            //                                                        <li>${segments[j].DepartureDateTime}</li>
            //                                                        <li>${segments[j].ArrivalDateTime}</li>
            //                                                        <li>${segments[j].Duration} min</li>
            //                                                       </ul>`
            //        }
            //    }
            //}


