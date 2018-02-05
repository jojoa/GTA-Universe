using GrandTheftMultiplayer.Server.API;
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
            API.consoleOutput(de.jojoa.methods.stringMethods.console_prefix + "Das Script wurde erfolgreich gestartet.");


        }
        #endregion events

    }
}
