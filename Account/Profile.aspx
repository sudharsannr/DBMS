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
                Your Recent 5 searches
            </p>
            <p>
                <asp:Repeater ID="RecentSearchRepeater" runat="server">
                    <HeaderTemplate>
                        <p>
                            Search Term
                        </p>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="SearchTerm" Text='<%# Eval("searchterm") %>' OnClick="SearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </p>
        </div>
        
        <div class="col-md-10">
            <p>
                Your most frequent searches
            </p>
            <p>
                <asp:Repeater ID="FrequentSearchRepeater" runat="server">
                    <HeaderTemplate>
                        <p>
                            Search Term
                        </p>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="FrequentSearchTerm" Text='<%# Eval("searchterm") %>' OnClick="FrequentSearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
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
                <asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="Search"
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

    
 
