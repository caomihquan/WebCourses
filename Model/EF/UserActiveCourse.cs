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

        public long? UserID { get; set; }

        public long? CourseActiveID { get; set; }
    }
}
