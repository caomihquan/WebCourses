
namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RequireCertificate")]
    public partial class RequireCertificate
    {
        public long ID { get; set; }

        public long UserID { get; set; }
        public long CertificateID { get; set; }

        [StringLength(50)]
        public string Email { get; set; }
        public DateTime? CreatedDate { get; set; }

    }
}
