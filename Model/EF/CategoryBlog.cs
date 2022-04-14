namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CategoryBlog")]
    public partial class CategoryBlog
    {
        public long ID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Danh Mục")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }

        [Display(Name = "Mã Danh Mục Cha")]
        public long? ParentsID { get; set; }

        [Display(Name = "Thứ Tự")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public int? DisplayOrder { get; set; }


        [StringLength(250)]
        [Display(Name = "Tiêu Đề SEO")]
        public string SeoTitle { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }
    }
}
