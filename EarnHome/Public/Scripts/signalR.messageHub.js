$(function () {
    var sendRequestHub = $.connection.sendRequestHub;
    sendRequestHub.client.addNewRequest = function ($.connection.hub.id, orderId) {
        $("#note_count").text = data("count")
        $("#noteText").text = data("text")
        $("#noteDate").text=data("date")

      
       
    };
    $.connection.hub.start().done(function () {
        $("#sendRequestHub").click(function () {
            sendRequestHub.server.send

        })
        

    }) 
})
