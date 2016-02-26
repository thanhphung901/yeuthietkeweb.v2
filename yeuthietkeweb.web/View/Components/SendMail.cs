using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Configuration;
using System.Configuration;
using vpro.functions;
namespace GiaNguyen.Components
{
    public class SendMail
    {

        public void SendMail_RecoverPassword(string to, string pass, string name)
        {
            string url = ConfigurationManager.AppSettings["URLWebsite"];
            string nameweb = ConfigurationManager.AppSettings["EmailDisplayName"];
            string strBody = "<html>"
                      + "<P>Xin chào:</P>" + name
                      + "<P>Bạn đang yêu cầu khôi phục mật khẩu của bạn tại <A href=\"" + url + "\"></A>.</P>"
                      + "<P> Mật khẩu : " + pass
                      + "<P>Chúng tôi xin cảm ơn những tình cảm tốt đẹp bạn đã dành cho chúng tôi và chúng tôi cũng luôn cầu chúc những điều tốt đẹp nhất sẽ đến với bạn và Gia đình.</P>"
                      + "<P>P/s : Đừng quên thỉnh thoảng ghé qua <A href=\"" + url + "\">" + nameweb + "</A> để cập nhật tin tức về chúng tôi nhé !</P>"
                      + "</html>";

            SendEmailSMTP("Khôi phục mật khẩu tại " + nameweb + " !", to.ToString().Trim(), "", "", strBody, true, false);
        }
        public void SendMail_ChangePassword(string Email, int OID, string Code_Active)
        {

            string link = System.Configuration.ConfigurationManager.AppSettings["URLWebsite"] + "/Resources/doi-mat-khau.aspx?code=" + Code_Active + "&id=" + OID;
            string strBody = "<html>"
                      + "<P>Xin chào,</P>"
                      + "<P> Để thay đổi mật khẩu vui lòng nhấp vào link sau : <a href=" + link + "> " + link + "</a>"
                      + "<P>Chúng tôi xin cảm ơn những tình cảm tốt đẹp bạn đã dành cho chúng tôi và chúng tôi cũng luôn cầu chúc những điều tốt đẹp nhất sẽ đến với bạn và Gia đình.</P>"
                      + "<P>P/s : Đừng quên thỉnh thoảng ghé qua <A href=\"http://dichvuviettel.com.vn\">dichvuviettel.com.vn</A> để cập nhật tin tức về chúng tôi nhé !</P>"
                      + "</html>";

            SendEmailSMTP("Khôi phục mật khẩu tại dichvuviettel.com.vn !", Email.ToString().Trim(), "", "", strBody, true, false);
        }

        public void Send_Link_ChangePassword(string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            string strBody = "<html>" + body + "</html>";
            SendEmailSMTP("Đổi mật khẩu tại dichvuviettel.com.vn !", toAddress.ToString().Trim(), "", "", strBody, true, false);
        }

        public void Send_Mail_Order(string toAddress, string ccAddress, string bccAddress, string body, string strSubject)
        {
            string strBody = "<html>" + body + "</html>";
            SendEmailSMTP(strSubject, toAddress, ccAddress, bccAddress, strBody, true, false);
        }

        public void SendMail_Active_Account(string toAddress, string ccAddress, string bccAddress, string body)
        {
            string strBody = "<html>" + body + "</html>";
            SendEmailSMTP("Xác nhận đăng ký tài khoản thành công !", toAddress, ccAddress, bccAddress, strBody, true, false);
        }

        public void SendEmail(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(FormAddress, System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]);
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = "<html>" + body + "</html>";
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }

        public void SendEmailSMTP(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("no-reply@" + Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = body;
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    //client.EnableSsl = isSSL;
                    //client.Host = Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    //client.Port = Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    //client.Credentials = new System.Net.NetworkCredential(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public void SendEmailSMTP_DCV(string strSubject, string toAddress, string ccAddress, string bccAddress, string body, bool isHtml, bool isSSL)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailDisplayName"]));
                    mail.To.Add(toAddress);
                    if (ccAddress != "")
                    {
                        mail.CC.Add(ccAddress);
                    }
                    if (bccAddress != "")
                    {
                        mail.Bcc.Add(bccAddress);
                    }
                    mail.Subject = strSubject;

                    string str = body;
                    mail.Body = str;
                    mail.IsBodyHtml = isHtml;
                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = isSSL;
                    client.Host = Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailHost"]);
                    client.Port = Utils.CIntDef(System.Configuration.ConfigurationManager.AppSettings["EmailPort"]);
                    client.Credentials = new System.Net.NetworkCredential(Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["Email"]), Utils.CStrDef(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"]));

                    client.Send(mail);
                }
            }
            catch (SmtpException ex)
            {
                clsVproErrorHandler.HandlerError(ex);
            }
        }
        public static string FormAddress
        {
            get
            {
                SmtpSection cfg = ConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection;
                return cfg.Network.UserName;
            }
        }


        internal void SendMail_Active_Account(System.Web.UI.HtmlControls.HtmlInputText txtEmail, string p, string p_2, string p_3)
        {
            throw new NotImplementedException();
        }
    }
}