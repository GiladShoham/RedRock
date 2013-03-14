using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace RedRock
{
    class gmail
    {
    

public static void email_send(string senderUser, string filePath)
{
    MailMessage mail = new MailMessage();
    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
    mail.From = new MailAddress("redrock.sender@gmail.com");
    mail.To.Add("shoham.gilad@gmail.com");
    mail.Subject = "Mail from " + senderUser;
    mail.Body = "use the red rock to open file";

    System.Net.Mail.Attachment attachment;
    //attachment = new System.Net.Mail.Attachment("C:/temp/file.txt");
    attachment = new System.Net.Mail.Attachment(filePath);
    mail.Attachments.Add(attachment);

    SmtpServer.Port = 587;
    SmtpServer.Credentials = new System.Net.NetworkCredential("redrock.sender@gmail.com", "1234qwer1234");
    SmtpServer.EnableSsl = true;

    SmtpServer.Send(mail);

}
    }
}
