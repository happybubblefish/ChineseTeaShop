using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseTea3.Models.Interfaces
{
    public interface IOrderProcessor
    {
        void ProcessOrder(ChineseTeaShopEntities dbContext, string userId, ShippingDetails shippingDetails);
    }
}