
<%@ Page Title = "Advanced Search" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="AdvancedSearch.aspx.cs" Inherits="AdvancedSearch" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
     <div class="row">
            <h2>Search Differently</h2>
            <p>
                Search anything ranging from Restaurant Name, State, City, Open,Close time to Food Item or Cuisine with your custom specification</p>
                  
            <div class="form-group">
                <div class="col-md-10">
                <table>
                <tr ><td>Restaurant Name <asp:TextBox  runat="server" ID="NameSearch" CssClass="form-control"/></td>
                <td>Cuisine <asp:TextBox runat="server" ID="CuisineSearch" CssClass="form-control" /></td></tr>
                <tr><td>Open Time<asp:TextBox runat="server" ID="OpenTimeSearch" CssClass="form-control" /></td>
                <td>Close Time<asp:TextBox runat="server" ID="CloseTimeSearch" CssClass="form-control" /></td></tr>
                <tr><td>City<asp:TextBox runat="server" ID="CitrySearch" CssClass="form-control" /></td>
                <td>Zip Code<asp:TextBox runat="server" ID="ZipSearch" CssClass="form-control" /></td></tr>
                <tr><td>State<asp:TextBox runat="server" ID="StateSearch" CssClass="form-control" /></td>
                <td>Food<asp:TextBox runat="server" ID="FoodSearch" CssClass="form-control" /></td></tr>
               
                </table>
                </div>
                
            </div>
            <div class="form-group">
                <div class="col-md-10">
                    <p><asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" /></p> 
                </div>
            </div>
   </div>
    <br />
    <br />
    <div class="row" style="align-content:center">
        <p>
        <asp:GridView ID="GridView2" runat="server" autogeneratecolumns="False" PageSize="25" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" AllowSorting="true" OnSorting="GridView2_Sorting">
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
<div class="row" id="NoResult" runat="server" visible="false">
    <p class="text-danger">
        No restaurants found. Please choose different search criteria
    </p>
</div>
</asp:Content>

