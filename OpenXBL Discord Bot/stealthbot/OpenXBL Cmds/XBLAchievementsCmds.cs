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
    public class XBLAchievementsCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();

        [Command("Achievements")]
        public async Task Achievementdownload()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string Achievementdownload = new WebClient().DownloadString(config.Global.Achievementdownload + "&APIKEY=" + GetApikey);
            string download = null;

            try
            {
                download = Achievementdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (config.Global.AchievementsUserString == "") { config.Global.AchievementsUserString = "Server isn't responding! try again"; }
                    else
                    {
                        Embed.AddField("Find Complete Achievement Game list here:", $"[LiFeOfAGaMeR]({config.Global.AchievementsUserString})");
                    }
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetPlayerAchievement")]
        public async Task AchievementListdownload(string gamertag)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string xboxprofile = new WebClient().DownloadString(config.Global.xboxprofile + gamertag); /*Download Gamer Profile save to local drive in json*/
            string xuid = new WebClient().DownloadString(config.Global.xuids);  /*Fetch XUID from new json file*/
            string GetPlayerAchievementListdownload = new WebClient().DownloadString(config.Global.GetPlayerAchievementListdownload + xuid + "&ACHXUID=" + xuid + "&APIKEY=" + GetApikey);
            string UserXUIDString = config.Global.UserXUIDString + xuid;
            string download = null;

            try
            {
                download = GetPlayerAchievementListdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserXUIDString == "") { UserXUIDString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete Achievement Game list here:", $"[LiFeOfAGaMeR]({UserXUIDString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetPlayerAchievementTitle")]
        public async Task GetPlayerAchievementTitledownload(string titleID)
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

            string GetPlayerAchievementTitle = new WebClient().DownloadString(config.Global.GetPlayerAchievementTitle + titleID + "&APIKEY=" + GetApikey);
            string UserString = config.Global.GetPlayerUserString;
            string download = null;

            try
            {
                download = GetPlayerAchievementTitle;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserString == "") { UserString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete Title Achievement list here:", $"[LiFeOfAGaMeR]({UserString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetFriendsAchStats")]
        public async Task GGetAchievementStatsdownload(string titleID, string clientName)
        {

            ClientInfo Client = new ClientInfo();
            mysql.GetFriendsCPUKey(clientName, ref Client);

            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + Client.CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + Client.CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
            string GetAchievementStats = new WebClient().DownloadString(config.Global.GetAchievementStatsD + titleID + "&APIKEY=" + GetApikey + "&ACHXUID=" + xuid);

            string UserString = config.Global.GetAchievementStatsUserString + Client.CPUKey;
            string download = null;

            try
            {
                download = GetAchievementStats;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserString == "") { UserString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete Achievement Stats here:", $"[LiFeOfAGaMeR]({UserString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetAchievementStats")]
        public async Task GGetAchievementStatsdownload(string titleID)
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
            string GetAchievementStats = new WebClient().DownloadString(config.Global.GetAchievementStatsD + titleID + "&APIKEY=" + GetApikey + "&ACHXUID=" + xuid);

            string UserString = config.Global.GetAchievementStatsUserString + CPUKey;
            string download = null;

            try
            {
                download = GetAchievementStats;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserString == "") { UserString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete Achievement Stats here:", $"[LiFeOfAGaMeR]({UserString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("GetAchievementTitleHistory")]
        public async Task GetAchievementTitleHistorydownload(string titleID)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetAchievementTitleHistorydownload = new WebClient().DownloadString(config.Global.GetAchievementTitleHistorydownload + titleID + "&APIKEY=" + GetApikey);
            string lastUnlock = new WebClient().DownloadString(config.Global.lastUnlock);
            //string titleId = new WebClient().DownloadString(config.Global.titleId);          
            string titleType = new WebClient().DownloadString(config.Global.titleType);
            string name = new WebClient().DownloadString(config.Global.GetAchievementTitleName);
            string earnedAchievements = new WebClient().DownloadString(config.Global.earnedAchievements);
            string currentGamerscore = new WebClient().DownloadString(config.Global.currentGamerscore);
            string maxGamerscore = new WebClient().DownloadString(config.Global.maxGamerscore);
            string rarityCategory1 = new WebClient().DownloadString(config.Global.rarityCategory1);
            string isRarestCategory = new WebClient().DownloadString(config.Global.isRarestCategory);
            string rarityCategory2 = new WebClient().DownloadString(config.Global.rarityCategory2);
            string isRarestCategory2 = new WebClient().DownloadString(config.Global.isRarestCategory2);
            string totalOfUnlocks2 = new WebClient().DownloadString(config.Global.totalOfUnlocks2);


            string download = null;

            try
            {
                download = GetAchievementTitleHistorydownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (lastUnlock == "") { lastUnlock = "Unknown"; }
                    //if (titleId == "") { titleId = "Unknown"; }
                    if (titleType == "") { titleType = "Unknown"; }
                    if (name == "") { name = "Unknown"; }
                    if (earnedAchievements == "") { earnedAchievements = "Unknown"; }
                    if (currentGamerscore == "") { currentGamerscore = "Unknown"; }
                    if (maxGamerscore == "") { maxGamerscore = "Unknown"; }
                    if (rarityCategory1 == "") { rarityCategory1 = "Unknown"; }
                    if (rarityCategory2 == "") { rarityCategory2 = "Unknown"; }
                    if (isRarestCategory == "") { isRarestCategory = "Unknown"; }
                    if (isRarestCategory2 == "") { isRarestCategory2 = "Unknown"; }
                    if (totalOfUnlocks2 == "") { totalOfUnlocks2 = "Unknown"; }


                    Embed.AddField("Last Achievement Unlocked? Date:", lastUnlock);
                    Embed.AddField("TitleId:", titleID);
                    Embed.AddField("TitleType:", titleType);
                    Embed.AddField("Game Name:", name);
                    Embed.AddField("Earned Achievements:", earnedAchievements);
                    Embed.AddField("Current Gamerscore:", currentGamerscore);
                    Embed.AddField("Max Gamerscore:", maxGamerscore);
                    Embed.AddField("Total to Unlock:", totalOfUnlocks2);
                    Embed.AddField("RarityCategory1:", rarityCategory1);
                    Embed.AddField("RarityCategory2:", rarityCategory2);
                    Embed.AddField("IsRarestCategory:", isRarestCategory);
                    Embed.AddField("IsRarestCategory2:", isRarestCategory2);
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        /*Get Another Player's specific title Achievement info*/
        [Command("GetPlayersACHGame")]
        public async Task achievementsanotherplayersgamedownload(string gamertag, string titleID)
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string xboxprofile = new WebClient().DownloadString(config.Global.xboxprofile + gamertag); /*Download Gamer Profile save to local drive in json*/
            string xuid = new WebClient().DownloadString(config.Global.xuids);  /*Fetch XUID from new json file*/
            string achievementsanotherplayersgamedownload = new WebClient().DownloadString(config.Global.achievementsanotherplayersgamedownload + xuid + "/title/" + titleID + "&ACHXUID=" + xuid + "&APIKEY=" + GetApikey);
            string UserXUIDString = config.Global.GetPlayersUserXUIDString + xuid;
            string download = null;

            try
            {
                download = achievementsanotherplayersgamedownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserXUIDString == "") { UserXUIDString = "Server isn't responding! try again"; }

                    Embed.AddField("Find Complete Achievement Game list here:", $"[LiFeOfAGaMeR]({UserXUIDString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
                else if (CheckApiKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself [Link]https://xbl.io then Message the bot to link key ");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }

            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

    }
}
