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
    class MySQL_Bank : Script
    {
        #region variables
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_Bank()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            loadBanks();
        }
        
        #region createAccount
        public static Bank_Account createAccount(Account owner)
        {
            if (!isTableCreated())
                return null;

            int count = methods.getMethods.getBanksByUser(owner).Count;
          
            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "INSERT INTO BankData (money,Owner) VALUES (@money,@owner)";
            cmd.Parameters.AddWithValue("@money", 0);
            cmd.Parameters.AddWithValue("@owner", owner.p.name);
            
            cmd.ExecuteNonQuery();
            Bank_Account ba = new Bank_Account(owner, Convert.ToInt16(cmd.LastInsertedId));
            return ba;
        }
        #endregion createAccount

        public static void loadBanks()
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM BankData";
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Account a = methods.getMethods.getAccountByName(reader.GetString("Owner"));
                int number = reader.GetInt32("number");
                Bank_Account ba = new Bank_Account(a, number);
            }
        }

        public static void Save(Bank_Account ba)
        {
            if (!isTableCreated())
                return;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "UPDATE INTO BankData money=@money WHERE Owner=@owner";
            cmd.Parameters.AddWithValue("@money", ba.money);
            cmd.Parameters.AddWithValue("@owner", ba.owner.p.name);
            cmd.ExecuteNonQuery();
        }
     
        public static int getInt(int banknumber, string get)
        {
            if (!isTableCreated())
                return 0;

            cmd = Database.getConnection().CreateCommand();
            cmd.CommandText = "SELECT * FROM BankData WHERE Number=@nr";
            cmd.Parameters.AddWithValue("@number", banknumber);
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

        public static Boolean isTableCreated()
        {
            try
            {
                cmd = Database.getConnection().CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS BankData(Owner VARCHAR(100), Number int AUTO_INCREMENT, money int, PRIMARY KEY (Number))";
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
