using GourmetGuide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    string emailID, userName;
    string subject, content;
	public SendMail(string emailID, string userName, string subject, string content)
	{
		//
		// TODO: Add constructor logic here
		//
        this.emailID = emailID;
        this.userName = userName;
        this.subject = subject;
        this.content = content;
	}

    public void send()
    {
        var fromAddress = new MailAddress("gourmetguideteam@gmail.com", "Gourmet Guide Team");
        var toAddress = new MailAddress(emailID, userName);
        string fromPassword = ProjectSettings.gmailKey;
        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            EnableSsl = true,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword)

        };
        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = content
        })
        {
            smtp.Send(message);
        }
    }
}