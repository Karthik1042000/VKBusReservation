$(document).ready(function () {
    $('#Busform').submit(function (e) {
        e.preventDefault();
        
        $.ajax({
            url: "/Home/SaveBus",
            type: "Post",
            data: $("#Busform").serialize(),
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    setTimeout(function () { window.location = '/Home/AllBuses'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});