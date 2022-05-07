namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Blog
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu Đề")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [Display(Name = "Mã Danh Mục Blog")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public long? CategoryBlogID { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình Ảnh")]
        public string Image { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Yêu cầu nhập mô tả bài viết")]
        public string Description { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Chi Tiết")]
        [Required(ErrorMessage = "Yêu cầu nhập nội dung bài viết")]
        public string Detail { get; set; }

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

        [Display(Name = "Lượt Xem")]
        public int? ViewCount { get; set; }

        [StringLength(500)]
        public string Tag { get; set; }
    }
}
