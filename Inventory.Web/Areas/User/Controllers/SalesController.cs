using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.BLL;
using Inventory.Model;

namespace Inventory.Web.Areas.User.Controllers
{
    public class SalesController : Controller
    {
        ProductCategoryBs productCategoryBs = new ProductCategoryBs();
        ProductDetailBs productDetailBs = new ProductDetailBs();
        SalesBs salesBs = new SalesBs();
        // GET: User/Sales
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SalesEntry()
        {
            ViewBag.ProudctCategory = new SelectList(productCategoryBs.ListAll(), "ProductCategoryID", "ProductCategoryName");
            ViewBag.ProudctName = new SelectList(productDetailBs.ListAll(), "ProductDetailID", "ProductName");
            return View();
        }

        [HttpGet]
        public ActionResult _AddItem()
        {
            //try
            //{
            //    ViewBag.UserId = Session["Username"].ToString();
            //}
            //catch
            //{
            //    Session["ConfirmLogin"] = "You must login first";
            //    return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            //}
            ViewBag.ProudctCategory = new SelectList(productCategoryBs.ListAll(), "ProductCategoryID", "ProductCategoryName");
            ViewBag.ProudctName = new SelectList(productDetailBs.ListAll(), "ProductDetailID", "ProductName");

            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult _AddItem(FormCollection collection)
        {
            ViewBag.ProudctCategory = new SelectList(productCategoryBs.ListAll(), "ProductCategoryID", "ProductCategoryName");
            ViewBag.ProudctName = new SelectList(productDetailBs.ListAll(), "ProductDetailID", "ProductName");

            return PartialView();
        }

    }
}