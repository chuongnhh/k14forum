$(document).ready(function () {
    $('#frm-update-user').submit(function (e) {
        e.preventDefault();
        var url = $(this).attr('action');
        var method = $(this).attr('method');
        var data = $(this).serialize();

        $.ajax({
            url: url,
            method: method,
            data: data,
            success: function (res) {
                if (res.status == true) {
                    bootbox.alert('Update user informations successed.', function (result) { });
                }
                else {
                    bootbox.alert('Update user informations failed.', function (result) { });
                }
            }
        })
    })
})