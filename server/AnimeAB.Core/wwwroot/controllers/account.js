

$(".index").removeClass("active");
$(".user").addClass("active");

async function JTables() {

    await $("#myTable").DataTable({
        paging: true,
        searching: true,
        pageLength: 10,
        processing: true,
        serverSide: false,
        "bSort": false,
        "responsive": true,
        ajax: {
            url: "/anime/accounts",
            method: "GET"
        },
        columns: [
            { data: "email", name: "email", 'className': 'text-center', orderable: false },
            { data: "displayName", name: "name", 'className': 'text-center', orderable: false },
            {
                data: "isEmailVerified",
                className: 'text-center',
                render: function (data) {
                    if (data === true) {
                        return "<button class='btn btn-outline-info btn-sm'>Hoạt động</button>";
                    }
                    else {
                        return "<button class='btn btn-outline-danger btn-sm'>Cần xác nhận</button>";
                    }
                }
            },
            { data: "role", name: "role", 'className': 'text-center', orderable: false }
        ],
        "columnDefs": [{
            "searchable": false,
            "visible": false,
            "checkboxes": {
                'selectRow': true
            },
            targets: [0]
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
    messages: {
        ...messages
    },
    ...options,
    submitHandler: function (form) {
        var data = $("#form-add").find(".form-control");
        var formData = new FormData();

        data.map(item => {
            var valueItem = $(data[item]).val();
            const key = $(data[item]).attr("data-key");

            formData.append(key, valueItem);
        })

        $.ajax({
            type: "POST",
            url: "/anime/signup",
            data: formData,
            processData: false,
            contentType: false,
            beforeSend: function () {
                $("#btn-add").html("Loading...");
                $("#btn-add").addClass("disabled");
            },
            success: function () {
                toastr["success"]("Tạo tài khoản thành công");
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Tạo tài khoản');
                $("#btn-add").removeClass("disabled");
                var data = $("#form-add").find("input[class='form-control']");
                data.map(item => {
                    $(data[item]).val("")
                });
                $("#form-add").find("textarea[class='form-control']").val("");
                reloadTables();
            },
            error: function (errors) {
                if (errors.responseText !== "") {
                    toastr["error"](errors.responseText);
                }
                else {
                    toastr["error"]("Interval Server Error");
                }
                $("#btn-add").html('<i class="fa fa-plus"></i>&nbsp;Tạo tài khoản');
                $("#btn-add").removeClass("disabled");
            }
        })
    }
});
