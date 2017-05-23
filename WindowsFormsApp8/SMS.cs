using System.Net;
/**
 * 
 * author:Fatima Abdel Monem
 * purpose: send sms to user to inform if there is ablood for the sent request
 * and to tell the meeting place 
 */
namespace WindowsFormsApp8
{
    class SMS
    {
        public static void SmsSend(string phone)
        {
            using (WebClient client = new WebClient())
            {
                string parameters = "api_key=6568e395&api_secret=2830a03ee956072f&to="+phone+"&from=BloodBank&text=There is a donation found that matches your request. " +
                    "We will contact you to provide info about the time to meet. The place will be Dom Zdravlja Novi Grad. Best Regards. Blood Bank";
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                string response = client.UploadString("https://rest.nexmo.com/sms/json", parameters);
                
            }
        }
    }
}
