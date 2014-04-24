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
    public string contact;
    public string Lat;
    public string lng;
    public float latr;
    public float longr;
    public float latt;
    public float longt;
    public DataTable dt1 = new DataTable();
    DataTable t;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (Request.QueryString["restaurant"] != null)
        {
            rid = Request.QueryString["restaurant"];
        }        
        if (!IsPostBack)
        {
            if (Request.QueryString["restaurant"] != null)
            {
                StringBuilder ConnectionString = new StringBuilder();
                ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                        .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                        .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                        .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                        .Append("USER ID=").Append(ProjectSettings.dbUser);
                OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
                conn.Open();
                string cmd2 = "select name, Distance, website,Latitude, Longitude from (select name, round(distance,3) as Distance, website, " + ProjectSettings.schema + ".location.Latitude as latitude, " + ProjectSettings.schema + ".location.Longitude as longitude from " + ProjectSettings.schema + ".location,(select restaurantid,touristid as ti,2*6373*ASIN(sqrt((sin((0.017453293*(" + ProjectSettings.schema + ".location.latitude-" + ProjectSettings.schema + ".restaurant.latitude)/2))*(sin(0.017453293*(" + ProjectSettings.schema + ".location.latitude-" + ProjectSettings.schema + ".restaurant.latitude)/2)))+(cos(0.017453293*(" + ProjectSettings.schema + ".location.latitude))*cos(0.017453293*(" + ProjectSettings.schema + ".restaurant.latitude))*(sin(0.017453293*(" + ProjectSettings.schema + ".location.longitude-" + ProjectSettings.schema + ".restaurant.longitude)/2))*(sin(0.017453293*(" + ProjectSettings.schema + ".location.longitude-" + ProjectSettings.schema + ".restaurant.longitude)/2))))) as distance from " + ProjectSettings.schema + ".RESTAURANT, " + ProjectSettings.schema + ".location where restaurantid =" + rid + " order by distance) where touristid = ti and website IS NOT NULL) where rownum<=10";
                System.Diagnostics.Debug.Write("Rid is " + rid);
                System.Diagnostics.Debug.Write("Query is " + cmd2);
                OleDbCommand name3 = new OleDbCommand(cmd2, conn);
                OleDbDataAdapter oAdapter2 = new OleDbDataAdapter(name3);
                oAdapter2.Fill(tbl2);
                DataView myDataView2 = new DataView();
                myDataView2 = tbl2.Tables[0].DefaultView;
                GridView2.DataSource = myDataView2;
                GridView2.DataBind();
                OleDbDataReader oReader = name3.ExecuteReader();
                DataColumn column;
                if (dt1.Columns.Count == 0)
                {
                    column = new DataColumn();
                    column.ColumnName = "Latitude";
                    column.DataType = System.Type.GetType("System.String");
                    dt1.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "Longitude";
                    column.DataType = System.Type.GetType("System.String");
                    dt1.Columns.Add(column);
                    column = new DataColumn();
                    column.ColumnName = "Name";
                    column.DataType = System.Type.GetType("System.String");
                    dt1.Columns.Add(column);
                }
                while (oReader.Read())
                {
                    DataRow row;
                    row = dt1.NewRow();
                    row["Latitude"] = oReader[3].ToString();
                    row["Longitude"] = oReader[4].ToString();
                    row["Name"] = oReader[0].ToString();
                    dt1.Rows.Add(row);
                }
                conn.Close();
            }
            ViewState["sortOrder"] = "";
            bindGridView("", "");            
        }
    }

    public string ConvertDatatoString(DataTable dt)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer= new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, string>> _DictionaryList = new List<Dictionary<string, string>>();
        Dictionary<string, string> dRow;
        foreach(DataRow dr in dt.Rows)
        {
            dRow = new Dictionary<string, string>();
            foreach (DataColumn dCol in dt.Columns)
            {
                string val = dr[dCol].ToString();
                dRow.Add(dCol.ColumnName.ToString(), val);
            }
            _DictionaryList.Add(dRow);
        }
        return serializer.Serialize(_DictionaryList);
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;   
        bindGridView("", "");
             
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
            string cmd = "SELECT NAME, DESCRIPTION, OPENTIME, CLOSETIME,ADDRESS1, ADDRESS2, CITY, STATE, ZIP, RESTAURANTID,NONWORKINGDAYS, PRIVATEDINING,LATITUDE,LONGITUDE,PRICE,RATING,CASHONLY,PARKING,SMOKING,ALCOHOL,WIFI, WEBSITE, TELEPHONE FROM " + ProjectSettings.schema + ".RESTAURANT WHERE RESTAURANTID = " + rid;
            string cmd1 = "SELECT DISTINCT " + ProjectSettings.schema + ".FOOD.NAME, " + ProjectSettings.schema + ".FDPRICECATALOG.PRICE FROM " + ProjectSettings.schema + ".FOOD INNER JOIN " + ProjectSettings.schema + ".FDPRICECATALOG ON " + ProjectSettings.schema + ".FOOD.FOODID = " + ProjectSettings.schema + ".FDPRICECATALOG.FOODID INNER JOIN " + ProjectSettings.schema + ".RESTAURANT ON " + ProjectSettings.schema + ".RESTAURANT.RESTAURANTID = " + ProjectSettings.schema + ".FDPRICECATALOG.RESTAURANTID WHERE " + ProjectSettings.schema + ".RESTAURANT.RESTAURANTID =" + rid;
            //string cmd2 = "select name, latitude, longitude from " + ProjectSettings.schema + ".location, (select touristid as ti from " + ProjectSettings.schema + ".nearby where restaurantid ="+rid+") where touristid=ti";
            OleDbConnection conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand name1 = new OleDbCommand(cmd, conn);
            OleDbDataReader oReader = name1.ExecuteReader();
            oReader.Read();
            rName = oReader[0].ToString();
            cuisine = oReader[1].ToString();
           
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
            Lat = oReader[12].ToString();
            lng = oReader[13].ToString();

            webAddr.NavigateUrl = oReader[21].ToString();
            webAddr.Text = oReader[21].ToString();

            if (webAddr.Text == "")
                addressStr = "N/A";
            else
                addressStr = "";
            
            contact = oReader[22].ToString();
            price.Text = "";
            if ( System.Convert.ToInt32(oReader[14]) == 0)
            price.Text = "N/A";
            for(int i=0;i<System.Convert.ToInt32(oReader[14]);i++)
            price.Text += " $";

            rating.Text = "";
            if (System.Convert.ToInt32(oReader[15]) == 0)
            rating.Text = "N/A";
            for (int i = 0; i < System.Convert.ToInt32(oReader[15]); i++)
                rating.Text += " ★";
            if(oReader[16].ToString().ToUpper().Equals("Y"))
                cashonly.Text = " Yes";
            else
                cashonly.Text = " No";

            if (oReader[17].ToString().ToUpper().Equals("Y"))
                parking.Text = " Yes";
            else
                parking.Text = " No";

            if (oReader[18].ToString().ToUpper().Equals("Y"))
                smoking.Text = " Yes";
            else
                smoking.Text = " No";

            if (oReader[19].ToString().ToUpper().Equals("Y"))
                alcohol.Text = " Yes";
            else
                alcohol.Text = " No";

            if (oReader[20].ToString().ToUpper().Equals("Y"))
                wifi.Text = " Yes";
            else
                wifi.Text = " No";

            if (oReader[12].ToString().ToUpper().Equals("Y"))
                privatedining.Text = " Yes";
            else
                privatedining.Text = " No";

            string[] days = { "Working All Days", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
            holiday.Text = days[System.Convert.ToInt32(oReader[10].ToString())];
            
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
        Response.Redirect("~/OrderFood?prev=rest&restaurant=" + rid);
    }  
}