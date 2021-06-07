namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DONDAT")]
    public partial class DONDAT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONDAT()
        {
            CHITIETDONDATs = new HashSet<CHITIETDONDAT>();
        }

        [Key]
        [DisplayName ("Mã đơn")]
        public int DD_MA { get; set; }

        public int? NV_MA { get; set; }

        [DisplayName("Khách hàng")]
        public int? KH_MA { get; set; }

        [DisplayName("Ngày đặt")]
        public DateTime? DD_NGAYDAT { get; set; }

        [StringLength(255)]
        [DisplayName("Địa chỉ")]
        public string DD_DIACHI { get; set; }

        [StringLength(12)]
        [DisplayName("Số điện thoại")]
        public string DD_SDT { get; set; }

        [StringLength(128)]
        [DisplayName("Ghi chú")]
        public string DD_GHICHU { get; set; }

        [DisplayName("Trạng thái")]
        public int? DD_TRANGTHAI { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDAT> CHITIETDONDATs { get; set; }

        public virtual NHANVIEN NHANVIEN { get; set; }

        public virtual KHACHHANG KHACHHANG { get; set; }
    }
}
