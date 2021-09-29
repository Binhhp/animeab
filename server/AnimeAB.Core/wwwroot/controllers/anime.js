$(".index").removeClass("active");
$(".anime").addClass("active");

(function Select2() {
    $("#categories").select2({
        maximumSelectionLength: 8,
        dropdownParent: "#modal-edit",
        allowClear: true,
        placeholder: "Chọn thể loại anime"
    });

    $("#cate-add").select2({
        maximumSelectionLength: 8,
        dropdownParent: "#modal-add",
        allowClear: true,
        placeholder: "Chọn thể loại anime"
    });
})();

$("#date-add").flatpickr({
    dateFormat: "Y-m-d",
    defaultDate: "today",
});

var flatpicker = $("#date-edit").flatpickr({
    dateFormat: "Y-m-d",
});

function JTables() {

    $("#myTable").DataTable({
        ...optionsDatabase,
        ajax: {
            url: "/anime/movies/all",
            type: "POST",
            contentType: "application/json",
            data: function (d) {
                d.Category = $("#sort-cate").val();
                d.Collection = $("#sort-collection").val();
                d.Status = parseInt($("#sort-status").val());
                d.Time = parseInt($("#sort-time").val());

                return JSON.stringify(d)
            },
        },
        columns: [
            { data: "key", name: "key", 'className': 'text-center', orderable: false },
            {
                data: "image", name: "image", 'className': 'text-center', orderable: false,
                render: function (data) {
                    return '<img src="' + data + '" class="image-admin"/>';
                }
            },
            { data: "title", name: "title", 'className': 'text-left', orderable: false },
            {
                data: "episode", name: "episode", 'className': 'text-center', orderable: false,
                render: function (data) {
                    if (data === 0) {
                        return "??? tập"
                    }
                    else {
                        return data + " tập"
                    }
                }
            },
            {
                data: "isStatus", name: "isStatus", 'className': 'text-center', orderable: false, render: function (data) {
                    if (data === 1) {
                        return '<span class="btn btn-outline-info btn-sm">Sắp công chiếu</span>'
                    }
                    if (data === 2) {
                        return '<span class="btn btn-outline-info btn-sm">Đang công chiếu</span>'
                    }
                    if (data === 3) {
                        return '<span class="btn btn-outline-info btn-sm">Đã hoàn thành</span>'
                    }
                }
            },
            {
                data: null, name: "categories", 'className': 'text-center', orderable: false,
                render: data => {
                    if (data.categories && data.categories !== null) {
                        return Object.keys(data.categories).toString();
                    }
                    return "";
                }
            },
            {
                data: null, name: "isBanner", 'className': 'text-center', orderable: false,
                render: function (data) {
                    if (data.isBanner) {
                        return `<button onClick="DestroyBanner(this)" data-key="` + data.key + `" class="btn btn-info btn-sm">Hủy</button>`;
                    }
                    else {
                        return '<button data-toggle="modal" data-target="#modal-banner" onClick="UploadBanner(this)" data-key="' + data.key + '" class="btn btn-info btn-sm">Đặt banner</button>';
                    }
                }
            },
            {
                data: null,
                className: "text-center",
                render: function (data, type, row) {

                    var dataCate = ""
                    if (data.categories) {
                        dataCate = Object.keys(data.categories).toString();
                    }
                    var button = '<a href="/anime/' + data.key + '" class="btn btn-outline-info btn-sm" title="Chi tiết phim"><i class="fa fa-search-plus"></i>Chi tiểt</a>&nbsp;'
                    button +=
                        '<a href="javascript:void(0);" class="btn btn-info btn-sm" data-toggle="modal" data-target="#modal-edit" onclick="Edit(this)"  data-key="' + data.key + '" data-title="' + data.title + '" data-titlevie="' + data.titleVie + '" data-description="' + data.description + '" data-trainer="' + data.trainer + '" data-episode="' + data.episode + '" data-seasonedit="' + data?.season + '" data-duration="' + data.movieDuration + '" data-date="' + data.dateRelease + '" data-url="' + data.image + '" data-type="' + data.type + '" data-categories="' + dataCate + '" title="Sửa"><i class="fa fa-edit"></i>Sửa</a>&nbsp;<a href="javascript:void(0);" class="btn btn-danger btn-sm" onclick="Delete(this)" data-key="' + data.key + '" title="Xóa"><i class="fa fa-trash"></i>Xóa</a></td>';
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

            if (key === 'Categories') {
                valueItem.map((item, index) => {
                    formData.append(`Categories[${index}]`, item);
                })
            }
            else {
                formData.append(key, valueItem);
            }
        })

        $.ajax({
            type: "POST",
            url: "/anime/movies",
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
                resetInput();
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
    resetInput();
});

function DestroyBanner(event) {
    const key = $(event).attr("data-key");
    $.ajax({
        type: 'GET',
        url: "/anime/movies/" + key + "/banner",
        contentType: false,
        processData: false,
        success: function () {
            toastr["success"]("Hủy thành công");
            reloadTables();
        },
        error: function (errors) {
            toastr["error"]("Error " + errors.responseText);
        }
    })
};

function UploadBanner(event) {
    $("#form-banner input[data-key='FileUpload']").val("");
    const key = $(event).attr("data-key");

    $("#form-banner input[data-key='Key']").val(key);
}
//Add
var validatorBanner = $("#form-banner").validate({
    rules: {
        file: {
            required: true,
            extension: "jpg|jpeg|png"
        }
    },
    messages: {
        file: {
            required: "Bạn cần chọn file ảnh",
            extension: "Định dạng ảnh không đúng"
        }
    },
    ...options,
    submitHandler: function (form) {
        var file = $("#form-banner input[data-key='FileUpload']");
        var formData = new FormData();
        var key = $("#form-banner input[data-key='Key']");
        var valueItem = ((file.get(0)).files)[0];

        formData.append("file", valueItem);
        $.ajax({
            type: "POST",
            url: "/anime/movies/" + key.val() + "/banner",
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-add-banner").html("Loading...");
                $("#btn-add-banner").addClass("disabled");
            },
            success: function () {
                toastr["success"]("Đã đặt banner cho phim");
                $("#btn-add-banner").html('<i class="fa fa-plus"></i>&nbsp;Đặt banner');
                $("#btn-add-banner").removeClass("disabled");
                $("#modal-banner").modal("toggle");
                resetInput();
                reloadTables();
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Đặt banner');
                $("#btn-add").removeClass("disabled");
            }
        })
    }
});
///Edit
function Edit(event) {

    $("#form-edit").find(".form-control").removeClass("is-invalid");

    var data = $(event).data();
    $("input[type='file']").val("");

    for (var item in data) {

        if (item === "date") {
            flatpicker.setDate(new Date(data[item]), true);
            continue;
        }
        if (item === "target" || item === "toggle") continue;
        if (item === "categories" && data[item] !== "") {
            const select2 = data[item].split(',');

            $("#categories").val(select2);
            $("#categories").trigger('change');
            continue;
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
            extension: "Định dạng ảnh không đúng"
        }
    },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-edit").find(".edit");
        var formData = new FormData();
        var k = "";

        data.map(item => {
            var valueItem = $(data[item]).val();

            const key = $(data[item]).attr("data-keyboard");

            if (key === "FileUpload") {
                const fileUpload = $(data[item]).get(0);
                valueItem = (fileUpload.files)[0];

            }

            if (key === 'Key') {
                valueItem = valueItem.toLowerCase();
                k = valueItem;
            }

            if (key === 'Categories') {
                valueItem.map((item, index) => {
                    formData.append(`Categories[${index}]`, item);
                })
            }
            else {
                formData.append(key, valueItem);
            }
        })

        $.ajax({
            type: "PUT",
            url: "/anime/movies/" + k,
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
                $("#form-edit").find("textarea[class='form-control']").val("");

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

function resetInput() {
    $("#form-add").find("input[class='form-control']").val("");
    $("#form-add").find("textarea[class='form-control']").val("");
    $("#form-add").find(".form-control").removeClass("is-invalid");
    $("#cate-add").val("").trigger("change");

    validator.resetForm();
};

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
            url: '/anime/movies/' + key,
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
}