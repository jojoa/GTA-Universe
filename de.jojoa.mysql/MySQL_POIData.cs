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
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_POIData()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadPOI();
        }
        
        public static int getID(Vector3 pos)
        {
            if (!isTableCreated())
                return 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE X=@x AND Y=@y AND Z=@z";
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            reader = cmd.ExecuteReader();
          
            while (reader.Read())
            {
                reader.Close();
                return reader.GetInt32("id");
            }

            reader.Close();
            return 0;
        }

        public static void loadPOI()
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData";
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

            reader.Close();
        }

        public static List<Vector3> getBanks()
        {
            if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=@type";
            cmd.Parameters.AddWithValue("@type", "Bank");
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
            reader.Close();
            return list;
        }
        
        public static Boolean isTableCreated()
        {
            try
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS POIData (Type VARCHAR(100), X double, Y double, Z double, id int AUTO_INCREMENT, PRIMARY KEY (id))";
                cmd.ExecuteNonQuery();

                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopData (X double, Y double, Z double, SPX double, SPY double, SPZ double, RTX double, RTY double, RTZ double , class VARCHAR(100), id int, PRIMARY KEY (id))";
                cmd.ExecuteNonQuery();

                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS ShopDataItems (Class VARCHAR(100), name VARCHAR(100), price int, id int,  PRIMARY KEY (id))";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void addBank(Vector3 pos)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z) VALUES (@type,@x,@y,@z)";
            cmd.Parameters.AddWithValue("@type", "Bank");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.ExecuteNonQuery();

            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Bank", pos.Add(new Vector3(0, 2, 0)), 5, 10);
        }

        public static List<Vector3> getShops()
        {
            if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM POIData WHERE Type=@type";
            cmd.Parameters.AddWithValue("@type", "Shop");
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
            reader.Close();
            return list;
        }

        public static List<string> getShopItems(Vector3 pos)
        {
            if (!isTableCreated())
                return null;

            int sid = methods.getMethods.getShopByPos(pos);
            List<string> ls = new List<string>();
            string clas = "";

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            reader = cmd.ExecuteReader();
            
            while (reader.Read())
            {
                clas = reader.GetString("class");
            }

            reader.Close();

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopDataItems WHERE Class=@clas";
            cmd.Parameters.AddWithValue("@clas", clas);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ls.Add(reader.GetString("name") + ":" + reader.GetInt16("price"));
            }

            reader.Close();
            return ls;
        }

        public static Vector3 getShopSpawn(Vector3 pos)
        {
            Vector3 posSP = new Vector3(0, 0, 0);

            if (!isTableCreated())
                return posSP;

            int sid = methods.getMethods.getShopByPos(pos);

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("SPX");
                posSP.Y = reader.GetFloat("SPY");
                posSP.Z = reader.GetFloat("SPZ");
            }

            reader.Close();
            return posSP;
        }

        public static Vector3 getShopSpawnRot(Vector3 pos)
        {
            Vector3 posSP = new Vector3(0, 0, 0);

            if (!isTableCreated())
                return posSP;

            int sid = methods.getMethods.getShopByPos(pos);

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM ShopData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", sid);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                posSP.X = reader.GetFloat("RTX");
                posSP.Y = reader.GetFloat("RTY");
                posSP.Z = reader.GetFloat("RTZ");
            }

            reader.Close();
            return posSP;
        }

        public static int addShop(Vector3 pos, string clas)
        {
            if (!isTableCreated())
                return 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO POIData (Type,X,Y,Z) VALUES (@type,@x,@y,@z)";
            cmd.Parameters.AddWithValue("@type", "Shop");
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.ExecuteNonQuery();

            long id = cmd.LastInsertedId;
            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO ShopData (id,X,Y,Z,class) VALUES (@id,@x,@y,@z,@class)";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@x", pos.X);
            cmd.Parameters.AddWithValue("@y", pos.Y);
            cmd.Parameters.AddWithValue("@z", pos.Z);
            cmd.Parameters.AddWithValue("@class", clas);
            cmd.ExecuteNonQuery();
            
            API.shared.createMarker(1, pos, new Vector3(), new Vector3(), new Vector3(1, 1, 1), 150, 0, 255, 0);
            API.shared.createTextLabel("Shop", pos.Add(new Vector3(0, 2, 0)), 5, 10);
            return Convert.ToInt32(id);
        }

        public static Boolean setSpawn(Vector3 pos, Vector3 rot, int sid)
        {
            if (!isTableCreated())
                return false;

            Vector3 p = null;
            
            cmd = Database.getConnection().CreateCommand();
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
            reader.Close();

            if(p == null)
            {
                return false;
            }

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "UPDATE ShopData SET (SPX,SPY,SPZ,RTX,RTY,RTZ) VALUES (@spx,@spy,@spz,@rtx,@rty,@rtz) WHERE id=@id";
            cmd.Parameters.AddWithValue("@spx", pos.X);
            cmd.Parameters.AddWithValue("@spy", pos.Y);
            cmd.Parameters.AddWithValue("@spz", pos.Z);
            cmd.Parameters.AddWithValue("@rtx", rot.X);
            cmd.Parameters.AddWithValue("@rty", rot.Y);
            cmd.Parameters.AddWithValue("@rtz", rot.Z);
            cmd.Parameters.AddWithValue("@id", sid);
            cmd.ExecuteNonQuery();
            return true;
        }
    }
}
