function makeRequest(url, method, payload) {
    var data = JSON.stringify(payload);
    $.ajax({
        url: url,
        type: method,
        dataType: "json",
        data: data,
        contentType: 'application/json; charset=utf-8'
    }).done(function (res) {
        alert("Success");
    }).fail(function () {
        alert('error');
    });

    //$.ajax({
    //    url: url, // to get the right path to controller from TableRoutes of Asp.Net MVC
    //    dataType: "json", //to work with json format
    //    type: "POST", //to do a post request
    //    contentType: 'application/json; charset=utf-8', //define a contentType of your request
    //    cache: false, //avoid caching results
    //    data: payload, // here you can pass arguments to your request if you need
    //    success: function (data) {

    //    },
    //    error: function (xhr) {
    //        alert('error');
    //    }
    //});

}

