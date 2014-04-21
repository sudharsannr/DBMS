<%@ Page Title = "Restaurant Search" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Search.aspx.cs" Inherits="Search" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        var toggle = false;
        var sType = '';
        $(document).ready(function () {
            sType = $('#searchType').val();
            panelLoad();
            $("#advSrchButton").click(function ()
            {
                displayAdvPanel();
            });
        });

        function displayAdvPanel() {
            console.log('Inside displayAdvPanel ' + sType);
            if (!toggle) {
                //advanced display
                $("#MainContent_searchDiv").slideUp();
                $("#MainContent_AdvSrchPanel").slideDown();
                $("#advSrchButton").val('Back to simple');
                $("#MainContent_srchHidden").val('advanced');
                toggle = true;
            }
            else {
                //simple search
                console.log('show simple');
                $("#MainContent_AdvSrchPanel").slideUp();
                $("#MainContent_searchDiv").slideDown();
                $("#advSrchButton").val('Advanced Search');
                $("#MainContent_srchHidden").val('simple');
                toggle = false;
            }
        }

        function panelLoad()
        {
            console.log("Inside panelLoad " + sType);
            if (sType != "") {
                if (sType == 'advanced') {
                    //advanced display
                    $("#MainContent_searchDiv").slideUp();
                    $("#MainContent_AdvSrchPanel").slideDown();
                    $("#advSrchButton").val('Back to Simple');
                    $("#MainContent_srchHidden").val('advanced');
                    toggle = true;
                }
                else {
                    //simple search
                    console.log('show simple');
                    $("#MainContent_AdvSrchPanel").slideUp();
                    $("#MainContent_searchDiv").slideDown();
                    $("#advSrchButton").val('Advanced Search');
                    $("#MainContent_srchHidden").val('simple');
                    toggle = false;
                }
            }
            else {
                //simple search
                console.log('show simple');
                $("#MainContent_AdvSrchPanel").hide();
                $("#MainContent_searchDiv").slideDown();
                $("#advSrchButton").val('Advanced Search');
                $("#MainContent_srchHidden").val('simple');
                toggle = false;
            }
        }
    </script>
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class="row">
        <h2>Need something different?</h2>
        <p>
            Search anything ranging from Restaurant Name, State, City to Food Item or Cuisine
        </p>

        <%--Search--%>
        <div class="form-group" id="searchDiv" runat="server">
            <p><asp:TextBox runat="server" ID="SearchTextBox" CssClass="form-control" Width="32%" /></p> 
                
            </div>
    </div>
    <div class="form-group" id="AdvSrchPanel" runat="server">
                <div class="SearchStyle">
                <table style="border: thick solid #333333;">
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">Restaurant Name <asp:TextBox  runat="server" ID="NameSearch" CssClass="form-control" Width="100%"/></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Cuisine <asp:TextBox runat="server" ID="CuisineSearch" CssClass="form-control" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">Open Time<asp:TextBox runat="server" ID="OpenTimeSearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Close Time<asp:TextBox runat="server" ID="CloseTimeSearch" CssClass="form-control" BorderStyle="Solid" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">City<asp:TextBox runat="server" ID="CitrySearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Zip Code<asp:TextBox runat="server" ID="ZipSearch" CssClass="form-control" Width="100%" /></td></tr>
                <tr><td style="border-color: none; border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 245px;">State<asp:TextBox runat="server" ID="StateSearch" CssClass="form-control" Width="100%" /></td>
                <td style="border-style: none; font-family: Arial, Helvetica, sans-serif; color: #FFFFFF; font-weight: bold; width: 278px;">Food<asp:TextBox runat="server" ID="FoodSearch" CssClass="form-control" Width="100%" /></td></tr>               
                </table>
                </div>
   </div>
    <input type="hidden" ID="searchType" Value="<%=srchType.ToString()%>">
    <asp:HiddenField ID="srchHidden" runat="server"/>
    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" />
    <input type="button" Value="Advanced Search" Class="btn btn-default" id="advSrchButton"/>
    <br />
    <br />
    <div class="row" style="align-content:center">
        <p>
        <asp:GridView CssClass="GridViewStyle" ID="GridView1" runat="server" autogeneratecolumns="False" PageSize="25" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
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