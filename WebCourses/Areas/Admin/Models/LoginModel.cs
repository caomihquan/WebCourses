using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebCourses.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Your UserName")]
        [Display(Name = "Tên đăng nhập")]
        public string UserName { set; get; }
        [Required(ErrorMessage = "Enter Your Password")]
        [Display(Name = "Mật Khẩu")]
        public string Password { set; get; }
        [Display(Name = "Nhớ Mật Khẩu")]
        public bool RememberMe { set; get; }
    }
}