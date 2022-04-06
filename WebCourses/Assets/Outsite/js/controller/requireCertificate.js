var user = {
    init: function () {
        user.registerEvents();
    },
    registerEvents: function () {
        $('.requirecertificate').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            if (confirm('Bạn Có Muốn Lấy Chứng Chỉ?')) {
                $.ajax({
                    url: "/User/RequireCertificate",
                    data: { id: id },
                    dataType: "json",
                    type: "POST",
                    success: function () {
                        swal({
                            icon: "success",
                            title: "Đã Gửi",
                            text: "Yêu Cầu Của Bạn Đang Phê Duyệt Vui Lòng Check Email Thường Xuyên",
                            buttons: false,
                            timer: 5000
                        });
                    }
                });
            }
        });
    }
}
user.init();