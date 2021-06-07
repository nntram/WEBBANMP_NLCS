namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("KHACHHANG")]
    public partial class KHACHHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KHACHHANG()
        {
            DONDATs = new HashSet<DONDAT>();
        }

        [Key]
        
        public int KH_MA { get; set; }

        [StringLength(128)]
        [DisplayName("Tên tài khoản:")]
        public string KH_TAIKHOAN { get; set; }

        [StringLength(255)]
        [DisplayName("Mật khẩu:")]
        public string KH_MATKHAU { get; set; }

        [StringLength(48)]
        [DisplayName("Họ và tên:")]
        public string KH_HOTEN { get; set; }

        [DisplayName("Giới tính")]
        public bool? KH_GIOITINH { get; set; }

        [DisplayName("Địa chỉ")]
        [StringLength(128)]
        public string KH_DIACHI { get; set; }

        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        public string KH_SDT { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONDAT> DONDATs { get; set; }
    }
}
