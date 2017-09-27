﻿using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using MySql.Data.MySqlClient;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.mysql
{
    class MySQL_PropertyData : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_PropertyData()
        {
            API.onResourceStart += API_onResourceStart;
            API.onClientEventTrigger += API_onClientEventTrigger;
        }

        public void API_onResourceStart()
        {
            conString = "SERVER=" + API.getSetting<string>("db_host") + ";" 
                        + "DATABASE=" + API.getSetting<string>("db_name") + ";" 
                        + "UID=" + API.getSetting<string>("db_user") + ";" 
                        + "PASSWORD=" + API.getSetting<string>("db_pass") + ";";

            isTableCreated();
        }

        private void API_onClientEventTrigger(Client p, string eventName, params object[] arguments)
        {
            switch(eventName)
            {
                case "getLoc_rt":
                    string street = arguments[1].ToString();
                    string zone = arguments[2].ToString();
                    string id = arguments[0].ToString();

                    con = new MySqlConnection(conString);
                    cmd = con.CreateCommand();
                    cmd.CommandText = "UPDATE PropertyData SET (street, zone) VALUES (@street,@zone) WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@zone", zone);
                    cmd.Parameters.AddWithValue("@id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();

                    break;
            }
        }

        
        #region methods
        public static void saveProperty(Property prop)
        {
           
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO PropertyData (Type,Price,Owner,Street,Zone,PosX,PosY,PosZ,invid) VALUES (@type,@price,@owner,@street,@zone,@x,@y,@z,@inv)";
            cmd.Parameters.AddWithValue("@type", prop.type);
            cmd.Parameters.AddWithValue("@price", prop.price);
            cmd.Parameters.AddWithValue("@owner", prop.owner.p.name);
            cmd.Parameters.AddWithValue("@street", prop.street);
            cmd.Parameters.AddWithValue("@zone", prop.zone);

            cmd.Parameters.AddWithValue("@x", prop.pos.X);
            cmd.Parameters.AddWithValue("@y", prop.pos.Y);
            cmd.Parameters.AddWithValue("@z", prop.pos.Z);

            cmd.Parameters.AddWithValue("@inv", prop.inv.id);
            con.Open();
            cmd.ExecuteNonQuery();
            long i = cmd.LastInsertedId;
            prop.ID = i.ToString();
            con.Close();
           

            

            
        }

        public static void loadProps()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM PropertyData";
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int type = reader.GetInt16("Type");

                string id = reader.GetString("id");
                int price = reader.GetInt16("Price");
                string owner = reader.GetString("Owner");
                double posX = reader.GetDouble("posX");
                double posY = reader.GetDouble("posY");
                double posZ = reader.GetDouble("posZ");
                int invid = reader.GetInt32("invid");
                Vector3 pos = new Vector3(posX, posY, posZ);
                Property prop = new Property(pos, price, type,invid);
                prop.ID = id;
                
                prop.owner = methods.getMethods.getAccountByName(reader.GetString("owner"));
                prop.street = reader.GetString("street");
                prop.zone = reader.GetString("zone");
                prop.getTypeName();
                prop.show_prop();
                
               
            }
            reader.Close();
            con.Close();
        }

        public static List<Property> getAll()
        {
            List<Property> lprop = new List<Property>();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM PropertyData";
            con.Open();
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Property prop = new Property(new Vector3(reader.GetInt16("PosX"), reader.GetInt16("PosY"), reader.GetInt16("PosZ")), reader.GetInt16("price"), reader.GetInt16("type"),reader.GetInt32("invid"));
                prop.owner.p.name = reader.GetString("owner");
                prop.ID = reader.GetString("id");
                prop.street = reader.GetString("street");
                prop.zone = reader.GetString("zone");
                lprop.Add(prop);
            }
            return lprop;
        }

        public static void RemoveProp(Property p)
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "DELETE FROM PropertyData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", p.ID);
            con.Open();
            cmd.ExecuteNonQuery();
        }
        #endregion methods
        public static String getString(Property p, string whattoget)
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT " + whattoget + " FROM PropertyData WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", p.ID);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                return reader.GetString(whattoget);
            }
            return null;
        }

        public static Boolean isTableCreated()
        {
            try
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS PropertyData(Type int ,Price int ,Owner VARCHAR(100) ,Street VARCHAR(100),Zone VARCHAR(100) ,PosX float,PosY float ,PosZ float ,invid int)";
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
