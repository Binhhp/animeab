$(".index").removeClass("active");
$(".anime-series").addClass("active");

async function JTables() {

    await $("#myTable").DataTable({
        ...optionsDatabase,
        ajax: {
            url: `/anime/series/${series}/all`,
            method: "GET"
        },
        columns: [
            {
                data: "key", name: "key", 'className': 'text-center', orderable: false
            },
            {
                data: "season", name: "season", 'className': 'text-center', orderable: false
            },
            {
                data: "yearProduce", name: "yearProduce", 'className': 'text-center', orderable: false
            },
            {
                data: null,
                className: "text-center",
                render: function (data, type, row) {
                    var button = `<a href="#" onClick="Delete(this)" data-key="` + data.key + `" class="btn btn-danger btn-sm" title="Xóa series anime"><i class="fa fa-trash"></i>Xóa</a>&nbsp;`;

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

$("#anime").select2({
    dropdownParent: "#modal-add"
});

//Add
var validator = $("#form-add").validate({
    rules: {
        ...rules
    },
    messages: {
        ...messages
    },
    ...options,
    submitHandler: function (form) {
        let formData = new FormData();
        $("#anime").val().map((item, index) => {
            formData.append(`idAnimes[${index}]`, item);
        })
        
        $.ajax({
            type: "POST",
            url: `/anime/series/${series}`,
            data: formData,
            processData: false,
            dataType: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-add").html("Loading...");
                $("#btn-add").addClass("disabled");
            },
            success: function () {
                toastr["success"]("Tạo series thành công");
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Tạo series');
                $("#btn-add").removeClass("disabled");
                var data = $("#form-add").find("input[class='form-control']");
                data.map(item => {
                    $(data[item]).val("")
                });
                reloadTables();
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Tạo series');
                $("#btn-add").removeClass("disabled");
            }
        })
    }
});

$("#modal__btn-add").click(function () {
    $("#form-add").find(".form-control").val("");
    $("#form-add").find(".form-control").removeClass("is-invalid");
    validator.resetForm();
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
            url: `/anime/series/${series}/` + key,
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
