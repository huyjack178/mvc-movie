var loginUrl = "/ClientUser/Login/";

function login() {
    var data = {
        __RequestVerificationToken: $(':input[name="__RequestVerificationToken"]').val(),
        UserName: $(".user-name").val(),
        Password: $(".password").val()
    };

    $.ajax({
        url: loginUrl,
        type: 'POST',
        data: data,
        success: function () {
            location.reload();
        },
        error: function (xhr, statusCode) {
            console.log(xhr.status);
            if (xhr.status === 404) {
                $(".invalid-error").text("Invalid User name or password");
            }
        }
    });
};