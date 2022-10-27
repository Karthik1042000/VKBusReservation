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
    $('.toast').toast('show');
    $('#TripStartTime').on('change', function () {
        var date = $(this).val();
        $('#TripEndTime').attr('min', date);
    });
    $('#TripEndTime').on('change', function () {
        if (new Date($(this).val()) < new Date($('#TripStartTime').val()))
        {
            alert('Trip End Date and Time Should be greater than Trip Start Date and Time')
        }
    });
});
