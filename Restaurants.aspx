<%@ Page Title="Restaurant Details" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Restaurants.aspx.cs" Inherits="Account_Restaurants" %>


<%--   <div id="Restaurant Name">
     <%=str%>
    </div>--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class="Restaurant details">
        <h4>
            <asp:TableCell runat="server" Font-Bold="True" Text=" "><b>Restaurant Name : </b></asp:TableCell><%=rName%></h4>
        <h4>
            <asp:TableCell runat="server" Font-Bold="True" Text=" "><b>Cuisine : </b></asp:TableCell><%=cuisine%></h4>
        <h4>
            <asp:TableCell runat="server" Font-Bold="true" Text=" "><b>Restaurant Address : </b></asp:TableCell><%=location %>
        </h4>
        <h4>
            <asp:TableCell runat="server" Font-Bold="true" Text=" "><b>Working Hours : </b></asp:TableCell><%=workingHours %>
        </h4>
        <h4>
            <asp:TableCell runat="server" Font-Bold="true" Text=" "><b>Restaurant Holidays : </b></asp:TableCell><%=holiday %>
        </h4>
    </div>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script src='<%=gUrl%>'>
    </script>
    <script type="text/javascript">
        /*
         * Call googleMaps api to show the location on Map
         */
        function gMaps(latitude,longitude) {
                var latlng = new google.maps.LatLng(latitude, longitude);
                var myOptions = {
                    zoom: 80,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.HYBRID
                }
                map = new google.maps.Map(document.getElementById("map"), myOptions);
                var marker = new google.maps.Marker({
                    position: latlng,
                    map: map,
                    title: 'Restaurant'
                });         
            
            }
        
    </script>
    <div class="row">
        <div class="col-md-5" id="NoResult" runat="server" visible="false">
            <h4>
                <p class="text-danger">
                    We're sorry. We don't have the details of Food Items available at the restaurant. Kindly contact the restaurant to obtain more information 
                </p>
            </h4>
        </div>
        <div class="col-md-5">
            <p>
                <asp:GridView ID="GridView1" runat="server" PageSize="25" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
                    <Columns>
                        <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME" />
                    </Columns>

                    <Columns>
                        <asp:BoundField DataField="PRICE" HeaderText="PRICE" SortExpression="PRICE" />
                    </Columns>
                </asp:GridView>
            </p>
        </div>
        <div class="col-md-5" id="map" style="width: 675px; height: 654px;">
        </div>
    </div>

    <div class="row" style="align-content: center">
        <p>
            <asp:Button ID="TableReserve" Text="Table Reserve" OnClick="TableReserve_Click" runat="server" CssClass="btn btn-default" />
        </p>
    </div>
</asp:Content>
