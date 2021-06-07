namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHACUNGCAP")]
    public partial class NHACUNGCAP
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGCAP()
        {
            PHIEUNHAPs = new HashSet<PHIEUNHAP>();
        }

        [Key]
        public int NCC_MA { get; set; }

        [StringLength(128)]
        [DisplayName ("Tên nhà cung cấp")]
        public string NCC_TEN { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string NCC_DIACHI { get; set; }

        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        public string NCC_SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    }
}
