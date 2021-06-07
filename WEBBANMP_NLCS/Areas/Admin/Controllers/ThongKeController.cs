using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBBANMP_NLCS.Models;
namespace WEBBANMP_NLCS.Areas.Admin.Controllers
{
    [Authorize]
    public class ThongKeController : Controller
    {
        ModelDbContext db = new ModelDbContext();    
        public ActionResult Index()
        {
            ViewBag.PageView = HttpContext.Application["PageView"].ToString();
            ViewBag.Online = HttpContext.Application["Online"].ToString();
            ViewBag.CountDH = db.DONDATs.Count().ToString();
            ViewBag.CountKH = db.KHACHHANGs.Count().ToString();
            return View();
        }
    }
}