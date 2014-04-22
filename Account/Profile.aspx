<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeFile="Profile.aspx.cs" Inherits="Account_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    
    <script src="Scripts/jquery-1.10.2.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            panelLoad();
            $('#recentSrch,#freqSrch,#SameLocfreqSrch,#FoodLiked,#RestaurantLiked').mouseenter(function (event) {
                $(this).css('color', 'blue');
                $(this).css('text-decoration', 'underline');
                $(this).css('cursor', 'hand');
            }
                );
            $('#recentSrch,#freqSrch,#SameLocfreqSrch,#FoodLiked,#RestaurantLiked').mouseleave(function (event) {
                $(this).css('color', 'black');
                $(this).css('text-decoration', 'none');
                $(this).css('cursor', 'pointer');
            }
                );
            $('#recentSrch').on('click', function (event) {
                $("#recentSrchup").toggle('show');
                $("#recentSrchdown").toggle('show');
                $('#recentSrchResult').toggle('show');
                
            });
            $('#freqSrch').on('click', function (event) {
                $("#freqSrchup").toggle('show');
                $("#freqSrchdown").toggle('show');
                $('#freqSrchResult').toggle('show');
            });
            $('#SameLocfreqSrch').on('click', function (event) {
                $("#SameLocfreqSrchup").toggle('show');
                $("#SameLocfreqSrchdown").toggle('show');
                $('#SameLocfreqSrchResult').toggle('show');
                
            });
            $('#FoodLiked').on('click', function (event) {
                $("#FoodLikedup").toggle('show');
                $("#FoodLikeddown").toggle('show');
                $('#FoodLikedResult').toggle('show');
            });
            $('#RestaurantLiked').on('click', function (event) {
                $("#RestaurantLikedup").toggle('show');
                $("#RestaurantLikeddown").toggle('show');
                $('#RestaurantLikedResult').toggle('show');
            });
        });

        function panelLoad() {
            console.log("Inside panelLoad ");
            $("#recentSrchResult").hide();
            $("#freqSrchResult").hide();
            $("#SameLocfreqSrchResult").hide();
            $("#FoodLikedResult").hide();
            $("#RestaurantLikedResult").hide();
            $("#recentSrchup").hide();
            $("#freqSrchup").hide();
            $("#SameLocfreqSrchup").hide();
            $("#FoodLikedup").hide();
            $("#RestaurantLikedup").hide();
        }
    </script>
    <div class="row">
        <div class="col-md-10">
            <h2>
                Welcome to your Profile Page!!
            </h2>
            <div id="recentSrch">
                <br />
                <p>
                    Your Recent 5 searches <label id ="recentSrchdown"> ▼ </label> <label id ="recentSrchup"> ▲ </label>
                </p>
            </div>
            <div id="recentSrchResult">
                <asp:Repeater ID="RecentSearchRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton  ForeColor="#000066" runat="server" AutoPostBack="false" ID="SearchTerm" Text='<%# Eval("searchterm") %>' OnClick="SearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
        
        <div class="col-md-10">
            <br />
            <div id="freqSrch">
                <p>
                    Your most frequent searches <label id ="freqSrchdown"> ▼ </label> <label id ="freqSrchup"> ▲ </label>
                </p>
            </div>
            <div id="freqSrchResult">
                <asp:Repeater ID="FrequentSearchRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton  ForeColor="#000066" runat="server" AutoPostBack="false" ID="FrequentSearchTerm" Text='<%# Eval("searchterm") %>' OnClick="FrequentSearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <br />
            <div id="SameLocfreqSrch">
                <p>
                    People from <% =city %> mostly searched for <label id ="SameLocfreqSrchdown"> ▼ </label> <label id ="SameLocfreqSrchup"> ▲ </label>
                </p>
            </div>
            <div id="SameLocfreqSrchResult">
                <asp:Repeater ID="SameLocFrequentSearchRepeater" runat="server"> 
                    <ItemTemplate>
                        <asp:LinkButton  ForeColor="#000066" runat="server" AutoPostBack="false" ID="SameLocFrequentSearchTerm" Text='<%# Eval("searchterm") %>' OnClick="SameLocFrequentSearchTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <br />
            <div id="FoodLiked">
                <p>
                    Your most liked food's based on your previous orders <label id ="FoodLikeddown"> ▼ </label> <label id ="FoodLikedup"> ▲ </label>
                </p>
            </div>
            <div id="FoodLikedResult">
                <asp:Repeater ID="FoodLikedRepeater" runat="server">
                    <ItemTemplate>
                        <asp:LinkButton  ForeColor="#000066" runat="server" AutoPostBack="false" ID="FoodLikedTerm" Text='<%# Eval("foodname") %>' OnClick="FoodLikedTerm_Click" CausesValidation="false"/>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="col-md-10">
            <br />
            <div id="RestaurantLiked">
                <p>
                    Restaurant's you prefer ordering from <label id ="RestaurantLikeddown"> ▼ </label> <label id ="RestaurantLikedup"> ▲ </label>
                </p>
            </div>
            <div id="RestaurantLikedResult">
                <asp:Repeater ID="RestaurantLikedRepeater" runat="server" OnItemDataBound="RestaurantLikedRepeater_ItemDataBound">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("restaurantid") %>' Visible="false"></asp:Label>
                        <asp:HyperLink ForeColor="#000066" runat="server" AutoPostBack="false" ID="RestaurantLikedTerm"  Text='<%# Eval("name") %>' CausesValidation="false"/>
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
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-default" OnClick="Button1_Click" />
                </div>
            </div>
        </div>


    </div>




</asp:Content>

    
 
