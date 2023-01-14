using Discord;
using Discord.Commands;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Discord.Net;
using Discord.WebSocket;
using stealthbot;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text;
using System.Threading;
using MySql.Data.MySqlClient;
using System.Net.Http;
using DiscordBot;

namespace OpenXbl
{
    public class XBLProfileCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();

        [Command("account")]
        public async Task account()
        {

            try
            {

                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
                var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);

                /*using (StreamReader r = new StreamReader("account.json")) {
                    string json = r.ReadToEnd();
                }*/
                string json = OpenXblHttp.RestClient.strResponseValue;
                var doc = JsonDocument.Parse(json);
                var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
                var profilepicture = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[0].GetProperty("value");
                var gamerscore = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[1].GetProperty("value");
                var gamertag = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[2].GetProperty("value");
                var AccountTier = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[3].GetProperty("value");
                var XboxOneRep = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[4].GetProperty("value");
                //var PreferredColor = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[5].GetProperty("value");
                var RealName = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[6].GetProperty("value");
                var Bio = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[7].GetProperty("value");
                var tenurelevel = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[8].GetProperty("value");

                Embed.AddField("GamerScore:", gamerscore, true);
                Embed.AddField("XUID:", (xuid.GetString(), "HEX:", Tools.StringToHex(xuid.GetString())), true);
                Embed.WithImageUrl(profilepicture.GetString());
                Embed.AddField("gamertag:", gamertag + ".", true);
                Embed.AddField("AccountTier:", AccountTier + ".", true);
                Embed.AddField("XboxOneRep:", XboxOneRep + ".", true);
                Embed.AddField("Bio:", Bio + ".", true);/* period is for discords BS no empty string garbage"*/
                Embed.AddField("Tenurelevel:", tenurelevel + ".", true);
                Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                await Context.Channel.SendMessageAsync("", false, Embed.Build());

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetUserByXuid")]
        public async Task GetUserByXuiddownload(string XUID)
        {

            string GetUserByXuid = new WebClient().DownloadString(config.Global.GetUserByXuid + XUID);
            string GamerScoreS = new WebClient().DownloadString(config.Global.GamerScoreS);
            string profilepictures = new WebClient().DownloadString(config.Global.profilepictures);
            string gamertag = new WebClient().DownloadString(config.Global.gamertag);
            string AccountTier = new WebClient().DownloadString(config.Global.AccountTier);
            string XboxOneRep = new WebClient().DownloadString(config.Global.XboxOneRep);
            string Bio = new WebClient().DownloadString(config.Global.Bio);
            string download = null;

            try
            {
                download = GetUserByXuid;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Profile Info");
                    return;
                }

                if (GamerScoreS == "") { GamerScoreS = "Unknown"; }
                if (profilepictures == "") { profilepictures = "Unknown"; }
                if (gamertag == "") { gamertag = "Unknown"; }
                if (AccountTier == "") { AccountTier = "Unknown"; }
                if (XboxOneRep == "") { XboxOneRep = "Unknown"; }
                if (Bio == "") { Bio = "Unknown"; }

                Embed.AddField("GamerScore:", GamerScoreS, true);
                Embed.AddField("XUID:", (XUID, "HEX:", Tools.StringToHex(XUID)), true);
                Embed.WithImageUrl(profilepictures);
                Embed.AddField("gamertag:", gamertag, true);
                Embed.AddField("AccountTier:", AccountTier, true);
                Embed.AddField("XboxOneRep:", XboxOneRep, true);
                Embed.AddField("Bio:", Bio, true);
                Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("RandomGamerTag")]
        public async Task randomGamertag()
        {

            try {
                /*Get Cpukey & APIKey*/
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                /*
                 {
                    "algorithm": 1,
                    "count": 3,
                    "seed": "",
                    "locale": "en-US"
                 }                 
                /*Post Request*/
                var post = 
                    "{\"algorithm\": \"1\",  " +
                    "\"count\": \"5\",  " +
                    "\"seed\": \"\",  " +
                    "\"locale\": \"en-US\"}";

                var json2 = JsonSerializer.Serialize(post);
                /*Get Random GamerTag*/
                var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.RandomGamerTag, post, GetApikey, config.Global.httpRequestA = false);
                string json = OpenXblHttp.RestClient.strResponseValue;
                var doc = JsonDocument.Parse(json);
                var Rgamertag = doc.RootElement.GetProperty("Gamertags");

                Embed.AddField("Random GamerTag(s):", Rgamertag, true);
                Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }
        [Command("XboxProfile")]
        public async Task xboxprofiledownload(string profile)
        {

            string xboxprofile = new WebClient().DownloadString(config.Global.xboxprofile + profile);
            string GamerScoreS = new WebClient().DownloadString(config.Global.xboxprofileGamerScore);
            string xuids = new WebClient().DownloadString(config.Global.xuids);
            string profilepictures = new WebClient().DownloadString(config.Global.xboxprofileprofilepictures);
            string gamertag = new WebClient().DownloadString(config.Global.xboxprofileGamertag);
            string AccountTier = new WebClient().DownloadString(config.Global.xboxprofileAccountTier);
            string XboxOneRep = new WebClient().DownloadString(config.Global.xboxprofileXboxOneRep);
            string Bio = new WebClient().DownloadString(config.Global.xboxprofileBio);
            string tenurelevel = new WebClient().DownloadString(config.Global.tenurelevel);
            string download = null;

            try
            {
                download = xboxprofile;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Profile Info");
                    return;
                }

                if (GamerScoreS == "") { GamerScoreS = "Unknown"; }
                if (xuids == "") { xuids = "Unknown"; }
                if (profilepictures == "") { profilepictures = "Unknown"; }
                if (gamertag == "") { gamertag = "Unknown"; }
                if (AccountTier == "") { AccountTier = "Unknown"; }
                if (XboxOneRep == "") { XboxOneRep = "Unknown"; }
                if (Bio == "") { Bio = "Unknown"; }
                if (tenurelevel == "") { tenurelevel = "Unknown"; }

                Embed.AddField("GamerScore:", GamerScoreS, true);
                Embed.AddField("XUID:", (xuids, "HEX:", Tools.StringToHex(xuids)), true);
                Embed.WithImageUrl(profilepictures);
                Embed.AddField("gamertag:", gamertag, true);
                Embed.AddField("AccountTier:", AccountTier, true);
                Embed.AddField("XboxOneRep:", XboxOneRep, true);
                Embed.AddField("Bio:", Bio, true);
                Embed.AddField("Tenurelevel:", tenurelevel, true);
                Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }
    }
}
