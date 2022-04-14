namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [StringLength(250)]
        [Display(Name = "Hình Ảnh")]
        [Required(ErrorMessage = "Yêu cầu")]
        public string Image { get; set; }
        [Display(Name = "Mã Danh Mục Cha")]
        public long? ParentsID { get; set; }
        [Display(Name = "Thứ Tự")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public int? DisplayOrder { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Tổng Quan")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Overview { get; set; }

        [StringLength(500)]
        [Display(Name = "Mô Tả")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Description { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu Đề SEO")]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public int? ViewCount { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
}
