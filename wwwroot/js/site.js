


$(function () {

    var PlaceHolderElement = $('#PlaceHolderHere');

    $('button[data-toggle="ajax-modal"]').click(function (event) {

        
        var url = $(this).data('url');


       
        $.get(url).done(function (data) {
            
            PlaceHolderElement.html(data);
            PlaceHolderElement.find('.modal').modal('show');
        })
    })

    PlaceHolderElement.on('click', '[data-save="modal"]', function (event) {
        var form = $(this).parents('.modal').find('form');
        var actionUrl = form.attr('action');
        var sendData = form.serialize();
        console.log(sendData);

        $.post(actionUrl,sendData).done(function (data) {
          
            PlaceHolderElement.find('.modal').modal('hide');
        })

    })


})