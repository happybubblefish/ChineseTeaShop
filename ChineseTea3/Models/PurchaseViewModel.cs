using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseTea3.Models
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            this.WeightList = new List<string>() {"25", "50", "100", "250", "500" };  //"choose an option", 
            this.AmountList = new List<string>(){"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"};
        }

        public List<string> WeightList { get; set; }

        public List<string> AmountList { get; set; }

        public string Weight { get; set; }

        public string Amount { get; set; }

        public decimal FinalPrice { get; set; }
    }
}