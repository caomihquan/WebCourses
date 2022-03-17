var product = {
    init: function () {
        product.registerEvents();
    },
    registerEvents: function () {
        $('#btnComment').off('click').on('click', function (e) {
            var bad_words = new Array("death", "kill", "murder" ,"địt mẹ","lồn","địt","cặc","cc","đéo","fuck","fuck you","đĩ","đỹ","buồi","chết","giết","dmm","dm");
            var error = 0;
            var check_text = $('#reviewbinhluan').val()
            for (var i = 0; i < bad_words.length; i++) {
                var val = bad_words[i];
                if ((check_text.toLowerCase()).indexOf(val.toString()) > -1) {
                    error = error + 1;
                }
            }
            if (error > 0) {
                e.preventDefault();
                /*alert('Nội Dung Chứa Những Từ Ngữ Không Hợp Lệ')*/
                swal("Cảnh Báo!", "Nội Dung Chứa Những Từ Ngữ Không Hợp Lệ", "error");
            }
        });

        $('#forgotpassword').off('click').on('click', function (e) {
            var testEmail = /^[A-Z0-9._%+-]+@([A-Z0-9-]+\.)+[A-Z]{2,4}$/i;
            var text = $('#quenmatkhau').val()
            if (!testEmail.test(text)) {
                e.preventDefault();
                /*alert('Nội Dung Chứa Những Từ Ngữ Không Hợp Lệ')*/
                swal("Cảnh Báo!", "Không Đúng Định Dạng Email", "warning");
            }
        });

    },

}
product.init();