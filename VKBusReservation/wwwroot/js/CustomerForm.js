$(document).ready(function ()
{
    $('#CustomerForm').submit(function (e) {
        e.preventDefault();
        $.ajax({
            url: "/Home/Save",
            type: "Post",
            data: $("#CustomerForm").serialize(),
            success: function (response) {
                alert(response.message);
                if (response.success == true) {
                    if (response.role == true) {
                        setTimeout(function () { window.location = '/Home/CustomerList'; }, 500);
                    }
                    else {
                        setTimeout(function () { window.location = '/Home/Index'; }, 500);
                    }
                    
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});