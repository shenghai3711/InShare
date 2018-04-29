using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace InShare.Common
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件(邮件服务器,发件人账号,授权码需要配置)
        /// </summary>
        /// <param name="mail">Email实体</param>
        /// <param name="email">接收者</param>
        /// <returns>是否成功</returns>
        public static bool SendMail(Email mail, string email)
        {
            SmtpClient client = new SmtpClient("smtp.qq.com")
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new System.Net.NetworkCredential("1051152168@qq.com", "siuvfudfjrvfbcca")//用户名，授权码
            };
            MailAddress from = new MailAddress("1051152168@qq.com", mail.DisplayName, Encoding.UTF8);//初始化发件人  
            MailAddress to = new MailAddress(email, "", Encoding.UTF8);//初始化收件人  
            //设置邮件内容  
            MailMessage message = new MailMessage(from, to)
            {
                Body = mail.Body,
                BodyEncoding = mail.BodyEncoding,
                Subject = mail.Subject,
                SubjectEncoding = mail.SubjectEncoding,
                IsBodyHtml = mail.IsBodyHtml
            };
            //发送邮件  
            try
            {
                client.Send(message);
                return true;
            }
            catch (InvalidOperationException iex)
            { }
            catch (Exception ex)
            { }
            return false;
        }
    }

    /// <summary>
    /// Eamil实体类
    /// </summary>
    public class Email
    {
        public Email()
        {
            BodyEncoding = Encoding.UTF8;
            SubjectEncoding = Encoding.UTF8;
            IsBodyHtml = false;
            Subject = "邮件主题";
            Body = "邮件内容";
        }
        /// <summary>
        /// 邮箱消息体
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 邮箱主题内容
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 消息体的编码
        /// </summary>
        public Encoding BodyEncoding { get; set; }

        /// <summary>
        /// 主题内容的编码
        /// </summary>
        public Encoding SubjectEncoding { get; set; }

        /// <summary>
        /// 指示邮件正文是否为html
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
    }
}
