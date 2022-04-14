namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CourseActive")]
    public partial class CourseActive
    {
        public long ID { get; set; }


        public int TransactionID { get; set; }

        public long UserID { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long CourseID { get; set; }

        public bool Status { get; set; }
    }
}
