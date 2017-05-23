using System.Text;
using System.Net.Mail;
using System.Net;
/**
 * 
 * author:Fatima Abdel Monem
 * purpose: send email to user to inform if the donation accepted
 * part of the code is from www.stackoverflow.com
 */
namespace WindowsFormsApp8
{
    class Email
    {

        string MailSmtpHost { get; set; }
        int MailSmtpPort { get; set; }
        string MailSmtpUsername { get; set; }
        string MailSmtpPassword { get; set; }
        string MailFrom { get; set; }

        public static bool SendEmail(string to)
        {
            NetworkCredential login;
            SmtpClient client;
            MailMessage msg;
            login = new NetworkCredential("blood20bank17@gmail.com", "bloodbank1720");
            client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = login;
            msg = new MailMessage
            {
                From = new MailAddress("blood20bank17@gmail.com",
                "Blood Bank",
                Encoding.UTF8)
            };
            msg.To.Add(new MailAddress(to));
            msg.Subject = "";
            msg.Body = "";
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.Normal;
            msg.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            string userstate = "Sending...";
            client.SendAsync(msg, userstate);
       
            return true;
        }
    }
}
