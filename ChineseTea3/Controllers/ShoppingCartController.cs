using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using ChineseTea3.Models;
using Microsoft.AspNet.Identity;

namespace ChineseTea3.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ChineseTeaShopEntities dbContext = new ChineseTeaShopEntities();

        
        public ActionResult Index(string id, string returnUrl)
        {
            ShoppingRecordViewModel cart = new ShoppingRecordViewModel();
            List<ShRecord> list = dbContext.ShRecords.Where(m => m.UserId == id).ToList();

            foreach (var record in list)
            {
                if (!record.IsCompleted)
                {
                    cart.recordCollection.Add(record);
                }
            }

            if (returnUrl == null)
            {
                returnUrl = "/TeaProducts/ViewProducts";
            }

            CartIndexViewModel ci = new CartIndexViewModel() {Cart = cart, ReturnUrl = returnUrl};

            return View(ci);
        }

        
        public ActionResult AddToCart(int productId, string returnUrl, decimal finalPrice, int quantity)
        {
            if (Request.IsAuthenticated){
                TeaProduct product = dbContext.TeaProducts.FirstOrDefault(m => m.ID == productId);
                ShoppingRecordViewModel cart = GetCart();

                if (product != null)
                {
                    cart.AddItem(dbContext, product, finalPrice, quantity, User.Identity.GetUserId());
                }

                string id = User.Identity.GetUserId();

                //can only pass int or string
                return RedirectToAction("Index", new {id, returnUrl});
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        //Actually you can remove all the items directly
        public ActionResult RemoveFromCart(int recordId, int productId, string returnUrl)
        {
            //Find the cart
            string id = User.Identity.GetUserId();

            ShoppingRecordViewModel cart = new ShoppingRecordViewModel();
            List<ShRecord> list = dbContext.ShRecords.Where(m => m.UserId == id).ToList();

            foreach (var record in list)
            {
                if (!record.IsCompleted)
                {
                    cart.recordCollection.Add(record);
                }
            }

            //make sure product is in the database, otherwise will make an error
            TeaProduct product = dbContext.TeaProducts.FirstOrDefault(m => m.ID == productId);
            List<ShRecord> deletedRecords = dbContext.ShRecords.Where(m => m.ID == recordId).ToList();
            ShRecord deletedRecord = new ShRecord();

            foreach (var record in deletedRecords)
            {
                if (!record.IsCompleted)
                {
                    deletedRecord = record;
                    break;
                }
            }

            if (product != null && deletedRecords != null)
            {
                cart.RemoveLine(product);
                dbContext.Entry(deletedRecord).State = System.Data.Entity.EntityState.Deleted;
                dbContext.SaveChanges();
            }

            return RedirectToAction("Index", new {id, returnUrl});
        }

        [Authorize(Roles = "admin")]
        public ActionResult Checkout(ShippingDetails shippingDetails)
        {
            ShoppingRecordViewModel cart = new ShoppingRecordViewModel();
            string userId = User.Identity.GetUserId();
            List<ShRecord> list = dbContext.ShRecords.Where(m => m.UserId == userId).ToList();

            foreach (var record in list)
            {
                if (!record.IsCompleted)
                {
                    cart.recordCollection.Add(record);
                }
            }

            if (cart.recordCollection.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                cart.Clear();

                Order order = new Order() {AddTime = DateTime.Now};
                foreach (var record in dbContext.ShRecords.Where(m => m.UserId == userId))
                {
                    record.IsCompleted = true;
                    record.OrderId = order.ID;
                }

                dbContext.Orders.Add(order);

                dbContext.SaveChanges();

                SendEmail("Your order has been placed. Estimated delivery date is...");
                return RedirectToAction("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public ActionResult Completed()
        {
            return View();
        }

        public static void SendEmail(string emailBody)
        {
            MailMessage mailMessage = new MailMessage("congls2013@gmail.com", "congls2013@gmail.com");
            mailMessage.Subject = "Order placed";
            mailMessage.Body = emailBody;

            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "congls2013@gmail.com",
                Password = "80520lin"
            };

            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);
        }

        private ShoppingRecordViewModel GetCart()
        {
            Guid userId = Guid.Parse(User.Identity.GetUserId());
            AspNetUser user = dbContext.AspNetUsers.FirstOrDefault(m => m.Id == userId.ToString());

            ShoppingRecordViewModel cart = new ShoppingRecordViewModel();
            cart.recordCollection = user.ShRecords.ToList();

            return cart;
        }
    }
}