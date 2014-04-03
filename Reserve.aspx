<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reserve.aspx.cs" Inherits="Account_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
<script src="http://code.jquery.com/jquery-1.9.1.js"></script>
<script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
<script>
    $(function () {
        $("#datepicker").datepicker();
    });
</script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Table ID="Table1" runat="server" Height="100px" Width="400px">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True">Restaurant Name :</asp:TableCell>
                <asp:TableCell ID="r_name" runat="server" Font-Bold="False"></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" Font-Bold="True">Address :</asp:TableCell>
                <asp:TableCell ID="r_address" runat="server"></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <br />
        Choose Seats&nbsp;&nbsp; :<br />
        <br />
        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" Text="2 Seater" AutoPostBack="True" />
&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
        </asp:DropDownList>
&nbsp;nos<br />
        <asp:CheckBox ID="CheckBox2" runat="server" Text="4 Seater" AutoPostBack="True" OnCheckedChanged="CheckBox2_CheckedChanged" />
&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList2" runat="server" AutoPostBack="True">
        </asp:DropDownList>
&nbsp;nos<br />
        <asp:CheckBox ID="CheckBox3" runat="server" Text="6 Seater" AutoPostBack="True" OnCheckedChanged="CheckBox3_CheckedChanged" />
&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList3" runat="server" AutoPostBack="True">
        </asp:DropDownList>
&nbsp;nos<br />
        <asp:CheckBox ID="CheckBox4" runat="server" Text="8 Seater" AutoPostBack="True" OnCheckedChanged="CheckBox4_CheckedChanged" />
&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList4" runat="server" AutoPostBack="True">
        </asp:DropDownList>
&nbsp;nos<br />
        <br />
        Choose Date and Time&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:TextBox ID="datepicker" runat="server" OnTextChanged="datepicker_TextChanged" AutoPostBack ="true"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:DropDownList ID="DropDownList5" runat="server">
        </asp:DropDownList>
&nbsp;&nbsp;&nbsp; Hrs<br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Reserve" />
        <br />
    </form>
</body>
</html>
