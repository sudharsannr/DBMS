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
using System.Drawing;
using System.Collections;

public partial class Account_Restaurants : System.Web.UI.Page
{
    DataSet tbl = new DataSet();
    DataSet tbl2 = new DataSet();
    public string str;
    public string cuisine;
    public string rName;
    public string tName;
    public string rid;
    public string tour;
    public string dist;
    public string addressStr;
    public string workingHours;
    public String location;
    public string gUrl = "http://maps.googleapis.com/maps/api/js?key=" + ProjectSettings.googleMapsKey + "&sensor=false";
    public string holiday;
    public string Lat;
    public string lng;
    public float latr;
    public float longr;
    public float latt;
    public float longt;
    DataTable t;
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
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindGridView("", "");
        GridView1.PageIndex = e.NewPageIndex;        
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView1.AutoGenerateColumns = false;
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
            rid = Request.QueryString["restaurant"];
            System.Diagnostics.Debug.WriteLine("Helkko " + rid);
            StringBuilder ConnectionString = new StringBuilder();
            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID,NONWORKINGDAYS, PRIVATEDINING,LATITUDE,LONGITUDE FROM SRAJAGOP.RESTAURANT WHERE RESTAURANTID = " + rid;
            string cmd1 = "SELECT DISTINCT SRAJAGOP.FOOD.NAME, SRAJAGOP.FDPRICECATALOG.PRICE FROM SRAJAGOP.FOOD INNER JOIN SRAJAGOP.FDPRICECATALOG ON SRAJAGOP.FOOD.FOODID = SRAJAGOP.FDPRICECATALOG.FOODID INNER JOIN SRAJAGOP.RESTAURANT ON SRAJAGOP.RESTAURANT.RESTAURANTID = SRAJAGOP.FDPRICECATALOG.RESTAURANTID WHERE SRAJAGOP.RESTAURANT.RESTAURANTID =" + rid;
            //string cmd2 = "select name, latitude, longitude from srajagop.location, (select touristid as ti from srajagop.nearby where restaurantid ="+rid+") where touristid=ti";
            string cmd2 = "select name, round(distance,3) as Distance from srajagop.location,(select restaurantid,touristid as ti,2*6373*ASIN(sqrt((sin((0.017453293*(srajagop.location.latitude-srajagop.restaurant.latitude)/2))*(sin(0.017453293*(srajagop.location.latitude-srajagop.restaurant.latitude)/2)))+(cos(0.017453293*(srajagop.location.latitude))*cos(0.017453293*(srajagop.restaurant.latitude))*(sin(0.017453293*(srajagop.location.longitude-srajagop.restaurant.longitude)/2))*(sin(0.017453293*(srajagop.location.longitude-srajagop.restaurant.longitude)/2))))) as distance from SRAJAGOP.RESTAURANT, srajagop.location where restaurantid ="+ rid +" order by distance) where touristid = ti and rownum<=10";
            System.Diagnostics.Debug.WriteLine("Quer y is " + cmd2);
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand name1 = new OleDbCommand(cmd, conn);
            OleDbDataReader oReader = name1.ExecuteReader();
            OleDbCommand name2 = new OleDbCommand(cmd2, conn);
            OleDbDataReader oReader2 = name2.ExecuteReader();
            
            oReader.Read();
            oReader2.Read();
            rName = oReader[0].ToString();
            cuisine = oReader[1].ToString();
            tName = oReader2[0].ToString();
         //   latr = Lat.to
            cuisine = cuisine.Replace("restaurant", "");
            location = "";
            for (int i = 4; i < 9; i++)
            {
                if (oReader[i].ToString().Trim().Length > 0)
                {
                    if (i != 8)
                        location += oReader[i].ToString() + ", ";
                    else
                        location += oReader[i].ToString();
                }
            }
            workingHours = oReader[2].ToString() + " - " + oReader[3].ToString();
            string[] days = { "Working All Days", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            holiday = days[System.Convert.ToInt32(oReader[10].ToString())];
            Lat = oReader[12].ToString();
            lng = oReader[13].ToString();
            OleDbCommand select_search = new OleDbCommand(cmd1, conn);
            OleDbDataAdapter oAdapter = new OleDbDataAdapter(select_search);
            oAdapter.Fill(tbl);
            if (tbl.Tables[0].Rows.Count == 0)
            {
                NoResult.Visible = true;
                TableReserve.Enabled = false;
                FoodReserve.Enabled = false;
            }
            else
            {
                NoResult.Visible = false;
                TableReserve.Enabled = true;
                FoodReserve.Enabled = true;

            }

            if (String.Equals(oReader[11].ToString(), "Y", StringComparison.OrdinalIgnoreCase))
                TableReserve.Enabled = true;
            else
                TableReserve.Enabled = false;

            DataView myDataView = new DataView();
            myDataView = tbl.Tables[0].DefaultView;
            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }
            GridView1.DataSource = myDataView;
            GridView1.DataBind(); 
            
            GridView1.PagerSettings.Mode = PagerButtons.Numeric;
            OleDbCommand name3 = new OleDbCommand(cmd2, conn);
            OleDbDataAdapter oAdapter2 = new OleDbDataAdapter(name3);
            oAdapter2.Fill(tbl2);
            DataView myDataView2 = new DataView();
            myDataView2 = tbl2.Tables[0].DefaultView;
            GridView2.DataSource = myDataView2;
            GridView2.DataBind(); 
            
                                
            conn.Close();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "gMaps()", true);
        }
    }    
    protected void TableReserve_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Reserve?restaurant=" + rid);
    }
    protected void FoodReserve_Click(object sender, EventArgs e)
    {
        string str = "~/OrderFood?restaurant=" + rid;
        Response.Redirect("~/OrderFood?restaurant=" + rid);
    }  
}