// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


$(function () {
    $("#loaderbody").addClass('hide');

    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

showInPopup = (url, title) => {
    $.ajax({
        type: "GET",
        url: url,
        success: function (res) {
            $("#form-modal .modal-body").html(res);
            $("#form-modal .modal-title").html(title);
            $("#form-modal").modal('show');
        }
    })
}

//jQueryAjaxPost = form => {
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
//                if (res.isValid) {
//                    //$("#view-all ").html(res.html);
//                    $("#form-modal .modal-body").html('');
//                    $("#form-modal .modal-title").html('');
//                    $("form-modal").modal('hide');
//                    $.notify('submitted successfully', { globalPosition: "top-center", className:"success" });

//                } else {
//                    $("#form-modal .modal-body").html(res.html);
//                }
//            }
//        })
//    } catch (e) {
//        console.log(e);
//    }
//    return false;
//}

//jQueryAjaxDelete = form => {
//    if (confirm('Are you confirm to delete this record'));
//    try {
//        $.ajax({
//            type: 'POST',
//            url: form.action,
//            data: new FormData(form),
//            contentType: false,
//            processData: false,
//            success: function (res) {
//                if (res.isValid) {
//                    //$("#view-all ").html(res.html);
//                    $.notify('Delete successfully', { globalPosition: "top-center", className:"success" });

//                }
//            }
//        })
//    } catch (e) {
//        console.log(e);
//    }
//    return false
//}



jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    // Clear the modal
                    $("#form-modal .modal-body").html('');
                    $("#form-modal .modal-title").html('');
                    $("#form-modal").modal('hide');

                    // Show alert message
                    alert('Your data has been submitted successfully.');

                    // Notify user with a success message (optional)
                    $.notify('Submitted successfully', { globalPosition: "top-center", className: "success" });

                    // Refresh the page to reflect changes
                    location.reload();
                } else {
                    // If validation failed, show the form again
                    $("#form-modal .modal-body").html(res.html);
                }
            }
        });
    } catch (e) {
        console.log(e);
    }
    return false;
}


jQueryAjaxDelete = form => {
    if (confirm('Are you sure you want to delete this record?')) {
        try {
            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {
                    console.log('Response:', res); // Log the response
                    if (res.isValid) {
                        alert('Record deleted successfully.');
                        $.notify('Deleted successfully', { globalPosition: "top-center", className: "success" });

                        // Refresh the page
                        location.reload();
                    } else {
                        $.notify('Failed to delete record.', { globalPosition: "top-center", className: "error" });
                    }
                },
                error: function (xhr, status, error) {
                    console.log('AJAX Error:', error);
                    $.notify('An error occurred while deleting the record.', { globalPosition: "top-center", className: "error" });
                }
            });
        } catch (e) {
            console.log('Exception:', e);
        }
    }
    return false;
}

