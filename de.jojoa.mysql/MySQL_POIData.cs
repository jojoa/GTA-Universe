using GrandTheftMultiplayer.Server.API;
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
    class MySQL_POIData : Script
    {
        #region variables
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_POIData()
        {
            
        }
        
        
        public static int getID(Vector3 pos)
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE X=@x AND Y=@y AND Z=@z";
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            con.Open();
            reader = cmd.ExecuteReader();
          
            while (reader.Read())
            {
                return reader.GetInt32("id");
                

            }
            return 0;
        }

        public static void loadPOI()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData";
            con.Open();
            reader = cmd.ExecuteReader();
          
            while (reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                string type = reader.GetString("Type");
                API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
                API.shared.createTextLabel(type, pos.Add(new Vector3(0, 2, 0)), 5, 10);
            }
        }

        public static List<Vector3> getBanks()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=@type";
            cmd.Parameters.AddWithValue("@type", "Bank");
            con.Open();
            reader = cmd.ExecuteReader();
            List <Vector3> list = new List<Vector3>();
            while(reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                list.Add(pos);
            }
            return list;
        }
        
        public static void createTable()
        {
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS POIData (Type VARCHAR(100), X double, Y double, Z double, id int AUTO_INCREMENT, PRIMARY KEY (id))";
            con.Open();
            cmd.ExecuteNonQuery();

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopData (X double, Y double, Z double, SPX double, SPY double, SPZ double, RTX double, RTY double, RTZ double , class VARCHAR(100), id int, PRIMARY KEY (id))";
            con.Open();
            cmd.ExecuteNonQuery();

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopDataItems (Class VARCHAR(100), name VARCHAR(100), price int, id int,  PRIMARY KEY (id))";
            con.Open();
            cmd.ExecuteNonQuery();

        }

        public static void addBank(Vector3 pos)
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z) VALUES (@type,@x,@y,@z)";
            cmd.Parameters.AddWithValue("@type", "Bank");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Bank", pos.Add(new Vector3(0, 2, 0)), 5, 10);
        }

        public static List<Vector3> getShops()
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=@type";
            cmd.Parameters.AddWithValue("@type", "Shop");
            con.Open();
            reader = cmd.ExecuteReader();
            List<Vector3> list = new List<Vector3>();
            while (reader.Read())
            {
                Vector3 pos = new Vector3();
                pos.X = reader.GetFloat("X");
                pos.Y = reader.GetFloat("Y");
                pos.Z = reader.GetFloat("Z");
                list.Add(pos);
            }
            return list;
        }
        public static List<string> getShopItems(Vector3 pos)
        {
            createTable();
             int sid = methods.getMethods.getShopByPos(pos);
            List<string> ls = new List<string>();
            string clas = "";
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            con.Open();
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                clas = reader.GetString("class");
            }

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopDataItems WHERE Class=@clas";
            cmd.Parameters.AddWithValue("@clas", clas);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ls.Add(reader.GetString("name") + ":" + reader.GetInt16("price"));
            }
            return ls;
        }

        public static Vector3 getShopSpawn(Vector3 pos)
        {
            createTable();
            Vector3 posSP = new Vector3(0, 0, 0);

            int sid = methods.getMethods.getShopByPos(pos);

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("SPX");
                    posSP.Y = reader.GetFloat("SPY");
                    posSP.Z = reader.GetFloat("SPZ");
            }

            return posSP;
        }

        public static Vector3 getShopSpawnRot(Vector3 pos)
        {
            createTable();
            Vector3 posSP = new Vector3(0, 0, 0);

             int sid = methods.getMethods.getShopByPos(pos);

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("RTX");
                posSP.Y = reader.GetFloat("RTY");
                posSP.Z = reader.GetFloat("RTZ");
            }

            return posSP;
        }

        public static int addShop(Vector3 pos, string clas)
        {
            createTable();
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z) VALUES (@type,@x,@y,@z)";
            cmd.Parameters.AddWithValue("@type", "Shop");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            con.Open();
            cmd.ExecuteNonQuery();
            long id = cmd.LastInsertedId;
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO ShopData (id,X,Y,Z,class) VALUES (@id,@x,@y,@z,@class)";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.Parameters.AddWithValue("@class", clas);
            con.Open();
            cmd.ExecuteNonQuery();
            
            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Shop", pos.Add(new Vector3(0, 2, 0)), 5, 10);
            return Convert.ToInt32(id);
        }

        public static Boolean setSpawn(Vector3 pos, Vector3 rot, int sid)
        {
            Vector3 p = null;
            
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                p = new Vector3(0, 0, 0);
                p.X = reader.GetFloat("X");
                p.Y = reader.GetFloat("Y");
                p.Z = reader.GetFloat("Z");
            }

            if(p == null)
            {
                return false;
            }

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE ShopData SET (SPX,SPY,SPZ,RTX,RTY,RTZ) VALUES (@spx,@spy,@spz,@rtx,@rty,@rtz) WHERE id=@id";
            cmd.Parameters.AddWithValue("@spx", pos.X);
            cmd.Parameters.AddWithValue("@spy", pos.Y);
            cmd.Parameters.AddWithValue("@spz", pos.Z);
            cmd.Parameters.AddWithValue("@rtx", rot.X);
            cmd.Parameters.AddWithValue("@rty", rot.Y);
            cmd.Parameters.AddWithValue("@rtz", rot.Z);
            cmd.Parameters.AddWithValue("@id", sid);
            con.Open();
            cmd.ExecuteNonQuery();
            return true;
        }


    }
}
