﻿function ConfirmDelete(id) {
    let result = confirm("Are you sure you want to delete?");
    if (result) {
        $.ajax({
            type: "get",
            url: "/Home/DeleteBus?id=" + id,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
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
    }
}
function GoBus() {
    setTimeout(function () { window.location = '/Home/AllBuses'; }, 100);
}