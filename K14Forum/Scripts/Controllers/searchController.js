$(document).ready(function () {
    $.ajax({
        url: '/Home/GetDataForSearch',
        method: 'GET',
        data: {},
        dataType: 'json',

        success: function (res) {
            var data = res.data;

            $("#txtSearch").autocomplete({
                source: data
            });

        }
    })
})