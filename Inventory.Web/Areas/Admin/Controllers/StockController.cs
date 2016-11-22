﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.BLL;
using Inventory.Model;

namespace Inventory.Web.Areas.Admin.Controllers
{
    public class StockController : Controller
    {
        ProductCategoryBs productCategoryBs = new ProductCategoryBs();
        ProductDetailBs productDetailBs = new ProductDetailBs();
        StockBs stockBs = new StockBs();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StockEntry()
        {
            ViewBag.ProudctCategory = new SelectList(productCategoryBs.ListAll(), "ProductCategoryID", "ProductCategoryName");
            ViewBag.ProudctName = new SelectList(productDetailBs.ListAll(), "ProductDetailID", "ProductName");
            return View(stockBs.ListAll());
        }
    }
}