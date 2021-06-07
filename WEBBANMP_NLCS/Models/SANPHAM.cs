namespace WEBBANMP_NLCS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SANPHAM")]
    public partial class SANPHAM
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SANPHAM()
        {
            CHITIETDONDATs = new HashSet<CHITIETDONDAT>();
            CHITIETPHIEUNHAPs = new HashSet<CHITIETPHIEUNHAP>();
            
        }

       
        [Key]
        public int SP_MA { get; set; }

        [DisplayName("Nhà sản xuất")]
        public int? NSX_MA { get; set; }

        [DisplayName("Loại sản phẩm")]
        public int? L_MA { get; set; }

        [Required]
        [StringLength(128)]
        [DisplayName("Tên sản phẩm")]
        public string SP_TEN { get; set; }

        [DisplayName("Giá")]
        [Column(TypeName = "money")]
        public decimal? SP_GIA { get; set; } 

        [DisplayName("Số lượng")]
        public int? SP_SL { get; set; } 

        [DisplayName("Mô tả")]
        public string SP_MOTA { get; set; }


        [DisplayName("Hình ảnh")]
        [StringLength(128)]
        public string SP_HINH { get; set; } 

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETDONDAT> CHITIETDONDATs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHITIETPHIEUNHAP> CHITIETPHIEUNHAPs { get; set; }

        public virtual LOAISANPHAM LOAISANPHAM { get; set; }

        public virtual NHASANXUAT NHASANXUAT { get; set; }
    }
}
