using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ModelLayer.Models
{
    public class Send
    {
        public string SendingMail(string emailTo,string token)
        {
            try
            {
                string emailfrom = "maddakaabhilash@gmail.com";
                MailMessage message = new MailMessage(emailfrom, emailTo);
                string mailbody = "Token Generated : " + token;
                message.Subject = "Generated Token will expire after 1 hour";
                message.Body = mailbody.ToString();
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                NetworkCredential credential = new NetworkCredential("maddakaabhilash@gmail.com", "rema wnuo agww adpq");


                smtpClient.EnableSsl=true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = credential;

                smtpClient.Send(message);

                return emailTo;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
