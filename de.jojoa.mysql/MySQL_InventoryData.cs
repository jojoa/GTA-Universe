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
    class MySQL_InventoryData : Script
    {
        #region variables
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_InventoryData()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadInvs();
        }

        public static Inventory createInv()
        {
            if (!isTableCreated())
                return null;

            int lastid = getLastID();

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO InventoryData (Name,id,amount) VALUES (@name,@id,@amount)";
            cmd.Parameters.AddWithValue("@id", lastid + 1);
            cmd.Parameters.AddWithValue("@amount", 1);
            cmd.Parameters.AddWithValue("@name", "dummy");
            cmd.ExecuteNonQuery();
            Inventory inv = new Inventory(lastid + 1);

            return inv;
        }

        public static int getLastID()
        {
            if (!isTableCreated())
                return 0;

            int i = 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM InventoryData";
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                int a = reader.GetInt32("id");
                if(a>i)
                {
                    i = a;
                }
            }
            reader.Close();
            return i;
        }

        public static void Update(Inventory inv)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "DELETE FROM InventoryData WHERE id=@id";
            cmd.Parameters.AddWithValue("@id", inv.id);
            cmd.ExecuteNonQuery();

            foreach(string i in inv.items.Keys)
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO InventoryData (id, name, amount) VALUES (@id,@name,@amount)";
                cmd.Parameters.AddWithValue("@id", inv.id);
                cmd.Parameters.AddWithValue("@name", i);
                cmd.Parameters.AddWithValue("@amount", inv.items[i]);
                cmd.ExecuteNonQuery();
            }
        }

        public static Boolean isTableCreated()
        {
            try
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS InventoryData(Name VARCHAR(100),amount int, id int AUTO_INCREMENT, PRIMARY KEY (id))";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void loadInvs()
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM InventoryData WHERE name=@name";
            cmd.Parameters.AddWithValue("@name", "dummy");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Inventory inv = new Inventory(reader.GetInt32("id"));
            }
            reader.Close();

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM InventoryData WHERE name!=@name";
            cmd.Parameters.AddWithValue("@name", "dummy");
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Inventory inv = methods.getMethods.getInvById(reader.GetInt32("id"));
                inv.addItem(reader.GetString("name"), reader.GetInt32("amount"));
            }
            reader.Close();
        }
    }
}
