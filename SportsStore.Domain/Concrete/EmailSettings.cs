using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Concrete
{
    public class EmailSettings
    {
        public string MailToAddres { get; set; } = "sona.arshakyan.00@mail.ru";
        public string MailFromAdress { get; set; } = "sportsstore@example.com";
        public bool UseSsl = true;
        public string UserName = "Sona";
        public string Password = "lalala";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\sports_store_emails";
    }
}