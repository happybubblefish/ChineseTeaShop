using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseTea3.Models
{
    public class CartIndexViewModel
    {
        public ShoppingRecordViewModel Cart { get; set; }

        public string ReturnUrl { get; set; }
    }
}