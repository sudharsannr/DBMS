using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using GourmetGuide;
using System.Text;


public partial class Account_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        //SqlConnection conn = new SqlConnection(ConnectionString);
        string cmd = "insert into " + ProjectSettings.schema + ".test values('Value1','Value2')";
        //SqlCommand selectMovieID = new SqlCommand(cmd, conn);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        OleDbTransaction tran = null;
        conn.Open();
        tran = conn.BeginTransaction();
        OleDbCommand oledb = new OleDbCommand(cmd,conn,tran);
        int rowsAffected = oledb.ExecuteNonQuery();
        tran.Commit();
        Label1.Text = rowsAffected.ToString();
        conn.Close();
    }
}