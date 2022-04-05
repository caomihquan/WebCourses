namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CertificateOwned")]
    public partial class CertificateOwned
    {
        public long ID { get; set; }

        public long? UserID { get; set; }
        [StringLength(250)]
        public string CertificateName { get; set; }

        public long? CertificateID { get; set; }

        [StringLength(250)]
        public string Logo { get; set; }

        [StringLength(250)]
        public string FileCertificate { get; set; }
    }
}
