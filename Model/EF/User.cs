namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Yêu cầu nhập tên đăng nhập")]
        public string UserName { get; set; }

        [StringLength(32, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 ký tự.")]
        
        [Display(Name = "Mật Khẩu")]
        public string Password { get; set; }

        [StringLength(20)]
        [Display(Name = "Nhóm Người Dùng")]
        public string GroupID { get; set; }

        [StringLength(50)]
        [Display(Name = "Họ Tên")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [StringLength(50)]
        [Display(Name = "Địa Chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        [EmailAddress(ErrorMessage = "Không đúng Định Dạng Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Display(Name = "Số Điện Thoại")]
        public string Phone { get; set; }
        [Display(Name = "Ngày Tạo")]
        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người Tạo")]
        public string CreatedBy { get; set; }
        [Display(Name = "Ngày Sửa")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        [Display(Name = "Người Sửa")]
        public string ModifiedBy { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
}
