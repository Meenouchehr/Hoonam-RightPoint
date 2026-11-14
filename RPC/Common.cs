using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RPC
{
    public class Common
    {
        public delegate void ChangeEventHandler(object sender, Result result);
        public event ChangeEventHandler Changed;

        protected virtual void OnChange(Result result)
        {
            if (Changed != null)
                Changed(this, result);
        }
        
        public static User CurrentUser { set; get; }
        public static String Language { set; get; }


        public static void SendEmail(String from, List<String> toList, List<String> ccList, List<String> bccList, String Subject, String body, String attachmentFilename, String fromTitle, String fromPassword, bool isBodyHTML, String smtpSerevr, int smtpPort)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
                System.Net.Mail.SmtpClient SmtpServer = new System.Net.Mail.SmtpClient(smtpSerevr);
                mail.From = new System.Net.Mail.MailAddress(from, fromTitle, System.Text.Encoding.UTF8);
                if (toList != null)
                    if (toList.Count > 0)
                    {
                        foreach (var to in toList)
                            mail.To.Add(to);
                    }

                if (ccList != null)
                    if (ccList.Count > 0)
                    {
                        foreach (var cc in ccList)
                            mail.CC.Add(cc);
                    }
                if (bccList != null)
                    if (bccList.Count > 0)
                    {
                        foreach (var bcc in bccList)
                            mail.Bcc.Add(bcc);
                    }

                mail.Subject = Subject;
                mail.SubjectEncoding = System.Text.Encoding.UTF8;
                mail.Body = body;
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = isBodyHTML;

                if (!String.IsNullOrEmpty(attachmentFilename))
                {
                    System.Net.Mail.Attachment attachment = new System.Net.Mail.Attachment(attachmentFilename);
                    mail.Attachments.Add(attachment);
                }

                SmtpServer.Port = smtpPort;
                SmtpServer.Credentials = new System.Net.NetworkCredential(from, fromPassword);

                SmtpServer.Send(mail);
            }

            catch (Exception ex) { }

        }
    }


    

    [Flags]
    public enum Operation : byte
    {
        Insert = 1,
        Update = 2,
        Delete = 3
    }


    [Flags]
    public enum Upshot : byte
    {
        Success = 1,
        Fail = 2,
        NotFound = 3,
        RelationError=4
    }


    [Flags]
    public enum RedundantType : byte
    {
        Coefficient = 1,
        Percentage = 2

    }

}
