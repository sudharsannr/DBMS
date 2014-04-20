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
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            userName = System.Web.HttpContext.Current.User.Identity.Name;
        srchHiddenStr = srchHidden.Value;
        //srchType = searchType.Value;
        if (Session["searchType"] != null)
        {
            if(Request.QueryString["SearchString"] == null && Request.QueryString["AdvancedSearch"] == null)
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
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE ? OR UPPER(CITY) LIKE ? OR UPPER(STATE) LIKE ? OR UPPER(DESCRIPTION) LIKE ? UNION SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND SRAJAGOP.FOOD.FOODID IN (SELECT SRAJAGOP.FOOD.FOODID FROM SRAJAGOP.FOOD WHERE (UPPER(SRAJAGOP.FOOD.NAME) LIKE ?)))";
            System.Diagnostics.Debug.WriteLine("Simpe query: " + cmd);
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            OleDbParameter param = new OleDbParameter();
            OleDbCommand simp_search = new OleDbCommand(cmd, conn);
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
        string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE " + namesearchString + " AND UPPER(DESCRIPTION) LIKE " + cuisinesearchString + " AND OPENTIME LIKE " + openTimesearchString + " AND CLOSETIME LIKE " + closeTimesearchString + " AND ZIP LIKE " + zipsearchString + " AND UPPER(CITY) LIKE " + citysearchString + " AND UPPER(STATE) LIKE " + statesearchString;
        if (foodsearchString != "UPPER(FOOD.NAME)")
        cmd += " INTERSECT SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND UPPER(SRAJAGOP.FOOD.NAME) LIKE " + foodsearchString;
        cmd += ")";
        System.Diagnostics.Debug.WriteLine("Query " + cmd);

        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_search = new OleDbCommand(cmd, conn);
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
}
