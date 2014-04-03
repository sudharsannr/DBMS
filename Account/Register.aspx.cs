using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.UI;
using GourmetGuide;
using System.Net.Mail;
using System.Data.OleDb;
using System.Text;


public partial class Account_Register : Page
{
    String str;
    int month;
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        if (val1 == true)
            Response.Redirect("/Account/Profile.aspx");
        
        if (IsPostBack)
        {
            if (!(String.IsNullOrEmpty(Password.Text.Trim())))
            {
                Password.Attributes["value"] = Password.Text;
                ConfirmPassword.Attributes["value"] = ConfirmPassword.Text;
            }
        }
    }

    protected void GetRegCode(object sender, EventArgs e)
    {
        
        string Text=UserName.Text;
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = Text };
        IdentityResult result = manager.Create(user, Password.Text);
        if (result.Succeeded)
        {
            //IdentityHelper.SignIn(manager, user, isPersistent: false);
            var fromAddress = new MailAddress("gourmetguideteam@gmail.com", "Gourmet Guide Team");
            var toAddress = new MailAddress(EMailID.Text, UserName.Text);
            string fromPassword = ProjectSettings.gmailKey;
            const string subject = "Greetings from Gourmet Guide";
            Random rnd = new Random();
            month = rnd.Next(100000, 999999);
            if (!checkRegUser(UserName.Text, EMailID.Text))
                insert_into_regUser(month);
            else
            {
                ErrorMessage.Text = "User Name/EMail Already Exists";
                return;
            }
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
            Response.Redirect("/Account/ConfirmCode.aspx?user=" + UserName.Text);
        }
        else
        {
            ErrorMessage.Text = result.Errors.FirstOrDefault();
            //Form1.Visible = true;
        }
      }

    private bool checkRegUser(string UserName,string eMail)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        System.Diagnostics.Debug.WriteLine(ConnectionString.ToString());
        string cmd = "select * from srajagop.registereduser where username='"+UserName+"'";
        OleDbDataReader oReader = null;
        OleDbConnection conn = null;
        try
        {
        conn = new OleDbConnection(ConnectionString.ToString());
        OleDbCommand select_regUser = new OleDbCommand(cmd, conn);
        conn.Open();
        oReader = select_regUser.ExecuteReader();
        bool hasUserName = oReader.HasRows;
        //conn.Close();
        cmd = "select * from srajagop.registereduser where EMailID='" + eMail + "'";
        OleDbCommand select_EMail = new OleDbCommand(cmd, conn);
        //conn.Open();
        oReader = select_EMail.ExecuteReader();
        bool hasEMail = oReader.HasRows;
        bool retVal = hasUserName || hasEMail;
        return retVal;
        }
        finally
        {
        oReader.Close();
        conn.Close();
        }
    }

    private void insert_into_regUser(int month)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "insert into srajagop.registereduser values(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
        OleDbTransaction tran = null;
        OleDbConnection conn = null;
        try
        {
        conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        tran = conn.BeginTransaction();
        OleDbParameter param = new OleDbParameter();
        OleDbCommand insert_regUser = new OleDbCommand(cmd, conn, tran);            
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = UserName.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = EMailID.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = Password.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = FirstName.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = LastName.Text;
        string Gender;
        if (GenderDropDown.SelectedIndex == 0)
            Gender = "M";
        else if (GenderDropDown.SelectedIndex == 1)
            Gender = "F";
        else
            Gender = "N";
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = Gender;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = StreetAddress.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = DoorNumber.Text;
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = City.Text;
        insert_regUser.Parameters.Add("?", OleDbType.Integer).Value = ZipCode.Text;
        System.Diagnostics.Debug.WriteLine(StreetAddress.Text + "," + City.Text + "," + State.Text);
        string[] latLong=Program.getCoord(StreetAddress.Text+","+City.Text+","+State.Text);
        System.Diagnostics.Debug.WriteLine(latLong[0]);
        System.Diagnostics.Debug.WriteLine(latLong[1]);
        insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = State.Text;
        if (latLong != null)
        {
            insert_regUser.Parameters.Add("?", OleDbType.Double).Value = Double.Parse(latLong[0]);
            insert_regUser.Parameters.Add("?", OleDbType.Double).Value = Double.Parse(latLong[1]);
        }
        else
        {
            insert_regUser.Parameters.Add("?", OleDbType.Double).Value = null;
            insert_regUser.Parameters.Add("?", OleDbType.Double).Value = null;
        }
        insert_regUser.Parameters.Add("?", OleDbType.Integer).Value = month.ToString();
        insert_regUser.ExecuteNonQuery();
        tran.Commit();
        }
        finally
        {
        conn.Close();
        }
    }
}