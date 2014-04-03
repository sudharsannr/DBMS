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
    DataSet tbl = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }  
        if(Request.QueryString["SearchString"]!=null)
        {
                string searchString = Request.QueryString["SearchString"].ToUpper();
                StringBuilder ConnectionString = new StringBuilder();
                ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                        .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                        .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                        .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                        .Append("USER ID=").Append(ProjectSettings.dbUser);
                string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE '%" + searchString + "%' OR UPPER(CITY) LIKE '%" + searchString + "%' OR UPPER(STATE) LIKE '%" + searchString + "%' UNION SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND UPPER(SRAJAGOP.FOOD.NAME) LIKE '%" + searchString + "%')";
                OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
                conn.Open();
                OleDbCommand select_search = new OleDbCommand(cmd, conn);
                OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
                oAdapter.Fill(tbl);
                //RestaurantRepeater.DataSource = oReader;
                //RestaurantRepeater.DataBind();
                GridView1.DataSource = tbl.Tables[0];
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
     }

    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Search.aspx?SearchString=" + SearchTextBox.Text);
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.DataSource = tbl.Tables[0];
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
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
        if (Request.QueryString["SearchString"] != null)
        {
            string searchString = Request.QueryString["SearchString"].ToUpper();
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM SRAJAGOP.RESTAURANT WHERE UPPER(NAME) LIKE '%" + searchString + "%' OR UPPER(CITY) LIKE '%" + searchString + "%' OR UPPER(STATE) LIKE '%" + searchString + "%' UNION SELECT DISTINCT SRAJAGOP.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, SRAJAGOP.RESTAURANT.RESTAURANTID FROM SRAJAGOP.RESTAURANT INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID INNER JOIN SRAJAGOP.FOOD ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID AND UPPER(SRAJAGOP.FOOD.NAME) LIKE '%" + searchString + "%')";
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

            GridView1.DataSource = myDataView;
            GridView1.DataBind();
            conn.Close();
        }
    }
}