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

public partial class Account_OrderFood : System.Web.UI.Page
{
    DataTable tbl = new DataTable();    
    public string rid;
    public bool val1 = false;
    OleDbConnection conn = null;
    OleDbDataReader oReader;
    protected void Page_Load(object sender, EventArgs e)
    {
        val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        CheckBoxValidator.Visible = false;
        if (val1)
        {
            EMailID.Enabled = false;
            rfvEmail.Enabled = false;
            EMailID.Visible = false;
            EMailLabel.Visible = false;
        }
        else
        {
            EMailID.Visible = true;
            EMailLabel.Visible = true;
        }
        if (Request.QueryString["restaurant"] != null)
        {
            rid = Request.QueryString["restaurant"].ToUpper();
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd1 = "SELECT DISTINCT ProjectSettings.schema.FOOD.NAME, ProjectSettings.schema.FDPRICECATALOG.PRICE FROM ProjectSettings.schema.FOOD INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID INNER JOIN ProjectSettings.schema.RESTAURANT ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID WHERE ProjectSettings.schema.RESTAURANT.RESTAURANTID =" + rid;
            conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand select_search = new OleDbCommand(cmd1, conn);
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
            oAdapter.Fill(tbl);            
            if (tbl.Rows.Count == 0)
            {
                NoResult.Visible = true;
            }
            else
            {
                NoResult.Visible = false;
                StringBuilder tblData = new StringBuilder();
                tblData.Append("<table>")
                        .Append("<th>")
                        .Append("<td>NAME</td>")
                        .Append("<td>COST</td>")
                        .Append("<td>QUANTITY</td>")
                        .Append("<td>TOTAL PRICE</td>")
                        .Append("</th>");
                int rowCount = 0;
                foreach (DataRow row in tbl.Rows)
                {
                    tblData.Append("<tr>")
                            .Append("<td>")
                            .Append("<input type='checkbox' id='cb_" + (++rowCount) + "' onclick='toggleFields(" + rowCount + ");'/>")
                            .Append("</td>")
                            .Append("<td>")
                            .Append("<label id='fd_name_" + rowCount + "'>" + row["name"].ToString() + "</label>")
                            .Append("</td>")
                            .Append("<td>")
                            .Append("<label id='fd_cost_" + rowCount + "'>" + row["price"].ToString() + "</label>")
                            .Append("</td>")
                            .Append("<td>")
                            .Append("<input type='text' size='4' maxlength='4' id='fd_qty_" + rowCount + "' placeholder='qty' disabled ")
                            .Append("onkeyup='calculateTotal(" + rowCount + ");' ")
                            .Append("onkeypress='return validate(event);'/>")
                            .Append("</td>")
                            .Append("<td>")
                            .Append("<label id='fd_total_" + rowCount + "'/>")
                            .Append("</td>")                            
                            .Append("</tr>");
                }
                tblData.Append("</table>");
                //System.Diagnostics.Debug.WriteLine("TblData : \n" + tblData.ToString());
                FoodTbl.InnerHtml = tblData.ToString();
            }
            conn.Close();
        }
    }

    protected void OrderBtn_Click(object sender, EventArgs e)
    {
        string tmpOrderData = OrderData.Value;
        if (tmpOrderData.Equals("no data", StringComparison.OrdinalIgnoreCase))
        {
            CheckBoxValidator.Visible = true;
            return;
        }
        string eMail;
        if (val1)
        {
            string cmd = "select EMAILID from srajagop.RegisteredUser where userName='" + System.Web.HttpContext.Current.User.Identity.Name + "'";
            //System.Diagnostics.Debug.WriteLine("UserName is " + System.Web.HttpContext.Current.User.Identity.Name);
            OleDbCommand select_EMail = new OleDbCommand(cmd, conn);
            conn.Open();
            oReader = select_EMail.ExecuteReader();
            oReader.Read();
            eMail = oReader[0].ToString();
            //System.Diagnostics.Debug.WriteLine("EMail is " + eMail);
            oReader.Close();
            conn.Close();
        }
        else
        {
            eMail = EMailID.Text;
        }
        
        string bookDetails = "";        
        string[] orderData = tmpOrderData.Split('~');
        
        string insertParkingcmd = "insert into srajagop.foodreserve values(?,?,?,?)";
        OleDbTransaction tran = null;
        conn.Open();      

        bookDetails += "Food order summary:\n " + "Item\t\t\t\t\t\t\t\tPrice\n";
        if(orderData.Length > 0)
        {
            foreach(string dt in orderData)
            {
                System.Diagnostics.Debug.WriteLine(dt + "??" + orderData.Length);
                //TODO: Rewrite in other format. Bad std.
                if(dt.Length > 0)
                {
                    string[] t = dt.Split('|');
                    System.Diagnostics.Debug.WriteLine(t[0] + "--" + t[1]);
                    bookDetails += t[0] + "\t\t\t\t\t\t" + t[1] + "\n";
                    //TODO: Can this be reused?
                    tran = conn.BeginTransaction();
                    OleDbCommand insert_foodreserve = new OleDbCommand(insertParkingcmd, conn, tran);
                    insert_foodreserve.Parameters.Add("?", OleDbType.Integer).Value = rid;
                    insert_foodreserve.Parameters.Add("?", OleDbType.VarChar).Value = eMail;
                    insert_foodreserve.Parameters.Add("?", OleDbType.VarChar).Value = t[0];
                    insert_foodreserve.Parameters.Add("?", OleDbType.VarChar).Value = t[1];
                    insert_foodreserve.ExecuteNonQuery();
                    tran.Commit();
                }
            }
            bookDetails += "Total purchase amount: " + TotalPrice.Value + "\n";
        }
        conn.Close();

        string subject;
        var content = "";
        subject = "GourmetGuide food order confirmation";
        content = "Hi. \n\nYou've ordered food from our site a few minutes ago. The following are the details:\n\n" + bookDetails + "\n\n"
                  + "GourmetGuide team.";
        SendMail sm = new SendMail(eMail, null, subject, content);
        sm.send();
        Response.Redirect("/Account/Profile.aspx", true);
    }
}