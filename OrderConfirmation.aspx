<%@ Page Title="Order confirmation" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="OrderConfirmation.aspx.cs" Inherits="Account_OrderConfirmation" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
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
            <br /><br />
            <asp:Label runat="server" AssociatedControlID="Address1" ID="Address1Label" CssClass="col-md-2 control-label">Address1</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Address1" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Address1"
                    CssClass="text-danger" ErrorMessage="Address field is required." />
            </div>
        </div>
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="Address2" ID="Label2" CssClass="col-md-2 control-label">Address2</asp:Label>
            <div class="col-md-10">
                <asp:TextBox runat="server" ID="Address2" CssClass="form-control"/>
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Address2"
                    CssClass="text-danger" ErrorMessage="Address field is required." />
            </div>
        </div>
       
    </div>
    <div class="col-md-5">
        <br />
        <asp:Button ID="ConfirmBtn" runat="server" Text="Confirm" CssClass="btn btn-default" OnClick="ConfirmBtn_Click" />
    </div>
</asp:Content>