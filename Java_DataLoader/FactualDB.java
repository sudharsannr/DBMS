package com.factual.demo;

import static com.factual.driver.FactualTest.factual;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.math.BigDecimal;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Date;
import java.util.List;
import java.util.Properties;
import java.util.Random;
import java.util.Map.Entry;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.json.JSONArray;
import org.json.JSONObject;

import com.factual.driver.Factual;
import com.factual.driver.Query;

public class FactualDB
{
	static StringBuffer connectionString = new StringBuffer();
	static int tID = 1;
	static Connection connection = null;
	static Statement stmt = null;
	static boolean dbOperations = false;
	static Properties myprop = new Properties();
	static String dbusername = null;
	static String dbpassword = null;
	
	static
	  {
		try
		{
			myprop.load(new FileInputStream("DBProperties.property"));
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
		  connectionString.append("jdbc:oracle:thin:@")
		  	.append(myprop.getProperty("host"))
		  	.append(":")
		  	.append(myprop.getProperty("port"))
		  	.append(":")
		  	.append(myprop.getProperty("sid"));
		  dbusername = myprop.getProperty("username");
		  dbpassword = myprop.getProperty("password");
	  }

	public static void main(String[] args) 
	{
	    Factual factual = factual();
	    String response = "";
	    String[] city = new String[]{"New York", "Los Angeles", "Chicago", "Houston", "Philadelphia", "Phoenix", "San Diego", "Dallas", "San Antonio", "Detroit", "San Jose", "INpolis", "San Francisco", "Jacksonville", "Columbus", "Austin", "Memphis", "Baltimore", "Milwaukee", "Boston", "Charlotte", "El Paso", "Washington", 
	    		"Nashville-Davidson", "Seattle", "Fort Worth", "Denver", "Portland", "Oklahoma city", "Las Vegas", "Tucson", "New Orleans", "Long Beach", "Cleveland", "Albuquerque", "Fresno", "Kansas City", "Sacramento", "Virginia Beach", "Mesa", "Atlanta", "Oakland", "Omaha", "Tulsa", "Honolulu CDP", "Minneapolis", "Miami", "Colorado Springs", "Wichita", "Arlington", "Santa Ana", "St. Louis", "Anaheim", "Pittsburgh", "Cincinnati", "Tampa", "Toledo", "Raleigh", "Buffalo", "Aurora", "St. Paul", "Corpus Christi", "Newark", "Riverside", "Anchorage", "Lexington-Fayette", "Stockton", "Bakersfield", "Louisville", "St. Petersburg", "Jersey", "Birmingham", "Norfolk", "Plano", "Lincoln", "Glendale", "Greensboro", "Hialeah", "Baton Rouge", "Garland", "Rochester", "Scottsdale", "Madison", "Akron", "Fort Wayne", "Fremont", "Chesapeake", "Henderson", "Lubbock", "Modesto", "Chandler", "Montgomery", "Glendale", "Shreveport", "Des Moines", "Augusta-Richmond", "Tacoma", "Richmond", "Yonkers", "Grand Rapids", "Spokane", "Irving", "Durham", "Mobile", "Chula Vista", "Huntington Beach", "Orlando", "San Bernardino", "Laredo", "Reno", "Arlington CDP", "Boise", "Winston-Salem", "Columbus", "Little Rock", "Salt Lake", "Jackson", "Newport News", "Oxnard", "Amarillo", "Providence", "Worcester", "Knoxville", "Garden Grove", "Oceanside", "Ontario", "Dayton", "Huntsville", "Irvine", "Santa Clarita", "Tempe", "Overland Park", "Fort Lauderdale", "Aurora", "Chattanooga", "Tallahassee", "Pomona", "Santa Rosa", "Springfield", "Rockford", "Springfield", "Moreno Valley", "Paterson", "Brownsville", "Vancouver", "Salinas", "KS", "Pembroke Pines", "Hampton", "Syracuse", "Pasadena", "Lakewood", "Rancho Cucamonga", "Fontana", "Hollywood", "Hayward", "Torrance", "Salem", "Eugene", "Bridgeport", "Pasadena", "Corona", "Warren", "Escondido", "North Las Vegas", "Naperville", "Grand Prairie", "Gilbert town", "Orange", "Alexandria", "Sioux Falls", "Sunnyvale", "Fullerton", "Mesquite", "Savannah", "Sterling Heights", "Coral Springs", "Concord", "Fort Collins", "Lancaster", "Hartford", "Palmdale", "Fayetteville", "New Haven", "Elizabeth", "Peoria", "Thousand Oaks", "Cedar Rapids", "Topeka", "Flint", "El Monte", "Stamford", "Vallejo", "Evansville", "Lansing", "Joliet", "Columbia", "Simi Valley", "Waco", "Abilene", "Ann Arbor", "Carrollton", "Inglewood", "McAllen", "Independence", "Cape Coral", "Bellevue", "Beaumont", "Peoria", "Springfield", "Lafayette", "West Valley", "Costa Mesa", "Downey", "Manchester", "Clearwater", "Waterbury", "West Covina", "South Bend", "Allentown", "Norwalk", "Clarksville", "Provo", "Lowell", "Athens-Clarke County", "Berkeley", "Ventura", "Westminster", "Pueblo", "Wichita Falls", "Burbank", "Richmond", "Arvada", "Erie", "Fairfield", "Daly", "Santa Clara", "Cambridge", "Green Bay", "Olathe", "Gary", "Livonia", "Antioch", "Portsmouth", "Centennial", "Charleston", "South Gate", "Port St. Lucie", "Cary town", "Dearborn", "Norman", "Davenport", "Everett", "Richardson", "Visalia", "Rialto", "Elgin", "Mission Viejo", "Macon", "Midland", "Compton", "El Cajon", "Brockton", "Gainesville", "Gresham", "Boulder", "New Bedford", "Roanoke", "Albany", "Thornton", "Vacaville", "Carson", "Killeen", "Fall River", "Kenosha", "Billings", "San Mateo", "Roseville", "Vista", "Lawton", "Waukegan", "Fargo", "Odessa", "Wilmington", "High Point", "Rochester", "Miramar", "Denton", "Lynn", "Miami Beach", "Westminster", "Santa Barbara", "Sandy", "Quincy", "Citrus Heights", "Sunrise", "Nashua", "Alhambra", "San Angelo", "Pompano Beach"};
	    String[] state = new String[]{"NY", "CA", "IL", "TX", "PA", "AZ", "CA", "TX", "TX", "MI", "CA", "IN", "CA", "FL", "OH", "TX", "TN", "MD", "WI", "MA", "NC", "TX", "DC", "TN", "WA", "TX", "CO", "OR", "OK", "NV", "AZ", "LA", "CA", "OH", "NM", "CA", "MO", "CA", "VT", "AZ", "GA", "CA", "NE", "OK", "HI", "MN", "FL", "CO", "KS", "TX", "CA", "MO", "CA", "PA", "OH", "FL", "OH", "NC", "NY", "CO", "MN", "TX", "NJ", "CA", "AK", "KY", "CA", "CA", "KY", "FL", "NJ", "AL", "VT", "TX", "NE", "AZ", "NC", "FL", "LA", "TX", "NY", "AZ", "WI", "OH", "IN", "CA", "VT", "NV", "TX", "CA", "AZ", "AL", "CA", "LA", "IA", "GA", "WA", "VT", "NY", "MI", "WA", "TX", "NC", "AL", "CA", "CA", "FL", "CA", "TX", "NV", "VT", "ID", "NC", "GA", "AR", "UT", "MS", "VT", "CA", "TX", "RI", "MA", "TN", "CA", "CA", "CA", "OH", "AL", "CA", "CA", "AZ", "KS", "FL", "IL", "TN", "FL", "CA", "CA", "MA", "IL", "MO", "CA", "NJ", "TX", "WA", "CA", "KS", "FL", "VT", "NY", "TX", "CO", "CA", "CA", "FL", "CA", "CA", "OR", "OR", "CT", "CA", "CA", "MI", "CA", "NV", "IL", "TX", "AZ", "CA", "VT", "SD", "CA", "CA", "TX", "GA", "MI", "FL", "CA", "CO", "CA", "CT", "CA", "NC", "CT", "NJ", "AZ", "CA", "IA", "KS", "MI", "CA", "CT", "CA", "IN", "MI", "IL", "SC", "CA", "TX", "TX", "MI", "TX", "CA", "TX", "MO", "FL", "WA", "TX", "IL", "IL", "LA", "UT", "CA", "CA", "NH", "FL", "CT", "CA", "IN", "PA", "CA", "TN", "UT", "MA", "GA", "CA", "CA", "CO", "CO", "TX", "CA", "CA", "CO", "PA", "CA", "CA", "CA", "MA", "WI", "KS", "IN", "MI", "CA", "VT", "CO", "SC", "CA", "FL", "NC", "MI", "OK", "IA", "WA", "TX", "CA", "CA", "IL", "CA", "GA", "TX", "CA", "CA", "MA", "FL", "OR", "CO", "MA", "VT", "NY", "CO", "CA", "CA", "TX", "MA", "WI", "MO", "CA", "CA", "CA", "OK", "IL", "ND", "TX", "NC", "NC", "MN", "FL", "TX", "MA", "FL", "CA", "CA", "UT", "MA", "CA", "FL", "NH", "CA", "TX", "FL"};
	    /*String[] city = new String[]{"New York"};
	    String[] state = new String[]{"NY"};*/
	    dbOperations = true;
	    boolean restaurant, location;
	    restaurant = false;
	    location = true;
		try
		{
			Class.forName("oracle.jdbc.driver.OracleDriver");
			if (dbOperations)
				connection = DriverManager.getConnection(
						connectionString.toString(),
						dbusername,
						dbpassword);
			if(restaurant)
			{
				int start = Arrays.asList(city).indexOf("Livonia");
				for(int i = start + 1; i < city.length; i++)
				{
					System.out.println(state[i] + " " + city[i]);
					Query q1 = new Query();
					for (int offset = 0; offset < 100; offset += 20)
					{
						q1.and(q1.field("country").isEqual("us"), 
								q1.field("region").isEqual(state[i]),
								q1.field("locality").isEqual(city[i]));
						q1.offset(offset);
						response = factual.fetch("restaurants-us", q1).toString();
						parseRestaurantResponse(response);
					}
				}
			}
			
			if(location)
			{
				//int start = Arrays.asList(city).indexOf("Cedar Rapids");
				int start = 0;
				for(int i = start; i < city.length; i++)
				{
					
					System.out.println(state[i] + " " + city[i]);
					Query q2 = new Query();
					for (int offset = 100; offset < 140; offset += 20)
					{
						q2.and(q2.field("country").isEqual("us"), 
								q2.field("region").isEqual(state[i]),
								q2.field("locality").isEqual(city[i]),
								q2.field("category_ids").greaterThanOrEqual(107),
								q2.field("category_ids").lessThanOrEqual(122)
								);
						q2.offset(offset);
						response = factual.fetch("places", q2).toString();
						parseLocationResponse(response);
					}
				}
			}
		}
		
	    catch (Exception e)
		{
			e.printStackTrace();
		}
		
		finally
		{
			if(dbOperations)
				try
				{
					connection.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				}
		}
	  }
	
	
	private static void parseLocationResponse(String response)
	{
		/*
		 *  TOURISTID	NUMBER(10,0)		No	
			NAME		VARCHAR2(50 BYTE)	No	
			CATEGORY	VARCHAR2(100 BYTE)	No	
			PRIORITY	NUMBER(1,0)			Yes		1
			LATITUDE	NUMBER(12,8)		No	
			LONGITUDE	NUMBER(12,8)		No	
			OPENTIME	VARCHAR2(20 BYTE)	Yes	
			CLOSETIME	VARCHAR2(20 BYTE)	Yes	
			TELEPHONE	VARCHAR2(20 BYTE)	Yes	
			WEBSITE		VARCHAR2(200 BYTE)	Yes
			STATE		VARCHAR2(2 BYTE)	Yes	
			CITY		VARCHAR2(20 BYTE)	Yes
			ZIP			NUMBER(5, 0)		Yes
			NONWORKINGDAYS	NUMBER(1, 0)		Yes				
		*/
		try
		{
			JSONObject obj = new JSONObject(response);
			JSONArray data = obj.getJSONObject("response").getJSONArray("data");
			JSONObject place;
			String name, category, priority, latitude, longitude,  opentime, closetime, telephone, website, state, city, zip, nonworkingdays; 
			String[] openTime = new String[]{"07:00","07:30","08:00","08:30","00:00"};
			String[] closeTime = new String[]{"20:00","20:30","21:00","21:30","22:00","22:30", "23:00","23:30", "24:00"};
			Random r = new Random();
			int prioritySet[] = {1, 2, 3, 4, 5};
			ArrayList<String> days = new ArrayList<String>();
		    days.add("Mon");
		    days.add("Tue");
		    days.add("Wed");
		    days.add("Thu");
		    days.add("Fri");
		    days.add("Sat");
		    days.add("Sun");
		    boolean[] dayCheck = new boolean[7];
			
			for(int i = 0; i < data.length(); i++)
			{
				category = "";
				city = "";
				zip = "";
				latitude="0.000000";
				longitude="0.000000";
				place = data.getJSONObject(i);
				name = place.getString("name");
				JSONArray catLabels = place.getJSONArray("category_labels");
				for(int j = 0; j < catLabels.length(); j++)
				{
					JSONArray catLabels2 = catLabels.getJSONArray(j);
					for(int k = 0; k < catLabels2.length(); k++)
					{
						if(k != catLabels2.length() - 1)
							category += catLabels2.get(k) + ", ";
						else
							category += catLabels2.get(k);
					}
				}
				priority = String.valueOf(prioritySet[r.nextInt(5)]);
				state = place.getString("region").toUpperCase();
				if(place.has("locality"))
					city = place.getString("locality");
				if(place.has("postcode"))
					zip = place.getString("postcode");
				if(place.has("latitude"))
					latitude = place.getString("latitude");
				if(place.has("longitude"))
					longitude = place.getString("longitude");
				if(place.has("hours_display"))
				{
					String t = place.getString("hours_display");
					String t1 = "", t2 = "";
					SimpleDateFormat displayFormat = new SimpleDateFormat("HH:mm");
					SimpleDateFormat parseFormat = new SimpleDateFormat("hh:mm a");
					Date date;
					Pattern p0 = Pattern.compile("(\\d+:\\d{2}\\s\\w{2})\\-(\\d+\\:\\d+\\s\\w{2})");
					Matcher m0 = p0.matcher(t);
					if(m0.find())
					{
						t1 = m0.group(1);
						t2 = m0.group(2);
						date = parseFormat.parse(t1);
						opentime = displayFormat.format(date);
						date = parseFormat.parse(t2);
						closetime = displayFormat.format(date);
					}
					else
					{
						opentime = openTime[r.nextInt(5)];
						closetime = closeTime[r.nextInt(9)];
					}
					nonworkingdays = String.valueOf(r.nextInt(8));
					if(!t.contains("Daily"))
					{
						Arrays.fill(dayCheck, false);
						Pattern p = Pattern.compile("\\w{3}");
						Matcher m = p.matcher(t);
						while (m.find())
						{
							if (days.contains(t.subSequence(m.start(), m.end())))
								dayCheck[days.indexOf(t.subSequence(m.start(), m.end()))] = true;
						}
						
						int i1 = 0;
						for(i1 =  0; i1 < 7; i1++)
						{
							if(!dayCheck[i1])
								break;
						}
						if(i1 == 6)
							nonworkingdays = String.valueOf(r.nextInt(8));
					}
				}
				else
				{
					opentime = openTime[r.nextInt(5)];
					closetime = closeTime[r.nextInt(9)];
					nonworkingdays = String.valueOf(r.nextInt(8));
				}
				
				website = "";
				telephone = "";
				
				if(place.has("website"))
					website = place.getString("website");
				if(place.has("tel"))
					telephone = place.getString("tel");
				
				//DB Operations
				if(dbOperations)
				{
					int rid = 0;
					String query = "SELECT LOCATION_TOURIST_ID.NEXTVAL FROM DUAL";
					stmt = connection.createStatement();
					ResultSet rs = stmt.executeQuery(query);
					while (rs.next()) {
						rid = rs.getInt(1);
						System.out.println(rid);
					}
					rs.close();
					stmt.close();
					/*
					 *  TOURISTID	NUMBER(10,0)		No	
						NAME		VARCHAR2(50 BYTE)	No	
						CATEGORY	VARCHAR2(100 BYTE)	No	
						PRIORITY	NUMBER(1,0)			Yes		1
						LATITUDE	NUMBER(12,8)		No	
						LONGITUDE	NUMBER(12,8)		No	
						OPENTIME	VARCHAR2(20 BYTE)	Yes	
						CLOSETIME	VARCHAR2(20 BYTE)	Yes	
						TELEPHONE	VARCHAR2(20 BYTE)	Yes	
						WEBSITE		VARCHAR2(200 BYTE)	Yes
						STATE		VARCHAR2(2 BYTE)	Yes	
						CITY		VARCHAR2(20 BYTE)	Yes
						ZIP			NUMBER(5, 0)		Yes
						NONWORKINGDAYS	NUMBER(1, 0)		Yes				
					*/
					
					String insertTableSQL = "INSERT INTO LOCATION VALUES "
							+ "(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
					PreparedStatement preparedStatement = connection.prepareStatement(insertTableSQL);
					preparedStatement.setInt(1, rid);
					preparedStatement.setString(2, name.substring(0, name.length() > 50 ? 49 : name.length()));
					preparedStatement.setString(3, category);
					preparedStatement.setInt(4, Integer.parseInt(priority));
					preparedStatement.setBigDecimal(5, new BigDecimal(latitude));
					preparedStatement.setBigDecimal(6, new BigDecimal(longitude));
					preparedStatement.setString(7, opentime);
					preparedStatement.setString(8, closetime);
					preparedStatement.setString(9, telephone);
					preparedStatement.setString(10, website);
					preparedStatement.setString(11, state);
					preparedStatement.setString(12, city);
					if(zip.length() == 0)
						zip = "0";
					preparedStatement.setInt(13, Integer.parseInt(zip));
					preparedStatement.setString(14, nonworkingdays);
					/*System.out.println(rid + "\t " + name + "\t" + category + "\t" + priority + "\t" 
							 + latitude + "\t" + longitude + "\t" + opentime + "\t" + closetime + "\t" + telephone + "\t" + website + "\t" + 
							state + "\t" + city);*/
					preparedStatement.executeUpdate();
					preparedStatement.close();
				}
			}
			
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}
	}

	private static void parseRestaurantResponse(String response)
	{
		/*
		 *  RESTAURANTID			NAME
			DESCRIPTION				RESERVEOPTIONS
			ADDRESS1				ADDRESS2
			CITY					STATE
			ZIP						LATITUDE
			LONGITUDE				OPENTIME
			CLOSETIME				NONWORKINGDAYS
			WEBSITE					PRICE
			RATING					CASHONLY
			PARKING					SMOKING
			ALCOHOL					WIFI
			PRIVATEDINING			TELEPHONE
			*/
		try
		{
			JSONObject obj = new JSONObject(response);
			JSONArray data = obj.getJSONObject("response").getJSONArray("data");
			JSONObject restaurant;
			String name, description, address1, address2, city, 
					state, opentime, closetime, website, cashonly, parking, smoking, alcohol, wifi, privatedining, telephone;
			String zip, latitude, longitude, price, rating, reserveoptions, nonworkingdays;
			String[] openTime = new String[]{"07:00","07:30","08:00","08:30","00:00"};
			String[] closeTime = new String[]{"20:00","20:30","21:00","21:30","22:00","22:30", "23:00","23:30", "24:00"};
			Random r = new Random();
			ArrayList<String> days = new ArrayList<String>();
		    days.add("Mon");
		    days.add("Tue");
		    days.add("Wed");
		    days.add("Thu");
		    days.add("Fri");
		    days.add("Sat");
		    days.add("Sun");
		    boolean[] dayCheck = new boolean[7];
			
			for(int i = 0; i < data.length(); i++)
			{
				description = "";
				address1 = "";
				address2 = "";
				city = "";
				zip = "";
				latitude="0.000000";
				longitude="0.000000";
				restaurant = data.getJSONObject(i);
				name = restaurant.getString("name");
				JSONArray catLabels = restaurant.getJSONArray("category_labels");
				for(int j = 0; j < catLabels.length(); j++)
				{
					JSONArray catLabels2 = catLabels.getJSONArray(j);
					for(int k = 0; k < catLabels2.length(); k++)
					{
						if(k != catLabels2.length() - 1)
							description += catLabels2.get(k) + ", ";
						else
							description += catLabels2.get(k);
					}
				}
				if(restaurant.has("address"))
					address1 = restaurant.getString("address");
				if(restaurant.has("extended_address"))
					address2 = restaurant.getString("extended_address");
				if(restaurant.has("locality"))
					city = restaurant.getString("locality");
				state = restaurant.getString("region").toUpperCase();
				if(restaurant.has("postcode"))
					zip = restaurant.getString("postcode");
				if(restaurant.has("latitude"))
					latitude = restaurant.getString("latitude");
				if(restaurant.has("longitude"))
					longitude = restaurant.getString("longitude");
				reserveoptions = String.valueOf(r.nextInt(8));
				if(restaurant.has("hours_display"))
				{
					String t = restaurant.getString("hours_display");
					String t1 = "", t2 = "";
					SimpleDateFormat displayFormat = new SimpleDateFormat("HH:mm");
					SimpleDateFormat parseFormat = new SimpleDateFormat("hh:mm a");
					Date date;
					Pattern p0 = Pattern.compile("(\\d+:\\d{2}\\s\\w{2})\\-(\\d+\\:\\d+\\s\\w{2})");
					Matcher m0 = p0.matcher(t);
					if(m0.find())
					{
						t1 = m0.group(1);
						t2 = m0.group(2);
						date = parseFormat.parse(t1);
						opentime = displayFormat.format(date);
						date = parseFormat.parse(t2);
						closetime = displayFormat.format(date);
					}
					else
					{
						opentime = openTime[r.nextInt(5)];
						closetime = closeTime[r.nextInt(9)];
					}
					nonworkingdays = String.valueOf(r.nextInt(8));
					if(!t.contains("Daily"))
					{
						Arrays.fill(dayCheck, false);
						Pattern p = Pattern.compile("\\w{3}");
						Matcher m = p.matcher(t);
						while (m.find())
						{
							if (days.contains(t.subSequence(m.start(), m.end())))
								dayCheck[days.indexOf(t.subSequence(m.start(), m.end()))] = true;
						}
						
						int i1 = 0;
						for(i1 =  0; i1 < 7; i1++)
						{
							if(!dayCheck[i1])
								break;
						}
						if(i1 == 6)
							nonworkingdays = String.valueOf(r.nextInt(8));
					}
				}
				else
				{
					opentime = openTime[r.nextInt(5)];
					closetime = closeTime[r.nextInt(9)];
					nonworkingdays = String.valueOf(r.nextInt(8));
				}
				
				website = "";
				price = "0";
				rating = "0";
				cashonly = "N";
				parking = "N";
				smoking = "N";
				alcohol = "N";
				wifi = "N";
				privatedining = "N";
				telephone = "";
				
				if(restaurant.has("website"))
					website = restaurant.getString("website");
				if(restaurant.has("price"))
					price = restaurant.getString("price");
				if(restaurant.has("rating"))
					rating = restaurant.getString("rating");
				if(restaurant.has("payment_cashonly"))
				{
					if(restaurant.getString("payment_cashonly").equalsIgnoreCase("true"))
						cashonly = "Y";
				}
				if(restaurant.has("parking"))
				{
					if(restaurant.getString("parking").equals("true"))
						parking = "Y";
				}
				if(restaurant.has("smoking"))
				{
					if(restaurant.getString("smoking").equals("true"))
						smoking = "Y";
				}
				if(restaurant.has("alcohol"))
				{
					if(restaurant.getString("alcohol").equals("true"))
						alcohol = "Y";
				}
				if(restaurant.has("wifi"))
				{
					if(restaurant.getString("wifi").equals("true"))
						wifi = "Y";
				}
				if(restaurant.has("room_private"))
				{
					if(restaurant.getString("room_private").equals("true"))
						privatedining = "Y";
				}
				if(restaurant.has("tel"))
					telephone = restaurant.getString("tel");
				
				//DB Operations
				if(dbOperations)
				{
					int rid = 0;
					String query = "SELECT RES_SEQUENCE_ID.NEXTVAL FROM DUAL";
					stmt = connection.createStatement();
					ResultSet rs = stmt.executeQuery(query);
					while (rs.next()) {
						rid = rs.getInt(1);
						System.out.println(rid);
					}
					rs.close();
					stmt.close();
					String insertTableSQL = "INSERT INTO RESTAURANT VALUES "
							+ "(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
					PreparedStatement preparedStatement = connection.prepareStatement(insertTableSQL);
					preparedStatement.setInt(1, rid);
					preparedStatement.setString(2, name);
					preparedStatement.setString(3, description);
					preparedStatement.setInt(4, Integer.parseInt(reserveoptions));
					preparedStatement.setString(5, address1);
					preparedStatement.setString(6, address2);
					preparedStatement.setString(7, city);
					preparedStatement.setString(8, state);
					if(zip.length() == 0)
						zip = "0";
					preparedStatement.setInt(9, Integer.parseInt(zip));
					preparedStatement.setBigDecimal(10, new BigDecimal(latitude));
					preparedStatement.setBigDecimal(11, new BigDecimal(longitude));
					preparedStatement.setString(12, opentime);
					preparedStatement.setString(13, closetime);
					preparedStatement.setString(14, nonworkingdays);
					preparedStatement.setString(15, website);
					preparedStatement.setInt(16, (int)Math.floor(Double.parseDouble(price)));
					preparedStatement.setInt(17, (int)Math.floor(Double.parseDouble(rating)));
					preparedStatement.setString(18, cashonly);
					preparedStatement.setString(19, parking);
					preparedStatement.setString(20, smoking);
					preparedStatement.setString(21, alcohol);
					preparedStatement.setString(22, wifi);
					preparedStatement.setString(23, privatedining);
					preparedStatement.setString(24, telephone);
					/*System.out.println(rid + "\t " + name + "\t" + description + "\t" + reserveoptions + "\t" + address1 + "\t" + address2
							 + "\t" + city + "\t" + state + "\t" + zip + "\t" + latitude + "\t" + longitude + "\t" + opentime + "\t" + closetime
							 + "\t" + nonworkingdays + "\t" + website + "\t" + price + "\t" + rating + "\t" + cashonly + "\t" + parking + "\t" + smoking
							  + "\t" + alcohol + "\t" + wifi + "\t" + privatedining + "\t" + telephone);*/
					preparedStatement.executeUpdate();
					preparedStatement.close();
				}
			}
			
		}
		catch(Exception e)
		{
			e.printStackTrace();
		}

	}

}
