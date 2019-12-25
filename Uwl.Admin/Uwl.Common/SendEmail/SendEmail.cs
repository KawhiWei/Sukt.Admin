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
        /// 发送电子邮件
        /// </summary>
        /// <param name="smtpserver">SMTP服务器</param>
        /// <param name="enableSsl">是否启用SSL加密</param>
        /// <param name="userName">登录帐号</param>
        /// <param name="pwd">登录密码</param>
        /// <param name="nickName">发件人昵称</param>
        /// <param name="fromMail">发件人</param>
        /// <param name="toMail">收件人</param>
        /// <param name="subj">主题</param>
        /// <param name="bodys">内容</param>
        /// <param name="attachments">附件集合，IO流和文件名</param>
        public static async Task SendMailAsync
            (string smtpserver, string userName, string pwd, string toMail, 
             string subj, string bodys, string fromMail = null, string nickName = null, bool enableSsl = false, Dictionary<Stream, string> attachments = null)
        {
            if (string.IsNullOrEmpty(fromMail))
            {
                //发件人如果为空则默认登录账户
                fromMail = userName;
            }
            if (string.IsNullOrEmpty(nickName))
            {
                //昵称如果为空则默认邮箱前缀
                nickName = fromMail.Split('@')[0];
            }
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();
            mailMessage.Subject = subj;//主题
            mailMessage.From = new MailAddress(fromMail, nickName);//发件人和昵称
            foreach (var item in toMail.Split(','))
            {
                mailMessage.To.Add(item);//收件人集合
            }
            mailMessage.Body = bodys;//内容
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码
            mailMessage.SubjectEncoding = Encoding.UTF8;//主题编码
            mailMessage.IsBodyHtml = true;//设置为HTML格式
            mailMessage.Priority = MailPriority.Normal;//优先级
            if (attachments != null)
            {
                //追加附件文件
                foreach (var item in attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(item.Key, item.Value));
                }
            }
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            smtpClient.Host = smtpserver;//指定SMTP服务器
            smtpClient.Credentials = new NetworkCredential(userName, pwd);//用户名和密码
            smtpClient.EnableSsl = enableSsl;
            await smtpClient.SendMailAsync(mailMessage);//发送邮件

        }
    }
}
