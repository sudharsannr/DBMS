using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using GourmetGuide;
using System.Data.OleDb;
using System.Text;

public partial class Account_RegisterExternalLogin : System.Web.UI.Page
{
    protected string ProviderName
    {
        get { return (string)ViewState["ProviderName"] ?? String.Empty; }
        private set { ViewState["ProviderName"] = value; }
    }

    protected string ProviderAccountKey
    {
        get { return (string)ViewState["ProviderAccountKey"] ?? String.Empty; }
        private set { ViewState["ProviderAccountKey"] = value; }
    }

    protected void Page_Load()
    {
        // Process the result from an auth provider in the request
        ProviderName = IdentityHelper.GetProviderNameFromRequest(Request);
        if (String.IsNullOrEmpty(ProviderName))
        {
            Response.Redirect("~/Account/Login");
        }
        if (!IsPostBack)
        {
            var manager = new UserManager();
            var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();
            if (loginInfo == null)
            {
                Response.Redirect("~/Account/Login");
            }
            var user = manager.Find(loginInfo.Login);
            if (user != null)
            {
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
            }
            else if (User.Identity.IsAuthenticated)
            {
                // Apply Xsrf check when linking
                var verifiedloginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo(IdentityHelper.XsrfKey, User.Identity.GetUserId());
                if (verifiedloginInfo == null)
                {
                    Response.Redirect("~/Account/Login");
                }

                var result = manager.AddLogin(User.Identity.GetUserId(), verifiedloginInfo.Login);
                if (result.Succeeded)
                {
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                }
                else
                {
                    AddErrors(result);
                    return;
                }
            }
            else
            {
                userName.Text = loginInfo.DefaultUserName;
            }
        }
    }

    private bool checkExistingUser()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "select * from srajagop.registereduser where username=?";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        OleDbParameter param = new OleDbParameter();
        OleDbCommand select_regUser = new OleDbCommand(cmd, conn);
        conn.Open();
        select_regUser.Parameters.Add("?", OleDbType.VarChar).Value = userName.Text;
        OleDbDataReader oReader = select_regUser.ExecuteReader();
        bool hasRows = oReader.HasRows;
        System.Diagnostics.Debug.WriteLine(hasRows.ToString());
        conn.Close();
        return hasRows;
    }

    protected void LogIn_Click(object sender, EventArgs e)
    {
        CreateAndLoginUser();
    }

    private void CreateAndLoginUser()
    {
        if (!IsValid)
        {
            return;
        }
        var manager = new UserManager();
        var user = new ApplicationUser() { UserName = userName.Text };
        IdentityResult result = manager.Create(user);
        if (result.Succeeded)
        {
            if (!checkExistingUser())
            {
                StringBuilder ConnectionString = new StringBuilder();
                ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                        .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                        .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                        .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                        .Append("USER ID=").Append(ProjectSettings.dbUser);
                string cmd = "insert into srajagop.registereduser(username) values(?)";
                OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
                OleDbTransaction tran = null;
                conn.Open();
                tran = conn.BeginTransaction();
                OleDbParameter param = new OleDbParameter();
                OleDbCommand insert_regUser = new OleDbCommand(cmd, conn, tran);
                insert_regUser.Parameters.Add("?", OleDbType.VarChar).Value = userName.Text;
                insert_regUser.ExecuteNonQuery();
                tran.Commit();
                conn.Close();
            }
            var loginInfo = Context.GetOwinContext().Authentication.GetExternalLoginInfo();

            if (loginInfo == null)
            {
                Response.Redirect("~/Account/Login");
                return;
            }
            result = manager.AddLogin(user.Id, loginInfo.Login);
            if (result.Succeeded)
            {
                IdentityHelper.SignIn(manager, user, isPersistent: false);
                //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                Response.Redirect("~/Account/Profile",true);
                return;
            }
            AddErrors(result);
        }        
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError("", error);
        }
    }
}