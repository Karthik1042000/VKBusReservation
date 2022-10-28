$(document).ready(function () {
    $('#LoginForm').onclick(function (e) {
        e.preventDefault();

        $.ajax({
            url: "/Home/Login",
            type: "Post",
            data: $("#LoginForm").serialize(),
            success: function (response) {
                
            },
            error: function () {
                alert("Enter the Valid Email Address");
            }
        });
    });
});

