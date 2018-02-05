using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_cmds : Script
    {
        [Command("test")]
        public void cmd_testing(Client p)
        {
            API.sendChatMessageToPlayer(p, "Hello, " + p.name);

        }
    }
}
