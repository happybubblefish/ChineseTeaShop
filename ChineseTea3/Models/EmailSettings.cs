using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChineseTea3.Models
{
    public class EmailSettings
    {
        public string MailToAddress = "congls2013@gmail.com";
        public string MailFromAddress = "congls2013@gmail.com";
        public bool UseSsl = true;
        public string Username = "congls2013@gmail.com";
        public string Password = "80520lin";
        public string SeverName = "smtp.gmail.com";
        public int ServerPort = 587;
    }
}