using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCourses.Models
{
    public class LoginModel
    {
        [Key]
        [Display(Name = "Tên Đăng Nhập")]
        [Required(ErrorMessage = "Bạn Phải Nhập Tài Khoản")]
        public string UserName { set; get; }
        [Display(Name = "Mật Khẩu")]
        [Required(ErrorMessage = "Bạn Phải Nhập Mật Khẩu")]
        public string Password { set; get; }
        public bool RememberMe { set; get; }
        public string JavascriptToRun { get; set; }
    }
}