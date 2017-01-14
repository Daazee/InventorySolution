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
        Sales salesObjMain = new Sales();
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

        public JsonResult MakeSales(Sales SalesObj)
        {
            //try
            //{
            //    ViewBag.UserId = Session["Username"].ToString();
            //}
            //catch
            //{
            //    ViewBag.UserId = "";
            //}

            if (SalesObj.HeaderDetail == "H")
            {
                salesObjMain.TransactionNo = GenerateTransNo();
            }
            else
            {
                salesObjMain.TransactionNo = Session["TransactionNo"].ToString(); ;
            }

            salesObjMain.ProductCategoryID = SalesObj.ProductCategoryID;
            salesObjMain.ProductDetailID = SalesObj.ProductDetailID;
            salesObjMain.Rate = Convert.ToDouble(SalesObj.Rate);
            salesObjMain.Quantity = SalesObj.Quantity;
            salesObjMain.TotalAmount = Convert.ToDouble(SalesObj.TotalAmount);
            salesObjMain.AmountPaid = Convert.ToDouble(SalesObj.AmountPaid);
            salesObjMain.TotalProductCostAmount = Convert.ToDouble(SalesObj.TotalProductCostAmount);
            Session["TransactionNo"] = salesObjMain.TransactionNo;
            salesObjMain.HeaderDetail = SalesObj.HeaderDetail;
            salesObjMain.CreatedBy = "admin";
            salesObjMain.CreatedOn = DateTime.Today;
            salesObjMain.ModifiedBy = "admin";
            salesObjMain.ModifiedOn = DateTime.Today;
            salesBs.Insert(salesObjMain);
            return Json(new { transNo = salesObjMain.TransactionNo}, JsonRequestBehavior.AllowGet);
        }

        public string GenerateTransNo()
        {
            string TransNo = "TN";
            string currentDay = DateTime.Today.Day.ToString();
            string currentMonth = DateTime.Today.Month.ToString();
            string currentYear = DateTime.Today.Year.ToString();

            if (currentDay.Length < 2)
                currentDay = "0" + currentDay;
            if (currentMonth.Length < 2)
                currentMonth = "0" + currentMonth;
            TransNo = TransNo + "/" + currentMonth + currentDay + currentYear;
            int LastTransNo = salesBs.GetLastTransNo(TransNo);
            int NewTransNo = LastTransNo + 1;
            TransNo = TransNo + "/" + NewTransNo.ToString();
            return TransNo;
        }

    }
}