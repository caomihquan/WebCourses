namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Courses")]
    public partial class Cours
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Chứng Chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Yêu cầu")]
        [Display(Name = "Hình Ảnh")]
        public string Image { get; set; }

        [StringLength(500)]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        [Display(Name = "Mô Tả")]
        public string Description { get; set; }

        [Display(Name = "Mã Danh Mục")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public long? CategoryID { get; set; }

        [StringLength(250)]
        [Display(Name = "Video Tổng Quan")]

        public string VideoOverview { get; set; }

        [StringLength(50)]
        [Display(Name = "Cấp Độ")]
        public string LevelCourse { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Tổng Quan")]
        public string Overview { get; set; }

        [Display(Name = "Giá")]
        public decimal? Price { get; set; }

        [StringLength(250)]
        [Display(Name = "Tiêu Đề SEO")]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [StringLength(50)]
        public string KeyActive { get; set; }
        [Display(Name = "Lượt Xem")]
        public long? ViewCount { get; set; }
        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }
    }
}
