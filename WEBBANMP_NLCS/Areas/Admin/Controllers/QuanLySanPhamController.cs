using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLySanPhamController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        
        public ActionResult Index()
        {
            var lstSP = db.SANPHAMs;
            return View(lstSP);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TaoMoiSanPham()
        {

            ViewBag.NSX_MA = new SelectList(db.NHASANXUATs.OrderBy(n => n.NSX_TEN), "NSX_MA", "NSX_TEN");
            ViewBag.L_MA = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.L_TEN), "L_MA", "L_TEN");
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult TaoMoiSanPham(SANPHAM sp, HttpPostedFileBase SP_HINH)

        {
            if (SP_HINH != null)
            {
                if (SP_HINH.ContentLength > 0 && SP_HINH.ContentType == "image/jpeg")
                {
                    var fileName = Path.GetFileName(SP_HINH.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName); //Chuyển ảnh vào thư mục
                    //if (System.IO.File.Exists(path))
                    //{
                    //    ViewBag.upload = "Hình đã tồn tại";
                    //    return View();
                    //}
                    //else
                    {
                        SP_HINH.SaveAs(path);
                        sp.SP_HINH = fileName;
                    }
                }
            }

            try
            {
                db.SANPHAMs.Add(sp);
                db.SaveChanges();

                TempData["success"] = "Thêm mới thành công!";
            }
            catch (Exception)
            {
                TempData["error"] = "Thêm thất bại!";
            }

            return RedirectToAction("Index");

        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult SuaSanPham(int? id)
        {

            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == id);

            if (sp == null)
                return HttpNotFound();

            ViewBag.NSX_MA = new SelectList(db.NHASANXUATs.OrderBy(n => n.NSX_TEN), "NSX_MA", "NSX_TEN", sp.NSX_MA);
            ViewBag.L_MA = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.L_TEN), "L_MA", "L_TEN", sp.L_MA);
            return View(sp);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult SuaSanPham(SANPHAM sp, HttpPostedFileBase HINH_SP)
        {

            if (HINH_SP != null)
            {
                if (HINH_SP.ContentLength > 0 && HINH_SP.ContentType == "image/jpeg")

                {
                    var fileName = Path.GetFileName(HINH_SP.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);

                    HINH_SP.SaveAs(path);
                    sp.SP_HINH = fileName;
                }
            }

            try
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                TempData["success"] = "Lưu thay đổi thành công!";
            }
            catch (Exception)
            {
                TempData["error"] = "Lưu thay đổi không thành công!";
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult XoaSanPham(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == id);

            if (sp == null)
                return HttpNotFound();

            ViewBag.NSX_MA = new SelectList(db.NHASANXUATs.OrderBy(n => n.NSX_TEN), "NSX_MA", "NSX_TEN", sp.NSX_MA);
            ViewBag.L_MA = new SelectList(db.LOAISANPHAMs.OrderBy(n => n.L_TEN), "L_MA", "L_TEN", sp.L_MA);

            var ctpn = db.CHITIETPHIEUNHAPs.FirstOrDefault(p => p.SP_MA == sp.SP_MA);
            var ctdd = db.CHITIETDONDATs.FirstOrDefault(p => p.SP_MA == sp.SP_MA);

            if(ctpn != null || ctdd != null)
            {
                TempData["error"] = "Không thể xóa do ràng buộc dữ liệu.";
                return RedirectToAction("Index");
            }

            return View(sp);

        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult XacNhanXoaSanPham(int? SP_MA)
        {
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(n => n.SP_MA == SP_MA);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), sp.SP_HINH);
            //Xoá ảnh trong thư mục ~/Content/Images
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            try
            {
                db.SANPHAMs.Remove(sp);
                db.SaveChanges();

                TempData["success"] = "Xóa thành công";
            }
            catch (Exception)
            {
                TempData["error"] = "Xóa không thành công!";
            }
            return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            //tìm kiếm theo ten sản phẩm
            var lstSP = db.SANPHAMs.Where(n => n.SP_TEN.Contains(sTuKhoa));
            if (lstSP.Count() > 0)
                return PartialView(lstSP);
            else
                return Content("<h2 class=\"text-center\">Không tìm thấy kết quả phù hợp.</h2>");
        }

    }
}
