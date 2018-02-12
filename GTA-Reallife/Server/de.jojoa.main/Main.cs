﻿using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using RealifeGM.de.jojoa.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.main
{
    class Main : Script
    {
        public static string CONNECTION_STRING;
        public Main()
        {
            API.onResourceStart += onScriptStart;

        }

        #region events
        public void onScriptStart()
        {
            API.consoleOutput(de.jojoa.methods.stringMethods.console_ressource_start);
            mysql.MySQL_InventoryData.loadInvs();
            mysql.MySQL_PlayerData.loadAccounts();
            JobAccounts();
            mysql.MySQL_PropertyData.loadProps();
            mysql.MySQL_Bank.loadBanks();
            mysql.MySQL_Vehicles.loadVehicles();
            mysql.MySQL_POIData.loadPOI();
            //test
        }
        #endregion events
        public void JobAccounts()
        {
            Account IM = new Account("IM", 0);
        }

        

    }
}
