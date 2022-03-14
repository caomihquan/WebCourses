var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.duyetblog').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Blog/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (response) {
                    if (response.status == true) {
                        btn.text('Đã Duyệt');
                        btn.addClass('duyetblog btn btn-success')
                    }else {
                        btn.text('Duyệt');
                        btn.addClass('duyetblog btn btn-primary').removeClass("btn-success")
                    }
                }
            });
        });
    }
}
user.init();