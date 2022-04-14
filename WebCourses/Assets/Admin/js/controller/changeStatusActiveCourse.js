var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.xetduyetkhoahoc').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/RequireActiveCourse/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Đã Mở');
                        btn.addClass('xetduyetkhoahoc btn btn-success')
                    } else {
                        btn.text('Đang Xử Lý');
                        btn.addClass('xetduyetkhoahoc btn btn-primary').removeClass("btn-success")
                    }
                }
            });
        });
    }
}
user.init();