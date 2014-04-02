using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        TextBox hidden = new TextBox();
        hidden.Text = UserName.Text;
        Response.Redirect("/Account/Profile.aspx?txtbox=" + UserName.Text.ToString(), false);
    }
}