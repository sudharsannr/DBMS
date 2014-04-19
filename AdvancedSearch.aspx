
<%@ Page Title = "Advanced Search" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="AdvancedSearch.aspx.cs" Inherits="AdvancedSearch" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
     <div class="row">
            <h2>Search Differently</h2>
            <p>
                Search anything ranging from Restaurant Name, State, City, Open,Close time to Food Item or Cuisine with your custom specification</p>
                  
            <div class="form-group">
                <div class="SearchStyle">
                <table style="border: thick solid #000000;">
                <tr ><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">Restaurant Name <asp:TextBox  runat="server" ID="NameSearch" CssClass="form-control" Width="100%"/></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Cuisine <asp:TextBox runat="server" ID="CuisineSearch" CssClass="form-control" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">Open Time<asp:TextBox runat="server" ID="OpenTimeSearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Close Time<asp:TextBox runat="server" ID="CloseTimeSearch" CssClass="form-control" BorderStyle="Solid" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">City<asp:TextBox runat="server" ID="CitrySearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Zip Code<asp:TextBox runat="server" ID="ZipSearch" CssClass="form-control" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">State<asp:TextBox runat="server" ID="StateSearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Food<asp:TextBox runat="server" ID="FoodSearch" CssClass="form-control" Width="100%" /></td></tr>
               
                </table>
                <br />
                    <p><asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" /></p> 
                </div>
            </div>
   </div>
    <br />
    <br />
    <div class="row" style="align-content:center">
        <p>
        <asp:GridView CssClass="GridViewStyle" ID="GridView2" runat="server" autogeneratecolumns="False" PageSize="25" AllowPaging="true" OnPageIndexChanging="GridView2_PageIndexChanging" AllowSorting="true" OnSorting="GridView2_Sorting">
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
                <asp:BoundField HeaderStyle-Font-Underline="false" ControlStyle-Font-Underline="true" DataField="Address1" HeaderText="Address Line 1" />
            </Columns>

            <Columns>
                <asp:BoundField HeaderStyle-Font-Underline="false" DataField="Address2" HeaderText="Address Line 2" />
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
            <RowStyle CssClass="RowStyle" />
            <EmptyDataRowStyle CssClass="EmptyRowStyle" />
            <PagerStyle CssClass="PagerStyle" />
            <SelectedRowStyle CssClass="SelectedRowStyle" />
            <EditRowStyle CssClass="EditRowStyle" />
            <AlternatingRowStyle CssClass="AltRowStyle" />
            <SortedAscendingHeaderStyle CssClass="sortasc" />
            <SortedDescendingHeaderStyle CssClass="sortdesc" />
        </asp:GridView>
            </p> 
    </div>

</asp:Content>

