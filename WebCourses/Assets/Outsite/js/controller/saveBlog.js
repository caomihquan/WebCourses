var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.saveblog').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Blog/AddChangeBlogSaveStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.empty();
                        btn.append('<i class="fas fa-bookmark"></i>');
                        swal({
                            type: "success",
                            title: "Đã Lưu",
                            text: "Bạn Đã Lưu Blog Thành Công",
                            buttons: false,
                            timer: 1500
                        });
                    } else {
                        btn.empty();
                        btn.append('<i class="far fa-bookmark"></i>');
                        swal({
                            type: "success",
                            title: "Đã Xóa",
                            text: "Bạn Đã Bỏ Lưu Blog",
                            buttons: false,
                            timer: 1500
                        });
                    }
                }
            });
        });
    }
}
user.init();