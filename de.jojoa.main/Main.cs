using GrandTheftMultiplayer.Server.API;
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
       
        public Main()
        {
            API.onResourceStart += onScriptStart;

        }

        #region events
        public void onScriptStart()
        {
            JobAccounts();
            API.consoleOutput(de.jojoa.methods.stringMethods.console_ressource_start);
        }
        #endregion events
        public void JobAccounts()
        {
            Account IM = new Account("IM", 0);
        }
    }
}
