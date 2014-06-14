package com.factual.demo;

import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.sql.DriverManager;
import java.sql.Connection;
import java.sql.PreparedStatement;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Random;
 
public class FoodPriceCatalogInsert 
{
	
	static Connection connection = null;
	try
	{
		BufferedReader br = new BufferedReader(new FileReader("restaurantid.txt"));
		String val = "";
		int i = 1;
		while((val = br.readLine()) != null)
		{
			if(i != Integer.parseInt(val))
			{
				System.out.println(i);
				i += Integer.parseInt(val) - i;
			}
			i++;
		}
	}
	catch (Exception e)
	{
		e.printStackTrace();
	}
	static Integer[] restaurantabsent = 
		{
			236, 475, 1043, 2232, 2386, 3187, 3968, 5415, 7704, 7917, 9379, 9998, 10428, 11254, 12193, 13608, 13715, 13932, 14241, 14815, 15028, 15111, 15188, 15258, 16049, 16085, 16835, 18179, 19322, 19525, 20323, 20421, 20816, 22702, 23527, 23644, 25740, 27367, 27402, 28926, 29267, 29401, 29778, 30570, 32360, 32617, 33119, 34660, 35147, 35359, 36147, 37702, 38720, 39750, 40174, 40647, 41010, 41481, 41656, 41863, 44276, 46663, 46668, 47544, 47853, 48667, 48868, 50838, 51494, 52585, 53243, 53886, 55508, 57538, 57873, 58046, 58108, 59073, 59279, 62535, 65304, 65990, 66578, 66682, 67122, 67777, 68353, 69366, 70810, 71931, 73105, 74120, 74642, 75061, 77791, 78779, 85638, 90540, 91256, 91315, 91847, 93065, 93201, 93283, 93988, 94696, 96815, 99052, 99215, 99700, 101533, 101792, 102262, 102319, 102514, 104266, 104574, 104597, 104719, 105192, 105347, 105585, 107758, 109433
		};
	static int restaurantIDMax = 110013;
	static int foodCount = 1682;
	
	public static void main(String[] argv) 
	{
		try 
		{
			Class.forName("oracle.jdbc.driver.OracleDriver");
		} 

		catch (ClassNotFoundException e) 
		{
			e.printStackTrace();
			return;
		}
		try 
		{
			connection = DriverManager.getConnection(
					FactualDB.connectionString.toString(), 
					FactualDB.dbusername,
					FactualDB.dbpassword);
			if (connection != null)
			{
				//Use whenever necessary
				//ParkingInsert();
				FoodPriceInsert();
				//TablesInsert();
			}
			else
			{
				System.out.println("Failed to make connection!");
			}
		} 
		
		catch (SQLException e) 
		{
			System.out.println("Connection Failed! Check output console");
			e.printStackTrace();
			return;
		}
		
		finally
		{
			try
			{
				if (connection != null)
					connection.close();
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		}
	}
	
	private static void TablesInsert()
	{
		Statement stmt = null;
		String insertTableSQL = "INSERT INTO TABLES VALUES(?, ?, ?)";
		int rCnt = 0;
		int[] gp = {2, 4, 6, 8};
	    PreparedStatement ps2;
	    Random r = new Random();
	    try 
	    {
	    	int i = 0;
	    	List<Integer> rAbsent = Arrays.asList(restaurantabsent);
	    	BufferedReader br = new BufferedReader(new FileReader("tables.txt"));
			String val = "";
			ArrayList<Integer> tablesRestaurants = new ArrayList<Integer>();
			while((val = br.readLine()) != null)
				tablesRestaurants.add(Integer.parseInt(val));
	    	
	    	for(i = 0; i < tablesRestaurants.size(); i++)
	    	{
    			for(int j = 0; j < 4; j++)
    			{
    				System.out.println(i);
    				rCnt = r.nextInt(26);
    				ps2 = connection.prepareStatement(insertTableSQL);
    				ps2.setInt(1, gp[j]);
    				ps2.setInt(2, tablesRestaurants.get(i));
    				ps2.setInt(3, rCnt);
    				ps2.executeUpdate();
    				ps2.close();
    			}
	    	}
	    } 
	    catch (Exception e ) 
	    {
	        e.printStackTrace();
	    } 
	    finally 
	    {
	        if (stmt != null) 
	        { 	
	        	try
				{
					stmt.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				} 
	        }
	    }
	}

	private static void ParkingInsert()
	{
		Statement stmt = null;
		String insertTableSQL = "INSERT INTO PARKING VALUES(?, ?)";
		int rCnt = 0;
		int[] ex = {0};
	    PreparedStatement ps2;
	    Random r = new Random();
	    try 
	    {
			BufferedReader br = new BufferedReader(new FileReader("parking.txt"));
			String val = "";
			ArrayList<Integer> parkingRestaurants = new ArrayList<Integer>();
			while((val = br.readLine()) != null)
				parkingRestaurants.add(Integer.parseInt(val));
	    	int i = 0;
	    	for(i = 0; i < parkingRestaurants.size(); i++)
	    	{
    			System.out.println(i);
    			rCnt = getRandomWithExclusion(r, 1, 250, ex);
    			ps2 = connection.prepareStatement(insertTableSQL);
    			ps2.setInt(1, parkingRestaurants.get(i));
    			ps2.setInt(2, rCnt);
    			ps2.executeUpdate();
    			ps2.close();
	    	}
	    } 
	    catch (Exception e ) 
	    {
	        e.printStackTrace();
	    } 
	    finally 
	    {
	        if (stmt != null) 
	        { 	
	        	try
				{
					stmt.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				} 
	        }
	    }

	}

	private static void FoodPriceInsert()
	{
		Statement stmt = null;
		String insertTableSQL2 = "INSERT INTO FDPRICECATALOG VALUES(?, ?, ?)";
		int rCnt = 1, menuCnt = 1;
		int[] ex = {0};
		double price  = 0.0;
	    PreparedStatement ps2;
	    Random r = new Random();
	    try 
	    {
	    	ArrayList<Integer> menuItemSet = new ArrayList<Integer>();
	    	ArrayList<Integer> tempMenu = new ArrayList<Integer>();
	    	for(int k = 1; k <= foodCount; k++)
	    		tempMenu.add(k);
	    	ArrayList<Integer> rAbsent = new ArrayList<Integer>(Arrays.asList(restaurantabsent));
	    	for(int i = 5034; i <= 9999; i++)
	    	{
	    		if(!rAbsent.contains(i))
	    		{
	    			System.out.println("*********" + i + "***********");
	    			menuCnt = getRandomWithExclusion(r, 1, 130, ex);
	    			menuItemSet = generateSets(menuCnt, tempMenu);
	    			for(int j : menuItemSet)
	    			{
	    				price = 2.5 * getRandomWithExclusion(r, 1, 20, ex);;
	    				ps2 = connection.prepareStatement(insertTableSQL2);
	    				ps2.setInt(1, i);
	    				ps2.setInt(2, j);
	    				ps2.setFloat(3, (float) price);
	    				//System.out.println(i + " " + j + " " + price);
	    				ps2.executeUpdate();
	    				ps2.close();
	    			}
	    			System.out.println();
	    		}
	    	}
	    	
	    } 
	    catch (SQLException e ) 
	    {
	        e.printStackTrace();
	    } 
	    finally 
	    {
	        if (stmt != null) 
	        { 	
	        	try
				{
					stmt.close();
				}
				catch (SQLException e)
				{
					e.printStackTrace();
				} 
	        }
	    }

	}

	private static ArrayList<Integer> generateSets(int menuCnt, ArrayList menuItemSet)
	{
		Collections.shuffle(menuItemSet);
		ArrayList<Integer> submenuItemSet = new ArrayList<Integer>(menuItemSet.subList(0, menuCnt - 1));
		return submenuItemSet;
	}

	public static int getRandomWithExclusion(Random rnd, int start, int end, int... exclude) {
	    int random = start + rnd.nextInt(end - start + 1 - exclude.length);
	    for (int ex : exclude) {
	        if (random < ex) {
	            break;
	        }
	        random++;
	    }
	    return random;
	}
}
 