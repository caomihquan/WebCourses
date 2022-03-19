namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BlogSave")]
    public partial class BlogSave
    {
        public long ID { get; set; }

        public long? BlogID { get; set; }

        public long? UserID { get; set; }

        [StringLength(250)]
        public string MetaTitle { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public long? CategoryBlogID { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public bool Status { get; set; }
    }
}
