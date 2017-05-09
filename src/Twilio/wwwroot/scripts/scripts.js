$(document).ready(function () {
    $('.send-message').submit(function (event) {
        event.preventDefault();
        var newBody = $("#body").val();
        console.log(newBody);
        //$.ajax({
        //    url: 'Home/SendMessage',
        //    type: 'POST',
        //    dataType: 'json',
        //    data: $(this).serialize(),
        //    success: function (result) {
        //        var editedCupcake = result.name;
        //        var cupcakeId = result.id.toString();
        //        $('#' + cupcakeId).text(editedCupcake);
        //    }
        //});
    });
)};