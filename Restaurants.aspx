<%@ Page Title = "Restaurants" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Restaurants.aspx.cs" Inherits="Account_Restaurants" %>
    

<%--   <div id="Restaurant Name">
     <%=str%>
    </div>--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <div class ="Restaurant details">
    <h4><asp:TableCell runat="server" Font-Bold="True" Text=" "><b>Restaurant Name :</b></asp:TableCell><%=str%></h4> 
                
    </div>
    
   
    <div class="row" style="align-content:center">
        <p>
        <asp:GridView ID="GridView1" runat="server" PageSize="25" autogeneratecolumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" AllowSorting="true" OnSorting="GridView1_Sorting">
            <Columns>
                <asp:BoundField DataField="NAME" HeaderText="Name" SortExpression="NAME"/>
            </Columns>
                       
            <Columns>
                <asp:BoundField DataField="PRICE" HeaderText="PRICE" SortExpression="PRICE"/>
            </Columns>
            
            
        </asp:GridView>
            </p> 
    </div>
   
<div class="row" style="align-content:center">
        <p>
             <asp:Button ID="TableReserve" Text="Table Reserve" OnClick="TableReserve_Click" runat="server" CssClass="btn btn-default"/>
        </p>
    </div>
</asp:Content>
