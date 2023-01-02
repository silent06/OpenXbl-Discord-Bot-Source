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
    public class XBLClubsCmds : ModuleBase<SocketCommandContext>
    {
        
        EmbedBuilder Embed = new EmbedBuilder();

        [Command("ClubsIOwn")]
        public async Task ClubsIOwn()
        {

            try
            {
                /*Get Cpukey & APIKey*/
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                /*Get XUID*/
                var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
                string json = OpenXblHttp.RestClient.strResponseValue;
                var doc = JsonDocument.Parse(json);
                var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");

                /*Get Club WebClient Info*/
                string ClubsIOwndownload = new WebClient().DownloadString(config.Global.ClubsIOwndownload + xuid + "&APIKEY=" + GetApikey);
                string UserXUIDString = config.Global.ClubUserXUIDString + xuid + "&CPUKEYForStats=" + CPUKey;

                /*Get Remaining Clubs info*/
                var httpResponses = OpenXblHttp.RestClient.makeRequestAsync(config.Global.ClubOwned, "null", GetApikey, config.Global.httpRequestA = true);
                string json2 = OpenXblHttp.RestClient.strResponseValue;
                var docs = JsonDocument.Parse(json2);
                var remainingClubs = docs.RootElement.GetProperty("remainingOpenAndClosedClubs");
                var secretClubsRemaining = docs.RootElement.GetProperty("remainingSecretClubs");

                string download = null;
                download = ClubsIOwndownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {

                    if (UserXUIDString == "") { UserXUIDString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete List here:", $"[LiFeOfAGaMeR]({UserXUIDString})");
                    Embed.WithImageUrl(config.Global.EmbededImage);
                    Embed.AddField("Clubs Remaining:", remainingClubs + ".");
                    Embed.AddField("Secret Clubs Remaining:", secretClubsRemaining + ".");
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

        [Command("ClubsSummary")]
        public async Task ClubsSummary(string clubid)
        {
            try
            {
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                /*Get XUID*/
                var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.CheckXBLAccount, "null", GetApikey, config.Global.httpRequestA = true);
                string json = OpenXblHttp.RestClient.strResponseValue;
                var doc = JsonDocument.Parse(json);
                var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");

                /*Get Club WebClient Info*/
                string ClubSummaryDl = new WebClient().DownloadString(config.Global.ClubSummaryDL + clubid + "&ACHXUID=" + xuid + "&APIKEY=" + GetApikey);
                string UserXUIDString = config.Global.ClubSummaryUserString + xuid;

                string download = null;

                download = ClubSummaryDl;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {

                    if (UserXUIDString == "") { UserXUIDString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete list here:", $"[LiFeOfAGaMeR]({UserXUIDString})");
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

        /*Doesn't Work */
        [Command("ClubCreate")]
        public async Task ClubCreate(string clubName, string Clubtype)
        {
            //Payload: {"name":"Hello World", "type":"[public/private/hidden]"}
            try
            {
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                //string Clubtype = "open";
                //var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.ClubCreate, "null", GetApikey, httpRequestA = true);
                /*Post Request*/
                var user = "{\"name\":" + "\"  " + clubName + "\"," +
                            "\"type\":" + "\"  " + Clubtype + "\",}";

                /*string user = "{\"name\":\"silentguy\"," +
                                "\"type\":\"public\"}";*/
                var json2 = JsonSerializer.Serialize(user);
                var httpPost = OpenXblHttp.RestClient.makeRequestAsync(config.Global.ClubCreate, user, GetApikey, config.Global.httpRequestA = false);


                Embed.AddField("Club Created:", clubName);

                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }
        }

        [Command("SingleClubSearch")]
        public async Task SingleClubSearh(string clubName) {

            try
            {
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                if (CheckApiKey == "APIKEY Already in database")
                {
                    var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.ClubSearch + clubName, "null", GetApikey, config.Global.httpRequestA = true);

                    string json = OpenXblHttp.RestClient.strResponseValue;
                    var doc = JsonDocument.Parse(json);
                    var ClubName = doc.RootElement.GetProperty("results")[0].GetProperty("text");
                    var ClubId = doc.RootElement.GetProperty("results")[0].GetProperty("result").GetProperty("id");
                    var displayimage = doc.RootElement.GetProperty("results")[0].GetProperty("result").GetProperty("displayImageUrl");

                    Embed.AddField($"ClubName:", ClubName);
                    Embed.AddField($"ClubId:", ClubId);
                    Embed.WithImageUrl(displayimage.ToString());
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

        /*Needs work*/
        [Command("ClubSearch")]
        public async Task ClubSearh(string clubName)
        {

            try
            {
                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

                if (CheckApiKey == "APIKEY Already in database")
                {
                    var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.ClubSearch + clubName, "null", GetApikey, config.Global.httpRequestA = true);
                    string json = OpenXblHttp.RestClient.strResponseValue;
                    var obj = JObject.Parse(json);
                    var ClubName = "";
                    var ClubId = "";
                    var displayimage = "";
                    EmbedBuilder Embed2 = new EmbedBuilder();
                    Embed.WithDescription("Listing Found Clubs");
                    foreach (var dataItem in obj["results"])
                    {

                        ClubName = dataItem["text"].Value<string>();
                        ClubId = dataItem["result"]["id"].Value<string>();
                        displayimage = dataItem["result"]["displayImageUrl"].Value<string>();
                        Embed.AddField($"ClubName(ID: {ClubId}):", ClubName);
                        //Embed.AddField($"ClubId:", ClubId);
                        //Embed2.WithImageUrl(displayimage.ToString());
                        //await Context.Channel.SendMessageAsync("", false, Embed2.Build());
                    }
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

        [Command("ReserveClub")]
        public async Task ReserveClub(string ClubName)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string ReserveClub = new WebClient().DownloadString(config.Global.resverClub + ClubName + "&APIKEY=" + GetApikey);
            string download = null;
            try
            {
                download = ReserveClub;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (ReserveClub == "success")
                    {

                        Embed.AddField("Success! ClubName Reserved:", ClubName);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());

                    }
                    else
                    {

                        Embed.AddField("Falied to Reserve ClubName:", ClubName);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());

                    }

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
