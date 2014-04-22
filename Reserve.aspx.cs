using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GourmetGuide;
using System.Text;

//TODO: Mandatory fields and email check if not null so that do not send mail
public partial class Account_Default : System.Web.UI.Page
{
    string name, description, opentime, closetime, city, state, address1, address2, zip;
    int nwdays, parkingCount;
    int[] acount = new int[4];
    bool val1;
    List<DropDownList> ls = new List<DropDownList>();
    
    
    OleDbDataReader oReader;
    OleDbDataReader oReader1;
    OleDbDataReader oReader2;
    OleDbDataReader oReader3;
    OleDbDataReader oReader4;
    OleDbDataReader oReader5;
    StringBuilder ConnectionString = new StringBuilder();
    OleDbConnection conn;
    List<RequiredFieldValidator> ddValidatorList; 
    protected void Page_Load(object sender, EventArgs e)
    {
        populateItems();
    }

    protected void datepicker_TextChanged(object sender, EventArgs e)
    {
        string[] date = datepicker.Text.Split('/');
        DateTime dateval = new DateTime(System.Convert.ToInt32(date[2]), System.Convert.ToInt32(date[0]), System.Convert.ToInt32(date[1]));
        int dayofweek = (int)dateval.DayOfWeek;
        opentime = opentime.Replace(":", "");
        int otime = System.Convert.ToInt32(opentime);
        closetime = closetime.Replace(":", "");
        int ctime = System.Convert.ToInt32(closetime);
        DropDownList5.Items.Clear();
        if (dayofweek == nwdays % 7)
        {
            DropDownList5.Items.Add(new ListItem("Restaurant Holiday", "Restaurant Holiday", true));
            DropDownList5.Enabled = false;
            Button1.Enabled = false;
        }
        else
        {
            Button1.Enabled = true;
            DropDownList5.Enabled = true;
            DropDownList5.Items.Add(new ListItem("-select-", "0", true));
            string val;
            for (int i = otime; i < ctime; i += 100)
            {
                val = i.ToString("D4");
                DropDownList5.Items.Add(new ListItem(val.Insert(2, ":"), val, true));
            }
        }

        //System.Diagnostics.Debug.WriteLine(date);
    }

    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //ddValidator1.Enabled = false;
        //ddValidator2.Enabled = false;
        //ddValidator3.Enabled = false;
        //ddValidator4.Enabled = false;
        if (CheckBox1.Checked)
        {
            if (acount[0] != 0)
            {
                DropDownList1.Enabled = true;
                ddValidator1.Enabled = true;
                DropDownList1.Items.Clear();
                DropDownList1.Items.Add(new ListItem("-select-", "0", true));
                for (int i = 1; i <= acount[0]; i++)
                {
                    DropDownList1.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
                }
            }
            //else
            //{
            //    ddValidatorList[0].Text = "Sorry! No 2 seaters available. Uncheck this option and choose an alternate seating ";
            //}
        }
        else
        {
            DropDownList1.Items.Clear();
            DropDownList1.Items.Add(new ListItem("-select-", "0", true));
            ddValidator1.Enabled = false;
            //DropDownList1.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList1.Enabled = false;
        }



    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //ddValidator1.Enabled = false;
        //ddValidator2.Enabled = false;
        //ddValidator3.Enabled = false;
        //ddValidator4.Enabled = false;
        if (CheckBox2.Checked)
        {
            if (acount[1] != 0)
            {
                DropDownList2.Enabled = true;
                ddValidator2.Enabled = true;
                DropDownList2.Items.Clear();
                DropDownList2.Items.Add(new ListItem("-select-", "0", true));
                for (int i = 1; i <= acount[1]; i++)
                {
                    DropDownList2.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
                }
            }
            //else
            //{
            //    ddValidatorList[1].Text = "Sorry! No 4 seaters available. Uncheck this option and choose an alternate seating ";
            //}
        }
        else
        {
            DropDownList2.Items.Clear();
            DropDownList2.Items.Add(new ListItem("-select-", "0", true));
            ddValidator2.Enabled = false;
            //DropDownList2.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList2.Enabled = false;
        }
    }
    protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
    {
        //ddValidator1.Enabled = false;
        //ddValidator2.Enabled = false;
        //ddValidator3.Enabled = false;
        //ddValidator4.Enabled = false;
        if (CheckBox3.Checked)
        {
            if (acount[2] != 0)
            {
                DropDownList3.Enabled = true;
                ddValidator3.Enabled = true;
                DropDownList3.Items.Clear();
                DropDownList3.Items.Add(new ListItem("-select-", "0", true));
                for (int i = 1; i <= acount[2]; i++)
                {
                    DropDownList3.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
                }
            }
            //else
            //{
            //    ddValidatorList[2].Text = "Sorry! No 6 seaters available. Uncheck this option and choose an alternate seating ";
            //}
        }
        else
        {
            DropDownList3.Items.Clear();
            DropDownList3.Items.Add(new ListItem("-select-", "0", true));
            ddValidator3.Enabled = false;
            //DropDownList3.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList3.Enabled = false;
        }
    }
    protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
    {
        //ddValidator1.Enabled = false;
        //ddValidator2.Enabled = false;
        //ddValidator3.Enabled = false;
        //ddValidator4.Enabled = false;
        if (CheckBox4.Checked)
        {
            if (acount[3] != 0)
            {
                DropDownList4.Enabled = true;
                ddValidator4.Enabled = true;
                DropDownList4.Items.Clear();
                DropDownList4.Items.Add(new ListItem("-select-", "0", true));
                for (int i = 1; i <= acount[3]; i++)
                {
                    DropDownList4.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
                }
            }
            //else
            //{
            //    ddValidatorList[3].Text = "Sorry! No 8 seaters available. Uncheck this option and choose an alternate seating ";
            //}
        }
        else
        {
            DropDownList4.Items.Clear();
            DropDownList4.Items.Add(new ListItem("-select-", "0", true));
            ddValidator4.Enabled = false;
            //DropDownList4.Items.Add(new ListItem("-N/A-", "0", true));
            DropDownList4.Enabled = false;
        }
    }

    private void populateItems()
    {
        if (Request.QueryString["restaurant"] != null)
        {
            ddValidatorList = new List<RequiredFieldValidator>();
            List<CheckBox> checkBoxList = new List<CheckBox>();
            ddValidatorList.Clear();
            ddValidatorList.Add(ddValidator1);
            ddValidatorList.Add(ddValidator2);
            ddValidatorList.Add(ddValidator3);
            ddValidatorList.Add(ddValidator4);
            ddValidatorList.Add(rfvParkingValidator);
            checkBoxList.Clear();
            checkBoxList.Add(CheckBox1);
            checkBoxList.Add(CheckBox2);
            checkBoxList.Add(CheckBox3);
            checkBoxList.Add(CheckBox4);
            checkBoxList.Add(CheckParking);
            ls.Clear();
            ls.Add(DropDownList1);
            ls.Add(DropDownList2);
            ls.Add(DropDownList3);
            ls.Add(DropDownList4);
            
            //ddValidator1.Enabled = false;
            //ddValidator2.Enabled = false;
            //ddValidator3.Enabled = false;
            //ddValidator4.Enabled = false;

            val1 = System.Web.HttpContext.Current.User.Identity.IsAuthenticated;
            if (val1)
            {
                EMailID.Enabled = false;
                rfvEmail.Enabled = false;
                EMailDiv.Visible = false;
            }
            else
            {
                EMailID.Visible = true;
                EMailLabel.Visible = true;
            }
            string restaurant_id = Request.QueryString["restaurant"];
            string cmd = "select name,description,opentime,closetime,city,state,address1,address2,zip,nonworkingdays from srajagop.restaurant where restaurantid = " + restaurant_id;
            string cmd1 = "select availabilitycount from srajagop.tables where groupid = 2 and restaurantid = " + restaurant_id;
            string cmd2 = "select availabilitycount from srajagop.tables where groupid = 4 and restaurantid = " + restaurant_id;
            string cmd3 = "select availabilitycount from srajagop.tables where groupid = 6 and restaurantid = " + restaurant_id;
            string cmd4 = "select availabilitycount from srajagop.tables where groupid = 8 and restaurantid = " + restaurant_id;
            string cmd5 = "select availability from srajagop.parking where restaurantid = " + restaurant_id;
            //System.Diagnostics.Debug.WriteLine(cmd5);

            ConnectionString.Append("Provider=").Append(ProjectSettings.dbProvider).Append(";")
                    .Append(" DATA SOURCE=").Append(ProjectSettings.dbHost).Append(":")
                    .Append(ProjectSettings.dbPort).Append("/").Append(ProjectSettings.dbSid).Append(";")
                    .Append("PASSWORD=").Append(ProjectSettings.dbKey).Append(";")
                    .Append("USER ID=").Append(ProjectSettings.dbUser);
            //System.Diagnostics.Debug.WriteLine(ConnectionString.ToString());
            conn = new OleDbConnection(ConnectionString.ToString());
            conn.Open();
            OleDbCommand select_restaurants = new OleDbCommand(cmd, conn);
            OleDbCommand select_tables_2 = new OleDbCommand(cmd1, conn);
            OleDbCommand select_tables_4 = new OleDbCommand(cmd2, conn);
            OleDbCommand select_tables_6 = new OleDbCommand(cmd3, conn);
            OleDbCommand select_tables_8 = new OleDbCommand(cmd4, conn);
            OleDbCommand select_parking = new OleDbCommand(cmd5, conn);
            oReader = select_restaurants.ExecuteReader();
            oReader1 = select_tables_2.ExecuteReader();
            oReader2 = select_tables_4.ExecuteReader();
            oReader3 = select_tables_6.ExecuteReader();
            oReader4 = select_tables_8.ExecuteReader();
            oReader5 = select_parking.ExecuteReader();


            oReader.Read();
            oReader1.Read();
            oReader2.Read();
            oReader3.Read();
            oReader4.Read();
            oReader5.Read();

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

            if (oReader1.HasRows)
                acount[0] = System.Convert.ToInt32(oReader1[0].ToString());
            //System.Diagnostics.Debug.WriteLine(acount[0]);
            if (oReader2.HasRows)
                acount[1] = System.Convert.ToInt32(oReader2[0].ToString());
            if (oReader3.HasRows)
                acount[2] = System.Convert.ToInt32(oReader3[0].ToString());
            if (oReader4.HasRows)
                acount[3] = System.Convert.ToInt32(oReader4[0].ToString());
            if (oReader5.HasRows)
                parkingCount = System.Convert.ToInt32(oReader5[0].ToString());
            r_name.Text = name;
            r_address.Text = address1 + "\n" + address2 + "\n" + city + ", " + state + " - " + zip;
            bool[] en = { CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, CheckBox4.Checked, CheckParking.Checked };
            DropDownList1.Enabled = en[0];
            DropDownList2.Enabled = en[1];
            DropDownList3.Enabled = en[2];
            DropDownList4.Enabled = en[3];
            ParkingDropDownList.Enabled = en[4];

            if (parkingCount == 0)
            {
                Parking.Visible = false;
                CheckParking.Enabled = false;
                rfvParkingValidator.Enabled = false;
                ParkingFull.Visible = true;
            }

            //Disable checkbox when the number of tables is zero
            int disabledCount = 0;
            for (int i = 0; i < acount.Length; i++ )
            {
                if (acount[i] == 0)
                {
                    checkBoxList[i].Enabled = false;
                    ddValidatorList[i].Enabled = false;
                    ls[i].Enabled = false;
                    disabledCount++;
                }

            }

            //If none of checkboxes is checked, generate error
            int uncheckedCount = 0;
            for (int i = 0; i < checkBoxList.Count; i++)
            {
                if (checkBoxList[i].Checked)
                    ddValidatorList[i].Enabled = true;
                else if(checkBoxList[i].Enabled)
                {
                    if(i < 4)
                        uncheckedCount++;
                    ddValidatorList[i].Enabled = false;
                }
            }

            if (uncheckedCount == 4 - disabledCount)
            {
                ddValidatorList[0].Enabled = true;
                ddValidatorList[0].Text = "Please select seats";
            }
            else
                ddValidatorList[0].Enabled = false;
            conn.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string eMail;
        if (val1)
        {
            string cmd = "select EMAILID from srajagop.RegisteredUser where userName='" + System.Web.HttpContext.Current.User.Identity.Name + "'";
            //System.Diagnostics.Debug.WriteLine("UserName is " + System.Web.HttpContext.Current.User.Identity.Name);
            OleDbCommand select_EMail = new OleDbCommand(cmd, conn);
            conn.Open();
            oReader = select_EMail.ExecuteReader();
            oReader.Read();
            eMail = oReader[0].ToString();
            //System.Diagnostics.Debug.WriteLine("EMail is " + eMail);
            oReader.Close();
            conn.Close();
        }
        else
        {
            eMail = EMailID.Text;
        }
        bool[] en = { CheckBox1.Checked, CheckBox2.Checked, CheckBox3.Checked, CheckBox4.Checked };
        int restaurantID = Int32.Parse(Request.QueryString["restaurant"]);
        int groupID = 1;
        string bookDetails = "";
        

        //int[] AvailabilityCount={Int32.Parse(DropDownList1.SelectedValue),Int32.Parse(DropDownList2.SelectedValue),Int32.Parse(DropDownList3.SelectedValue),Int32.Parse(DropDownList4.SelectedValue)};
        int avlCnt = 0;
        int totalpersons = 0;
        bookDetails += "Restaurant: " + name + "\n";
        bookDetails += " Date and Time : " + datepicker.Text + " at " + DropDownList5.SelectedValue + "\n";
        for (int i = 0; i < 4; i++)
        {
            avlCnt = 0;
            if (en[i] == true)
            {
                groupID = (i + 1) * 2;
                //System.Diagnostics.Debug.WriteLine("Value of Dropdown "+ls[i].Text);
                avlCnt = Int32.Parse(ls[i].Text);
            }
            if (avlCnt != 0)
            {
                string insertcmd = "insert into srajagop.tablereserve values(?,?,?,?,?)";
                OleDbTransaction tran = null;
                conn.Open();
                tran = conn.BeginTransaction();
                OleDbCommand insert_Tblreserve = new OleDbCommand(insertcmd, conn, tran);
                insert_Tblreserve.Parameters.Add("?", OleDbType.Integer).Value = restaurantID;
                insert_Tblreserve.Parameters.Add("?", OleDbType.Integer).Value = groupID;
                insert_Tblreserve.Parameters.Add("?", OleDbType.VarChar).Value = eMail;
                insert_Tblreserve.Parameters.Add("?", OleDbType.Integer).Value = avlCnt;
                insert_Tblreserve.Parameters.Add("?", OleDbType.VarChar).Value = DropDownList5.SelectedValue;
                insert_Tblreserve.ExecuteNonQuery();
                tran.Commit();
                string updateAvlCnt = "update srajagop.tables set availabilitycount=? where groupID=? and restaurantID=?";
                tran = null;
                tran = conn.BeginTransaction();
                OleDbCommand update_AvlCnt = new OleDbCommand(updateAvlCnt, conn, tran);
                update_AvlCnt.Parameters.Add("?", OleDbType.Integer).Value = acount[i] - avlCnt;
                update_AvlCnt.Parameters.Add("?", OleDbType.Integer).Value = groupID;
                //System.Diagnostics.Debug.WriteLine(acount[i] - avlCnt);
                update_AvlCnt.Parameters.Add("?", OleDbType.Integer).Value = restaurantID;
                update_AvlCnt.ExecuteNonQuery();
                tran.Commit();

                bookDetails += "Table for " + groupID + " : " + avlCnt + " nos.\n";
                totalpersons += groupID * avlCnt;
                conn.Close();
            }
        }
            bookDetails += "Party size : " + totalpersons + " persons.\n\n";
        if (CheckParking.Checked)
        {
            string insertParkingcmd = "insert into srajagop.parkingreserve values(?,?)";
            OleDbTransaction tran = null;
            conn.Open();
            tran = conn.BeginTransaction();
            OleDbCommand insert_parkingreserve = new OleDbCommand(insertParkingcmd, conn, tran);
            insert_parkingreserve.Parameters.Add("?", OleDbType.Integer).Value = restaurantID;
            insert_parkingreserve.Parameters.Add("?", OleDbType.VarChar).Value = eMail;
            insert_parkingreserve.ExecuteNonQuery();
            tran.Commit();
            string updatePrkCnt = "update srajagop.parking set availability=? where restaurantID=?";
            tran = null;
            tran = conn.BeginTransaction();
            OleDbCommand update_PrkCnt = new OleDbCommand(updatePrkCnt, conn, tran);
            System.Diagnostics.Debug.WriteLine("Selected Value is " + System.Convert.ToInt32(ParkingDropDownList.Text));
            update_PrkCnt.Parameters.Add("?", OleDbType.Integer).Value = parkingCount - System.Convert.ToInt32(ParkingDropDownList.Text);

            //System.Diagnostics.Debug.WriteLine(parkingCount - 1);
            update_PrkCnt.Parameters.Add("?", OleDbType.Integer).Value = restaurantID;
            update_PrkCnt.ExecuteNonQuery();
            tran.Commit();
            bookDetails += "Parking valet reserved for " + ParkingDropDownList.Text;
            if (ParkingDropDownList.Text == "1")
                bookDetails += " slot";
            else
                bookDetails += " slots";
            bookDetails += ".\n\n";
        }

        string subject = "";
        var content = "";
        subject = "GourmetGuide reservation confirmation";
        content = "Hi. \n\nHere are the details for your reservation done a few minutes ago:\n\n" + bookDetails + "\n\n"
                  + "GourmetGuide team.";
        SendMail sm = new SendMail(eMail, null, subject, content);
        sm.send();

        /*string subject;
        var content = "";
        subject = "GourmetGuide reservation confirmation";
        content = "Hi. \n\nHere are the details for your reservation done a few minutes ago:\n\n" + bookDetails + "\n\n"
                  + "GourmetGuide team.";
        SendMail sm = new SendMail(eMail, null, subject, content);
        sm.send();
         */
        //Response.Redirect("/Account/Profile.aspx", true);
        
        if (Preorder.Checked)
            Response.Redirect("~/OrderFood.aspx/?prev=reserve&restaurant=" + Request.QueryString["restaurant"]);
        else
        {
            Response.Redirect("~/HomePage.aspx", true);
        }
    }

    protected void CheckParking_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckParking.Checked)
        {
            ParkingDropDownList.Enabled = true;
            rfvParkingValidator.Enabled = true;
            ParkingDropDownList.Items.Clear();
            ParkingDropDownList.Items.Add(new ListItem("-select-", "0", true));
            for (int i = 1; i <= parkingCount; i++)
            {
                ParkingDropDownList.Items.Add(new ListItem(i.ToString(), i.ToString(), true));
            }
        }
        else
        {
            ParkingDropDownList.Items.Clear();
            ParkingDropDownList.Items.Add(new ListItem("-select-", "0", true));
            rfvParkingValidator.Enabled = false;
            //DropDownList1.Items.Add(new ListItem("-N/A-", "0", true));
            ParkingDropDownList.Enabled = false;
        }

    }
}
