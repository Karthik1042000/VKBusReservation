$(document).ready(function () {
    $('#Reserve').submit(function (e) {
        e.preventDefault();
        if ($('#BusId').val()) {
            $.ajax({
                url: "/Home/GetBus",
                type: "Post",
                data: $("#Reserve").serialize(),
                success: function (response) {
                    alert(response.message);
                },
                error: function () {
                    alert("error");
                }
            });
        }
        else {
            alert("Please select the bus");
        }
    });
});
function getBusDetail() {
    if ($('#From').val() && $('#To').val()) {
        $.ajax({
            url: "/Home/GetBus?from=" + $('#From').val() + "&to=" + $('#To').val(),
            type: "Get",
            success: function (data) {
                console.log(data);
                var table = $('#busList tbody');
                table.html('');
                $.each(data, function (i, v) {
                    let sTime = new Date(v.tripStartTime);
                    let eTime = new Date(v.tripEndTime);
                    table.append('<tr>').append('<td class="text-center">' + v.busId + '</td>')
                        .append('<td class="text-center">' + v.busName + '</td>')
                        .append('<td class="text-center">' + v.busNumber + '</td>')
                        .append('<td class="text-center">' + v.ticketPrice + '</td>')
                        .append('<td class="text-center">' + v.totalSeats + '</td>')
                        .append('<td class="text-center">' + sTime.getHours() + ":" + sTime.getMinutes() + '</td>')
                        .append('<td class="text-center">' + eTime.getHours() + ":" + eTime.getMinutes() + '</td>')
                        .append('<td class="text-center"><button class="btn btn-success" onclick="setBus(\'' + v.busId + '\',\'' + v.busNumber + '\')">Select</button></td>')
                        .append('</tr>');

                });
                $('#busList').show();
            },
            error: function () {
                alert("error");
            }
        });
    }
    else {
        $('#busList').hide();
    }
}

function setBus(busId,busNumber) {
    $('#BusId').val(busId);
    $('#busNumber').text(busNumber);
    //$('#reservedSeat').text(bus.busNumber);
    //$('#availableSeat').text(bus.busNumber);
    $('#busList').hide();
    alert("Bus Selected Successfully");
}

function getReservation(busId) {
    $.ajax({
        url: "/Home/GetReservation?id=" + busId,
        type: "Get",
        success: function (response) {
            if (response == null) {
                response.available
            }
        }
    });
}