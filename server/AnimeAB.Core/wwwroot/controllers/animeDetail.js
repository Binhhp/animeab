$(".index").removeClass("active");
$(".anime").addClass("active");

async function JTables() {

    await $("#myTable").DataTable({
        ...optionsDatabase,
        ajax: {
            url: `/anime/${animeKey}/all`,
            method: "GET"
        },
        columns: [
            { data: "key", name: "key", 'className': 'text-center', orderable: false },
            {
                data: "image", name: "image", 'className': 'text-center', orderable: false,
                render: function (data) {
                    return '<img src="' + data + '" class="image-admin" />';
                }
            },
            { data: "title", name: "title", 'className': 'text-center', orderable: false },
            { data: "views", name: "views", 'className': 'text-center', orderable: false },
            { data: "link", name: "link", 'className': 'text-center text-impact', orderable: false },
            {
                data: "iframe", name: "iframe", 'className': 'text-center', orderable: false,
                render: function (data) {
                    if (data) {
                        return '<button class="btn btn-light btn-sm">Iframe</button>';
                    }
                    else {
                        return '<button class="btn btn-light btn-sm">Mp4 - Video</button>';
                    }
                }
            },
            {
                data: null,
                className: "text-center",
                render: function (data, type, row) {
                    var button =
                        '<a href="javascript:void(0);" class="btn btn-info btn-sm" data-toggle="modal" data-target="#modal-edit" onclick="Edit(this)" data-linkvg="' + data?.linkVuighe + '" data-linkhh="' + data?.linkHH247 + '"   data-key="' + data.key + '" data-title="' + data.title + '" data-url="' + data.image + '" data-iframe="' + data.iframe + '" data-link="' + data.link + '" data-episode="' + data.episode + '" title="Sửa"><i class="fa fa-edit"></i>Sửa</a>&nbsp;<a href="javascript:void(0);" class="btn btn-danger btn-sm" onclick="Delete(this)" data-key="' + data.key + '" title="Xóa"><i class="fa fa-trash"></i>Xóa</a></td>';
                    return button;
                },
            }
        ],
        "columnDefs": [{
            "searchable": false,
            "visible": false,
            "checkboxes": {
                'selectRow': true
            }
        }],
        "select": {
            'style': 'multi'
        },
    });
};

JTables();

//Reload tables
async function reloadTables() {
    await $("#myTable").DataTable().ajax.reload();
};

//Add
var validator = $("#form-add").validate({
    rules: {
        ...rules,
        file: {
            required: true,
            extension: "jpg|jpeg|png"
        }
    },
    messages: {
        ...messages,
        file: {
            required: "Bạn cần chọn file ảnh",
            extension: "Định dạng ảnh không đúng"
        }
    },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-add").find(".form-control");
        var formData = new FormData();

        data.map(item => {
            var valueItem = $(data[item]).val();
            const key = $(data[item]).attr("data-key");

            if (key === "FileUpload") {
                const fileUpload = $(data[item]).get(0);
                valueItem = (fileUpload.files)[0];

            }

            if (key === 'Key') {
                valueItem = valueItem.toLowerCase().replace("/", "");
            }

            if (key === 'Iframe') {
                valueItem = $(data[item]).is(":checked");
            }

            formData.append(key, valueItem);
        })

        $.ajax({
            type: "POST",
            url: `/anime/${animeKey}`,
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-add").html("Loading...");
                $("#btn-add").addClass("disabled");
            },
            success: function () {
                toastr["success"]("Thêm mới thành công");
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Thêm mới');
                $("#btn-add").removeClass("disabled");

                $("#form-add").find("input[class='form-control']").val("");

                reloadTables();
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Thêm mới');
                $("#btn-add").removeClass("disabled");
            }
        })
    }
});

$("#btn-toggle-add").click(function () {
    $("#form-add").find("input[class='form-control']").val("");
    $("#form-add").find(".form-control").removeClass("is-invalid");
    validator.resetForm();
});
///Edit
function Edit(event) {
    validatorEdit.resetForm();

    $("#form-edit").find(".form-control").removeClass("is-invalid");

    var data = $(event).data();
    $("input[type='file']").val("");

    for (var item in data) {
        if (item === "target" || item === "toggle") continue;
        if (item === "iframe") {
            $("#form-edit").find('*[name=' + item + ']').prop('checked', data[item]);
        }
        var input = $("#form-edit").find('*[name=' + item + ']');
        input.val(data[item]);
    }
};


var validatorEdit = $("#form-edit").validate({
    rules: {
        ...rules,
        file: {
            required: false,
            extension: "jpg|jpeg|png"
        }
    },
    messages: {
        ...messages,
        file: {
            required: "Bạn cần chọn file ảnh",
            extension: "Định dạng ảnh không đúng"
        }
    },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-edit").find(".edit");
        var formData = new FormData();

        data.map(item => {
            var valueItem = $(data[item]).val();

            const key = $(data[item]).attr("data-keyboard");

            if (key === "FileUpload") {
                const fileUpload = $(data[item]).get(0);
                valueItem = (fileUpload.files)[0];

            }

            if (key === 'Key') {
                valueItem = valueItem.toLowerCase();
            }

            if (key === 'Iframe') {
                valueItem = $(data[item]).is(":checked");
            }

            formData.append(key, valueItem);
        })

        $.ajax({
            type: "PUT",
            url: `/anime/${animeKey}`,
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-edit").html("Loading...");
                $("#btn-edit").addClass("disabled");
            },
            success: function () {
                toastr["success"]("Cập nhật thành công");
                $("#modal-edit").modal("toggle");
                $("#btn-edit").html('<i class="fa fa-plus"></i>&nbsp;Cập nhật');
                $("#btn-edit").removeClass("disabled");
                $("#form-edit").find("input[class='form-control']").val("");
                reloadTables();
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-edit").html('<i class="fa fa-plus"></i>&nbsp;Cập nhật');
                $("#btn-edit").removeClass("disabled");

            }
        })
    }
});

function Delete(event) {
    swal({
        title: "Bạn có muốn xóa anime này?",
        type: "warning",
        showCancelButton: true,
        confirmButtonText: "Có, xóa nó!",
        cancelButtonText: "Không, không cần đâu!",
        closeOnConfirm: false,
        closeOnCancel: true
    }, (isConfirm) => {
        if (isConfirm) {
            halderDelete();
        }
    });

    const halderDelete = async () => {
        var key = $(event).attr('data-key');

        $.ajax({
            type: 'DELETE',
            url: `/anime/${animeKey}/` + key,
            contentType: false,
            processData: false,
            success: function () {
                toastr["success"]("Xóa thành công");
                swal.close();
                reloadTables();
            },
            error: function (errors) {
                swal("Oh no!", "Error " + errors.status + " " + errors.responseText, "error");
            }
        })
    }
};
