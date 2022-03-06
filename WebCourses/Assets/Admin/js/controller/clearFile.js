var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.btn-clearallfile').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Lesson/ClearFilePath",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function () {
                    alert('Xóa Tất Cả File Thành Công');
                    location.reload();
                }
            });
        });
    }
}
user.init();