// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.




//get the fields
var postString;


function getData() {
    var departure = document.getElementById("Departure").value;
    var destination = document.getElementById("Destination").value;
       postString = `inboundDate=2019-11-13&cabinClass=business&children=0&infants=0&country=US&currency=USD&locale=en-US&originPlace=${departure}&destinationPlace=${destination}&outboundDate=2019-11-12&adults=1`;
}

var departure = document.getElementById("Departure").value;
var destination = document.getElementById("Destination").value;

document.getElementById("submitButton").addEventListener("click", function () {
    getData();
    makePOST();

});

//DO POST Method


function makePOST() {
    var xhr = new XMLHttpRequest();
    var data = postString;
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === this.DONE) {
            console.log(this.responseText);
        }
    });

    xhr.open("POST", "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/v1.0");
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");
    xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded");

    xhr.send(data);

    xhr.onreadystatechange = function () {
        if (this.readyState == this.HEADERS_RECEIVED) {
            var contentType = xhr.getResponseHeader("location");
            console.log(contentType);
    ///nincs lekezelve ha valami szar
        }
    }
  
}


// DO GET Method
//var data = null;

//var xhr = new XMLHttpRequest();
//xhr.withCredentials = true;

//xhr.addEventListener("readystatechange", function () {
//	if (this.readyState === this.DONE) {
//		console.log(this.responseText);
//	}
//});

//xhr.open("GET", "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/pricing/uk2/v1.0/05bff005-00de-40fd-bf84-0e004ca227ad?pageIndex=0&pageSize=10");
//xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
//xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");

//xhr.send(data);

//console.log("success")