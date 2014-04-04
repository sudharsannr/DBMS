<%@ Page Title="Reservation Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Reserve.aspx.cs" Inherits="Account_Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
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
    <asp:Label runat="server" AssociatedControlID="CheckBox1" ID="Label1" CssClass="col-md-2 control-label">Choose Seats:</asp:Label>
    <div class="col-md-10">

        <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true" Text="2 Seater" />
        &nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem>-select-</asp:ListItem>
        </asp:DropDownList>
        &nbsp;nos<br />
        <asp:RequiredFieldValidator ID="ddValidator1" runat="server" ControlToValidate="DropDownList1"
            CssClass="text-danger" ErrorMessage="Please select a value." InitialValue="-select-"/>
        <br />
        <asp:CheckBox ID="CheckBox2" runat="server" Text="4 Seater" AutoPostBack="true" OnCheckedChanged="CheckBox2_CheckedChanged" />
        &nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList2" runat="server">
            <asp:ListItem>-select-</asp:ListItem>
        </asp:DropDownList>
        &nbsp;nos<br />
        <asp:RequiredFieldValidator ID="ddValidator2" runat="server" ControlToValidate="DropDownList2"
            CssClass="text-danger" ErrorMessage="Please select a value." InitialValue="-select-"/>
        <br />
        <asp:CheckBox ID="CheckBox3" runat="server" Text="6 Seater"  AutoPostBack="true" OnCheckedChanged="CheckBox3_CheckedChanged" />
        &nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList3" runat="server">
            <asp:ListItem>-select-</asp:ListItem>
        </asp:DropDownList>
        &nbsp;nos<br />
        <asp:RequiredFieldValidator ID="ddValidator3" runat="server" ControlToValidate="DropDownList3"
            CssClass="text-danger" ErrorMessage="Please select a value." InitialValue="-select-"/>
        <br />
        <asp:CheckBox ID="CheckBox4" runat="server" Text="8 Seater"  AutoPostBack="true" OnCheckedChanged="CheckBox4_CheckedChanged" />
        &nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DropDownList4" runat="server">
            <asp:ListItem>-select-</asp:ListItem>
        </asp:DropDownList>
        &nbsp;nos<br />
        <asp:RequiredFieldValidator ID="ddValidator4" runat="server" ControlToValidate="DropDownList4"
            CssClass="text-danger" ErrorMessage="Please select a value." InitialValue="-select-"/>
        <br />
        <br />
    </div>
    <asp:Label runat="server" AssociatedControlID="CheckBox1" ID="Label2" CssClass="col-md-2 control-label">Choose Date and Time</asp:Label>
    <div class="col-md-10">
        <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.4/themes/smoothness/jquery-ui.css">
        <script src="http://code.jquery.com/jquery-1.9.1.js"></script>
        <script src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
        <script>
            $(function () {
                $("#MainContent_datepicker").datepicker();
            });
        </script>
        <asp:TextBox ID="datepicker" AutoPostBack="true" runat="server" OnTextChanged="datepicker_TextChanged"></asp:TextBox>
        <br />
        <br />
        <br />
        
        <asp:DropDownList ID="DropDownList5" runat="server">
            <asp:ListItem>-select-</asp:ListItem>
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp; Hrs
        <br />
        <asp:RequiredFieldValidator ID="ddValidator5" runat="server" ControlToValidate="DropDownList5"
            CssClass="text-danger" ErrorMessage="Please select the time." InitialValue="-select-"/>
        <br />
        <br />
        <br />
    </div>
    <asp:Label runat="server" AssociatedControlID="EMailID" ID="EMailLabel" CssClass="col-md-2 control-label">EMail Address*</asp:Label>
    <div class="col-md-10">
        <asp:TextBox runat="server" ID="EMailID" CssClass="form-control" />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="EMailID"
            CssClass="text-danger" ErrorMessage="EMail ID is required." />
        <asp:RegularExpressionValidator id="RegularExpressionValidator5" 
                ControlToValidate="EMailID"
                ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
                Display="Static"
                ErrorMessage="Invalid E-Mail Address"
                CssClass="text-danger"
                EnableClientScript="True" 
                runat="server"/>

    </div>
    <div class="col-md-10">
        <p>
            <asp:CheckBox ID="CheckParking" runat="server" Text="Check for Reserving Parking Slot"/>
        </p>
    </div>
    <div class="col-md-10">
        <p>
            <asp:Button ID="Button1" runat="server" Text="Reserve" CssClass="btn btn-default" OnClick="Button1_Click" />
        </p>
    </div>     
</asp:Content>
