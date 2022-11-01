$(document).ready(function () {
    $('#Reserve').submit(function (e) {
        e.preventDefault();
        if ($('#busId').val()) {
            $.ajax({
                url: "/Home/SaveTicket",
                type: "Post",
                data: $("#Reserve").serialize(),
                success: function (response) {
                    alert(response.message);
                    if (response.success == true) {
                        setTimeout(function () { window.location = '/Home/Index'; }, 500);
                    }
                },
                error: function () {
                    alert("error");
                }
            });
        }
        else {
            alert("error ");
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
                if (data.length != 0) {

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
                            .append('<td class="text-center"><button type="button" class="btn btn-outline-success" onclick="setBus(\'' + v.busId + '\',\'' + v.busNumber + '\',\'' + v.totalSeats + '\')">Select</button></td>')
                            .append('</tr>');

                    });
                    $('#busList').show();
                }
                   
                else {
                    table.append('<tr>')
                        .append('<td colspan="8" class="text-center"> No Data To Display !!!! </td>')
                        .append('</tr>');
                    $('#busList').show();
                }
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

function setBus(busId,busNumber,totalSeat) {
    $('#busId').val(busId);
    $('#totalNumberOfSeat').val(totalSeat);
    $('#busNumber').text(busNumber);
    $('#busNumber').val(busNumber);
    alert("Bus Selected Successfully");
    $('#busList').hide();
}

function getReservation() {
    console.log($('#busId').val());
    
    console.log($('#Reservationdate').val());
    if ($('#busId').val() && $('#Reservationdate').val()) {
        $.ajax({
            url: "/Home/GetReservation?id=" + $('#busId').val() + "&date=" + $('#Reservationdate').val(),
            type: "Post",
            success: function (data) {
                if (data == null) {
                    $('#reservedSeat').text(0);
                    $('#availableSeat').text($('#totalNumberOfSeat').val());
                    $('#reservedSeats').val(0);
                    $('#availableSeats').val($('#totalNumberOfSeat').val());
                }
                else {
                    $('#reservedSeat').text(data.reservedSeats);
                    $('#availableSeat').text(data.availableSeats);
                    $('#reservedSeats').val(data.reservedSeats);
                    $('#availableSeats').val(data.availableSeats);
                }
            }
        });
    }
    else {
        alert("error");
    }
}