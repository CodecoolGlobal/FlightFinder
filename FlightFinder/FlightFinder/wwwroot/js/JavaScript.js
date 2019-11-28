function getcountries() {
    var data = null;

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === this.DONE) {

            var result = JSON.parse(this.responseText.substring(13).slice(0, -1));
            autocomplite(result, "#countries");
            autocomplite(result, "#countries2");

        }
    });

    xhr.open("GET", "https://skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/reference/v1.0/countries/hu-HU");
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");

    xhr.send(data);

}
    
function getairports(country, pickerid) {
    var data = null;

    var xhr = new XMLHttpRequest();
    xhr.withCredentials = true;

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === this.DONE) {
            console.log(this.responseText);
            var result = JSON.parse(this.responseText.substring(10).slice(0, -1));
            var picker = document.getElementById(pickerid)
            for (let i in result) {
                var node = document.createElement("option");
                node.textContent = result[i]['PlaceId'];
                picker.appendChild(node);
            }
        }
    });


    xhr.open("GET", "https://" + `skyscanner-skyscanner-flight-search-v1.p.rapidapi.com/apiservices/autosuggest/v1.0/HU/HUF/hu-HU/?query=${country}`);
    xhr.setRequestHeader("x-rapidapi-host", "skyscanner-skyscanner-flight-search-v1.p.rapidapi.com");
    xhr.setRequestHeader("x-rapidapi-key", "ce1241679dmshdbe323b73c0dde6p1f7e5ejsn386ae855ecfa");

    xhr.send(data);
}

getcountries();


function autocomplite(json, idstring) {
    var options = {

        data: json,

        getValue: "Name",

        list: {
            match: {
                enabled: true
            }
        },

        theme: "square"
    };

    $(idstring).easyAutocomplete(options);
}

var from = document.getElementById("countries");
document.onkeydown = checkKey;

var to = document.getElementById("countries2")

function checkKey(e) {

    e = e || window.event;

    if (e.keyCode == '13') {
        removeChilds();
        getairports(encode_utf8(from.value), "airportFrom");
        getairports(encode_utf8(to.value), "airportTo")
    }

}
function removeChilds() {
    var list = document.getElementById("airportFrom");
    var list2 = document.getElementById("airportTo");

    while (list.hasChildNodes() && list2.hasChildNodes()) {
        list.removeChild(list.firstChild);
        list2.removeChild(list2.firstChild);
    }
    while (list2.hasChildNodes()) {
        list2.removeChild(list2.firstChild);
    }
}

function encode_utf8(s) {
    return encodeURIComponent(s);
}




