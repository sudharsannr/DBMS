<%@ Page Title="Order food" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="OrderFood.aspx.cs" Inherits="Account_OrderFood" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="row">
        <div class="col-md-5" id="NoResult" runat="server" visible="false">
            <h4 class="text-danger">                
                    We're sorry. We don't have the details of Food Items available at the restaurant. Kindly contact the restaurant to obtain more information                 
            </h4>
        </div>
    </div>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src="Scripts/OrderFood.js"></script>    
    <div class="col-md-5" style="border: solid; border-color:white; border-width: thick; color: black; font-family: Arial, Sans-Serif; font-size:small; table-layout: auto; background-color: #f0f0f0;"  id="FoodTbl" runat="server">
    </div>
       
    <div class="col-md-5" style="font-size: medium; background-color: #f0f0f0; color: black; border: solid; border-color:white; border-width: thick;">
       
        <br />
      <div id ="emaildiv" runat="server">
    <asp:Label runat="server" AssociatedControlID="EMailID" ID="EMailLabel">EMail Address*</asp:Label>
        <br />
        <br />
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
        <br />
       </div>
    <asp:Label runat="server" AssociatedControlID="EMailID" ID="EMailLabel0">Your Order Total is : $ </asp:Label>
        <label id="totalPrice"></label>
        <br />
        <asp:Button ID="OrderBtn" runat="server" Text="Order Now!" CssClass="btn btn-default" OnClick="OrderBtn_Click" OnClientClick="gatherData();"/>        
        <br />
        <br />
        <asp:HiddenField ID="OrderData" runat="server" ></asp:HiddenField>
        <asp:HiddenField ID="TotalPrice" runat="server"></asp:HiddenField>        
    </div>
</asp:Content>