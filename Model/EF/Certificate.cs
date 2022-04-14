namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Certificate")]
    public partial class Certificate
    {
        public long ID { get; set; }

        [StringLength(250)]
        [Display(Name = "Tên Chứng Chỉ")]
        [Required(ErrorMessage = "Yêu cầu nhập")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập")]
        [Display(Name = "Mã Khóa Học")]
        public long? IDCourse { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Yêu cầu")]
        public string Logo { get; set; }

        [StringLength(250)]
        public string Image { get; set; }
    }
}
