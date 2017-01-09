$(document).ready(function () {
    $.ajax({
        url: '/Article/GetTags',
        method: 'GET',
        data: {},
        dataType: 'json',

        success: function (res) {
            var data = res.data;

            $('#Tags').tokenfield({
                autocomplete: {
                    source: data,
                    delay: 100
                },
                showAutocompleteOnFocus: true
            })

        }
    })
})
