using Data.Models;
using GrandTheftMultiplayer.Server.API;
using GrandTheftMultiplayer.Server.Elements;
using GTAReallife.Data;
using RealifeGM.de.jojoa.main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.de.jojoa.data
{
    public class AccountController
    {
        public static bool AccountExists(Client player)
        {
            bool exists = false;

            using (var db = new DataModel(Main.CONNECTION_STRING))
            {
                exists = db.Accounts.AsNoTracking().Where(p => p.SocialclubName.ToLower() == player.socialClubName.ToLower()).Count() > 0;
            }

                return exists;
        }

        public static void updateJoin(Client player)
        {
            AccountModel a = GetAccount(player.socialClubName);
            using (var db = new DataModel(Main.CONNECTION_STRING))
            {
                AccountModel ac = db.Accounts.Single(p => p.AccountId == a.AccountId);
                ac.LastIp = player.address;
                ac.LastSeen = DateTime.UtcNow;
                db.SaveChanges();
                
            }
        }

        public static AccountModel GetAccount(int id)
        {
            AccountModel a = null;

            using (var db = new DataModel(Main.CONNECTION_STRING))
            {
                try
                {
                    a = db.Accounts.Single(p => p.AccountId == id);
                } catch (Exception e)
                {
                    API.shared.consoleOutput(e.Message);
                }
            }
            return a;
        }

        public static AccountModel GetAccount(string socialClubName)
        {
            AccountModel a = null;

            using (var db = new DataModel(Main.CONNECTION_STRING))
            {
                try
                {
                    a = db.Accounts.Single(p => p.SocialclubName.ToLower() == socialClubName.ToLower());
                }
                catch (Exception e)
                {
                    API.shared.consoleOutput(e.Message);
                }
            }
            return a;
        }

        public static bool Login(string name, string password)
        {
            bool result = false;

            using (var db = new DataModel(Main.CONNECTION_STRING))
            {
                try
                {
                    AccountModel a = db.Accounts.AsNoTracking().Single(p => p.SocialclubName.ToLower() == name.ToLower());
                    result = API.shared.verifyPasswordHashBCrypt(password, a.Password);
                } catch (Exception e)
                {
                    API.shared.consoleOutput("AccountController: " + e.Message);
                }
            }

            return result;

        }


    }
}
