<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Profile.aspx.cs" Inherits="Account_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Gourmet Guide</h1>
        <p class="lead">Your one stop guide from searching a restaurant to ordering food is here.</p>
    </div>
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            panelLoad();
            $('#recentSrch').on('click', function (event) {
                $('#recentSrchResult').toggle('show');
            });
            $('#freqSrch').on('click', function (event) {
                $('#freqSrchResult').toggle('show');
            });
            $('#SameLocfreqSrch').on('click', function (event) {
                $('#SameLocfreqSrchResult').toggle('show');
            });
            $('#FoodLiked').on('click', function (event) {
                $('#FoodLikedResult').toggle('show');
            });
            $('#RestaurantLiked').on('click', function (event) {
                $('#RestaurantLikedResult').toggle('show');
            });
        });

        function panelLoad() {
            console.log("Inside panelLoad ");
            $("#recentSrchResult").hide();
            $("#freqSrchResult").hide();
            $("#SameLocfreqSrchResult").hide();
            $("#FoodSrchResult").hide();
            $("#RestaurantLikedResult").hide();
        }
    </script>
    <div class="row">
        <div class="col-md-10">
            <h2>
                Welcome to your Profile Page!!
            </h2>
            <div id="recentSrch">
                <p>
                    Your Recent 5 searches
                </p>
            </div>
            <div id="recentSrchResult">
                <asp:Repeater ID="RecentSearchRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="SearchTerm" Text='<%# Eval("searchterm") %>' OnClick="SearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        
        <div class="col-md-10">
            <div id="freqSrch">
                <p>
                    Your most frequent searches
                </p>
            </div>
            <div id="freqSrchResult">
                <asp:Repeater ID="FrequentSearchRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="FrequentSearchTerm" Text='<%# Eval("searchterm") %>' OnClick="FrequentSearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <div id="SameLocfreqSrch">
                <p>
                    People from <% =city %> mostly searched for
                </p>
            </div>
            <div id="SameLocfreqSrchResult">
                <asp:Repeater ID="SameLocFrequentSearchRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="SameLocFrequentSearchTerm" Text='<%# Eval("searchterm") %>' OnClick="SameLocFrequentSearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <div id="FoodLiked">
                <p>
                    Your most liked food's based on your previous orders
                </p>
            </div>
            <div id="FoodLikedResult">
                <asp:Repeater ID="FoodLikedRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" AutoPostBack="false" ID="FoodLikedTerm" Text='<%# Eval("foodname") %>' OnClick="FoodLikedTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <div id="RestaurantLiked">
                <p>
                    Restaurant's you prefer ordering from
                </p>
            </div>
            <div id="RestaurantLikedResult">
                <asp:Repeater ID="RestaurantLikedRepeater" runat="server" OnItemDataBound="RestaurantLikedRepeater_ItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("restaurantid") %>' Visible="false"></asp:Label>
                        <asp:HyperLink runat="server" AutoPostBack="false" ID="RestaurantLikedTerm"  Text='<%# Eval("name") %>' CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
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

    
 
