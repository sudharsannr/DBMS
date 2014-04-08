<%@ Page Title="Order food" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="OrderFood.aspx.cs" Inherits="Account_OrderFood" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class="row">
        <div class="col-md-5" id="NoResult" runat="server" visible="false">
            <h4>
                <p class="text-danger">
                    We're sorry. We don't have the details of Food Items available at the restaurant. Kindly contact the restaurant to obtain more information 
                </p>
            </h4>
        </div>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/OrderFood.js"></script>    
    <div class="col-md-5" id="FoodTbl" runat="server">
    </div>
    <asp:Label runat="server" AssociatedControlID="EMailID" ID="EMailLabel" CssClass="col-md-2 control-label">EMail Address*</asp:Label>
        <asp:TextBox runat="server" ID="EMailID" CssClass="form-control" />
        <asp:Label ID="CheckBoxValidator" runat="server" CssClass="text-danger" Text="Please check and enter your booking details."/>
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
    <div class="col-md-5">
        <label id="totalPrice"></label>
        <asp:Button ID="OrderBtn" runat="server" Text="Order Now!" CssClass="btn btn-default" OnClick="OrderBtn_Click" OnClientClick="gatherData();"/>        
        <asp:HiddenField ID="OrderData" runat="server" ></asp:HiddenField>
        <asp:HiddenField ID="TotalPrice" runat="server"></asp:HiddenField>        
    </div>
</asp:Content>