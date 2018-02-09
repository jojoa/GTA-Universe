﻿using GrandTheftMultiplayer.Server.API;
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
        public static string conString = "SERVER=localhost;" + "DATABASE=realifegm;" + "UID=root;" + "PASSWORD=;";
        public static MySqlConnection con;
        public static MySqlCommand cmd;
        public static MySqlDataReader reader;
        #endregion variables

        public MySQL_Bank()
        {
            API.onResourceStart += API_onResourceStart;
        }

        private void API_onResourceStart()
        {
            conString = "SERVER=" + API.getSetting<string>("db_host") + ";" 
                        + "DATABASE=" + API.getSetting<string>("db_name") + ";" 
                        + "UID=" + API.getSetting<string>("db_user") + ";" 
                        + "PASSWORD=" + API.getSetting<string>("db_pass") + ";";
        }
        
        #region createAccount
        public static Bank_Account createAccount(Account owner)
        {
            if (!isTableCreated())
                return null;

            int count = methods.getMethods.getBanksByUser(owner).Count;
          
            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO BankData (money,Owner) VALUES (@money,@owner)";
            con.Open();
            cmd.Parameters.AddWithValue("@money", 0);
            cmd.Parameters.AddWithValue("@owner", owner.p.name);
            
            cmd.ExecuteNonQuery();
            Bank_Account ba = new Bank_Account(owner,Convert.ToInt16(cmd.LastInsertedId));
            con.Close();
            return ba;
        }
        #endregion createAccount

        public static void loadBanks()
        {
            if (!isTableCreated())
                return;

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM BankData";
            con.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
                Account a = methods.getMethods.getAccountByName(reader.GetString("Owner"));
                int number = reader.GetInt32("number");
                Bank_Account ba = new Bank_Account(a, number);
            }
            con.Close();
        }

        public static void Save(Bank_Account ba)
        {
            if (!isTableCreated())
                return;

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "UPDATE INTO BankData money=@money WHERE Owner=@owner";
            con.Open();
            cmd.Parameters.AddWithValue("@money", ba.money);
            cmd.Parameters.AddWithValue("@owner", ba.owner.p.name);
            cmd.ExecuteNonQuery();
            con.Close();
        }
     
        public static int getInt(int banknumber, string get)
        {
            if (!isTableCreated())
                return 0;

            con = new MySqlConnection(conString);
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM BankData WHERE Number=@nr";
            cmd.Parameters.AddWithValue("@number", banknumber);
            con.Open();
            reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int getted = reader.GetInt16(get);
                reader.Close();
                con.Close();
                return getted;
            }
            reader.Close();
            con.Close();
            return 0;
        }

        public static Boolean isTableCreated()
        {
            try
            {
                con = new MySqlConnection(conString);
                cmd = con.CreateCommand();
                cmd.CommandText = "CREATE TABLE IF NOT EXISTS BankData(Owner VARCHAR(100), Number int AUTO_INCREMENT, money int, PRIMARY KEY (Number))";
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}