using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using GourmetGuide;
using Microsoft.AspNet.Identity;
using System.Text;

public partial class Account_ConfirmCodeaspx : System.Web.UI.Page
{
    string Text = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["url"] != null)
            ConfMsg.InnerText = "Please enter the confirmation code which was already mailed to you.";
            
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Text = Request.QueryString["user"];
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        string cmd = "select RegistrationCode from " + ProjectSettings.schema + ".RegisteredUser where UserName = '" + Text +"'";
        OleDbCommand select_regCode = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_regCode.ExecuteReader();
        int EnteredCode = Int32.Parse(ConfirmCode.Text);
        int RegCode=0;
        while(oReader.Read())
        {
            RegCode = Int32.Parse(oReader["RegistrationCode"].ToString());
        }
        conn.Close();
        if (EnteredCode != RegCode)
            ErrorMessage.Text= "Entered Codes Do not Match";
        else
        {
            var manager = new UserManager();
            //var user = new ApplicationUser() { UserName = Text };
            ApplicationUser user = manager.Find(Text, retrievePassword(Text));
            if (user!=null)
            {
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                updateRegCode(Text, 0);
                Response.Redirect("/Account/Profile.aspx");
            }
        }
    }

    private void updateRegCode(string Text,int month)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        string cmd = "update " + ProjectSettings.schema + ".RegisteredUser set RegistrationCode=? where UserName = '" + Text + "'";
        OleDbParameter param = new OleDbParameter();
        OleDbCommand update_regCode = new OleDbCommand(cmd, conn);
        update_regCode.Parameters.Add("?", OleDbType.Integer).Value = month;
        update_regCode.ExecuteNonQuery();
        conn.Close();
    }

    private string retrievePassword(string Text)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        string cmd = "select passwd from " + ProjectSettings.schema + ".RegisteredUser where UserName = '" + Text + "'";
        OleDbCommand select_passwd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_passwd.ExecuteReader();
        string str="";
        while (oReader.Read())
        {
            str=oReader["passwd"].ToString();
        }
        conn.Close();
        return str;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Text = Request.QueryString["user"];
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        string cmd = "select EMailID from " + ProjectSettings.schema + ".RegisteredUser where UserName = '" + Text + "'";
        OleDbCommand select_EMail = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_EMail.ExecuteReader();
        string EMail = "";
        while (oReader.Read())
        {
            EMail = oReader["EMailID"].ToString();
        }
        var fromAddress = new MailAddress("gourmetguideteam@gmail.com", "Gourmet Guide Team");
        var toAddress = new MailAddress(EMail, Text);
        const string fromPassword = "Gourmet123";
        const string subject = "Greetings from Gourmet Guide";
        Random rnd = new Random();
        int month = rnd.Next(100000, 999999);
        var body = "Hi. Thank you for registering with us.\n\n Please enter the code in the Registration Page to confirm account activation \n\n" + month;
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
            Body = body
        })
        {
            smtp.Send(message);
        }
        conn.Close();
        updateRegCode(Text, month);
    }
}