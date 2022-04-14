namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserActiveCourse")]
    public partial class UserActiveCourse
    {
        public long ID { get; set; }

        [Display(Name = "Mã Người Dùng")]
        public long? UserID { get; set; }
        [Display(Name = "Mã Khóa Học")]
        public long? CourseActiveID { get; set; }
    }
}
