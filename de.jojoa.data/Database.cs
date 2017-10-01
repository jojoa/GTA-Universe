using GrandTheftMultiplayer.Server.API;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using MySql.Data.MySqlClient;

namespace RealifeGM.de.jojoa.data
{
    class Database : Script
    {
        private static MySqlConnection connection = null;

        public Database()
        {
            Connect();

            API.onResourceStop += API_onResourceStop;
        }

        private void API_onResourceStop()
        {
			connection.Close();
        }

        private static void Connect()
        {
            try
            {
                API.shared.consoleOutput("Trying to connect to database...");

                XmlDocument config = new XmlDocument();
                config.Load("universe.xml");

                string conString = "SERVER=" + config.GetElementsByTagName("db_host")[0].InnerText + ";" 
                        			+ "DATABASE=" + config.GetElementsByTagName("db_name")[0].InnerText + ";" 
                        			+ "UID=" + config.GetElementsByTagName("db_user")[0].InnerText + ";" 
                        			+ "PASSWORD=" + config.GetElementsByTagName("db_pass")[0].InnerText + ";";

                connection = new MySqlConnection(conString);
                connection.Open();

                API.shared.consoleOutput("Database connection created");
            }
            catch(MySqlException e)
            {
                API.shared.consoleOutput("Database connection failed: " + e.Message);
                API.shared.stopResource(API.shared.getThisResource());
            }
        }

        public static MySqlConnection getConnection()
        {
            if(connection == null)
                Connect();

            if(connection.State == ConnectionState.Open)
                return connection;
            else
            {
                connection.Open();
                return connection;
            }
        }
    }
}
