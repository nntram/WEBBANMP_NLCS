
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Controllers
{
    public class GioHangController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: GioHang

        public List<ITEMGIOHANG> LayGioHang()
        {
            List<ITEMGIOHANG> lstGioHang = Session["GioHang"] as List<ITEMGIOHANG>; //ép kiểu cho sesion

            if (lstGioHang == null)
            {
                //nếu giỏ hàng chưa tồn tại
                lstGioHang = new List<ITEMGIOHANG>();
                Session["GioHang"] = lstGioHang;
            }

            return lstGioHang;
        }

        public ActionResult ThemGioHangAjax(int MaSP)
        {
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == MaSP);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng
            List<ITEMGIOHANG> lstGioHang = LayGioHang();

            //sp đã tồn tại trong giỏ hàng
            ITEMGIOHANG spCheck = lstGioHang.SingleOrDefault(n => n.SP_MA == MaSP);
            if (spCheck != null)
            {
                if (sp.SP_SL <= spCheck.SOLUONG)
                {
                   // TempData["error"] = "Sản phẩm bạn chọn trong kho hiện không đủ!";
                }
                else
                {
                  //  TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công!";
                    spCheck.SOLUONG++;
                    spCheck.THANHTIEN = spCheck.SOLUONG * spCheck.GIABAN;
                }


            }
            else
            {
                //sp chưa tồn tại trong giỏ hàng
               // TempData["success"] = "Thêm sản phẩm mới vào giỏ hàng thành công!";
                ITEMGIOHANG itemGH = new ITEMGIOHANG(MaSP);
                lstGioHang.Add(itemGH);

            }

            ViewBag.TongSL = TinhTongSoLuong();
            return PartialView("GioHangPartial");
        }

        [HttpPost]
        public ActionResult ThemGioHangCT(FormCollection f)
        {
            int MaSP = int.Parse(f["maSP"]);
            int SoLuong = int.Parse(f["SoLuong"]);
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == MaSP);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng
            List<ITEMGIOHANG> lstGioHang = LayGioHang();

            //sp đã tồn tại trong giỏ hàng
            ITEMGIOHANG spCheck = lstGioHang.SingleOrDefault(n => n.SP_MA == MaSP);
            if (spCheck != null)
            {
                if (sp.SP_SL <= spCheck.SOLUONG)
                {
                    TempData["error"] = "Sản phẩm bạn chọn trong kho hiện không đủ!";
                }
                else
                {
                    TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công!";
                    spCheck.SOLUONG += SoLuong;
                    spCheck.THANHTIEN = spCheck.SOLUONG * spCheck.GIABAN;
                }


            }
            else
            {
                //sp chưa tồn tại trong giỏ hàng
                TempData["success"] = "Thêm sản phẩm vào giỏ hàng thành công!";
                ITEMGIOHANG itemGH = new ITEMGIOHANG(MaSP, SoLuong);
                lstGioHang.Add(itemGH);

            }


            return RedirectToAction("XemChiTiet", "SanPham", new { id = MaSP });
        }


        public int TinhTongSoLuong()
        {
            List<ITEMGIOHANG> lstGioHang = Session["GioHang"] as List<ITEMGIOHANG>;

            if (lstGioHang == null) return 0;

            return (int)lstGioHang.Sum(n => n.SOLUONG);

        }

        public decimal TinhTongTien()
        {
            List<ITEMGIOHANG> lstGioHang = Session["GioHang"] as List<ITEMGIOHANG>;

            if (lstGioHang == null) return 0;

            return (decimal)lstGioHang.Sum(n => n.THANHTIEN);

        }

        public ActionResult GioHangPartial()
        {

            ViewBag.TongSL = TinhTongSoLuong();

            return PartialView();
        }


        public ActionResult XemGioHang()
        {
            List<ITEMGIOHANG> lstGioHang = LayGioHang();
            if(Session["TaiKhoan"] != null)
            {
                ViewBag.TaiKhoanKH = Session["TaiKhoan"] as KHACHHANG;
            }
           
            return View(lstGioHang);

        }

        public ActionResult SuaGioHang(int MaSP)
        {

            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == MaSP);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng
            List<ITEMGIOHANG> lstGioHang = LayGioHang();
            ITEMGIOHANG spCheck = lstGioHang.SingleOrDefault(n => n.SP_MA == MaSP);

            if (spCheck == null)
                return RedirectToAction("Index", "Home");

            ViewBag.GioHang = lstGioHang;
            return View(spCheck);
        }


        [HttpPost]
        public ActionResult CapNhatGioHang(ITEMGIOHANG itemGH)
        {
            List<ITEMGIOHANG> lstGioHang = LayGioHang();
            ITEMGIOHANG itemUpdate = lstGioHang.Find(n => n.SP_MA == itemGH.SP_MA);
            itemUpdate.SOLUONG = itemGH.SOLUONG;
            itemUpdate.THANHTIEN = itemUpdate.SOLUONG * itemUpdate.GIABAN;
            return RedirectToAction("XemGioHang");
        }

        public ActionResult XoaGioHang(int MaSP)
        {

            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");
            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == MaSP);

            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            //lấy giỏ hàng
            List<ITEMGIOHANG> lstGioHang = LayGioHang();
            ITEMGIOHANG spCheck = lstGioHang.SingleOrDefault(n => n.SP_MA == MaSP);

            if (spCheck == null)
                return RedirectToAction("Index", "Home");

            lstGioHang.Remove(spCheck);

            return RedirectToAction("XemGioHang");
        }

        [HttpPost]
        public ActionResult DatHang(FormCollection f)
        {
            if (Session["GioHang"] == null)
                return RedirectToAction("Index", "Home");

            if (Session["TaiKhoan"] == null)
            {
                TempData["info"] = "Vui lòng đăng nhập để tiếp tục đặt hàng!";
                return RedirectToAction("DangNhap", "Home");
            }

            DONDAT dh = new DONDAT();

            //thêm đơn đặt
            dh.DD_NGAYDAT = DateTime.Now;
            dh.DD_TRANGTHAI = 0;

            KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
            dh.KH_MA = kh.KH_MA;
            dh.DD_DIACHI = f["DiaChi"];
            dh.DD_SDT = f["SoDienThoai"];
            dh.DD_GHICHU = f["GhiChu"];
            db.DONDATs.Add(dh);
            db.SaveChanges();

            //thêm chi thiết đơn đặt
            List<ITEMGIOHANG> lstGioHang = LayGioHang();
            foreach(var item in lstGioHang)
            {
                CHITIETDONDAT ctdh = new CHITIETDONDAT();
                ctdh.DD_MA = dh.DD_MA;
                ctdh.SP_MA = item.SP_MA;
                ctdh.SOLUONG = item.SOLUONG;
                ctdh.GIABAN = item.GIABAN;
                db.CHITIETDONDATs.Add(ctdh);
            }

            db.SaveChanges();
            Session["GioHang"] = null;
            TempData["success"] = "Đặt hàng thành công, cảm ơn quý khách!";
            return RedirectToAction("XemGioHang");
        }

    }
}

