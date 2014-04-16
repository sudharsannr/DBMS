using GourmetGuide;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_OrderConfirmation : System.Web.UI.Page
{
    string str_eMail = "";
    string str_bookDetails = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        str_eMail = Request.QueryString["email"];
        System.Diagnostics.Debug.WriteLine("eMail : " + str_eMail);
        str_bookDetails = Request.QueryString["orderDetails"];
        string str_registered = Request.QueryString["registered"];
        str_bookDetails = str_bookDetails.Replace("~", System.Environment.NewLine);
        System.Diagnostics.Debug.WriteLine("bookDetails : " + str_bookDetails);
        bookDetails.Text = str_bookDetails;
        if (string.Equals(str_registered, "true", StringComparison.OrdinalIgnoreCase)) 
        {
            fetchAddress();
        }
    }

    private void fetchAddress()
    {
        DataTable tbl = new DataTable();    
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd1 = "SELECT ADDRESSLINE1, ADDRESSLINE2 FROM ProjectSettings.schema.REGISTEREDUSER WHERE EMAILID = '" + str_eMail + "'";
        System.Diagnostics.Debug.WriteLine(cmd1);
        string address1 = "", address2 = "";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_search = new OleDbCommand(cmd1, conn);
        OleDbDataReader oReader = select_search.ExecuteReader();
        //TODO: If no rows display message.
        oReader.Read();
        address1 = oReader[0].ToString();
        address2 = oReader[1].ToString();
        if (address1 != null)
            Address1.Text = address1;
        if (address2 != null)
            Address2.Text = address2;
        conn.Close();
    }

    protected void ConfirmBtn_Click(object sender, EventArgs e)
    {
        string subject = "GourmetGuide food order confirmation";
        var content = "Hi. \n\nYou've ordered food from our site a few minutes ago. The following are the details:\n\n" + str_bookDetails + "\n\n"
                  + "GourmetGuide team.";
        SendMail sm = new SendMail(str_eMail, null, subject, content);
        sm.send();
        Response.Redirect("/Account/Profile.aspx");
    }
}