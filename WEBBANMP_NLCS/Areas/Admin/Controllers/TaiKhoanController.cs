using MyLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    public class TaiKhoanController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Admin/TaiKhoan

        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(NHANVIEN nv)
        {
            if (ModelState.IsValid)
            {
                string sname = nv.NV_TAIKHOAN;
                string spass = Tool.MaHoaMatKhau(nv.NV_MATKHAU);

                //Nhân viên đăng nhập
                NHANVIEN nvdn = db.NHANVIENs.SingleOrDefault(n => n.NV_TAIKHOAN == sname && n.NV_MATKHAU == spass && n.NV_QUYENSD != "disabled");

                //nếu tìm thấy bản ghi hợp lệ
                if (nvdn != null)
                {
                    //Lấy quyền sử dụng
                    String quyen = nvdn.NV_QUYENSD;

                    //Gọi tới hàm phân quyền
                    PhanQuyen(nvdn.NV_TAIKHOAN, quyen);

                    //Lưu session
                    nvdn.NV_MATKHAU = ""; //Không lưu mật khẩu để bảo mật
                    Session["TaiKhoan"] = nvdn;

                    return RedirectToAction("Index", "ThongKe");

                }

                //nếu không tìm thấy tài khoản hợp lệ
                TempData["error"] = "Sai tên đăng nhập hoặc mật khẩu.";
                return View();
                
            }

            //nếu không thành công
            return View();
        }

        //Đăng xuất
        [Authorize]
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("DangNhap");
        }



        //Tạo trang ngăn chặn quyền truy cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }


        //Hàm phân quyền
        private void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          TaiKhoan, //username
                                          DateTime.Now, //Thời gian bắt đầu
                                          DateTime.Now.AddHours(3), //Thời gian kết thúc
                                          false, //Ghi nhớ tài khoản?
                                          Quyen, // "admin/user"
                                          FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }


        //Đổi mật khẩu
        [Authorize]
        [HttpGet]
        public ActionResult DoiMatKhau()
        {
            NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN;
            ViewBag.taikhoan = nv.NV_TAIKHOAN;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult DoiMatKhau(FormCollection f)
        {
            var ktmk = (f["matkhaumoi"].ToString() == (f["matkhaumoi2"].ToString()));

            string spass = Tool.MaHoaMatKhau(f["matkhaucu"]);
            string sname = f["NV_TAIKHOAN"].ToString();

            NHANVIEN taiKhoan = db.NHANVIENs.FirstOrDefault(n => n.NV_TAIKHOAN == sname && n.NV_MATKHAU == spass);

            if (taiKhoan != null && ktmk)
            {
                taiKhoan.NV_MATKHAU = Tool.MaHoaMatKhau(f["matkhaumoi"]);
                db.SaveChanges();
                TempData["success"] = "Đổi mật khẩu thành công, vui lòng đăng nhập lại.";
                DangXuat();
                return RedirectToAction("DangNhap");
            }
            else if (taiKhoan == null)
            {
                TempData["error"] = "Sai mật khẩu.";
                NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN;
                ViewBag.taikhoan = nv.NV_TAIKHOAN;
                return View();
            }
            else
            {
                TempData["error"] = "Mật khẩu nhập lại không chính xác.";
                NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN;
                ViewBag.taikhoan = nv.NV_TAIKHOAN;
                return View();
            }
        }

    }
}

         