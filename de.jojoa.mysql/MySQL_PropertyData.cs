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
    class MySQL_PropertyData : Script
    {
        #region variables
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
            isTableCreated();

            loadProps();
        }

        private void API_onClientEventTrigger(Client p, string eventName, params object[] arguments)
        {
            if (!isTableCreated())
                return;

            switch(eventName)
            {
                case "getLoc_rt":
                    string street = arguments[1].ToString();
                    string zone = arguments[2].ToString();
                    string id = arguments[0].ToString();

                    cmd = Database.getConnection().CreateCommand();
                    cmd.CommandText = "UPDATE PropertyData SET (street, zone) VALUES (@street,@zone) WHERE ID=@id";
                    cmd.Parameters.AddWithValue("@street", street);
                    cmd.Parameters.AddWithValue("@zone", zone);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();

                    break;
            }
        }
        
        #region methods
        public static void saveProperty(Property prop)
        {
           if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO PropertyData (Type,Price,Owner,street,zone,PosX,PosY,PosZ,invid) VALUES (@type,@price,@owner,@street,@zone,@x,@y,@z,@inv)";
            cmd.Parameters.AddWithValue("@type", prop.type);
            cmd.Parameters.AddWithValue("@price", prop.price);
            cmd.Parameters.AddWithValue("@owner", prop.owner.p.name);
            cmd.Parameters.AddWithValue("@street", prop.street);
            cmd.Parameters.AddWithValue("@zone", prop.zone);

            cmd.Parameters.AddWithValue("@x", prop.pos.X);
            cmd.Parameters.AddWithValue("@y", prop.pos.Y);
            cmd.Parameters.AddWithValue("@z", prop.pos.Z);

            cmd.Parameters.AddWithValue("@inv", prop.inv.id);
            cmd.ExecuteNonQuery();
            long i = cmd.LastInsertedId;
            prop.ID = i.ToString();
        }

        public static void loadProps()
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM PropertyData";
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int type = reader.GetInt16("Type");
                int price = reader.GetInt16("Price");
                int invid = reader.GetInt32("invid");
                string id = reader.GetString("ID");
                string owner = reader.GetString("Owner");
                double posX = reader.GetDouble("posX");
                double posY = reader.GetDouble("posY");
                double posZ = reader.GetDouble("posZ");

                Vector3 pos = new Vector3(posX, posY, posZ);
                Property prop = new Property(pos, price, type,invid);

                prop.ID = id;
                prop.owner = methods.getMethods.getAccountByName(owner);
                prop.street = reader.GetString("street");
                prop.zone = reader.GetString("zone");
                prop.getTypeName();
                prop.show_prop();
            }
            reader.Close();
        }

        public static List<Property> getAll()
        {
            if (!isTableCreated())
                return null;

            List<Property> lprop = new List<Property>();

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM PropertyData";
            reader = cmd.ExecuteReader();
            while(reader.Read())
            {
                Property prop = new Property(new Vector3(reader.GetInt16("PosX"), reader.GetInt16("PosY"), reader.GetInt16("PosZ")), reader.GetInt16("Price"), reader.GetInt16("Type"),reader.GetInt32("invid"));
                prop.owner.p.name = reader.GetString("Owner");
                prop.ID = reader.GetString("ID");
                prop.street = reader.GetString("street");
                prop.zone = reader.GetString("zone");
                lprop.Add(prop);
            }

            reader.Close();
            return lprop;
        }

        public static void RemoveProp(Property p)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "DELETE FROM PropertyData WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", p.ID);
            cmd.ExecuteNonQuery();
        }
        #endregion methods

        public static String getString(Property p, string whattoget)
        {
            if (!isTableCreated())
                return null;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT " + whattoget + " FROM PropertyData WHERE ID=@id";
            cmd.Parameters.AddWithValue("@id", p.ID);
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                reader.Close();
                return reader.GetString(whattoget);
            }

            reader.Close();
            return null;
        }

        public static Boolean isTableCreated()
        {
            try
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS PropertyData(Type int ,Price int ,Owner VARCHAR(100) ,street VARCHAR(100),zone VARCHAR(100) ,PosX float,PosY float ,PosZ float ,invid int,ID int AUTO_INCREMENT, PRIMARY KEY (ID))";
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
