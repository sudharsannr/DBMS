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
    public string city;
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Title = System.Web.HttpContext.Current.User.Identity.Name + "'s Profile Page";
        recentlysearched();
        frequentlysearched();
        samelocfrequentlysearched();
        foodliked();
        restaurantLiked();
        city = getUserCity();
    }

    private void restaurantLiked()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select restaurantid,name from srajagop.restaurant where restaurantid = (select r1.restaurantid from (select restaurantid from (select count(*) as cnt,restaurantid from srajagop.foodOrder where emailID=(select emailID from srajagop.registereduser where username='" + System.Web.HttpContext.Current.User.Identity.Name + "') group by restaurantid order by cnt desc ) where rowNum<=5 ) r1 )";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        RestaurantLikedRepeater.DataSource = oReader;
        RestaurantLikedRepeater.DataBind();
        conn.Close();
    }

    private void foodliked()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select foodName from (select count(*) as cnt,foodname from srajagop.foodreserve where emailID=(select emailID from srajagop.registereduser where username='" + System.Web.HttpContext.Current.User.Identity.Name + "') group by foodname order by cnt desc) where rownum<=5";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        FoodLikedRepeater.DataSource = oReader;
        FoodLikedRepeater.DataBind();

        //oReader.Read();
        //searchTerm = oReader[0].ToString();
        conn.Close();
    }

    private string getUserCity()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select city from srajagop.registeredUser where username='" + System.Web.HttpContext.Current.User.Identity.Name + "'";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        oReader.Read();
        string city = oReader[0].ToString();
        oReader.Close();
        conn.Close();
        if (city == "")
            city = "your location";
        return city;
    }

    private void samelocfrequentlysearched()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select searchTerm from (select count(*) as cnt,searchTerm from srajagop.UserSearch where username in (select distinct srajagop.registeredUser.userName from srajagop.registeredUser where srajagop.registeredUser.city=(select distinct sRajagop.registeredUser.city from sRajagop.registeredUser where sRajagop.registeredUser.userName='" + System.Web.HttpContext.Current.User.Identity.Name + "') and srajagop.registeredUser.username<>'" + System.Web.HttpContext.Current.User.Identity.Name + "') group by searchTerm order by cnt desc) where rownum<=5";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_cmd = new OleDbCommand(cmd, conn);
        OleDbDataReader oReader = select_cmd.ExecuteReader();
        SameLocFrequentSearchRepeater.DataSource = oReader;
        SameLocFrequentSearchRepeater.DataBind();
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
        redirect(sender);
    }
    protected void FrequentSearchTerm_Click(object sender, EventArgs e)
    {
        redirect(sender);        
    }
    protected void SameLocFrequentSearchTerm_Click(object sender, EventArgs e)
    {
        redirect(sender);
    }
    protected void FoodLikedTerm_Click(object sender, EventArgs e)
    {
        redirect(sender);
    }
    private void redirect(object sender)
    {
        LinkButton l = (LinkButton)sender;
        string str = l.Text;
        Response.Redirect("~/Search.aspx?SearchString=" + str);
    }    
    protected void RestaurantLikedRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        HyperLink hypl = (HyperLink)e.Item.FindControl("RestaurantLikedTerm");
        Label lbl = (Label)e.Item.FindControl("Label1");
        hypl.NavigateUrl = "~/Restaurants.aspx?restaurant=" + lbl.Text;
    }
}