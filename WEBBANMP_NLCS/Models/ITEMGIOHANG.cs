using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBBANMP_NLCS.Models
{
    public class ITEMGIOHANG
    {
        public int SP_MA { get; set; }

        public string SP_TEN { get; set; }

        public string SP_HINH { get; set; }

        public int? SOLUONG { get; set; }

        public decimal? GIABAN { get; set; }

        public decimal? THANHTIEN { get; set; }

        public int? SOLUONGTON { get; set; }

        public ITEMGIOHANG() { }

        public ITEMGIOHANG(int MaSP)
        {
            using (ModelDbContext db = new ModelDbContext())
            {
                this.SP_MA = MaSP;
                SANPHAM sp = db.SANPHAMs.Single(n => n.SP_MA == MaSP);
                this.SP_TEN = sp.SP_TEN;
                this.SP_HINH = sp.SP_HINH;
                this.GIABAN = sp.SP_GIA.Value;
                this.SOLUONG = 1;
                this.THANHTIEN = GIABAN * SOLUONG;
                this.SOLUONGTON = sp.SP_SL;
            }
        }

        public ITEMGIOHANG(int MaSP, int soluong)
        {
            using (ModelDbContext db = new ModelDbContext())
            {
                this.SP_MA = MaSP;
                SANPHAM sp = db.SANPHAMs.Single(n => n.SP_MA == MaSP);
                this.SP_TEN = sp.SP_TEN;
                this.SP_HINH = sp.SP_HINH;
                this.GIABAN = sp.SP_GIA.Value;
                this.SOLUONG = soluong;
                this.THANHTIEN = GIABAN * SOLUONG;
                this.SOLUONGTON = sp.SP_SL;
            }
        }
    }
}