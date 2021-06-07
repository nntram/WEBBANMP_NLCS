using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;
namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLyDonDatController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: Admin/QuanLyDonDat

        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ChuaGiao()
        {
            var lstDH = db.DONDATs.Where(n => n.DD_TRANGTHAI == 0).OrderBy(n => n.DD_NGAYDAT);
            return View(lstDH);
        }

        public ActionResult DaGiao()
        {
            var lstDH = db.DONDATs.Where(n => n.DD_TRANGTHAI == 1).OrderBy(n => n.DD_NGAYDAT);
            return View(lstDH);
        }

        public ActionResult DaHoanThanh()
        {
            var lstDH = db.DONDATs.Where(n => n.DD_TRANGTHAI == 2).OrderByDescending(n => n.DD_NGAYDAT);
            return View(lstDH);
        }

        public ActionResult DaHuy()
        {
            var lstDH = db.DONDATs.Where(n => n.DD_TRANGTHAI == -1).OrderByDescending(n => n.DD_NGAYDAT);
            return View(lstDH);
        }

        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            var dh = db.DONDATs.FirstOrDefault(n => n.DD_MA == id);
            var ct = db.CHITIETDONDATs.Where(n => n.DD_MA == id).ToList();
            if (ct == null || dh == null)
            {
                TempData["error"] = "Không tìm thấy đơn hàng";
                return RedirectToAction("ChuaGiao");
            }

            ViewBag.CT = ct;
            return View(dh);
        }

        [HttpPost]
        public ActionResult DuyetDonHang(DONDAT dhupdate)
        {
            var dh = db.DONDATs.FirstOrDefault(n => n.DD_MA == dhupdate.DD_MA);
            var ct = db.CHITIETDONDATs.Where(n => n.DD_MA == dhupdate.DD_MA).ToList();
            if (ct == null || dh == null)
            {
                TempData["error"] = "Không tìm thấy đơn hàng";
                return RedirectToAction("ChuaGiao");
            }

            try
            {
                NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN; //Lấy mã nhân viên đang đăng nhập duyệt đơn hàng
                dh.NV_MA = nv.NV_MA;
                dh.DD_TRANGTHAI = dhupdate.DD_TRANGTHAI;
                db.SaveChanges();
                TempData["success"] = "Cập nhật thành công";
            }
            catch (Exception)
            {
                TempData["error"] = "Cập nhật không thành công";
            }

            ViewBag.CT = ct;
            return View(dh);
        }


        [HttpPost]
        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            
            int n = int.Parse(sTuKhoa); //đã ràng buộc điều kiện bên view
            //tìm kiếm theo ten sản phẩm
            var lstDD = db.DONDATs.Where(p => p.DD_MA == n);
            if (lstDD.Count() > 0)
                return PartialView(lstDD);
            else
                return Content("<h2 class=\"text-center\">Không tìm thấy kết quả phù hợp.</h2>");






        }



    }
}