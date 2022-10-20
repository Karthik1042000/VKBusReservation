$(document).ready(function () {
    $('#ReservationForm').submit(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Home/SaveTicket",
            type: "Post",
            data: $("#ReservationForm").serialize(),
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    setTimeout(function () { window.location = '/Home/ReservationList'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});