<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="ConfirmCode.aspx.cs" Inherits="Account_ConfirmCodeaspx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p> 

    <div class="row">
        <div class="col-md-4">
            <p>
                A confirmation code has been sent to your E-Mail. Please enter that here</p>
                  
            <%--Search--%>
            <div class="form-group">
                <div class="col-md-10">
                <asp:TextBox runat="server" ID="ConfirmCode" CssClass="form-control" Text=""/>
                </div>
            </div>
           <div class="form-group">
                <div class="col-md-10">
                <asp:Button ID="Button1" runat="server" Text="Confirm Registration" CssClass="btn btn-default" OnClick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Text="Resend Registration Code" CssClass="btn btn-default" OnClick="Button2_Click"/>
                </div>
           </div>
        </div>
     </div>
</asp:Content>
