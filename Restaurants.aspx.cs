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

public partial class Account_Restaurants : System.Web.UI.Page
{
    DataSet tbl = new DataSet();
    public string str;
    public string cuisine;
    public string rName;
    public string rid;
    public string addressStr;
    public string workingHours;
    public String location;
    public string gUrl = "http://maps.googleapis.com/maps/api/js?key=" + ProjectSettings.googleMapsKey + "&sensor=false";
    public string holiday;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            bindGridView("", "");
        }
        if (Request.QueryString["restaurant"] != null)
        {
            rid = Request.QueryString["restaurant"];
            System.Diagnostics.Debug.WriteLine("Helkko " + rid);
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID,NONWORKINGDAYS FROM ProjectSettings.schema.RESTAURANT WHERE RESTAURANTID = " + rid;
            string cmd1 = "SELECT DISTINCT ProjectSettings.schema.FOOD.NAME, ProjectSettings.schema.FDPRICECATALOG.PRICE FROM ProjectSettings.schema.FOOD INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID INNER JOIN ProjectSettings.schema.RESTAURANT ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID WHERE ProjectSettings.schema.RESTAURANT.RESTAURANTID =" + rid;
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand name = new OleDbCommand(cmd, conn);
            OleDbDataReader oReader = name.ExecuteReader();
            oReader.Read();
            rName = oReader[0].ToString();
            cuisine = oReader[1].ToString();
            cuisine = cuisine.Replace("restaurant", "");
            location = oReader[4].ToString() + ", "
                              + oReader[5].ToString() + ", "
                              + oReader[6].ToString() + ", "
                              + oReader[7].ToString() + ", "
                              + oReader[8].ToString();
            workingHours = oReader[2].ToString() + " - " + oReader[3].ToString();
            string[] days = { "Working All Days", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            holiday = days[System.Convert.ToInt32(oReader[10].ToString())];
            
            OleDbCommand select_search = new OleDbCommand(cmd1, conn);
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
            oAdapter.Fill(tbl);
            if (tbl.Tables[0].Rows.Count == 0)
            {
                NoResult.Visible = true;
            }
            else
                NoResult.Visible = false;
            //RestaurantRepeater.DataSource = oReader;
            //RestaurantRepeater.DataBind();
            GridView1.DataSource = tbl.Tables[0];
            GridView1.DataBind();
            GridView1.PagerSettings.Mode = PagerButtons.Numeric;
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "gMaps('" + location + "')", true);
        }
    }
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
        if (Request.QueryString["restaurant"] != null)
        {
            rid = Request.QueryString["restaurant"].ToUpper();
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd1 = "SELECT DISTINCT ProjectSettings.schema.FOOD.NAME, ProjectSettings.schema.FDPRICECATALOG.PRICE FROM ProjectSettings.schema.FOOD INNER JOIN ProjectSettings.schema.FDPRICECATALOG ON ProjectSettings.schema.FOOD.FOODID = ProjectSettings.schema.FDPRICECATALOG.FOODID INNER JOIN ProjectSettings.schema.RESTAURANT ON ProjectSettings.schema.RESTAURANT.RESTAURANTID = ProjectSettings.schema.FDPRICECATALOG.RESTAURANTID WHERE ProjectSettings.schema.RESTAURANT.RESTAURANTID =" + rid;
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand select_search = new OleDbCommand(cmd1, conn);
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
    protected void TableReserve_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reserve?restaurant=" + rid);
    }
}