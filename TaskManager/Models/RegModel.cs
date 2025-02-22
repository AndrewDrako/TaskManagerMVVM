﻿using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class RegModel
    {
        /// <summary>
        /// Method sends key to email async
        /// </summary>
        /// <param name="s"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task SendEmailAsync(string s, int key)
        {
            MailAddress from = new MailAddress("msbigheadtti@gmail.com", "Andrew");
            MailAddress to = new MailAddress(s);
            MailMessage m = new MailMessage(from, to);
            m.Subject = "Код для регистрации в приложении Task Manager";
            m.Body = $"Введите ключ который написан ниже\n{key}";
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("msbigheadtti@gmail.com", "it$myb1ngpot");
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(m);
        }
    }
}
