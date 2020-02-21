using MailKit.Security;
using MimeKit;
using MimeKit.Text;
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
            smtpClient.Port = 25;//指定端口
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
        public static async Task SendMailAvailableAsync(string fromMail, string pwd, string toMail, string subj, string bodys,string [] CCMail, string smtpserver = "smtp.163.com")
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpserver;//指定SMTP服务器
            smtpClient.Port = 25;//指定端口
            smtpClient.Credentials = new NetworkCredential(fromMail, pwd);//发件人用户名和密码
            MailAddress fromAddress = new MailAddress(fromMail);
            MailAddress toAddress = new MailAddress(toMail);
            MailMessage mailMessage = new MailMessage(fromAddress, toAddress);
            if(CCMail.Length>0)
            {
                foreach (var item in CCMail)
                {
                    mailMessage.CC.Add(item);
                }
            }
            mailMessage.Subject = subj;//主题
            mailMessage.Body = bodys;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.High;//优先级
            await smtpClient.SendMailAsync(mailMessage);
        }
        public static async Task  SendEmailByMailKit(string fromMail, string pwd, string toMail, string subj, string bodys, string smtpserver = "smtp.163.com")
        {
            var messageToSend = new MimeMessage
            {
                Sender = new MailboxAddress("王泽威", fromMail),
                Subject = subj,
            };
            //添加发件人信息和以前有所不同，MailKit居然支持多个发件人，所以From是一个集合类型，要通过Add方法来添加：
            //messageToSend.From.Add(new MailboxAddress("发件人姓名", "发件人邮箱账号名"));
            //邮件正文（Body属性）支持多种格式，最常用的是纯文本和HTML。需要用TextPart类来安排，TextPart的构造函数里可以指定正文格式，例如HTML：
            messageToSend.Body = new TextPart(TextFormat.Html) { Text = bodys };
            //或者纯文本
            //messageToSend.Body = new TextPart(TextFormat.Plain) { Text = bodys };
            //添加收件人信息：
            messageToSend.To.Add(new MailboxAddress(toMail));
            //添加抄送（CC）信息：
            messageToSend.Cc.Add(new MailboxAddress("1162321341@qq.com"));
            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                smtp.Connect(smtpserver, 465, true);
                smtp.Authenticate(fromMail, pwd);
                await smtp.SendAsync(messageToSend);
                //断开
                smtp.Disconnect(true);
            }
        }
    }
}
