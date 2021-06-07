namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("NHANVIEN")]
    public partial class NHANVIEN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHANVIEN()
        {
            DONDATs = new HashSet<DONDAT>();
            PHIEUNHAPs = new HashSet<PHIEUNHAP>();
        }

        [Key]
        public int NV_MA { get; set; }

        [StringLength(128)]
        [DisplayName("Tài khoản")]
        public string NV_TAIKHOAN { get; set; }

        [StringLength(128)]
        [DisplayName("Mật khẩu")]
        public string NV_MATKHAU { get; set; }

        [StringLength(48)]
        [DisplayName("Họ tên")]
        public string NV_HOTEN { get; set; }

        [DisplayName("Giới tính")]
        public bool? NV_GIOITINH { get; set; }

        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        public string NV_SDT { get; set; }

        [StringLength(48)]
        public string NV_QUYENSD { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDAT> DONDATs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHIEUNHAP> PHIEUNHAPs { get; set; }
    }
}
