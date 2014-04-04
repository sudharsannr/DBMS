<%@ Page Title = "Restaurant Search" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Search.aspx.cs" Inherits="Search" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class="row">
            <h2>Need something different?</h2>
            <p>
                Search anything ranging from Restaurant Name, State, City to Food Item or Cuisine</p>
                  
            <%--Search--%>
            <div class="form-group">
                <div class="col-md-10">
                <p><asp:TextBox runat="server" ID="SearchTextBox" CssClass="form-control" /></p>
                </div>
                
            </div>
            <div class="form-group">
                <div class="col-md-10">
                    <p><asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" />
                        <asp:Button ID="Button2" runat="server" Text="Advanced Search" CssClass="btn btn-default" OnClick="Button2_Click" /></p> 
                </div>
            </div>
   </div>
    <br />
    <br />
    <div class="row" style="align-content:center">
        <p>
        <asp:GridView ID="GridView1" runat="server" autogeneratecolumns="False" PageSize="25" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
            <Columns>
                <asp:HyperLinkField DataTextField="NAME" HeaderText="Restaurant Name" DataNavigateUrlFields="RESTAURANTID" 
                    DataNavigateUrlFormatString="Restaurants.aspx?restaurant={0}" Text="NAME" SortExpression="NAME"/>
            </Columns> 
            <Columns>
                <asp:BoundField DataField="Description" HeaderText="Cuisine" SortExpression="DESCRIPTION"/>
            </Columns>
                       
            <Columns>
                <asp:BoundField DataField="OpenTime" HeaderText="Open Time" SortExpression="OPENTIME"/>
            </Columns>
            
            <Columns>
                <asp:BoundField DataField="CloseTime" HeaderText="Close Time" SortExpression="CLOSETIME"/>
            </Columns>

            <Columns>
                <asp:BoundField DataField="Address1" HeaderText="Address Line 1" />
            </Columns>

            <Columns>
                <asp:BoundField DataField="Address2" HeaderText="Address Line 2" />
            </Columns>

            <Columns>
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="CITY"/>
            </Columns>

            <Columns>
                <asp:BoundField DataField="State" HeaderText="State" SortExpression="STATE"/>
            </Columns>

            <Columns>
                <asp:BoundField DataField="Zip" HeaderText="Zip Code" SortExpression="ZIP"/>
            </Columns>

            <Columns>
                <asp:BoundField Visible="false" DataField="RestaurantID" HeaderText="Restaurant ID" /> 
            </Columns>
        </asp:GridView>
            </p> 
    </div>
</asp:Content>