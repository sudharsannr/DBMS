<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h2><%: Title %></h2>
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>

    <div class="form-horizontal">
        <h4>Create a new account.</h4>
        <hr />
        <asp:ValidationSummary runat="server" CssClass="text-danger" />
        
        <%--<asp:View ID="Form1" runat="server">--%>
        
        <%--User Name--%>
        <div id="Form1" runat="server" visible="true">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="UserName" ID="UserNameLabel" CssClass="col-md-2 control-label">User name*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="UserName" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            </div>
        </div>

        <%--EMail Address--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="EMailID" ID="EMailIDLabel" CssClass="col-md-2 control-label">E-Mail ID*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="EMailID" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
                <asp:RegularExpressionValidator id="RegularExpressionValidator5" 
                     ControlToValidate="EMailID"
                     ValidationExpression="[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}"
                     Display="Static"
                     ErrorMessage="Invalid E-Mail Address"
                     CssClass="text-danger"
                     EnableClientScript="True" 
                     runat="server"/>
            </div>
        </div>

        <%--Password--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Password" ID="PasswordLabel" CssClass="col-md-2 control-label">Password*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Password" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Password"
                    CssClass="text-danger" ErrorMessage="The password field is required." />
                <asp:RegularExpressionValidator id="RegularExpressionValidator3" 
                     ControlToValidate="Password"
                     ValidationExpression="^(?=.*[^a-zA-Z])(?=.*[a-z])(?=.*[A-Z])\S{6,}$"
                     Display="Static"
                     ErrorMessage="Make sure there are no white-space characters
                     minimum length of 6,
                    Make sure there is at least:one non-alpha character,
                    one upper case character,one lower case character"
                     CssClass="text-danger"
                     EnableClientScript="True" 
                     runat="server"/>
            </div>
        </div>

        <%--Confirm Password--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ConfirmPassword" ID="ConfirmPasswordLabel" CssClass="col-md-2 control-label">Confirm password*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmPassword" TextMode="Password" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The confirm password field is required." />
                <asp:CompareValidator runat="server" ControlToCompare="Password" ControlToValidate="ConfirmPassword"
                    CssClass="text-danger" Display="Dynamic" ErrorMessage="The password and confirmation password do not match." />
            </div>
       </div>

        <%--First Name--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="FirstName" ID="FirstNameLabel" CssClass="col-md-2 control-label">First name*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="FirstName" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="FirstName"
                    CssClass="text-danger" ErrorMessage="The First name field is required." />
            </div>
        </div>

        <%--LastName--%>
        <div id="Div2" runat="server" visible="true">
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="LastName" ID="LastNameLabel" CssClass="col-md-2 control-label">Last name*</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="LastName" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="LastName"
                    CssClass="text-danger" ErrorMessage="The Last name field is required." />
            </div>
        </div>

        <%--State--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="GenderDropDown" ID="GenderLabel" CssClass="col-md-2 control-label">Gender</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="GenderDropDown" CssClass="form-control">
                    <asp:ListItem>Male</asp:ListItem>
                    <asp:ListItem>Female</asp:ListItem>
                    <asp:ListItem>Not Specified</asp:ListItem>
                 </asp:DropDownList>
            </div>
         </div>
        <%--Phone Number--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="PhoneNumber" ID="PhoneNumberLabel" CssClass="col-md-2 control-label">Phone Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="PhoneNumber" CssClass="form-control" />
                <asp:RegularExpressionValidator id="RegularExpressionValidator1" 
                     ControlToValidate="PhoneNumber"
                     ValidationExpression="^[0-9]{10,10}$"
                     Display="Static"
                     ErrorMessage="Enter 10 Digits"
                     CssClass="text-danger"
                     EnableClientScript="True" 
                     runat="server"/>
            </div>
        </div>

        <%--Date Of Birth--%>
        <%--<div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DateOfBirth" ID="DateOfBirthLabel" CssClass="col-md-2 control-label">Date Of Birth</asp:Label>
            <div class="col-md-10">
                <asp:Calendar runat="server" ID="DateOfBirth" OnSelectionChanged="DateOfBirth_SelectionChanged" OnVisibleMonthChanged="DateOfBirth_VisibleMonthChanged">

                </asp:Calendar>
       --%>         <%--<asp:RequiredFieldValidator runat="server" ControlToValidate="DateOfBirth"
                    CssClass="text-danger" ErrorMessage="The user name field is required." />
            --%><%--</div>
        </div>--%>

        <%--Street Address--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="StreetAddress" ID="StreetAddressLabel" CssClass="col-md-2 control-label">Street Address</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="StreetAddress" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="StreetAddress"
                    CssClass="text-danger" ErrorMessage="Required Field" />
            </div>
        </div>

        <%--Door Number--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="DoorNumber" ID="DoorNumberLabel" CssClass="col-md-2 control-label">Door Number/Apartment Number</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="DoorNumber" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="DoorNumber"
                    CssClass="text-danger" ErrorMessage="Required Field" />
            </div>
        </div>

        <%--City--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="City" ID="CityLabel" CssClass="col-md-2 control-label">City</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="City" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                    CssClass="text-danger" ErrorMessage="Required Field" />
            </div>
        </div>

        <%--State--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="State" ID="StateLabel" CssClass="col-md-2 control-label">State</asp:Label>
            <div class="col-md-10">
                <asp:DropDownList runat="server" ID="State" CssClass="form-control">
                    <asp:ListItem>AL</asp:ListItem>
                    <asp:ListItem>AK</asp:ListItem>
                    <asp:ListItem>AZ</asp:ListItem>
                    <asp:ListItem>AR</asp:ListItem>
                    <asp:ListItem>CA</asp:ListItem>
                    <asp:ListItem>CO</asp:ListItem>
                    <asp:ListItem>CT</asp:ListItem>
                    <asp:ListItem>DE</asp:ListItem>
                    <asp:ListItem>DC</asp:ListItem>
                    <asp:ListItem>FL</asp:ListItem>
                    <asp:ListItem>GA</asp:ListItem>
                    <asp:ListItem>HI</asp:ListItem>
                    <asp:ListItem>ID</asp:ListItem>
                    <asp:ListItem>IL</asp:ListItem>
                    <asp:ListItem>IN</asp:ListItem>
                    <asp:ListItem>IA</asp:ListItem>
                    <asp:ListItem>KS</asp:ListItem>
                    <asp:ListItem>KY</asp:ListItem>
                    <asp:ListItem>LA</asp:ListItem>
                    <asp:ListItem>ME</asp:ListItem>
                    <asp:ListItem>MD</asp:ListItem>
                    <asp:ListItem>MA</asp:ListItem>
                    <asp:ListItem>MI</asp:ListItem>
                    <asp:ListItem>MN</asp:ListItem>
                    <asp:ListItem>MS</asp:ListItem>
                    <asp:ListItem>MO</asp:ListItem>
                    <asp:ListItem>MT</asp:ListItem>
                    <asp:ListItem>NE</asp:ListItem>
                    <asp:ListItem>NV</asp:ListItem>
                    <asp:ListItem>NH</asp:ListItem>
                    <asp:ListItem>NJ</asp:ListItem>
                    <asp:ListItem>NM</asp:ListItem>
                    <asp:ListItem>NY</asp:ListItem>
                    <asp:ListItem>NC</asp:ListItem>
                    <asp:ListItem>ND</asp:ListItem>
                    <asp:ListItem>OH</asp:ListItem>
                    <asp:ListItem>OK</asp:ListItem>
                    <asp:ListItem>OR</asp:ListItem>
                    <asp:ListItem>PA</asp:ListItem>
                    <asp:ListItem>RI</asp:ListItem>
                    <asp:ListItem>SC</asp:ListItem>
                    <asp:ListItem>SD</asp:ListItem>
                    <asp:ListItem>TN</asp:ListItem>
                    <asp:ListItem>TX</asp:ListItem>
                    <asp:ListItem>UT</asp:ListItem>
                    <asp:ListItem>VT</asp:ListItem>
                    <asp:ListItem>VA</asp:ListItem>
                    <asp:ListItem>WA</asp:ListItem>
                    <asp:ListItem>WV</asp:ListItem>
                    <asp:ListItem>WI</asp:ListItem>
                    <asp:ListItem>WY</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="State"
                    CssClass="text-danger" ErrorMessage="Required Field" />
            </div>
        </div>

        <%--Zip Code--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ZipCode" ID="ZipCodeLabel" CssClass="col-md-2 control-label">Zip Code</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="ZipCode" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="City"
                    CssClass="text-danger" ErrorMessage="Required Field" />
                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="" ControlToValidate="ZipCode" Type="Integer"
                 Operator="DataTypeCheck" CssClass="text-danger" ErrorMessage="Numbers Only!" />
                <asp:RegularExpressionValidator id="RegularExpressionValidator2" 
                     ControlToValidate="ZipCode"
                     ValidationExpression="^[0-9]{5,5}$"
                     Display="Static"
                     ErrorMessage="Please Enter Only Valid 5 Digit ZipCode"
                     CssClass="text-danger"
                     EnableClientScript="True" 
                     runat="server"/>
            </div>
        </div>

        <%--Calorie Count--%>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="CalorieCount" ID="CalorieCountLabel" CssClass="col-md-2 control-label">Calorie Count</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="CalorieCount" CssClass="form-control" />
                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="CalorieCount" Type="Integer"
                 Operator="DataTypeCheck" CssClass="text-danger" ErrorMessage="Enter Numbers Only!" />
            </div>
            </div>
            </div>
<%--        </asp:View>--%>
            <%--Get Registration Code--%>
            <div class="col-md-10">
                <asp:Button ID="Button2" runat="server" Text="Get Registration Code" CssClass="btn btn-default" OnClick="GetRegCode" AutoPostBack="false"/>
            </div>
    </div>
    </div>
</asp:Content>

