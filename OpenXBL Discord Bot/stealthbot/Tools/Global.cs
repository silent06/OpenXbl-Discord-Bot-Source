using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stealthbot
{


    class Global
    {
        public static string host;
        public static string Username;
        public static string password;
        public static string Database;
        public static string DiscordApiToken;
    }

    public struct ClientInfo
    {
        public string CPUKey;
        public string Discord;
        public string Discordid;
        public string Username;
        public string email;
    }

    public struct OpenXBL {

        public string APIKEY;
        public static string VPS;
        public static string URL;
    }

}
