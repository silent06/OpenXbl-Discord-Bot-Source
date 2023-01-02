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
    public class XBLActivityCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();

        [Command("ActivityFeed")]
        public async Task ActivityFeeddownload()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var gamertag = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[2].GetProperty("value");
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
            string ActivityFeeddownload = new WebClient().DownloadString(config.Global.ActivityFeedDL + GetApikey + "&ACHXUID=" + xuid);
            string ActivityUserString = config.Global.ActivityFeedUserString + xuid;
            string NumberOfPosts = new WebClient().DownloadString(config.Global.NumberoFPosts + xuid);
            string download = null;

            try
            {
                download = ActivityFeeddownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    Embed.AddField("Activity Feed for: ", gamertag);
                    Embed.AddField("Number of Posts: ", NumberOfPosts);
                    Embed.AddField("Activity Feed here:", $"[LiFeOfAGaMeR]({ActivityUserString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
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

        [Command("ActivityHistory")]
        public async Task ActivityHistorydownload()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var gamertag = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[2].GetProperty("value");
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
            string ActivityHistorydownload = new WebClient().DownloadString(config.Global.ActivityHistoryDL + GetApikey + "&ACHXUID=" + xuid);
            string HistoryUserString = config.Global.ActivityHistoryUserString + xuid;
            string NumberOfPosts = new WebClient().DownloadString(config.Global.NumberoFPostsh + xuid);

            string download = null;

            try
            {
                download = ActivityHistorydownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    Embed.AddField("Activity Feed for: ", gamertag);
                    Embed.AddField("Total Posts: ", NumberOfPosts);
                    Embed.AddField("Find Complete list here:", $"[LiFeOfAGaMeR]({HistoryUserString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
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

        [Command("PostActivityFeed")]
        public async Task PostActivityFeeddownload(string message)
        {
            string strResponseValue = string.Empty;
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var gamertag = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("settings")[2].GetProperty("value");

            /*Post Request*/
            var user = "{\"message\":" + "\"  " + message + "\"}";
            var json2 = JsonSerializer.Serialize(user);
            var httpPost = OpenXblHttp.RestClient.makeRequestAsync("activity/feed", user, GetApikey, config.Global.httpRequestA = false);

            try
            {

                if (CheckApiKey == "APIKEY Already in database")
                {
                    Embed.AddField("Activity Posted for: ", gamertag);
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
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
