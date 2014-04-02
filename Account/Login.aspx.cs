using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Web;
using System.Web.UI;
using GourmetGuide;
using System.Data.OleDb;
using System.Text;

public partial class Account_Login : Page
{
        protected void Page_Load(object sender, EventArgs e)
        {
            bool val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1 == true)
                Response.Redirect("/Account/Profile.aspx");
            else
            {
                RegisterHyperLink.NavigateUrl = "Register";
                OpenAuthLogin.ReturnUrl = Request.QueryString["ReturnUrl"];
                var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
                if (!String.IsNullOrEmpty(returnUrl))
                {
                    RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
                }
            }
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if (IsValid)
            {
                // Validate the user password
                if (!checkRegistrationComplete(UserName.Text))
                    Response.Redirect("/Account/ConfirmCode.aspx?user=" + UserName.Text);
                else
                {
                    var manager = new UserManager();
                    ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                    if (user != null)
                    {
                        IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                        //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        Response.Redirect("/Account/Profile.aspx", true);
                    }
                    else
                    {
                        FailureText.Text = "Invalid username or password.";
                        ErrorMessage.Visible = true;
                    }
                }
            }
        }

        private bool checkRegistrationComplete(string UserName)
        {
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
                            
            System.Diagnostics.Debug.WriteLine(ConnectionString);
            string cmd = "select REGISTRATIONCODE from srajagop.registereduser where username='" + UserName + "'";
            System.Diagnostics.Debug.WriteLine(cmd);
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            OleDbCommand select_regCode = new OleDbCommand(cmd, conn);
            conn.Open();
            OleDbDataReader oReader = select_regCode.ExecuteReader();
            bool retVal = false;
            while(oReader.Read())
            {
                int regCode = Int32.Parse(oReader["REGISTRATIONCODE"].ToString());
                if (regCode == 0)
                    retVal= true;
                else
                    retVal=false;
            }
            conn.Close();
            return retVal;
        }
}