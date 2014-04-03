using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Default : System.Web.UI.Page
{
    string name, description, opentime, closetime, city, state, address1, address2, zip;
    int nwdays;
    int[] acount = new int[4];
    protected void Page_Load(object sender, EventArgs e)
    {
        string restaurant_id = Request.QueryString["restaurant"];
        string ConnectionString = "Provider=OraOLEDB.Oracle; DATA SOURCE=oracle.cise.ufl.edu:1521/ORCL;PASSWORD=Rsm3171990;USER ID=sr8";
        string cmd = "select name,description,opentime,closetime,city,state,address1,address2,zip,nonworkingdays from srajagop.restaurant where restaurantid = " + restaurant_id;
        string cmd1 = "select availabilitycount from srajagop.tables where groupid = 2 and restaurantid = " + restaurant_id;
        string cmd2 = "select availabilitycount from srajagop.tables where groupid = 4 and restaurantid = " + restaurant_id;
        string cmd3 = "select availabilitycount from srajagop.tables where groupid = 6 and restaurantid = " + restaurant_id;
        string cmd4 = "select availabilitycount from srajagop.tables where groupid = 8 and restaurantid = " + restaurant_id;
        System.Diagnostics.Debug.WriteLine(cmd);
        System.Diagnostics.Debug.WriteLine(ConnectionString);
        OleDbConnection conn = new OleDbConnection(ConnectionString);
        OleDbTransaction tran = null;
        conn.Open();
        OleDbParameter param = new OleDbParameter();
        OleDbCommand select_restaurants = new OleDbCommand(cmd, conn);
        OleDbCommand select_tables_2 = new OleDbCommand(cmd1, conn);
        OleDbCommand select_tables_4 = new OleDbCommand(cmd2, conn);
        OleDbCommand select_tables_6 = new OleDbCommand(cmd3, conn);
        OleDbCommand select_tables_8 = new OleDbCommand(cmd4, conn);
        OleDbDataReader oReader = select_restaurants.ExecuteReader();
        OleDbDataReader oReader1 = select_tables_2.ExecuteReader();
        OleDbDataReader oReader2 = select_tables_4.ExecuteReader();
        OleDbDataReader oReader3 = select_tables_6.ExecuteReader();
        OleDbDataReader oReader4 = select_tables_8.ExecuteReader();
        
        
        oReader.Read();
        oReader1.Read();
        oReader2.Read();
        oReader3.Read();
        oReader4.Read();

        name = oReader[0].ToString();
        description = oReader[1].ToString();
        opentime = oReader[2].ToString();
        closetime = oReader[3].ToString();
        city = oReader[4].ToString();
        state = oReader[5].ToString();
        address1 = oReader[6].ToString();
        address2 = oReader[7].ToString();
        zip = oReader[8].ToString();
        nwdays = System.Convert.ToInt32(oReader[9].ToString());

        acount[0] = System.Convert.ToInt32(oReader1[0].ToString());
        System.Diagnostics.Debug.WriteLine(acount[0]);
        acount[1] = System.Convert.ToInt32(oReader2[0].ToString());
        acount[2] = System.Convert.ToInt32(oReader3[0].ToString());
        acount[3] = System.Convert.ToInt32(oReader4[0].ToString());

        r_name.Text = name;
        r_address.Text = address1 + "\n" + address2 + "\n" + city + ", " + state + " - " + zip;
        bool[] en = { CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, CheckBox4.Checked };
        DropDownList1.Enabled = en[0];
        DropDownList2.Enabled = en[1];
        DropDownList3.Enabled = en[2];
        DropDownList4.Enabled = en[3];
        
            
        conn.Close();
    }
    
    private void select_restaurant(int restaurant_id)
    {
          
        
           
    }

    protected void datepicker_TextChanged(object sender, EventArgs e)
    {
        string[] date = datepicker.Text.Split('/');
        DateTime dateval = new DateTime(System.Convert.ToInt32(date[2]),System.Convert.ToInt32(date[0]),System.Convert.ToInt32(date[1]));
        int dayofweek = (int)dateval.DayOfWeek;
        opentime = opentime.Replace(":","");
        int otime = System.Convert.ToInt32(opentime);
        closetime = closetime.Replace(":","");
        int ctime = System.Convert.ToInt32(closetime);
        DropDownList5.Items.Clear();
        if (dayofweek == nwdays)
        {
            DropDownList5.Items.Add(new ListItem("Restaurant Holiday", "Restaurant Holiday", true));
            DropDownList5.Enabled = false;
        }
        else
        {
            DropDownList5.Enabled = true;
            DropDownList5.Items.Add(new ListItem("-select-", "-select-", true));
            string val;
            for (int i = otime; i < ctime; i += 100)
            {
                val = i.ToString("D4");
                DropDownList5.Items.Add(new ListItem(val.Insert(2,":"), val, true));
            }
        }

        //System.Diagnostics.Debug.WriteLine(date);
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox1.Checked) {
        DropDownList1.Enabled = true;
        DropDownList1.Items.Clear();
        DropDownList1.Items.Add(new ListItem("-select-", "0", true));
        for (int i = 1; i <= acount[0]; i++)
        {
            DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
        } 
        }
        else {
            DropDownList1.Items.Clear();
            //DropDownList1.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList1.Enabled = false;
        }



    }    
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked)
        {
        DropDownList2.Enabled = true;
        DropDownList2.Items.Clear();
        DropDownList2.Items.Add(new ListItem("-select-", "0", true));
        for (int i = 1; i <= acount[1]; i++)
        {
            DropDownList2.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
        }
        }
        else {
            DropDownList2.Items.Clear();
            //DropDownList2.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList2.Enabled = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox3.Checked)
        {
        DropDownList3.Enabled = true;
        DropDownList3.Items.Clear();
        DropDownList3.Items.Add(new ListItem("-select-", "0", true));
        for (int i = 1; i <= acount[2]; i++)
        {
            DropDownList3.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
        }
        }
        else {
            DropDownList3.Items.Clear();
            //DropDownList3.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList3.Enabled = false;
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox4.Checked)
        {
        DropDownList4.Enabled = true;
        DropDownList4.Items.Clear();
        DropDownList4.Items.Add(new ListItem("-select-", "0", true));
        for (int i = 1; i <= acount[3]; i++)
        {
            DropDownList4.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
        }
        }
        else {
            DropDownList4.Items.Clear();
            //DropDownList4.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList4.Enabled = false;
        }
    }
}