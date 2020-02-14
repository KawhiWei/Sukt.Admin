using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace Uwl.Common.SendEmail
{
    public class SendEmail
    {
        /// <summary>
        /// 可以使用的发送电子邮件方法
        /// </summary
        /// <param name="fromMail">发件人邮箱</param>
        /// <param name="pwd">发件人密码</param>
        /// <param name="toMail"></param>
        /// <param name="subj"></param>
        /// <param name="bodys"></param>
        /// <param name="smtpserver">发件人所使用的邮箱服务器</param>
        /// <returns></returns>
        public static async Task SendMailAvailableAsync(string fromMail, string pwd, string toMail, string subj, string bodys, string smtpserver = "smtp.163.com")
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpserver;//指定SMTP服务器
            smtpClient.Credentials = new NetworkCredential(fromMail, pwd);//发件人用户名和密码
            MailAddress fromAddress = new MailAddress(fromMail);
            MailAddress toAddress = new MailAddress(toMail);
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            mailMessage.Subject = subj;//主题
            mailMessage.Body = bodys;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.High;//优先级
            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
