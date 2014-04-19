using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using GourmetGuide;
using System.Text;
using System.Data.OleDb;

public partial class Account_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = System.Web.HttpContext.Current.User.Identity.Name + "'s Profile Page";
        recentlysearched();
        frequentlysearched();
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select searchTerm from(select distinct searchTerm from(select searchTerm from srajagop.usersearch where username='"+System.Web.HttpContext.Current.User.Identity.Name+"' order by searchTime desc)) where rownum<=5";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        RecentSearchRepeater.DataSource = oReader;
        RecentSearchRepeater.DataBind();
        //oReader.Read();
        //searchTerm = oReader[0].ToString();
        conn.Close();
    }

    private void frequentlysearched()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select searchterm from (select r1.searchterm,r1.searchcount from (select count(*) as searchcount,searchterm from srajagop.usersearch where username='" + System.Web.HttpContext.Current.User.Identity.Name + "' group by searchTerm) r1 order by r1.searchcount desc) where rownum<=5";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        FrequentSearchRepeater.DataSource = oReader;
        FrequentSearchRepeater.DataBind();
        //oReader.Read();
        //searchTerm = oReader[0].ToString();
        conn.Close();
    }

    private void recentlysearched()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select searchTerm from(select distinct searchTerm from(select searchTerm from srajagop.usersearch where username='" + System.Web.HttpContext.Current.User.Identity.Name + "' order by searchTime desc)) where rownum<=5";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        RecentSearchRepeater.DataSource = oReader;
        RecentSearchRepeater.DataBind();
        //oReader.Read();
        //searchTerm = oReader[0].ToString();
        conn.Close();
    }

    //private OleDbDataReader fillData()
    //{
        
    //    return oReader;
    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "insert into srajagop.userSearch values(?,?,CURRENT_TIMESTAMP)";
        OleDbTransaction tran = null;
        OleDbConnection conn = null;
        try
        {
            conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            tran = conn.BeginTransaction();
            OleDbParameter param = new OleDbParameter();
            OleDbCommand insert_search = new OleDbCommand(cmd, conn, tran);
            insert_search.Parameters.Add("?", OleDbType.VarChar).Value = System.Web.HttpContext.Current.User.Identity.Name;
            insert_search.Parameters.Add("?", OleDbType.VarChar).Value = Search.Text.ToUpper();
            insert_search.ExecuteNonQuery();
            tran.Commit();

        }
        finally
        {
            conn.Close();
        }
        Response.Redirect("~/Search.aspx?SearchString=" + Search.Text);
    }
    protected void SearchTerm_Click(object sender, EventArgs e)
    {
        //rfvSearch.Enabled = false;
        LinkButton l=(LinkButton)sender;
        string str=l.Text;
        //l.PostBackUrl = "~/Search.aspx?SearchString=" + str;
        Response.Redirect("~/Search.aspx?SearchString=" + str);
    }
    protected void FrequentSearchTerm_Click(object sender, EventArgs e)
    {

    }
}