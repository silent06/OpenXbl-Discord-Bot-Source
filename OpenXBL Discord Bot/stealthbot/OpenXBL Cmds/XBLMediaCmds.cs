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
    public class XBLMediaCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();
        EmbedBuilder Embed2 = new EmbedBuilder();
        [Command("GameCLips")]
        public async Task GameCLipsdownload()
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string Clipdownload = new WebClient().DownloadString(config.Global.ClipListdownload + "&APIKEY=" + GetApikey);
            string UserString = config.Global.ClipUserString;

            string download = null;

            try
            {
                download = Clipdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (UserString == "") { UserString = "Server isn't responding! try again"; }
                    Embed.AddField("Find Complete list here:", $"[LiFeOfAGaMeR]({UserString})");
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
            catch (Exception)
            {
                await ReplyAsync("server is offline");
                //await Context.Channel.SendMessageAsync(ex.Message);/*Use for Debugging*/
            }

        }

        [Command("GameclipsByGamerTag")]
        public async Task GameclipsByXUIDdownload(string gamertag)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string xboxprofile = new WebClient().DownloadString(config.Global.xboxprofile + gamertag); /*Download Gamer Profile save to local drive in json*/
            string xuid = new WebClient().DownloadString(config.Global.xuids);  /*Fetch XUID from new json file*/
            string GameclipsByXUIDdownload = new WebClient().DownloadString(config.Global.GameclipsByXUIDdownload + xuid + "&APIKEY=" + GetApikey + "&ACHXUID=" + xuid);
            string GameclipsByXUIDUserString = config.Global.GameclipsByXUIDUserString + xuid;

            string download = null;

            try
            {
                download = GameclipsByXUIDdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    Embed.AddField("Clip List for ", gamertag);
                    Embed.AddField("Find Complete list here:", $"[LiFeOfAGaMeR]({GameclipsByXUIDUserString})");
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
            catch (Exception)
            {
                await ReplyAsync("server is offline");
                //await Context.Channel.SendMessageAsync(ex.Message);/*Use for Debugging*/
            }

        }

        [Command("Screenshots")]
        public async Task Screenshots()
        {

            try
            {

                string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
                string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
                string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
                var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.GetScreenshots, "null", GetApikey, config.Global.httpRequestA = true);

                string json = OpenXblHttp.RestClient.strResponseValue;
                var obj = JObject.Parse(json);

                if (CheckApiKey == "APIKEY Already in database")
                {

                    Embed.WithDescription("Recent Screenshots:");

                    var gametitle = "";
                    var titleId = "";
                    var captureDate = "";
                    var screenshot = "";

                    foreach (var dataItem in obj["values"])
                    {


                        gametitle = dataItem["titleName"].Value<string>();
                        titleId = dataItem["titleId"].Value<string>();
                        captureDate = dataItem["captureDate"].Value<string>();

                        Embed.AddField("Title:", gametitle, true);
                        Embed.AddField("TitleId:", titleId, true);
                        Embed.AddField("CaptureDate:", captureDate, true);
                    }

                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                    foreach (var dataItem in obj["values"])
                    {

                        screenshot = dataItem["contentLocators"][0]["uri"].Value<string>();

                        Embed2.WithImageUrl(screenshot.ToString());
                        await Context.Channel.SendMessageAsync("", false, Embed2.Build());
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
            catch (Exception)
            {

                await ReplyAsync("server is offline");

                //await Context.Channel.SendMessageAsync(ex.Message);/*Use for Debugging*/
            }

        }



    }
}
