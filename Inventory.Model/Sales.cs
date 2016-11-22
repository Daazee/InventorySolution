﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Model
{
  public class Sales
    {
        public int SalesID { get; set; }

        public string TransactionNo { get; set; }

        [Display(Name = "Product Category")]
        public int ProductCategoryID { get; set; }

        [Display(Name = "Product Code")]
        public int ProductDetailID { get; set; }

        public int Quantity { get; set; }

        public double Rate { get; set; }

        //Quantity * Rate
        [Display(Name = "Total Amount")]
        public double TotalAmount { get; set; }

        [Display(Name = "Total Cost")]
        public double TotalProductCostAmount { get; set; }

        [Display(Name = "Amount Paid")]
        public double AmountPaid { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

    }
}