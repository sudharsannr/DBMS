using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Text;
using GourmetGuide;

public partial class Account_Restaurants : System.Web.UI.Page
{
    
    public string str;
    public string str1;
    public string rid;
    protected void Page_Load(object sender, EventArgs e)
    {
        rid = Request.QueryString["restaurant"];
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE RESTAURANTID = " +rid;
        string cmd1 = "SELECT DISTINCT SRAJAGOP.FOOD.NAME, SRAJAGOP.FDPRICECATALOG.PRICE FROM SRAJAGOP.FOOD INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID INNER JOIN SRAJAGOP.RESTAURANT ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID WHERE SRAJAGOP.RESTAURANT.RESTAURANTID ="+rid+" AND ROWNUM <=50";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        OleDbTransaction tran = null;
        conn.Open();
        System.Diagnostics.Debug.WriteLine("Helksako " + cmd1);
        OleDbParameter param = new OleDbParameter();
        OleDbCommand select_search = new OleDbCommand(cmd, conn);
        OleDbCommand select_food = new OleDbCommand(cmd1, conn);
        OleDbDataReader oReader = select_search.ExecuteReader();
        OleDbDataReader oReader1 = select_food.ExecuteReader();

       
        while (oReader.Read())
        {
            str += @"<tr><td>" + oReader[0].ToString() + @"</td><td> " + oReader[1].ToString() + @"</td> <td>" + oReader[2].ToString() + @"</td> <td>" + oReader[3].ToString() + @"</td> <td>" + oReader[4].ToString() + @"</td> <td>" + oReader[5].ToString() + @"</td> <td>" + oReader[6].ToString() + @"</td> <td>" + oReader[7].ToString() + @"</td> <td>" + oReader[8].ToString() + @"</tr>";
        }

        System.Diagnostics.Debug.WriteLine(str);
        str1 += @"<b>Menu Present</b><br><table><tr><th>Name</th> <th> Price </th></tr>";
        while (oReader1.Read())
        {
            str1 += @"<tr><td>" + oReader1[0].ToString() + @"</td><td> " + oReader1[1].ToString()+@"</td></tr>";
        }
        str1 += @"</table>";
        conn.Close();
    }
    
}