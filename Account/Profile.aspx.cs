using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.Identity;
using System.Web.UI.WebControls;
using GourmetGuide;

public partial class Account_Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(Request.QueryString["txtbox"]))
        {
            string txtBoxValue = Request.QueryString["txtbox"];
            Label1.Text = txtBoxValue;
        }
            //string userName, password;
            //userName = ((TextBox)PreviousPage.FindControl("TextBox1")).Text;
            //Label1.Text=Request.Form["UserName"].ToString();
            //if (userName != "")
            //    Label1.Text = userName;
            //else
            //    Label1.Text = "Empty String";
            //var manager = new UserManager();
            //var user = new ApplicationUser() { UserName = userName};
            //IdentityResult result = manager.Create(user, password);
            //if (result.Succeeded)
            //{
            //    IdentityHelper.SignIn(manager, user, isPersistent: false);
            //    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            //}
        //TextBox SourceTextBox=new TextBox();
        
        //if (Page.PreviousPage != null)
        //{
        //SourceTextBox = (TextBox)Page.PreviousPage.FindControl("hidden");
        //}
        //else
        //{
        //    Label1.Text = "Previous Page Null";
        //}
        //if (SourceTextBox != null)
        //{
        //    if (SourceTextBox.Text == "")
        //    {
        //        Label1.Text = "Hiiiii";
        //    }
        //    else
        //    {
        //        Label1.Text = SourceTextBox.Text;
        //    }
        //}
        //else
        //{
        //    Label1.Text = "UserName null";
        //}
   }
}