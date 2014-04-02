using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// ProjectSettings contains the global variables and definitions that are required for the project
/// </summary>

namespace GourmetGuide
{
    public class ProjectSettings
    {        
        public static string dbProvider = "OraOLEDB.Oracle";
        public static string dbHost = "";
        public static string dbPort = "1521";
        public static string dbSid = "ORCL";
        public static string dbUser = "";
        public static string dbKey = "";
        public static string gmailKey = "";
        public static string googleAPIKey = "";
    }
}