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
                        
                    } else {
                        btn.empty();
                        btn.append('<i class="far fa-bookmark"></i>');
                    }
                }
            });
        });
    }
}
user.init();