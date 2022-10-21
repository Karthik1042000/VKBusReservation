$(document).ready(function () {
    $('#Reserve').submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Home/SelectBus",
            type: "Post",
            data: $("#Reserve").serialize(),
            success: function (response) {
                alert(response.message);
               
            },
            error: function () {
                alert("error");
            }
        });
    });
});
function getBusDetail() {
    if ($('#from').val() && $('#to').val()) {
        $.ajax({
            url: "/Home/GetBus?from=" + $('#from').val() + "&to=" + $('#to').val(),
            type: "Get",
            success: function (response) {
                console.log(response);
            },
            error: function () {
                alert("error");
            }
        });
    }
}