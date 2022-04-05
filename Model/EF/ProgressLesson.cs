namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProgressLesson")]
    public partial class ProgressLesson
    {
        public long ID { get; set; }

        public long? UserID { get; set; }

        public long? CourseID { get; set; }

        public long? LessonID { get; set; }

        public DateTime? CreatedDate { get; set; }

        
    }
}
