namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHASANXUAT")]
    public partial class NHASANXUAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHASANXUAT()
        {
            SANPHAMs = new HashSet<SANPHAM>();
        }

        [Key]
        public int NSX_MA { get; set; }

        [StringLength(128)]
        [DisplayName ("Tên nhà sản xuất")]
        public string NSX_TEN { get; set; }

        [StringLength(255)]
        [DisplayName("Mô tả")]
        public string NSX_MOTA { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}
