using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using ChineseTea3.Models.Interfaces;

namespace ChineseTea3.Models
{
    public class EmailOrderProcessor : IOrderProcessor
    {
        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(ChineseTeaShopEntities dbContext, string userId, ShippingDetails shippingInfo)
        {
            ShoppingRecordViewModel cart = new ShoppingRecordViewModel();
            List<ShRecord> shList = dbContext.ShRecords.Where(m => m.UserId == userId).ToList();
            cart.recordCollection = shList;

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSsl;
                smtpClient.Host = emailSettings.SeverName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.Username, emailSettings.Password);

                StringBuilder body = new StringBuilder()
                    .AppendLine("A new order has been submitted")
                    .AppendLine("---")
                    .AppendLine("Items:");

                foreach (var line in cart.recordCollection)
                {
                    var subtotal = line.TeaProduct.Price*line.Quantity;
                    body.AppendFormat("{0} * {1} (subtotal: {2:c})",
                        line.Quantity,
                        line.TeaProduct.Name,
                        subtotal);
                }

                body.AppendFormat("Total order value: {0:c}",
                    cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Ship to:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("---")
                    .AppendFormat("Gift wrap: {0}", shippingInfo.GiftWrap ? "Yes" : "No");
                    
                MailMessage mailMessage = new MailMessage(
                    emailSettings.MailFromAddress,
                    emailSettings.MailToAddress,
                    "New order submitted!",
                    body.ToString());

                smtpClient.Send(mailMessage);
            }
        }
    }
}