<%@ Page Title="Order confirmation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="OrderConfirmation.aspx.cs" Inherits="Account_OrderConfirmation" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
     
         
         
    <div class="row">
        <div class="col-md-5" id="NoResult" runat="server" visible="false">
            <h4 class="text-danger">                
                    Data fetch error. Please return to the previous page.
            </h4>
        </div>
    </div>
    <div>
        <div class="form-group">
             <div style ="col-md-5" id="bookDetails" runat="server">
        </div>
             <br />
            <div id="addressdiv" runat="server" class="col-md-10">
            <asp:Label runat="server" AssociatedControlID="Address1" ID="Address1Label">Address1</asp:Label>
                <br />
                <asp:TextBox runat="server" ID="Address1" CssClass="form-control"/>
                <asp:RequiredFieldValidator ID ="rfvaddress1" runat="server" ControlToValidate="Address1"
                    CssClass="text-danger" ErrorMessage="Address field is required." />
                <br />
            <asp:Label runat="server" AssociatedControlID="Address2" ID="Label2" >Address2</asp:Label>
                <asp:TextBox runat="server" ID="Address2" CssClass="form-control"/>
                <%--<asp:RequiredFieldValidator ID ="rfvaddress2"  runat="server" ControlToValidate="Address2"
                    CssClass="text-danger" ErrorMessage="Address field is required."/>--%>
            </div>
        </div>
       
    </div>
    <div class="col-md-5">
        <br />
        <asp:Button ID="ConfirmBtn" runat="server" Text="Confirm" CssClass="btn btn-default" OnClick="ConfirmBtn_Click" />
    </div>
</asp:Content>