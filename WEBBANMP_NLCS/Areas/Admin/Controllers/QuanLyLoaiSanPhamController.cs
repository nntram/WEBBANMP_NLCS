using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLyLoaiSanPhamController : Controller
    {
        ModelDbContext db = new ModelDbContext();

        public ActionResult Index()
        {
            var lstLSP = db.LOAISANPHAMs;
            return View(lstLSP);
        }

        [Authorize (Roles ="admin")]
        [HttpGet]
        public ActionResult TaoMoiLoaiSanPham()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult TaoMoiLoaiSanPham(LOAISANPHAM loai)

        {
            try
            {
                db.LOAISANPHAMs.Add(loai);
                db.SaveChanges();

                TempData["success"] = "Thêm mới thành công!";
            }
            catch(Exception)
            {
                TempData["error"] = "Thêm thất bại!";
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult SuaLoaiSanPham(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            LOAISANPHAM loai = db.LOAISANPHAMs.SingleOrDefault(n => n.L_MA == id);

            if (loai == null)
                return HttpNotFound();

            
            return View(loai);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SuaLoaiSanPham(LOAISANPHAM loai)
        {
            try
            {
                db.Entry(loai).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thay đổi thành công!";
            }
            catch(Exception)
            {
                TempData["error"] = "Lưu thay đổi không thành công!";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult XoaLoaiSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            LOAISANPHAM loai = db.LOAISANPHAMs.SingleOrDefault(n => n.L_MA == id);

            if (loai == null)
                return HttpNotFound();


            var sp = db.SANPHAMs.FirstOrDefault(p => p.L_MA == loai.L_MA);

            if (sp != null)
            {
                TempData["error"] = "Không thể xóa do ràng buộc dữ liệu.";
                return RedirectToAction("Index");
            }

            return View(loai);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult XacNhanXoaLoaiSanPham(int? L_MA)
        {
            LOAISANPHAM loai = db.LOAISANPHAMs.FirstOrDefault(n => n.L_MA == L_MA);
            if (loai == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            try
            {
                db.LOAISANPHAMs.Remove(loai);
                db.SaveChanges();

                TempData["success"] = "Xóa thành công";
            }
            catch(Exception)
            {
                TempData["error"] = "Xóa không thành công";
            }
           
            return RedirectToAction("Index");

        }
    }
}