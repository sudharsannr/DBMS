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
    DataSet tbl = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            userName = System.Web.HttpContext.Current.User.Identity.Name;
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        if (userName != "")
            insert_userSearch();
        Response.Redirect("Search.aspx?SearchString=" + SearchTextBox.Text);
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
            insert_search.Parameters.Add("?", OleDbType.VarChar).Value = Search.Text.ToUpper();
            insert_search.ExecuteNonQuery();
            tran.Commit();

        }
        finally
        {
            conn.Close();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("AdvancedSearch.aspx");
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
        bindGridView("", "");
        GridView1.PageIndex = e.NewPageIndex;        
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
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM (SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID FROM ProjectSettings.schema.RESTAURANT WHERE UPPER(NAME) LIKE '%" + searchString + "%' OR UPPER(CITY) LIKE '%" + searchString + "%' OR UPPER(STATE) LIKE '%" + searchString + "%' OR UPPER(DESCRIPTION) LIKE '%" + searchString + "%' UNION SELECT DISTINCT ProjectSettings.schema.RESTAURANT.NAME, DESCRIPTION, OPENTIME, CLOSETIME, ADDRESS1, ADDRESS2, CITY, STATE, ZIP, ProjectSettings.schema.RESTAURANT.RESTAURANTID FROM ProjectSettings.schema.RESTAURANT INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID INNER JOIN ProjectSettings.schema.FOOD ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID AND ProjectSettings.schema.FOOD.FOODID IN (SELECT ProjectSettings.schema.FOOD.FOODID FROM ProjectSettings.schema.FOOD WHERE (UPPER(ProjectSettings.schema.FOOD.NAME) LIKE '% " + searchString + " %') OR (UPPER(ProjectSettings.schema.FOOD.NAME) LIKE '% " + searchString + "') OR (UPPER(ProjectSettings.schema.FOOD.NAME) LIKE '" + searchString + " %') OR (UPPER(ProjectSettings.schema.FOOD.NAME) = '" + searchString + "')))";
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand select_search = new OleDbCommand(cmd, conn);
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
            tbl.Clear();
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
}