using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.Data;
using System.Text;
using GourmetGuide;

public partial class Search : System.Web.UI.Page
{
    public string str = "";
    public string rID = "";
    public string rName = "";
    public string userName = "";
    public string srchType = "";
    public string srchHiddenStr = "";
    DataSet tbl = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["AdvancedSearch"] != null)
            FilterDiv.Visible = true;
        else if (Request.QueryString["SearchString"] == null)
            FilterDiv.Visible = false;
        else
            FilterDiv.Visible = true;
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            userName = System.Web.HttpContext.Current.User.Identity.Name;
        srchHiddenStr = srchHidden.Value;
        //srchType = searchType.Value;
        if (Session["searchType"] != null)
        {
            if (Request.QueryString["SearchString"] == null && Request.QueryString["AdvancedSearch"] == null)
            {
                Session.Remove("searchType");
            }
            else
            {
                srchType = Session["searchType"].ToString();
                System.Diagnostics.Debug.WriteLine("Session retrieved " + srchType);
                //Session.Remove("searchType");
            }
            //TODO: Assign search to textboxes pending
        }
        System.Diagnostics.Debug.WriteLine("Searchtype :" + srchType);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (userName != "")
        {
            insert_userSearch();
        }

        srchHiddenStr = srchHidden.Value;
        if (String.Equals(srchHiddenStr, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            Session["searchType"] = "advanced";
            String query = findParameters();
            //Page_Load(sender, e);
            /*
            if (!IsPostBack)
            {
                ViewState["sortOrder"] = "";
                bindGridView("", "");
            }
             */
            Response.Redirect("Search.aspx?AdvancedSearch=true&" + query);
        }
        else
        {
            Session["searchType"] = "simple";
            Response.Redirect("Search.aspx?SearchString=" + SearchTextBox.Text);
        }
    }

    private string findParameters()
    {
        string namesearchString;
        string cuisinesearchString;
        string openTimesearchString;
        string closeTimesearchString;
        string citysearchString;
        string zipsearchString;
        string statesearchString;
        string foodsearchString;
        if (NameSearch.Text.Trim().Length == 0)
            namesearchString = "UPPER(NAME)";
        else
            namesearchString = HttpUtility.UrlEncode("\'%" + NameSearch.Text.ToString().ToUpper() + "%\'");

        if (CuisineSearch.Text.Trim().Length == 0)
            cuisinesearchString = "UPPER(DESCRIPTION)";
        else
            cuisinesearchString = HttpUtility.UrlEncode("\'%" + CuisineSearch.Text.ToString().ToUpper() + "%\'");


        if (OpenTimeSearch.Text.Trim().Length == 0)
            openTimesearchString = "UPPER(OPENTIME)";
        else
            openTimesearchString = HttpUtility.UrlEncode("\'%" + OpenTimeSearch.Text.ToString().ToUpper() + "%\'");

        if (CloseTimeSearch.Text.Trim().Length == 0)
            closeTimesearchString = "UPPER(CLOSETIME)";
        else
            closeTimesearchString = HttpUtility.UrlEncode("\'%" + CloseTimeSearch.Text.ToString().ToUpper() + "%\'");

        if (CitrySearch.Text.Trim().Length == 0)
            citysearchString = "UPPER(CITY)";
        else
            citysearchString = HttpUtility.UrlEncode("\'%" + CitrySearch.Text.ToString().ToUpper() + "%\'");

        if (ZipSearch.Text.Trim().Length == 0)
            zipsearchString = "ZIP";
        else
            zipsearchString = HttpUtility.UrlEncode("\'%" + ZipSearch.Text.ToString().ToUpper() + "%\'");

        if (StateSearch.Text.Trim().Length == 0)
            statesearchString = "UPPER(STATE)";
        else
            statesearchString = HttpUtility.UrlEncode("\'%" + StateSearch.Text.ToString().ToUpper() + "%\'");

        if (FoodSearch.Text.Trim().Length == 0)
            foodsearchString = "UPPER(FOOD.NAME)";
        else
            foodsearchString = HttpUtility.UrlEncode("\'%" + FoodSearch.Text.ToString().ToUpper() + "%\'");

        StringBuilder sb = new StringBuilder()
                            .Append("restaurant=").Append(namesearchString).Append("&")
                            .Append("cuisine=").Append(cuisinesearchString).Append("&")
                            .Append("opentime=").Append(openTimesearchString).Append("&")
                            .Append("closetime=").Append(closeTimesearchString).Append("&")
                            .Append("city=").Append(citysearchString).Append("&")
                            .Append("state=").Append(statesearchString).Append("&")
                            .Append("zip=").Append(zipsearchString).Append("&")
                            .Append("food=").Append(foodsearchString);
        System.Diagnostics.Debug.WriteLine("Query" + sb.ToString());
        return sb.ToString();
    }

    private void insert_userSearch()
    {
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "insert into srajagop.userSearch values(?,?,CURRENT_TIMESTAMP)";
        OleDbTransaction tran = null;
        OleDbConnection conn = null;
        try
        {
            conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            tran = conn.BeginTransaction();
            OleDbParameter param = new OleDbParameter();
            OleDbCommand insert_search = new OleDbCommand(cmd, conn, tran);
            insert_search.Parameters.Add("?", OleDbType.VarChar).Value = System.Web.HttpContext.Current.User.Identity.Name;
            //insert_search.Parameters.Add("?", OleDbType.VarChar).Value = Search.Text.ToUpper();
            insert_search.Parameters.Add("?", OleDbType.VarChar).Value = SearchTextBox.Text.ToUpper();
            insert_search.ExecuteNonQuery();
            tran.Commit();

        }
        finally
        {
            conn.Close();
        }
    }

    //protected void Button2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("AdvancedSearch.aspx");
    //}

    //protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        HyperLink hl = (HyperLink)e.Row.FindControl("link");
    //        if (hl != null)
    //        {

    //            hl.NavigateUrl = "~/Restaurants.aspx?"
    //        }
    //    } 
    //}
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        System.Diagnostics.Debug.WriteLine("page index changed " + e.NewPageIndex);
        bindGridView("", "");

    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        bindGridView(e.SortExpression, sortOrder);
    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }

    public void bindGridView(string sortExp, string sortDir)
    {
        System.Diagnostics.Debug.WriteLine("Checking " + srchType + "--" + srchHiddenStr);
        if (String.Equals(srchType, "advanced", StringComparison.OrdinalIgnoreCase) ||
            String.Equals(srchHiddenStr, "advanced", StringComparison.OrdinalIgnoreCase))
        {
            System.Diagnostics.Debug.WriteLine("advanced search");
            string namesearchString = Request.QueryString["restaurant"];
            string cuisinesearchString = Request.QueryString["cuisine"];
            string openTimesearchString = Request.QueryString["opentime"];
            string closeTimesearchString = Request.QueryString["closetime"];
            string citysearchString = Request.QueryString["city"];
            string zipsearchString = Request.QueryString["zip"];
            string statesearchString = Request.QueryString["state"];
            string foodsearchString = Request.QueryString["food"];
            AdvancedSearch(namesearchString, cuisinesearchString, openTimesearchString,
                closeTimesearchString, citysearchString, zipsearchString,
                statesearchString, foodsearchString, sortExp, sortDir);
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("simple search");
            simpleSearch(sortExp, sortDir);
        }
    }

    private void simpleSearch(String sortExp, String sortDir)
    {
        if (Request.QueryString["SearchString"] != null)
        {
            string searchString = Request.QueryString["SearchString"].ToUpper();
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM ProjectSettings.schema.RESTAURANT WHERE UPPER(NAME) LIKE ? OR UPPER(CITY) LIKE ? OR UPPER(STATE) LIKE ? OR UPPER(DESCRIPTION) LIKE ? UNION SELECT DISTINCT ProjectSettings.schema.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ProjectSettings.schema.RESTAURANT.RESTAURANTID FROM ProjectSettings.schema.RESTAURANT INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID INNER JOIN ProjectSettings.schema.FOOD ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID AND ProjectSettings.schema.FOOD.FOODID IN (SELECT ProjectSettings.schema.FOOD.FOODID FROM ProjectSettings.schema.FOOD WHERE (UPPER(ProjectSettings.schema.FOOD.NAME) LIKE ?)))";
            System.Diagnostics.Debug.WriteLine("Simpe query: " + cmd);
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            OleDbCommand simp_search = new OleDbCommand(cmd, conn);
            OleDbParameter param = new OleDbParameter();
            simp_search.Parameters.Add("@p1", OleDbType.VarChar).Value = "%" + searchString + "%";
            simp_search.Parameters.Add("@p2", OleDbType.VarChar).Value = "%" + searchString + "%";
            simp_search.Parameters.Add("@p3", OleDbType.VarChar).Value = "%" + searchString + "%";
            simp_search.Parameters.Add("@p4", OleDbType.VarChar).Value = "%" + searchString + "%";
            simp_search.Parameters.Add("@p5", OleDbType.VarChar).Value = "%" + searchString + "%";
            conn.Open();
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(simp_search);
            //tbl.Clear();
            oAdapter.Fill(tbl);
            DataView myDataView = new DataView();
            myDataView = tbl.Tables[0].DefaultView;
            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind();
            GridView1.PagerSettings.Mode = PagerButtons.Numeric;

            foreach (GridViewRow gr in GridView1.Rows)
            {
                HyperLink hp = new HyperLink();
                hp.Text = gr.Cells[0].Text;
                rName = hp.Text;
                rID = gr.Cells[9].Text;
                rID = "~/Restaurants.aspx?restaurant=" + rID;
                gr.Cells[0].Controls.Add(hp);
            }
            conn.Close();
        }
    }

    private void AdvancedSearch(string namesearchString, string cuisinesearchString, string openTimesearchString, string closeTimesearchString, string citysearchString, string zipsearchString, string statesearchString, string foodsearchString, string sortExp, string sortDir)
    {
        System.Diagnostics.Debug.WriteLine("Inside advancedsearch");
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        //string searchString = "GA";
        string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM ProjectSettings.schema.RESTAURANT WHERE UPPER(NAME) LIKE ?  AND UPPER(DESCRIPTION) LIKE " + cuisinesearchString + " AND OPENTIME LIKE " + openTimesearchString + " AND CLOSETIME LIKE " + closeTimesearchString + " AND ZIP LIKE " + zipsearchString + " AND UPPER(CITY) LIKE " + citysearchString + " AND UPPER(STATE) LIKE " + statesearchString;
        if (foodsearchString != "UPPER(FOOD.NAME)")
            cmd += " INTERSECT SELECT DISTINCT ProjectSettings.schema.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ProjectSettings.schema.RESTAURANT.RESTAURANTID FROM ProjectSettings.schema.RESTAURANT INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID INNER JOIN ProjectSettings.schema.FOOD ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID AND UPPER(ProjectSettings.schema.FOOD.NAME) LIKE " + foodsearchString;
        cmd += ")";
        System.Diagnostics.Debug.WriteLine("Query " + cmd);

        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_search = new OleDbCommand(cmd, conn);
        OleDbParameter param = new OleDbParameter();
        select_search.Parameters.Add("@p1", OleDbType.VarChar).Value = namesearchString.Trim('\'');
        OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
        oAdapter.Fill(tbl);
        //RestaurantRepeater.DataSource = oReader;
        //RestaurantRepeater.DataBind();
        //oAdapter.Fill(tbl);
        DataView myDataView = new DataView();
        myDataView = tbl.Tables[0].DefaultView;
        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }

        GridView1.DataSource = myDataView;
        GridView1.DataBind();
        GridView1.PagerSettings.Mode = PagerButtons.Numeric;

        foreach (GridViewRow gr in GridView1.Rows)
        {
            HyperLink hp = new HyperLink();
            hp.Text = gr.Cells[0].Text;
            rName = hp.Text;
            rID = gr.Cells[9].Text;
            rID = "~/Restaurants.aspx?restaurant=" + rID;
            gr.Cells[0].Controls.Add(hp);
        }
        conn.Close();
    }
    protected void Filter_Click(object sender, EventArgs e)
    {
        filter("", "");
    }

    private void filter(string sortExp, string sortDir)
    {
        string searchString = Request.QueryString["SearchString"].ToUpper();
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string cmd = "SELECT NAME,DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM ProjectSettings.schema.RESTAURANT WHERE UPPER(NAME) LIKE ? OR UPPER(CITY) LIKE ? OR UPPER(STATE) LIKE ? OR UPPER(DESCRIPTION) LIKE ? AND PRICE <= ? AND RATING >=? AND SMOKING = ? AND ALCOHOL = ? AND  WIFI = ? AND PRIVATEDINING = ? AND CASHONLY = ? AND PARKING = ? UNION SELECT DISTINCT ProjectSettings.schema.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ProjectSettings.schema.RESTAURANT.RESTAURANTID FROM ProjectSettings.schema.RESTAURANT INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID INNER JOIN ProjectSettings.schema.FOOD ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID AND ProjectSettings.schema.FOOD.FOODID IN (SELECT ProjectSettings.schema.FOOD.FOODID FROM ProjectSettings.schema.FOOD WHERE (UPPER(ProjectSettings.schema.FOOD.NAME) LIKE ?)))";
        System.Diagnostics.Debug.WriteLine("Simpe query: " + cmd);
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        OleDbCommand simp_search = new OleDbCommand(cmd, conn);
        OleDbParameter param = new OleDbParameter();
        string priceValue;
        string ratingValue;
        string smokingButton = "SMOKING"; ;
        string alcoholButton = "ALCOHOL";
        string wifibutton = "WIFI";
        string privatediningbutton = "PRIVATEDINING";
        string cashonlybutton = "CASHONLY";
        string parkingonlybutton = "PARKING";
        if (SmokingButton.SelectedIndex != -1)
            smokingButton = SmokingButton.SelectedItem.Value;
        if (AlcoholButton.SelectedIndex != -1)
            alcoholButton = AlcoholButton.SelectedItem.Value;
        if (Wifibutton.SelectedIndex != -1)
            wifibutton = Wifibutton.SelectedItem.Value;
        if (PrivateDining.SelectedIndex != -1)
            privatediningbutton = PrivateDining.SelectedItem.Value;
        if (CashOnlyButton.SelectedIndex != -1)
            cashonlybutton = CashOnlyButton.SelectedItem.Value;
        if (ParkingButton.SelectedIndex != -1)
            parkingonlybutton = ParkingButton.SelectedItem.Value;
        priceValue = PriceDropDown.SelectedValue;
        if (priceValue == "0")
            priceValue = "5";
        ratingValue = RatingDropDown.SelectedValue;
        if (ratingValue == "0")
            ratingValue = "1";
        simp_search.Parameters.Add("@p1", OleDbType.VarChar).Value = "%" + searchString + "%";
        simp_search.Parameters.Add("@p2", OleDbType.VarChar).Value = "%" + searchString + "%";
        simp_search.Parameters.Add("@p3", OleDbType.VarChar).Value = "%" + searchString + "%";
        simp_search.Parameters.Add("@p4", OleDbType.VarChar).Value = "%" + searchString + "%";
        simp_search.Parameters.Add("@p5", OleDbType.Integer).Value = priceValue;
        simp_search.Parameters.Add("@p6", OleDbType.Integer).Value = ratingValue;
        simp_search.Parameters.Add("@p7", OleDbType.VarChar).Value = smokingButton;
        simp_search.Parameters.Add("@p8", OleDbType.VarChar).Value = alcoholButton;
        simp_search.Parameters.Add("@p9", OleDbType.VarChar).Value = wifibutton;
        simp_search.Parameters.Add("@p10", OleDbType.VarChar).Value = privatediningbutton;
        simp_search.Parameters.Add("@p11", OleDbType.VarChar).Value = cashonlybutton;
        simp_search.Parameters.Add("@p12", OleDbType.VarChar).Value = parkingonlybutton;
        simp_search.Parameters.Add("@p13", OleDbType.VarChar).Value = "%" + searchString + "%";
        conn.Open();
        OleDbDataAdapter oAdapter = new OleDbDataAdapter(simp_search);
        //tbl.Clear();
        oAdapter.Fill(tbl);
        DataView myDataView = new DataView();
        myDataView = tbl.Tables[0].DefaultView;
        if (sortExp != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();
        GridView1.PagerSettings.Mode = PagerButtons.Numeric;

        foreach (GridViewRow gr in GridView1.Rows)
        {
            HyperLink hp = new HyperLink();
            hp.Text = gr.Cells[0].Text;
            rName = hp.Text;
            rID = gr.Cells[9].Text;
            rID = "~/Restaurants.aspx?restaurant=" + rID;
            gr.Cells[0].Controls.Add(hp);
        }
        conn.Close();
    }
    protected void Clear_Click(object sender, EventArgs e)
    {
        PriceDropDown.SelectedIndex = 0;
        RatingDropDown.SelectedIndex = 0;
        PrivateDining.SelectedIndex = -1;
        SmokingButton.SelectedIndex = -1;
        Wifibutton.SelectedIndex = -1;
        AlcoholButton.SelectedIndex = -1;
        CashOnlyButton.SelectedIndex = -1;
        ParkingButton.SelectedIndex = -1;
    }
}
