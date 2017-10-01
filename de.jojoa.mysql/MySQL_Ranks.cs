using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using MySql.Data.MySqlClient;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.mysql
{
    class MySQL_Ranks : Script
    {
        #region variables
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_Ranks()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
        }

        #region registerPlayer
        public static void registerPlayer(Account name)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO RankData (Name, createprop, removeprop, god, fly , kick, pos,id,createShop,createBank,spawncar,givemoney) VALUES (@name,@cp,@rp,@god,@fly,@kick,@pos,@id,@cs,@cb,@sc,@gm)";
            cmd.Parameters.AddWithValue("@name", name.name);
            cmd.Parameters.AddWithValue("@cp", 0);
            cmd.Parameters.AddWithValue("@rp", 0);
            cmd.Parameters.AddWithValue("@god", 0);
            cmd.Parameters.AddWithValue("@fly", 0);
            cmd.Parameters.AddWithValue("@kick", 0);
            cmd.Parameters.AddWithValue("@pos", 0);
            cmd.Parameters.AddWithValue("@cs", 0);
            cmd.Parameters.AddWithValue("@cb", 0);
            cmd.Parameters.AddWithValue("@sc", 0);
            cmd.Parameters.AddWithValue("@gm", 0);
            cmd.Parameters.AddWithValue("@id", name.id);
            cmd.ExecuteNonQuery();
        }
        #endregion registerPlayer

        #region get
        public static String getString(Client p, string get)
        {
            if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", p.name);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string getted = reader.GetString(get);
                reader.Close();
                return getted;
            }

            reader.Close();
            return null;
        }

        public static String getStringByName(string name, string get)
        {
           if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", name);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string getted = reader.GetString(get);
                reader.Close();
                return getted;
            }

            reader.Close();
            return null;
        }

        public static int getInt(Client p, string get)
        {
            if (!isTableCreated())
                return 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", p.name);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int getted = reader.GetInt16(get);
                reader.Close();
                return getted;
            }

            reader.Close();
            return 0;
        }

        public static int getIntByName(string name, string get)
        {
            if (!isTableCreated())
                return 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", name);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int getted = reader.GetInt16(get);
                reader.Close();
                return getted;
            }

            reader.Close();
            return 0;
        }
        #endregion get

        #region set
        public static void set(Client p, string set, string whattoset)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "UPDATE RankData SET " + set + "=@whattoset WHERE Name=@name";
            cmd.Parameters.AddWithValue("@whattoset", whattoset);
            cmd.Parameters.AddWithValue("@name", p.name);
            cmd.ExecuteNonQuery();
        }
        #endregion set

        #region othermethods
        public static Boolean isTableCreated()
        {
            try
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS RankData(Name VARCHAR(100),id int AUTO_INCREMENT,createprop tinyint,removeprop tinyint,fly tinyint,god tinyint,kick tinyint,pos tinyint ,createShop tinyint, createBank tinyint, spawncar tinyint, givemoney tinyint, PRIMARY KEY(id))";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static Dictionary<string,Boolean> getRank(string user)
        {
            if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM RankData WHERE Name=@name";
            cmd.Parameters.AddWithValue("@name", user);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, Boolean> dic = new Dictionary<string, bool>();
                dic.Add("createprop", reader.GetBoolean("createprop"));
                dic.Add("removeprop", reader.GetBoolean("removeprop"));
                dic.Add("fly", reader.GetBoolean("fly"));
                dic.Add("god", reader.GetBoolean("god"));
                dic.Add("kick", reader.GetBoolean("kick"));
                dic.Add("pos", reader.GetBoolean("pos"));
                dic.Add("createShop", reader.GetBoolean("createShop"));
                dic.Add("createBank", reader.GetBoolean("createBank"));
                dic.Add("spawncar", reader.GetBoolean("spawncar"));
                dic.Add("givemoney", reader.GetBoolean("givemoney"));

                reader.Close();
                return dic;
            }
            
            reader.Close();
            return null;
        }
        #endregion othermethods
    }
}
