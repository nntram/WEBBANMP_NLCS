using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;
using PagedList;

namespace WebsiteBanMP.Controllers
{
    public class SanPhamController : Controller
    {
        ModelDbContext db = new ModelDbContext();
        // GET: SanPham
        public ActionResult Index(int? page)
        {
           var lstSP = db.SANPHAMs.Where(n=>n.SP_SL>0);
            ViewBag.titlePartial = "Tất cả sản phẩm";

            //thực hiện phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1); //nếu hông có giá trị thì gán là 1

            return View(lstSP.OrderBy(n => n.SP_MA).ToPagedList(pageNumber, pageSize));
        }

        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            
            return PartialView();
        }

        public ActionResult XemChiTiet(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // đường dẫn không hợp lệ
            }

            SANPHAM sp = db.SANPHAMs.SingleOrDefault(n => n.SP_MA == id);

           
            if (sp == null)
            {
                return HttpNotFound();
            }

            ViewBag.DeXuat = db.SANPHAMs.Where(n => n.L_MA == sp.L_MA && n.SP_MA != sp.SP_MA).OrderByDescending(n => n.SP_MA);


            return View(sp);
        }

        
        public ActionResult LoaiSanPham(int? MaLoaiSP, int? page)
        {
            if (MaLoaiSP == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // đường dẫn không hợp lệ
            }

            var lstSP = db.SANPHAMs.Where(n => n.L_MA == MaLoaiSP);

            ViewBag.titlePartial = db.LOAISANPHAMs.SingleOrDefault(n => n.L_MA == MaLoaiSP).L_TEN;

            if(lstSP.Count()==0)
            {
                return HttpNotFound();
            }

            //thực hiện phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1); //nếu hông có giá trị thì gán là 1
            ViewBag.MaLoaiSP = MaLoaiSP;

            return View(lstSP.OrderBy(n => n.SP_MA).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult ThuongHieu(int? MaNSX, int? page)
        {
            if (MaNSX == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // đường dẫn không hợp lệ
            }

            var lstSP = db.SANPHAMs.Where(n => n.NSX_MA == MaNSX);

            ViewBag.titlePartial = db.NHASANXUATs.SingleOrDefault(n => n.NSX_MA == MaNSX).NSX_TEN;

            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }

            //thực hiện phân trang
            int pageSize = 9;
            int pageNumber = (page ?? 1); //nếu hông có giá trị thì gán là 1
            ViewBag.MaNSX = MaNSX;

            return View(lstSP.OrderBy(n => n.SP_MA).ToPagedList(pageNumber, pageSize));

        }


        [HttpPost]
        public ActionResult KQTimKiem(string sTuKhoa)
        {
         
            //tìm kiếm theo ten sản phẩm
            var lstSP = db.SANPHAMs.Where(n => n.SP_TEN.Contains(sTuKhoa));
            ViewBag.titlePartial = "Kết quả tìm kiếm cho \"" + sTuKhoa + "\"";
            return View(lstSP.OrderBy(n => n.SP_TEN));
        }
        


    }
}