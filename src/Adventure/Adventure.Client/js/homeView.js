$(document).ready(function() {

    var request = {
        url: '/api/products',
        type: 'GET',
        //contentType: 'application/json',
        contentType: 'application/x-msgpack; charset=UTF-8',
        //encoding: 'UTF-8',
        inputEncoding: 'UTF-8',
        //dataType: 'x-msgpack',
        //success: function(results) {
        //    console.log(results);
        //}
    };
    console.log('starting');

    $.ajax(request)
     .done(function (data) {
        //console.log("raw");
        console.log(data);
         window.prodRaw = data;
        //var products = msgpack.unpack(data);
        //var products = CSMsgPack.unpack(data);
        console.log('Products');
        //console.log(products);
     })
     .error(function(error) {
         console.log(error);
     });

    //var msgPost = msgpack.pack({ id: 666, name: 'pen' });
    //console.log(msgPost);
    //var response = {
    //    url: '/api/product',
    //    type: 'POST',
    //    //contentType: 'application/json',
    //    //contentType: 'application/x-msgpack; charset=UTF-8',
    //    data: msgPost
    //};
    
    //$.ajax(response)
    // .done(function(result) {
    //     console.log(result);
    // });
});