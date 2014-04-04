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

public partial class AdvancedSearch : System.Web.UI.Page
{
    public string str = "";
    public string rID = "";
    public string rName = "";
    DataSet tbl = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string namesearchString;
        string cuisinesearchString;
        string openTimesearchString;
        string closeTimesearchString;
        string citysearchString;
        string zipsearchString;
        string statesearchString;
        string foodsearchString;
        Page_Load(sender, e);
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }

        if (NameSearch.Text.Trim().Length == 0)
        {
            namesearchString = "UPPER(NAME)";
        }
        else {
            namesearchString = "\'%"+NameSearch.Text.ToString().ToUpper()+"%\'";
        }


        if (CuisineSearch.Text.Trim().Length == 0)
        {
            cuisinesearchString = "UPPER(DESCRIPTION)";
        }
        
        else
        {
            cuisinesearchString = "\'%" + CuisineSearch.Text.ToString().ToUpper() + "%\'";
        }


        if (OpenTimeSearch.Text.Trim().Length == 0)
        {
            openTimesearchString = "UPPER(OPENTIME)";
        }
        else
        {
            openTimesearchString = "\'%" + OpenTimeSearch.Text.ToString().ToUpper() + "%\'";
        }
        
        if (CloseTimeSearch.Text.Trim().Length == 0)
        {
            closeTimesearchString = "UPPER(CLOSETIME)";
        }
        else
        {
            closeTimesearchString = "\'%" + CloseTimeSearch.Text.ToString().ToUpper() + "%\'";
        }
        
        if (CitrySearch.Text.Trim().Length == 0)
        {
            citysearchString = "UPPER(CITY)";
        }
        else
        {
            citysearchString = "\'%" + CitrySearch.Text.ToString().ToUpper() + "%\'";
        }
        
        if (ZipSearch.Text.Trim().Length == 0)
        {
            zipsearchString = "ZIP";
        }
        else
        {
            zipsearchString = "\'%" + ZipSearch.Text.ToString().ToUpper() + "%\'";
        }
        
        if (StateSearch.Text.Trim().Length == 0)
        {
            statesearchString = "UPPER(STATE)";
        }
        else
        {
            statesearchString = "\'%" + StateSearch.Text.ToString().ToUpper() + "%\'";
        }
        
        if (FoodSearch.Text.Trim().Length == 0)
        {
            foodsearchString = "UPPER(FOOD.NAME)";
        }
        else
        {
            foodsearchString = "\'%" + FoodSearch.Text.ToString().ToUpper() + "%\'";
        }
        
        System.Diagnostics.Debug.Write("Here Restaurant " + namesearchString+"    " +OpenTimeSearch.ToString().Length);
        System.Diagnostics.Debug.Write("Here Cuisine " + CuisineSearch.ToString().Length);
        System.Diagnostics.Debug.Write("Here Open time " + openTimesearchString);
        System.Diagnostics.Debug.Write("Here Close time " + closeTimesearchString);
        System.Diagnostics.Debug.Write("Here City " + citysearchString);
        System.Diagnostics.Debug.Write("Here Zip " + zipsearchString);
        System.Diagnostics.Debug.Write("Here State " + statesearchString);
        System.Diagnostics.Debug.Write("Here Food " + foodsearchString);
        
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string searchString = "GA";
        string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE " + namesearchString + " AND UPPER(DESCRIPTION) LIKE " + cuisinesearchString + " AND OPENTIME LIKE " + openTimesearchString + " AND CLOSETIME LIKE " + closeTimesearchString + " AND ZIP = " +zipsearchString+ " AND UPPER(CITY) LIKE " + citysearchString + " AND UPPER(STATE) LIKE " + statesearchString + " INTERSECT SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND UPPER(SRAJAGOP.FOOD.NAME) LIKE " + foodsearchString + ")";
        System.Diagnostics.Debug.Write("Here Qery " + cmd);
        
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
        conn.Open();
        OleDbCommand select_search = new OleDbCommand(cmd, conn);
        OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
        oAdapter.Fill(tbl);
        //RestaurantRepeater.DataSource = oReader;
        //RestaurantRepeater.DataBind();
        GridView2.DataSource = tbl.Tables[0];
        GridView2.DataBind();
        GridView2.PagerSettings.Mode = PagerButtons.Numeric;

        foreach (GridViewRow gr in GridView2.Rows)
        {
            HyperLink hp = new HyperLink();
            hp.Text = gr.Cells[0].Text;
            rName = hp.Text;
            rID = gr.Cells[9].Text;
            rID = "~/Restaurants.aspx?restaurant=" + rID;
            gr.Cells[0].Controls.Add(hp);
        }
        //System.Diagnostics.Debug.WriteLine("Query is " + cmd);
        ////GridView1.DataSource = oReader;
        ////GridView1.DataBind();
        //str += @"<table>";
        //while (oReader.Read())
        //{
        //    str += @"<tr><td><a href='Restaurants.aspx?restaurant=" + oReader[9].ToString() + "'>" + oReader[0].ToString() + @"</a></td><td>" + oReader[1].ToString() + @"</td> <td>" + oReader[2].ToString() + @"</td> <td>" + oReader[3].ToString() + @"</td> <td>" + oReader[4].ToString() + @"</td> <td>" + oReader[5].ToString() + @"</td> <td>" + oReader[6].ToString() + @"</td> <td>" + oReader[7].ToString() + @"</td> <td>" + oReader[8].ToString() + @"</tr>";
        //}
        //System.Diagnostics.Debug.WriteLine(str);
        //str += "</table>";
        conn.Close();
    }
    
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
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.DataSource = tbl.Tables[0];
        GridView2.PageIndex = e.NewPageIndex;
        GridView2.DataBind();
    }
    protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
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
           // string searchString = Request.QueryString["SearchString"].ToUpper();
        string namesearchString;
        string cuisinesearchString;
        string openTimesearchString;
        string closeTimesearchString;
        string citysearchString;
        string zipsearchString;
        string statesearchString;
        string foodsearchString;
        //TODO: Check correctness
        /*if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }*/

        if (NameSearch.Text.Trim().Length == 0)
        {
            namesearchString = "UPPER(NAME)";
        }
        else
        {
            namesearchString = "\'%" + NameSearch.Text.ToString().ToUpper() + "%\'";
        }


        if (CuisineSearch.Text.Trim().Length == 0)
        {
            cuisinesearchString = "UPPER(DESCRIPTION)";
        }

        else
        {
            cuisinesearchString = "\'%" + CuisineSearch.Text.ToString().ToUpper() + "%\'";
        }


        if (OpenTimeSearch.Text.Trim().Length == 0)
        {
            openTimesearchString = "UPPER(OPENTIME)";
        }
        else
        {
            openTimesearchString = "\'%" + OpenTimeSearch.Text.ToString().ToUpper() + "%\'";
        }

        if (CloseTimeSearch.Text.Trim().Length == 0)
        {
            closeTimesearchString = "UPPER(CLOSETIME)";
        }
        else
        {
            closeTimesearchString = "\'%" + CloseTimeSearch.Text.ToString().ToUpper() + "%\'";
        }

        if (CitrySearch.Text.Trim().Length == 0)
        {
            citysearchString = "UPPER(CITY)";
        }
        else
        {
            citysearchString = "\'%" + CitrySearch.Text.ToString().ToUpper() + "%\'";
        }

        if (ZipSearch.Text.Trim().Length == 0)
        {
            zipsearchString = "ZIP";
        }
        else
        {
            zipsearchString = "\'%" + ZipSearch.Text.ToString().ToUpper() + "%\'";
        }

        if (StateSearch.Text.Trim().Length == 0)
        {
            statesearchString = "UPPER(STATE)";
        }
        else
        {
            statesearchString = "\'%" + StateSearch.Text.ToString().ToUpper() + "%\'";
        }

        if (FoodSearch.Text.Trim().Length == 0)
        {
            foodsearchString = "UPPER(FOOD.NAME)";
        }
        else
        {
            foodsearchString = "\'%" + FoodSearch.Text.ToString().ToUpper() + "%\'";
        }

        System.Diagnostics.Debug.Write("Here we " + namesearchString);
        StringBuilder ConnectionString = new StringBuilder();
        ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                .Append("USER ID=").Append(ProjectSettings.dbUser);
        string searchString = "GA";
        string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE " + namesearchString + " AND UPPER(DESCRIPTION) LIKE " + cuisinesearchString + " AND OPENTIME LIKE " + openTimesearchString + " AND CLOSETIME LIKE " + closeTimesearchString + " AND ZIP = " + zipsearchString + " AND UPPER(CITY) LIKE " + citysearchString + " AND UPPER(STATE) LIKE " + statesearchString + " INTERSECT SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND UPPER(SRAJAGOP.FOOD.NAME) LIKE " + foodsearchString + ")";
        OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand select_search = new OleDbCommand(cmd, conn);
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
            oAdapter.Fill(tbl);
            DataView myDataView = new DataView();
            myDataView = tbl.Tables[0].DefaultView;
            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }

            GridView2.DataSource = myDataView;
            GridView2.DataBind();
            conn.Close();
      
    }
}