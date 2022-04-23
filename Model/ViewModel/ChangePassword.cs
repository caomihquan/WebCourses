using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Model.ViewModel
{
    public class ChangePassword
    {
        [Key]
        public long ID { set; get; }
        [Display(Name = "Mật khẩu Cũ")]
        
        public string Password { set; get; }
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không đúng.")]
        public string ConfirmPassword { set; get; }
        [Display(Name = "Mật Khẩu Mới")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        [Required(ErrorMessage = "Yêu cầu nhập mật khẩu mới")]
        public string NewPassword { set; get; }
    }
}