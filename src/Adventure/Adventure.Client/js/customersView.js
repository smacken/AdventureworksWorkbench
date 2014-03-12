/// <reference path="../Scripts/protobuf.js" />

$(document).ready(function () {

    var customersRequest = {
        url: '/api/customers',
        type: "GET",
        contentType: 'application/x-protobuf; charset=UTF-8',
    };

    $.ajax(customersRequest)
     .done(function(response) {
         console.log(response);
         var protobuf = new PROTO.ByteArrayStream;
         //response.serializeToStream(protobuf);
         //var results = protobuf.ParseFromStream(response);
         //console.log(protobuf);
         //PROTO.de
         //window.protobuf = new PROTO.Base64Stream;
         //window.customers = response;
     });

});