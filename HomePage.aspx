<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <p class="text-danger">
        <asp:Literal runat="server" ID="ErrorMessage" />
    </p>  
    <div class="row">
        <div class="col-md-4">
            <h2>Have an Account? Sign in</h2>
            <p>
                Sign in to recieve benefits of being a Gourmet Guide Customer</p>
            <p>
                <a class="btn btn-default" href="/Account/Login">Log In &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Hungry? Why wait? Search for your restaurant here</h2>
            <p>
                Search anything ranging from Restaurant Name, State, City to Food Item or Cuisine</p>
                  
            <%--Search--%>
            <div class="form-group">
                <div class="col-md-10">
                <asp:TextBox runat="server" ID="Search" CssClass="form-control" />
                <asp:RequiredFieldValidator runat="server" ControlToValidate="Search"
                    CssClass="text-danger" ErrorMessage="Please enter a search criteria for better results" />
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-offset-2 col-md-10">
                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>