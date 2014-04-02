<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test.aspx.cs" Inherits="Account_Test" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Label ID="Label1" runat="server"></asp:Label>
    </div>
        <div>
            <asp:Button ID="Button1" Text="Submit" runat="server" OnClick="Button1_Click" />
        </div>
    </form>
</body>
</html>
