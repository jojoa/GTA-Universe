using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealifeGM.de.jojoa.player
{
    class player_login : Script
    {
        public player_login()
        {
            API.onPlayerConnected += onPlayerJoin;

        }
        #region events
        public void onPlayerJoin(Client p)
        {
            if (!de.jojoa.mysql.MySQL_PlayerData.playerExists(p))
            {
                de.jojoa.mysql.MySQL_PlayerData.registerPlayer(p, "test12");
                API.sendChatMessageToPlayer(p, "Du hast dich erfolgreich registriert");

            }
            else
            {
                API.sendChatMessageToPlayer(p, "Du bist bereits registriert");
            }
        }
        #endregion events
    }

}
