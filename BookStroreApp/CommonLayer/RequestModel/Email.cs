using CommonLayer.Model;
using CommonLayer.ResponseModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace RepositoryLayer.Service
{
    public class Email
    {
        string HtmlBody;
        SmtpClient smtp = new SmtpClient();
       

        private readonly IConfiguration config;
        public Email(IConfiguration config)
        {
            this.config = config;

            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("karlesona1998@gmail.com", "Sadanandk");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        }

        public void EmailService(MSMQModel link)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader("C:\\Users\\User\\source\\repos\\Bookstore\\BookStroreApp\\CommonLayer\\RequestModel\\ResetPassword.html", Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                HtmlBody = HtmlBody.Replace("JwtToken", link.JwtToken);
                MailMessage message = new MailMessage();
                message.From = new MailAddress("karlesona1998@gmail.com");
                message.To.Add(new MailAddress(link.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;


                smtp.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        internal void SendOrderEmail(CustomerOrder order)
        {
            try
            {
                using (StreamReader streamReader = new StreamReader("C:\\Users\\User\\source\\repos\\Bookstore\\BookStroreApp\\CommonLayer\\RequestModel\\Order.html", Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                HtmlBody = HtmlBody.Replace("{OrderID}", order.OrderID.ToString());
                HtmlBody = HtmlBody.Replace("{OrderDate}", order.OrderDate.ToString());
                HtmlBody = HtmlBody.Replace("{TotalCost}", order.TotalCost.ToString());
                MailMessage message = new MailMessage();

                message.From = new MailAddress("karlesona1998@gmail.com");
                message.To.Add(new MailAddress(order.Email));
                message.Subject = "order email";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Send(message);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
