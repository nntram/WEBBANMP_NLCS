using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using MyLibrary;

namespace WEBBANMP_NLCS.Controllers
{
    public class HomeController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        public ActionResult Index()
        {
            ViewBag.lstSanPham = db.SANPHAMs.OrderByDescending(n => n.SP_MA).Take(9);
            ViewBag.titlePartial = "Sản phẩm mới";
            return View();
        }

        [ChildActionOnly]
        public ActionResult MenuPartial()
        {
            var lstSP = db.SANPHAMs;

            return PartialView(lstSP);
        }


        [HttpGet]
        public ActionResult DangKy()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(KHACHHANG kh, FormCollection f)
        {
            ViewBag.ThongBao = "";
            //Kiểm tra captcha hợp lệ
            if (!this.IsCaptchaValid("Captcha is not valid!"))
                ViewBag.ThongBao = "Sai mã captcha!";
            else
            {
                if (ModelState.IsValid)
                {
                    var kttk = db.KHACHHANGs.FirstOrDefault(n => n.KH_TAIKHOAN == kh.KH_TAIKHOAN);
                    var ktmk = (f["nhapmatkhau"].ToString() == (f["nhaplaimatkhau"].ToString()));

                    if (kttk == null && ktmk)
                    {
                        ViewBag.ThongBao = "Đăng ký thành công! Vui lòng đăng nhập để đặt hàng.";

                        kh.KH_MATKHAU = Tool.MaHoaMatKhau(f["nhapmatkhau"]);

                        db.KHACHHANGs.Add(kh); //lưu vào dataset
                        db.SaveChanges();//thêm vào csdl

                    }
                    else if(kttk != null)
                        ViewBag.ThongBao = "Tên tài khoản đã tồn tại! Vui lòng chọn tên tài khoản khác.";
                    else
                        ViewBag.ThongBao = "Mật khẩu mới gõ lại không chính xác.";
                }

            }

            return View();
        }

  
        [HttpGet]
        public ActionResult DangNhap()
        {

            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            if (ModelState.IsValid)
            {
                string sname = f["username"].ToString();
                string spass = Tool.MaHoaMatKhau(f["password"]);

                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.KH_TAIKHOAN == sname && n.KH_MATKHAU == spass);

                //nếu thành công
                if (kh != null)
                {
                    kh.KH_MATKHAU = ""; //Không lưu mật khẩu để bảo mật
                    Session["TaiKhoan"] = kh;
                    return JavaScript("window.location = '" + Url.Action("XemGioHang", "GioHang") + "'");
                }

                return Content("Sai tên đăng nhập hoặc mật khẩu!");
            }

            //nếu không thành công
            return View();
            
        }

        
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;

            return RedirectToAction("Index");
        }

     
        [ChildActionOnly]
        public ActionResult ThongBaoPartial()
        {
            return PartialView();
        }


        [HttpGet]
        public ActionResult SuaThongTin()
        {
            if (Session["TaiKhoan"] != null)
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                return View(kh);
            }
            
                return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult SuaThongTin(KHACHHANG kh)
        {
            KHACHHANG kh_edit = db.KHACHHANGs.FirstOrDefault(n => n.KH_MA == kh.KH_MA);

            if(kh_edit != null)
            {

                kh_edit.KH_HOTEN = kh.KH_HOTEN;
                kh_edit.KH_SDT = kh.KH_SDT;
                kh_edit.KH_DIACHI = kh.KH_DIACHI;
                kh_edit.KH_GIOITINH = kh.KH_GIOITINH;

                db.SaveChanges();
                TempData["success"] = "Lưu thay đổi thành công!";
                Session["TaiKhoan"] = kh_edit;
            }

            return View(kh_edit);
        }

        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            if (Session["TaiKhoan"] != null)
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                return View(kh);
            }
            
                return RedirectToAction("Index");
        }


       

        [HttpPost]
        public ActionResult DoiMatKhau(KHACHHANG kh, FormCollection f)
        {
            var ktmk = (f["matkhaumoi"].ToString() == (f["nhaplaimatkhau"].ToString()));

            string spass = Tool.MaHoaMatKhau(f["matkhaucu"]);


            KHACHHANG taiKhoan = db.KHACHHANGs.FirstOrDefault(n => n.KH_TAIKHOAN == kh.KH_TAIKHOAN && n.KH_MATKHAU == spass);

            if(taiKhoan != null && ktmk)
            {
                taiKhoan.KH_MATKHAU = Tool.MaHoaMatKhau(f["matkhaumoi"]);
                db.SaveChanges();
                TempData["success"] = "Đổi mật khẩu thành công";
                Session["TaiKhoan"] = taiKhoan;
                return RedirectToAction("Index");
            }
            else if(taiKhoan == null)
            {
                TempData["error"] = "Sai mật khẩu.";
                return View(kh);
            }
            else
            {
                TempData["error"] = "Mật khẩu gõ lại không chính xác.";
                return View(kh);
            }

        }

        public ActionResult LichSuMuaHang()
        {
            if (Session["TaiKhoan"] != null)
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                var lstDH = db.DONDATs.Where(n => n.KH_MA == kh.KH_MA).OrderByDescending(n=>n.DD_NGAYDAT);
                return View(lstDH);
            }
           
                return RedirectToAction("Index");
        }

        public ActionResult XemDonHang(int? id)
        {
            if (Session["TaiKhoan"] != null)
            {
                KHACHHANG kh = Session["TaiKhoan"] as KHACHHANG;
                DONDAT DH = db.DONDATs.FirstOrDefault(n => n.KH_MA == kh.KH_MA && n.DD_MA == id);

                if(DH != null)
                {
                    var ct = db.CHITIETDONDATs.Where(n => n.DD_MA == id).ToList();
                    ViewBag.CT = ct;
                    return View(DH);
                }
                    
            }
            
                return RedirectToAction("Index");
        }




    }
}