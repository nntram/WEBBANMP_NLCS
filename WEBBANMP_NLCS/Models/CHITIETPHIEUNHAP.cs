namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CHITIETPHIEUNHAP")]
    public partial class CHITIETPHIEUNHAP
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SP_MA { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PN_MA { get; set; }

        public int? SOLUONGNHAP { get; set; }

        [Column(TypeName = "money")]
        public decimal? DONGIANHAP { get; set; }

        public virtual PHIEUNHAP PHIEUNHAP { get; set; }

        public virtual SANPHAM SANPHAM { get; set; }
    }
}
