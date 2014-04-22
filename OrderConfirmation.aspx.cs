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
    string str_orderType = "";
    string str_registered = "";
    StringBuilder booking = new StringBuilder();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["preorder"].ToString().Equals("true"))
        {
            addressdiv.Visible = false;
            rfvaddress1.Enabled = false;

        }
        else
        {
            addressdiv.Visible = true;
            rfvaddress1.Enabled = true;
        }
        if (Session["eMail"] != null)
        {
            str_eMail = Session["eMail"].ToString();
            System.Diagnostics.Debug.WriteLine("Email: " + str_eMail);
           // Session.Remove("eMail");
        }
        if (Session["type"] != null)
        {
            str_orderType = Session["type"].ToString();
            System.Diagnostics.Debug.WriteLine("Type: " + str_orderType);
            Session.Remove("type");
        }
        System.Diagnostics.Debug.WriteLine("eMail : " + str_eMail);
        if (Session["bookDetails"] != null)
        {
            str_bookDetails = Session["bookDetails"].ToString();
            string[] str_booking = str_bookDetails.Split('~');
            str_bookDetails = "";
            booking.Append("<b>"+str_booking[0].ToString()+"</b>");
            str_bookDetails += str_booking[0].ToString()+"\n";
            booking.Append("<table cellspacing = 10 cellpadding = 5>")
                    .Append("<tr><th>"+str_booking[1]+"</th>")
                    .Append("<th>"+str_booking[2]+"</th></tr>");
            str_bookDetails += str_booking[1].ToString() + " ..... " + str_booking[2].ToString() + "\n";
            for (int i = 3; i < str_booking.Length-3; i+=2 )
            {
                booking.Append("<tr><td>"+str_booking[i] + "</td>");
                booking.Append("<td>" + str_booking[i + 1] + "</td> </tr>");
                str_bookDetails += str_booking[i].ToString() + " ..... $" + str_booking[i + 1].ToString() + "\n"; 
            }
            booking.Append("<tr><th>" + str_booking[str_booking.Length-3] + "</th>");
            booking.Append("<th>" + str_booking[str_booking.Length - 2] + "</th> </tr>");
            str_bookDetails += str_booking[str_booking.Length - 3].ToString() + " ..... " + str_booking[str_booking.Length - 2].ToString() + "\n"; 
            booking.Append("</table>");
            bookDetails.InnerHtml = booking.ToString();

            System.Diagnostics.Debug.WriteLine("BookDetails: " + str_bookDetails);
            //Session.Remove("bookDetails");
        }
        if (Session["registered"] != null)
        {
            str_registered = Session["registered"].ToString();
            System.Diagnostics.Debug.WriteLine("Registered: " + str_registered);
            Session.Remove("registered");
        }
        System.Diagnostics.Debug.WriteLine("Registered" + str_registered);
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
        string subject = "";
        var content = "";
        str_bookDetails = Session["bookDetails"].ToString();
        string[] str_booking = str_bookDetails.Split('~');
        str_bookDetails = str_booking[0].ToString() + "\n";
        str_bookDetails += str_booking[1].ToString() + str_booking[2].ToString() + "\n";
        for (int i = 3; i < str_booking.Length - 3; i += 2)
        {
            str_bookDetails += str_booking[i].ToString() + "$" + str_booking[i + 1].ToString() + "\n";
        }
        str_bookDetails += str_booking[str_booking.Length - 3].ToString() + str_booking[str_booking.Length - 2].ToString() + "\n";
        subject = "GourmetGuide food order confirmation";
        content = "Hi. \n\nYou've ordered food from our site a few minutes ago. The following are the details:\n\n" + str_bookDetails + "\n\n" + "GourmetGuide team.";
        System.Diagnostics.Debug.WriteLine("Final email: " + Session["eMail"]);
        SendMail sm = new SendMail(Session["eMail"].ToString(), null, subject, content);
        Session.Remove("bookDetails");
        Session.Remove("eMail");
        sm.send();
        Response.Redirect("/Account/Profile.aspx");
    }
}