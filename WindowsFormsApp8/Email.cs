using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Threading.Tasks;
using MySql.Data.MySqlClient.Memcached;
using System.Web;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mime;
using System.Net;
using System.ComponentModel;

namespace WindowsFormsApp8
{
    class Email
    {

        string MailSmtpHost { get; set; }
        int MailSmtpPort { get; set; }
        string MailSmtpUsername { get; set; }
        string MailSmtpPassword { get; set; }
        string MailFrom { get; set; }

        public static bool SendEmail(string to, string subject, string body)
        {
            
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg;
            login = new NetworkCredential("fatima.abdelmonem@gmail.com", "rosaamondpond");
            client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg = new MailMessage
            {
                From = new MailAddress("fatima.abdelmonem@gmail.com",
                "Blood Bank",
                Encoding.UTF8) };
            msg.To.Add(new MailAddress("fatima.abdelmonem@gmail.com"));
            msg.Subject = subject;
            msg.Body = body;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Sending...";
            client.SendAsync(msg, userstate);
            /*
        MailMessage mail = new MailMessage("fatima.abdelmonem@gmail.com", "fatima.abdelmonem@gmail.com");
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.EnableSsl = true;
        client.Host = "smtp.gmail.com";
        mail.Subject = subject;
        mail.Body = body;
        client.Send(mail);
        **/
            return true;
       

        }

    
    }
}
