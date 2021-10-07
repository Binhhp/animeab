$(".index").removeClass("active");
$(".categories").addClass("active");

async function JTables() {

    await $("#myTable").DataTable({
        ...optionsDatabase,
        ajax: {
            url: "/anime/categories/all",
            method: "GET"
        },
        columns: [
            { data: "key", name: "key", 'className': 'text-center', orderable: false },
            { data: "title", name: "title", 'className': 'text-center', orderable: false },
            { data: "description", name: "description", 'className': 'text-center', orderable: false },
            {
                data: null,
                className: "text-center",
                render: function (data, type, row) {
                    var button =
                        '<a href="javascript:void(0);" class="btn btn-info btn-sm" data-toggle="modal" data-target="#modal-edit" onclick="Edit(this)"  data-key="' + data.key + '" data-title="' + data.title + '" data-description="' + data.description + '" title="Sửa"><i class="fa fa-edit"></i>Sửa</a>&nbsp;<a href="javascript:void(0);" class="btn btn-danger btn-sm" onclick="Delete(this)"  data-key="' + data.key + '" title="Xóa"><i class="fa fa-trash"></i>Xóa</a></td>';
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
        ...rules
    },
    messages: { ...messages },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-add").find(".form-control");
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
            url: "/anime/categories",
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

$("#modal__btn-add").click(function () {
    $("#form-add").find("input[class='form-control']").val("");
    $("#form-add").find(".form-control").removeClass("is-invalid");
    validator.resetForm();
});
///Edit
function Edit(event) {
    validatorEdit.resetForm();

    $("#form-edit").find(".form-control").removeClass("is-invalid");

    var data = $(event).data();

    for (var item in data) {
        if (item === "target" || item === "toggle") continue;
        var input = $("#form-edit").find('*[name=' + item + ']');
        input.val(data[item]);
    }
};

var validatorEdit = $("#form-edit").validate({
    rules: {
        ...rules
    },
    messages: { ...messages },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-edit").find(".edit");
        var formData = new FormData();
        var k = "";

        data.map(item => {
            var valueItem = $(data[item]).val();

            const key = $(data[item]).attr("data-keyboard");

            if (key === 'Key') {
                valueItem = valueItem.toLowerCase();
                k = valueItem;
            }

            formData.append(key, valueItem);
        })

        $.ajax({
            type: "PUT",
            url: "/anime/categories/" + k,
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
                $("#form-edit").find("input[class='edit']").val("");

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
///Delete

function Delete(event) {
    swal({
        title: "Bạn có muốn xóa bản ghi này?",
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
            url: '/anime/categories/' + key,
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