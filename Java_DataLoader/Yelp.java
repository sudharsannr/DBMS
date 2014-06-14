/*
 Example code based on code from Nicholas Smith at http://imnes.blogspot.com/2011/01/how-to-use-yelp-v2-from-java-including.html
 For a more complete example (how to integrate with GSON, etc) see the blog post above.
 */

import java.io.BufferedReader;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStreamReader;
import java.math.BigDecimal;
import java.net.HttpURLConnection;
import java.net.URL;
import java.net.URLEncoder;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.PreparedStatement;
import java.sql.ResultSet;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Map.Entry;
import java.util.Properties;
import java.util.Random;

import org.apache.commons.collections4.keyvalue.MultiKey;
import org.apache.commons.collections4.map.MultiKeyMap;
import org.json.JSONArray;
import org.json.JSONObject;
import org.scribe.builder.ServiceBuilder;
import org.scribe.model.OAuthRequest;
import org.scribe.model.Response;
import org.scribe.model.Token;
import org.scribe.model.Verb;
import org.scribe.oauth.OAuthService;

/**
 * Example for accessing the Yelp API.
 */
public class Yelp {

  OAuthService service;
  Token accessToken;
  //Update tokens here from Yelp developers site, Manage API access.
  static Properties myprop = new Properties();
  static StringBuffer connectionString = new StringBuffer();
  static ArrayList<String> googleAPIKey = new ArrayList<String>();
  static int tID = 1;  
  static Connection connection = null;
  static Statement stmt = null;
  static Yelp yelp;
  static boolean dbOperations = false;
  static int gKeyidx = 2;

  static
  {
	try
	{
		myprop.load(new FileInputStream("DBProperties.property"));
	}
	catch (FileNotFoundException e)
	{
		e.printStackTrace();
	}
	catch (IOException e)
	{
		e.printStackTrace();
	}
	  connectionString.append("jdbc:oracle:thin:@")
	  	.append(myprop.getProperty("host"))
	  	.append(":")
	  	.append(myprop.getProperty("port"))
	  	.append(":")
	  	.append(myprop.getProperty("sid"));
	  String[] test = myprop.getProperty("googleAPIKeys").split("~~");
	  for(String t : test)
		  googleAPIKey.add(t);
	  tID = 1;  
	  yelp = new Yelp(myprop.getProperty("consumerKey"), 
		  			  myprop.getProperty("consumerSecret"), 
	  			  	  myprop.getProperty("token"), 
	  			  	  myprop.getProperty("tokenSecret"));
  }

  /**
   * Setup the Yelp API OAuth credentials.
   *
   * OAuth credentials are available from the developer site, under Manage API access (version 2 API).
   *
   * @param consumerKey Consumer key
   * @param consumerSecret Consumer secret
   * @param token Token
   * @param tokenSecret Token secret
   */
  public Yelp(String consumerKey, String consumerSecret, String token, String tokenSecret) {
    this.service = new ServiceBuilder().provider(YelpApi2.class).apiKey(consumerKey).apiSecret(consumerSecret).build();
    this.accessToken = new Token(token, tokenSecret);
  }

  /**
   * Search with term and location.
   *
   * @param term Search term
   * @param latitude Latitude
   * @param longitude Longitude
   * @return JSON string response
   */
  public String search(String term, double latitude, double longitude) {
    OAuthRequest request = new OAuthRequest(Verb.GET, "http://api.yelp.com/v2/search");
    request.addQuerystringParameter("term", term);
    request.addQuerystringParameter("ll", latitude + "," + longitude);
    this.service.signRequest(this.accessToken, request);
    Response response = request.send();
    return response.getBody();
  }
  
  public String searchterm(YelpRequestParameters yelpParams) {
	    OAuthRequest request = new OAuthRequest(Verb.GET, "http://api.yelp.com/v2/search");
	    request.addQuerystringParameter("term", yelpParams.getTerm());
	    request.addQuerystringParameter("sort", yelpParams.getSort());
	    request.addQuerystringParameter("limit", yelpParams.getLimit());
	    request.addQuerystringParameter("location", yelpParams.getLocation());
	    request.addQuerystringParameter("offset", yelpParams.getOffset());
	    this.service.signRequest(this.accessToken, request);
	    Response response = request.send();
	    return response.getBody();
	  }

  // CLI
  public static void main(String[] args) {
    int offset = 0;
    String response = "";
	//TODO: Read states 
    String[] stateCd = new String[]{"AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA", "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD", "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ", "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"};
    dbOperations = true;
    try
	{
		Class.forName("oracle.jdbc.driver.OracleDriver");
		if(dbOperations)
			connection = DriverManager.getConnection(connectionString.toString(), myprop.getProperty("username"),myprop.getProperty("password"));	
	}
	catch (Exception e)
	{
		e.printStackTrace();
	}
    YelpRequestParameters yelpParams = new YelpRequestParameters();
    for(String sCd : stateCd)
    {
    	System.out.println(sCd);
    	for(offset = 0; offset < 200; offset += 20)
    	{
    		yelpParams.setTerm("restaurants");
    		yelpParams.setOffset(Integer.toString(offset));
    		yelpParams.setLimit(Integer.toString(20));
    		yelpParams.setSort(Integer.toString(0));
    		yelpParams.setLocation(sCd);
    		response = yelp.searchterm(yelpParams);
    		parseResponse(offset, sCd, response);
    	}
    }
  }


private static void parseResponse(int offset, String stCd, String response)
{
	TouristLocation tt = null;
	try
	{
		JSONObject obj = new JSONObject(response);
		MultiKey mk;
		MultiKeyMap<MultiKey, TouristLocation> mp = new MultiKeyMap();
		//MultiKey<String, String, String> mk = new MultiKey<String, String, String>();
		TouristLocation tl = null;
		JSONArray businesses = obj.getJSONArray("businesses");
		String name, description = "", address1, address2, city, state, opentime, closetime;
		String[] openTime = new String[]{"07:00","07:30","08:00","08:30","00:00"};
		String[] closeTime = new String[]{"20:00","20:30","21:00","21:30","22:00","22:30", "23:00","23:30", "24:00"};
		int reserveoptions, zip, nonworkingdays;
		BigDecimal latitude, longitude, tlatitude, tlongitude;
		JSONObject temp=new JSONObject();
		Random r = new Random();
		JSONArray ja = new JSONArray();
		
		JSONObject obj2 = null;
		JSONArray touristBusinesses = null;
		PreparedStatement preparedStatement;
	    String insertTableSQL = "";
	    
		for(int i = 0; i < businesses.length(); i++)
		{
			temp = businesses.getJSONObject(i);
			name = temp.getString("name");
			ja = temp.getJSONArray("categories");
			description = "";
			for(int j = 0; j < ja.length(); j++)
			{
				if(j != ja.length()-1)
					description += ja.getJSONArray(j).get(0) + ",";
				else
					description += ja.getJSONArray(j).get(0) + " restaurant";
			}
			reserveoptions = r.nextInt(8);
			JSONObject temp2=new JSONObject();
			temp2 = temp.getJSONObject("location");
			address1=temp2.getJSONArray("display_address").getString(0);
			address2=temp2.getJSONArray("display_address").getString(1);
			city=temp2.getString("city");
			state=temp2.getString("state_code");
			if(temp2.has("postal_code"))
				zip=Integer.parseInt(temp2.getString("postal_code"));
			else
				zip=0;
			opentime = openTime[r.nextInt(5)];
			closetime = closeTime[r.nextInt(9)];
			nonworkingdays = r.nextInt(8);
			//latitude, longitude
			ArrayList<BigDecimal> latLng = null;
			//TODO
			latLng = requestGeoLocation2(address1, address2, city, state);
			
			latitude = BigDecimal.ZERO;
			longitude = BigDecimal.ZERO;
			if(null != latLng)
			{
				latitude = latLng.get(0);
				longitude = latLng.get(1);
			}
			
			int rid = 0;
			//DB Operations
			if(dbOperations)
			{
				String query = "SELECT RES_SEQUENCE_ID.NEXTVAL FROM DUAL";
				stmt = connection.createStatement();
				ResultSet rs = stmt.executeQuery(query);
				while (rs.next()) {
					rid = rs.getInt(1);
					System.out.println(rid);
				}
				rs.close();
				stmt.close();
				insertTableSQL = "INSERT INTO RESTAURANT VALUES "
						+ "(?,?,?,?,?,?,?,?,?,?,?,?,?,?)";
				preparedStatement = connection.prepareStatement(insertTableSQL);
				latitude = BigDecimal.ZERO;
				longitude = BigDecimal.ZERO;
				if(null != latLng)
				{
					latitude = latLng.get(0);
					longitude = latLng.get(1);
				}
				
				preparedStatement.setInt(1, rid);
				preparedStatement.setString(2, name);
				preparedStatement.setString(3, description);
				preparedStatement.setInt(4, reserveoptions);
				preparedStatement.setString(5, address1);
				preparedStatement.setString(6, address2);
				preparedStatement.setString(7, city);
				preparedStatement.setString(8, state);
				preparedStatement.setInt(9, zip);
				if(null != latLng)
				{
					preparedStatement.setBigDecimal(10, latitude);
					preparedStatement.setBigDecimal(11, longitude);
				}
				else
				{
					System.out.println("Setting zero RESTAURANT");
					preparedStatement.setBigDecimal(10, BigDecimal.ZERO);
					preparedStatement.setBigDecimal(11, BigDecimal.ZERO);
				}
				preparedStatement.setString(12, opentime);
				preparedStatement.setString(13, closetime);
				preparedStatement.setInt(14, nonworkingdays);
				preparedStatement.executeUpdate();
				preparedStatement.close();
			}
		    
		    //Get tourist locations
			String touristResponse = "";
			YelpRequestParameters yelpParams = new YelpRequestParameters();
			yelpParams.setOffset(Integer.toString(offset));
			yelpParams.setLimit(Integer.toString(10));
			yelpParams.setSort(Integer.toString(0));
			yelpParams.setLocation(city);
			yelpParams.setTerm("tourist attractions");
			touristResponse = yelp.searchterm(yelpParams);
			obj2 = new JSONObject(touristResponse);
			touristBusinesses = obj2.getJSONArray("businesses");
			String stCategory = "";
			String taddress1 = "", taddress2 = "", tcity = "", tstate = "";
			for(int i1 = 0; i1 < touristBusinesses.length(); i1++)
			{
				temp = touristBusinesses.getJSONObject(i1);
				if(temp.has("categories"))
				{
					ja = temp.getJSONArray("categories");
				}
				taddress1=temp.getJSONObject("location").getJSONArray("display_address").getString(0);
				if(temp.getJSONObject("location").getJSONArray("display_address").length() > 1)
					taddress2=temp.getJSONObject("location").getJSONArray("display_address").getString(1);
				tcity=temp.getJSONObject("location").getString("city");
				tstate=temp.getJSONObject("location").getString("state_code");
				ArrayList<BigDecimal> tlatLng = null;
				//TODO
				tlatLng = requestGeoLocation2(taddress1, taddress2, tcity, tstate);
				tlatitude = BigDecimal.ZERO;
				tlongitude = BigDecimal.ZERO;
				if(null != tlatLng)
				{
					tlatitude = tlatLng.get(0);
					tlongitude = tlatLng.get(1);
				}
				else
				{
					//System.out.println("Setting zeroes LOCATION");
				}
				stCategory = "";
				for(int j = 0; j < ja.length(); j++)
				{
					if(j != ja.length()-1)
						stCategory += ja.getJSONArray(j).get(0) + ",";
					else
						stCategory += ja.getJSONArray(j).get(0);
				}
				tl = new TouristLocation(tID, temp.getString("name"), stCategory, 1, tlatitude, tlongitude, rid, city);
				mk = new MultiKey<String>(temp.getString("name"), stCategory, city);
				if(!mp.containsKey(mk))
				{
					mp.put(mk, tl);
					tID++;
				}
				else
				{
					tt = (TouristLocation) mp.get(mk);
					tt.restaurantID.add(rid);
					mp.remove(mk);
					mp.put(mk, tt);
				}
			}

			if(!dbOperations)
			{
				System.out.println
				(
						name + "\t" + description + "\t" + reserveoptions 
						+ "\t" + address1 + "\t" + address2 + "\t" + city + "\t"
						+ "\t" + state + "\t" + zip + "\t" + latitude + "\t" + longitude
						+ "\t" + opentime + "\t" + closetime + "\t" + nonworkingdays
						);
				System.out.println("************************************************");
			}
		}
		
		for (Entry<MultiKey<? extends MultiKey>, TouristLocation> entry : mp.entrySet()) 
		{
		    MultiKey<? extends MultiKey> key = entry.getKey();
		    tt = entry.getValue();
		    //DB Operations continue
		    if(dbOperations)
		    {
		    	
		    	String query = "SELECT LOCATION_TOURIST_ID.NEXTVAL FROM DUAL";
		    	stmt = connection.createStatement();
		    	ResultSet rs = stmt.executeQuery(query);
		    	int tlid = 0;
		    	while (rs.next()) {
		    		tlid = rs.getInt(1);
		    		//System.out.println(tlid);
		    	}
		    	rs.close();
		    	stmt.close();
		    	
		    	String insertTableSQL2 = "INSERT INTO NEARBY VALUES(?, ?)";
		    	PreparedStatement ps2;
		    	insertTableSQL = "INSERT INTO LOCATION VALUES (?,?,?,?,?,?)";
		    	preparedStatement = connection.prepareStatement(insertTableSQL);
		    	preparedStatement.setInt(1, tlid);
		    	if(tt.name.length() < 50)
		    		preparedStatement.setString(2, tt.name.substring(0, tt.name.length()));
		    	else
		    		preparedStatement.setString(2, tt.name.substring(0, 50));
		    	preparedStatement.setString(3, tt.category);
		    	preparedStatement.setInt(4, tt.priority);
		    	preparedStatement.setBigDecimal(5, tt.latitude);
		    	preparedStatement.setBigDecimal(6, tt.longitude);
		    	preparedStatement.executeUpdate();
		    	preparedStatement.close();
		    	
		    	for(int r1 : tt.restaurantID)
		    	{
		    		System.out.print("*");
		    		ps2 = connection.prepareStatement(insertTableSQL2);
		    		ps2.setInt(1, r1);
		    		ps2.setInt(2, tlid);
		    		ps2.executeUpdate();
		    		ps2.close();
		    	}
		    	System.out.println();
		    }
		    //TODO
		    if(!dbOperations)
		    {
		    	tt.print();
		    	for(int r1 : tt.restaurantID)
		    		System.out.println(r1 + " " + tt.touristID);
		    }
		}

	}
	catch (Exception e)
	{
		e.printStackTrace();
	}
}

private static ArrayList<BigDecimal> requestGeoLocation2(String address1,
		String address2, String city, String state)
{
	StringBuffer url = new StringBuffer();
	HttpURLConnection urlConn = null;
	ArrayList<BigDecimal> latLng = new ArrayList<BigDecimal>();
	try
	{
		String address = address1 + "," +  city + "," + state;
		String charSet = "UTF-8";
		url.append("https://maps.googleapis.com/maps/api/geocode/json?")
		   .append("address=")
		   .append(URLEncoder.encode(address, charSet))
		   .append("&sensor=")
		   .append(URLEncoder.encode("false", charSet))
		   .append("&key=")
		   .append(URLEncoder.encode(googleAPIKey.get(gKeyidx), charSet));
		urlConn = (HttpURLConnection) new URL(url.toString()).openConnection();
		urlConn.connect();

		BufferedReader in = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
        String inputLine;
        StringBuffer inputsb = new StringBuffer();
        while ((inputLine = in.readLine()) != null)
            inputsb.append(inputLine);
        in.close();
        
        JSONObject obj = new JSONObject(inputsb.toString());
        if(obj.getString("status").equalsIgnoreCase("OK"))
        {
        	JSONArray objArray = obj.getJSONArray("results");
        	JSONObject temp = objArray.getJSONObject(0);
        	latLng.add(new BigDecimal(temp.getJSONObject("geometry").
        			getJSONObject("location")
        			.getString("lat")));
        	latLng.add(new BigDecimal(temp.getJSONObject("geometry").
        			getJSONObject("location")
        			.getString("lng")));
        	if((latLng.get(0).compareTo(BigDecimal.ZERO) == 0) && (latLng.get(1).compareTo(BigDecimal.ZERO) == 0))
        	{
        		System.out.println("Latitude and longitude ZERO!!!");
        	}
        }
        else
        {
        	System.out.println("Google API error: " + obj.getString("status"));
        	if(obj.getString("status").equalsIgnoreCase("OVER_QUERY_LIMIT"))
        	{
        		System.out.println("Retrying");
        		gKeyidx++;
        		if(gKeyidx > 10)
        			System.exit(0);
        	}
        }
	}
	
	catch(Exception e)
	{
		e.printStackTrace();
	}

	if(latLng.size() > 0)
		return latLng;
	else
		return null;
	}
}