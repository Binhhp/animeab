//Call Url from Destop
$("#btn-update").click(function () {
    var fileUpload = $("#fileUpload").get(0);
    var file = fileUpload.files;
    if (file.length === 0) {
        $("#error-file").text("Bạn cần chọn ảnh của bạn");
        return;
    }

    var display = $("#displayName").val();

    if (display === "") {
        $("#error-display-name").text("Bạn cần nhập tên của bạn");
        return;
    }
    var token = $("#token").val();
    if (token === "") return;

    var formData = new FormData();
    formData.append("FileUpload", file[0]);
    formData.append("DisplayName", display);
    formData.append("UserToken", token);

    $.ajax({
        type: "POST",
        url: "/anime/profile",
        data: formData,
        processData: false,
        contentType: false,
        beforeSend: function () {
            $("#btn-update").text("Loading...");
        },
        success: function (data) {
            toastr["success"]("Thay đổi thành công");

            $(".user-img").attr("src", data.photoUrl);
            $(".display-name").text(data.displayName);
            $(".name").text(data.displayName);
            $("#btn-update").text("Thay đổi");
            $("#error-file").text("");
            $("#error-display-name").text("");
            $("#fileUpload").val("");
            $("#displayName").val("");
        },
        error: function (error) {
            $("#btn-update").text("Thay đổi");
            $("#error-file").text("");
            $("#error-display-name").text("");
            $("#fileUpload").val("");
            $("#displayName").val("");
            toastr["error"](error.statusText + " " + error.status);
        }
    })
})
