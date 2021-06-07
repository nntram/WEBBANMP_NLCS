using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;
using MyLibrary;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class QuanLyNhanVienController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        
        public ActionResult Index()
        {
            var lstNV = db.NHANVIENs;
            return View(lstNV);
        }

        [HttpGet]
        public ActionResult TaoMoiNhanVien()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TaoMoiNhanVien(NHANVIEN nv)

        {
            var kttk = db.NHANVIENs.FirstOrDefault(n => n.NV_TAIKHOAN == nv.NV_TAIKHOAN);//Kiểm tra tên tài khoản có trùng trong csdl không
            if (kttk == null)
            {
                nv.NV_MATKHAU = Tool.MaHoaMatKhau("THUDK44"); //Mật khẩu mặt định
                try
                {
                    db.NHANVIENs.Add(nv);
                    db.SaveChanges();

                    TempData["success"] = "Thêm mới thành công!";
                }
                catch (Exception)
                {
                    TempData["error"] = "Thêm thất bại!";
                }
            }
            else
            {
                TempData["error"] = "Thêm thất bại! Tài khoản đã tồn tại";
                return View();
            }

            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult SuaNhanVien(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.NV_MA == id);

            if (nv == null)
                return HttpNotFound();

            
            return View(nv);
        }

        [HttpPost]
        public ActionResult SuaNhanVien(NHANVIEN nv)
        {
            NHANVIEN nv_n = db.NHANVIENs.FirstOrDefault(n => n.NV_MA == nv.NV_MA);

            if (nv_n != null)
            {
                nv_n.NV_HOTEN = nv.NV_HOTEN;
                nv_n.NV_SDT = nv.NV_SDT;
                nv_n.NV_GIOITINH = nv.NV_GIOITINH;
                nv_n.NV_QUYENSD = nv.NV_QUYENSD;

                try
                {

                    db.SaveChanges();
                    TempData["success"] = "Lưu thay đổi thành công!";
                }
                catch (Exception)
                {
                    TempData["error"] = "Lưu thay đổi không thành công!";
                }
            }
            else
            {
                TempData["error"] = "Không tìm thấy nhân viên!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult XoaNhanVien(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHANVIEN nv = db.NHANVIENs.SingleOrDefault(n => n.NV_MA == id);

            if (nv == null)
                return HttpNotFound();


            var dd = db.DONDATs.FirstOrDefault(p => p.NV_MA == nv.NV_MA);
            var pn = db.PHIEUNHAPs.FirstOrDefault(p => p.NV_MA == nv.NV_MA);

            if (dd != null || pn != null)
            {
                TempData["error"] = "Không thể xóa do ràng buộc dữ liệu.";
                return RedirectToAction("Index");
            }

            return View(nv);

        }

        [HttpPost]
        public ActionResult XacNhanXoaNhanVien(int? NV_MA)
        {
            NHANVIEN nv = db.NHANVIENs.FirstOrDefault(n => n.NV_MA == NV_MA);
            if (nv == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            try
            {
                db.NHANVIENs.Remove(nv);
                db.SaveChanges();
                TempData["success"] = "Xóa thành công";
            }
            catch (Exception)
            {
                TempData["error"] = "Xóa không thành công!";
            }
            return RedirectToAction("Index");

        }
    }
}