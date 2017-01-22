using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.Model;
using Inventory.BLL;

namespace Inventory.Web.Areas.Admin.Controllers
{
    public class ManageUserController : Controller
    {
        UserBs userBs = new UserBs();
        // GET: Admin/ManageUser
        public ActionResult ListUsers()
        {
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                Session["ConfirmLogin"] = "You must login first";
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            }

            return View(userBs.ListAll());
        }

        public ActionResult UpdateUser(string username)
        {
            try
            {
                ViewBag.UserId = Session["Username"].ToString();
            }
            catch
            {
                Session["ConfirmLogin"] = "You must login first";
                return RedirectToAction("Login", new { Area = "Security", Controller = "Access" });
            }

            return View(userBs.GetByUsername(username));
        }

    }
}