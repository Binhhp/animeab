$(".index").removeClass("active");
$(".cache").addClass("active");

(async function JTables() {

    await $("#myTable").DataTable({
        ...optionsDatabase,
        ajax: {
            url: "/cache/all",
            method: "GET"
        },
        columns: [
            { data: "key", name: "key", 'className': 'text-center', orderable: false },
            {
                data: null,
                className: "text-center",
                render: function (data, type, row) {
                    var button =
                        '<a href="javascript:void(0);" class="btn btn-danger btn-sm" onclick="Delete(this)"  data-key="' + data.key + '" title="Xóa"><i class="fa fa-trash"></i>Xóa</a></td>';
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
})();


//Reload tables
async function reloadTables() {
    await $("#myTable").DataTable().ajax.reload();
};
///Delete

function Delete(event) {
    swal({
        title: "Bạn có muốn xóa cache này?",
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
            url: `/cache?cacheKey=${key}`,
            success: function () {
                toastr["success"]("Xóa cache thành công");
                swal.close();
                reloadTables();
            },
            error: function (errors) {
                swal("Oh no!", "Error " + errors.status + " " + errors.responseText, "error");
            }
        })
    }
}