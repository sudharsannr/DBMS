<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="ConfirmCode.aspx.cs" Inherits="Account_ConfirmCodeaspx" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
     
    
    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p> 

    <div class="row">
        <div class="col-md-4">
            <label id="ConfMsg" runat="server">
                A confirmation code has been sent to your E-Mail. Please enter that here
            </label>
                  
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
