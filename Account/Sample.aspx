<%@ Page Title="Register" Language="C#" AutoEventWireup="true" CodeFile="Sample.aspx.cs" Inherits="Account_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
    <h4>Create a new account.</h4>
    <hr />
    <form id="form1" runat="server" method="post">
    <%--User Name--%>
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" CssClass="col-md-2 control-label">User name</asp:Label>
            <div class="col-md-offset-2 col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control" AutoPostBack="false"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
                <%--^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.(?:[A-Z]{2}|com|org|net|edu|gov|mil|--%>
                <asp:RegularExpressionValidator id="RegularExpressionValidator3" 
                     ControlToValidate="UserName"
                     ValidationExpression="\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b"
                     Display="Static"
                     ErrorMessage="Invalid E-Mail Address"
                     CssClass="text-danger"
                     EnableClientScript="True" 
                     runat="server"/>



            </div>
        </div>
        <div class="form-group">
            <div class="col-md-offset-2 col-md-10">
                <asp:Button ID="Button1" runat="server" Text="Register" CssClass="btn btn-default" OnClick="Button1_Click"/>
            </div>
        </div>
    </form>
</body>
</html>



