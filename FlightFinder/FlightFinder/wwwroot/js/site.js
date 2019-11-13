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
    postString = "inboundDate=2019-11-14&" +
        "cabinClass=business&" +
        "children=0&" +
        "infants=0&" +
        "country=HU&" +
        "currency=HUF&" +
        "locale=hun-HU&" +
        `originPlace=${departure}&` +
        `destinationPlace=${destination}&` +
        `outboundDate=2019-11-13&` +
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
            document.getElementById("JSONPLACE").textContent = this.responseText;
        }
    });

    var data = "https://" + `skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/${sessionKey}?pageIndex=0&pageSize=10`

    xhr.open("GET", data);
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");

    xhr.send(data);

}

// After the button pressed get your api response with the chosen criteria
document.getElementById("submitButton").addEventListener("click", function () {
    getData();
    doPOST(doGET);
});