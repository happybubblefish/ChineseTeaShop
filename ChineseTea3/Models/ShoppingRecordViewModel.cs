using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseTea3.Models
{
    //This view model will perform as a shopping cart
    public class ShoppingRecordViewModel
    {
        public List<ShRecord> recordCollection = new List<ShRecord>();

        public void AddItem(ChineseTeaShopEntities dbContext, TeaProduct product, decimal finalPrice, int quantity, string userId)
        {
            //Search whether cart has desired product
            ShRecord record = recordCollection.FirstOrDefault(m => m.TeaProduct == product);

            if (record == null || record.IsCompleted == true)
            {
                ShRecord updateRecord = new ShRecord { ProductId = product.ID, TeaProduct = product, FinalPrice = finalPrice, Quantity = quantity, AddTime = DateTime.Now, UserId = userId };

                //update database
                dbContext.ShRecords.Add(updateRecord);
                dbContext.SaveChanges();

                //update shopping cart
                recordCollection.Add(updateRecord);
            }
            else if (record.IsCompleted == false)
            {
                record.Quantity += quantity;
                record.FinalPrice += finalPrice;
                dbContext.Entry(record).State = System.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void RemoveLine(TeaProduct product)
        {
            recordCollection.RemoveAll(m => m.TeaProduct.ID == product.ID);
        }

        public decimal? ComputeTotalValue()
        {
            return recordCollection.Sum(m => m.FinalPrice);
        }

        public IEnumerable<ShRecord> Records
        {
            get { return recordCollection;}
        }

        public void Clear()
        {
            recordCollection.Clear();
        }
    }
}