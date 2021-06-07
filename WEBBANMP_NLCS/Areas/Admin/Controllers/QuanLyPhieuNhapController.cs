using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;

namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class QuanLyPhieuNhapController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: QuanLyPhieuNhap
        public ActionResult Index()
        {
            var lstPN = db.PHIEUNHAPs.OrderByDescending(n=>n.PN_NGAYNHAP);
            return View(lstPN);
        }

        [HttpGet]
        public ActionResult TaoMoiPhieuNhap()
        {
            ViewBag.NCC_MA = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.NCC_TEN), "NCC_MA", "NCC_TEN");
            ViewBag.SP_MA = new SelectList(db.SANPHAMs.OrderBy(n => n.SP_TEN), "SP_MA", "SP_TEN");
            return View();
        }


        [HttpPost]
        public ActionResult TaoMoiPhieuNhap(PHIEUNHAP pn, IEnumerable<CHITIETPHIEUNHAP> ctpn)
        {
            try
            {
                NHANVIEN nv = Session["TaiKhoan"] as NHANVIEN; //Lấy thông tin nhân viên đang đăng nhập
                pn.NV_MA = nv.NV_MA;// dang nhap
                db.PHIEUNHAPs.Add(pn);
                db.SaveChanges();

                SANPHAM sp;
                foreach(var item in ctpn)
                {
                    item.PN_MA = pn.PN_MA;
                    sp = db.SANPHAMs.First(s=>s.SP_MA == item.SP_MA);
                    sp.SP_SL += item.SOLUONGNHAP;
                }

                db.CHITIETPHIEUNHAPs.AddRange(ctpn);
                db.SaveChanges();

                TempData["success"] = "Thêm mới thành công";
                return RedirectToAction("Index");
            }
            catch(Exception)
            {
                TempData["error"] = "Thêm không thành công";

                ViewBag.NCC_MA = new SelectList(db.NHACUNGCAPs.OrderBy(n => n.NCC_TEN), "NCC_MA", "NCC_TEN");
                ViewBag.SP_MA = new SelectList(db.SANPHAMs.OrderBy(n => n.SP_TEN), "SP_MA", "SP_TEN");
                return View();
            }

        }

        public ActionResult XemPhieuNhap(int? id)
        {
            PHIEUNHAP pn = db.PHIEUNHAPs.FirstOrDefault(p => p.PN_MA == id);
            if (pn == null)
            {
                TempData["error"] = "Không tìm thấy phiếu nhập";
                return RedirectToAction("Index");
            }
            ViewBag.ChiTietPN = db.CHITIETPHIEUNHAPs.Where(n => n.PN_MA == id).ToList();
            return View(pn);
        }


      


    }
}