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
                    setTimeout(function () { window.location = '/Home/CustomerList'; }, 1000);
                }
            },
            error: function () {
                alert("error");
            }
        });
    });
});