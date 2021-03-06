﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inventory.BLL;
using Microsoft.Reporting.WebForms;
using System.Data;
using Inventory.Web.ReportDataset;
namespace Inventory.Web.Areas.User.Controllers
{
    public class ReportController : Controller
    {
        ReportBs NewReportBs = new ReportBs();
        HashHelper helper = new HashHelper();
        CompanyDetailBs NewCompanyDetailBs = new CompanyDetailBs();
        // GET: User/Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        // public ActionResult SalesReport)
        public ActionResult SalesReport(string txtStartDate = "01/01/1900", string txtEndDate = "01/01/1900", string PageNo = null)
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
            DateTime StartDate = DateTime.Today;
            DateTime EndDate = DateTime.Today;
            int pageNo = int.Parse(PageNo == null ? "1" : PageNo);
            ViewBag.CurrentPage = pageNo;
            ViewBag.StartDate = txtStartDate;
            ViewBag.EndDate = txtEndDate;
            if (Convert.ToDateTime(txtStartDate) > Convert.ToDateTime(txtEndDate))
            {
                ViewBag.Message = "StartDate must not be greater than end date";
                return View();
            }
            Session["StartDate"] = StartDate = Convert.ToDateTime(txtStartDate);
            Session["EndDate"] = EndDate = Convert.ToDateTime(txtEndDate);
            ViewBag.TotalPages = NewReportBs.SalesReportPages(StartDate, EndDate);
            return View(NewReportBs.SalesReport(StartDate, EndDate).Skip((pageNo - 1) * 10).Take(10));
        }

        [HttpGet]
        public ActionResult SalesReportPaging(string str_date, string end_date, string PageNo)
        {
            return RedirectToAction("SalesReport", new { txtStartDate = str_date, txtEndDate = end_date, PageNo = PageNo });
        }


        public FileResult ExportSalesReport(string ReportType = "Pdf")
        {
            LocalReport localReport = new LocalReport();
           localReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
            //localReport.ReportPath = Server.MapPath("~/Reports/SalesReport.rdlc");
            localReport.ReportPath = Server.MapPath("~/Reports/SalesReport.rdlc");
            ReportDataSource reportDtSource = new ReportDataSource();
            DataRow dr;
            InventoryDs ds = new InventoryDs();

            foreach (var sale in NewReportBs.SalesReport(Convert.ToDateTime(Session["StartDate"]), Convert.ToDateTime(Session["EndDate"])))
            {
                dr = ds.Tables["SalesReportDT"].Rows.Add();
                dr["SalesAmount"] = sale.SalesAmount;
                dr["PaymentDate"] = sale.KeyDate;
                dr["PaymentNo"] = sale.PaymentNo;
                dr["StartDate"] = Session["StartDate"];
                dr["EndDate"] = Session["EndDate"];
            }
            reportDtSource.Name = "SalesReportDataSet";
            reportDtSource.Value = ds.Tables["SalesReportDT"];
            localReport.DataSources.Add(reportDtSource);
            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : "pdf";
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            renderedBytes = localReport.Render(reportType, "", out mimeType, out encoding, out fileNameExtension,
                                            out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=SalesReport " + DateTime.Now + "." + fileNameExtension);
            return File(renderedBytes, fileNameExtension);
        }

        void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            LocalReport localReport = new LocalReport();

            DataRow dr;
            InventoryDs ds = new InventoryDs();
            var companyDetail = NewCompanyDetailBs.GetCompanyDetail();
            dr = ds.Tables["CompanyDetiailDT"].Rows.Add();
            dr["CompanyCode"] = companyDetail.CompanyCode;
            dr["CompanyName"] = companyDetail.CompanyName;
            dr["CompanyAddress"] = companyDetail.CompanyAddress;
            dr["CompanyPhone1"] = companyDetail.CompanyPhone1;
            dr["CompanyPhone2"] = companyDetail.CompanyPhone2;
            dr["CompanyEmail"] = companyDetail.CompanyEmail;

            //reportDtSource.Name = "CompDetDataSet";
            //reportDtSource.Value = ds.Tables["CompanyDetiailDT"];
            e.DataSources.Add(new ReportDataSource()
            {
                Name = "CompDetDataSet",
                Value = ds.Tables["CompanyDetiailDT"]
        });
        }
    }
}