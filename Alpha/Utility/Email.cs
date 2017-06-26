using Alpha.Bo.Exceptions;
using Alpha.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Alpha.Utility
{
    public class Email
    {
        public static void Send(EmailModel item) {

            try
            {
                MailMessage mM = new MailMessage();
                mM.From = new MailAddress("criendslk@gmail.com");
                mM.To.Add(item.ToPrimary);
                foreach (var emails in item.ToSecondary)
                {
                    mM.To.Add(emails);
                }
                mM.Subject = item.Subject;
                mM.Body = item.Body;
                mM.IsBodyHtml = true;
                SmtpClient sC = new SmtpClient("smtp.gmail.com");
                sC.Port = 587;
                sC.Credentials = new NetworkCredential("criendslk@gmail.com", "janson@123");
                sC.EnableSsl = true;
                sC.Send(mM);
            }
            catch (Exception ex)
            {
                throw new EmailSendFailException();
            }
        }
    }
}