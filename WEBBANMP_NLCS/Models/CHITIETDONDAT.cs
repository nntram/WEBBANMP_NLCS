namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETDONDAT")]
    public partial class CHITIETDONDAT
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SP_MA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DD_MA { get; set; }

        public int? SOLUONG { get; set; }

        [Column(TypeName = "money")]
        public decimal? GIABAN { get; set; }

        public virtual DONDAT DONDAT { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
