using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

public partial class _Default : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
        Session["searchType"] = "simple";
        if(val1==true)
            Response.Redirect("/Account/Profile.aspx");
     }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx?SearchString=" + Search.Text);
    }
}