//Login
var formLogin = $("#form-login").validate({
    rules: {
        ...rules
    },
    messages: { ...messages },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-login").find(".form-control");
        var formData = new FormData();

        data.map(item => {
            var valueItem = $(data[item]).val();
            const key = $(data[item]).attr("data-key");

            if (key === 'Key') {
                valueItem = valueItem.toLowerCase();
            }

            formData.append(key, valueItem);
        })

        $.ajax({
            type: "POST",
            url: "/anime/login",
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-login").html("Loading...");
                $("#btn-login").addClass("disabled");
            },
            success: function (url) {
                window.location.href = url;
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-login").html('<i class="fa fa-sign-in fa-lg fa-fw"></i>Đăng nhập');
                $("#btn-login").removeClass("disabled");
            }
        })
    }
});
//Add
var formResetPassword = $("#form-reset").validate({
    rules: {
        ...rules
    },
    messages: { ...messages },
    ...options,
    submitHandler: function (form) {
        var email = $("#form-reset").find(".form-control");

        $.ajax({
            type: "GET",
            url: "/anime/password/" + email.val(),
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-reset").html("Loading...");
                $("#btn-reset").addClass("disabled");
            },
            success: function (url) {
                window.location.href = '@Url.Action("ConfirmEmail", "Account", Context.Request.Scheme)';
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                toastr["error"]("Error " + errors.responseText);
                $("#btn-reset").html('<i class="fa fa-unlock fa-lg fa-fw"></i>Cài lại');
                $("#btn-reset").removeClass("disabled");
            }
        })
    }
});