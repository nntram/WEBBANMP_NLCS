using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLyNhaCungCapController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        
        public ActionResult Index()
        {
            var lstNCC = db.NHACUNGCAPs;
            return View(lstNCC);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TaoMoiNhaCungCap()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult TaoMoiNhaCungCap(NHACUNGCAP ncc)

        {
            try
            {
                db.NHACUNGCAPs.Add(ncc);
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
        public ActionResult SuaNhaCungCap(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(n => n.NCC_MA == id);

            if (ncc == null)
                return HttpNotFound();

            
            return View(ncc);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SuaNhaCungCap(NHACUNGCAP ncc)
        {
            try
            {
                db.Entry(ncc).State = System.Data.Entity.EntityState.Modified;
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
        public ActionResult XoaNhaCungCap(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            NHACUNGCAP ncc = db.NHACUNGCAPs.SingleOrDefault(n => n.NCC_MA == id);

            if (ncc == null)
                return HttpNotFound();


            var pn = db.PHIEUNHAPs.FirstOrDefault(p => p.NCC_MA == ncc.NCC_MA);

            if (pn != null)
            {
                TempData["error"] = "Không thể xóa do ràng buộc dữ liệu.";
                return RedirectToAction("Index");
            }

            return View(ncc);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult XacNhanXoaNhaCungCap(int? NCC_MA)
        {
            NHACUNGCAP ncc = db.NHACUNGCAPs.FirstOrDefault(n => n.NCC_MA == NCC_MA);
            if (ncc == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            try
            {
                db.NHACUNGCAPs.Remove(ncc);
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