using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLyNhaSanXuatController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        
        public ActionResult Index()
        {
            var lstNSX = db.NHASANXUATs;
            return View(lstNSX);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TaoMoiNhaSanXuat()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult TaoMoiNhaSanXuat(NHASANXUAT nsx)

        {
            try
            {
                db.NHASANXUATs.Add(nsx);
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
        public ActionResult SuaNhaSanXuat(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHASANXUAT nsx = db.NHASANXUATs.SingleOrDefault(n => n.NSX_MA == id);

            if (nsx == null)
                return HttpNotFound();

            
            return View(nsx);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SuaNhaSanXuat(NHASANXUAT nsx)
        {
            try
            {
                db.Entry(nsx).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult XoaNhaSanXuat(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHASANXUAT nsx = db.NHASANXUATs.SingleOrDefault(n => n.NSX_MA == id);

            if (nsx == null)
                return HttpNotFound();


            var sp = db.SANPHAMs.FirstOrDefault(p => p.NSX_MA == nsx.NSX_MA);

            if (sp != null)
            {
                TempData["error"] = "Không thể xóa do ràng buộc dữ liệu.";
                return RedirectToAction("Index");
            }

            return View(nsx);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult XacNhanXoaNhaSanXuat(int? NSX_MA)
        {
            NHASANXUAT nsx = db.NHASANXUATs.FirstOrDefault(n => n.NSX_MA == NSX_MA);
            if (nsx == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            try
            {
                db.NHASANXUATs.Remove(nsx);
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