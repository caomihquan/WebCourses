namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Lesson
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Bài Học")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [Display(Name = "Mã Bài Học Cha")]
        public long? ParentsID { get; set; }

        [StringLength(250)]
        public string Video { get; set; }

        [StringLength(500)]
        [Display(Name = "Tệp")]
        public string MoreFiles { get; set; }

        [Display(Name = "Mã Khóa học")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public long? CourseID { get; set; }

        [Display(Name = "Thứ Tự")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public int? DisplayOrder { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Bài Tập")]
        public string HomeWork { get; set; }

        [Column(TypeName = "ntext")]
        [Display(Name = "Nội Dung")]
        public string Content { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        [Display(Name = "Trạng Thái")]
        public bool Status { get; set; }

        [Display(Name = "Lượt Xem")]
        public int? ViewCount { get; set; }
    }
}
