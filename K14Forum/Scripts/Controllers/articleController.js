$(document).ready(function () {
    $('.delete-article').off('click').on('click', function (e) {
        e.preventDefault();
        bootbox.confirm('Bạn chắc chắn muốn xóa bài viết này?', function (result) {
            if (result == true) {
                var id = $('.delete-article').data('id');
                $.ajax({
                    url: '/Article/Delete/' + id,
                    data: {},
                    method: 'POST',

                    success: function (res) {
                        $('#tr-article-' + id).remove();
                    }
                })
            }
        })
    })
})