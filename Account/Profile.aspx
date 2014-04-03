<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Profile.aspx.cs" Inherits="Account_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class="row">
        <div class="col-md-10">
            <h2>
                Welcome to your Profile Page!!
            </h2>
            <p>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
            </p>
        </div>
        
        <div class="col-md-10">
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

    
 
