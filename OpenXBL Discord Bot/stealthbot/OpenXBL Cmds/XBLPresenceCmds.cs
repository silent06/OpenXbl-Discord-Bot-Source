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
    public class XBLPresenceCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();

        [Command("PlayerSummary")]
        public async Task PlayerSummarydownload()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string PlayerSummarydownload = new WebClient().DownloadString(config.Global.PlayerSummarydownload + GetApikey);
            string GamerScoreS = new WebClient().DownloadString(config.Global.PlayerSummaryGamerScoreS);
            string profilepictures = new WebClient().DownloadString(config.Global.PlayerSummaryprofilepictures);
            string gamertag = new WebClient().DownloadString(config.Global.PlayerSummarygamertag);
            string xuids = new WebClient().DownloadString(config.Global.PlayerSummaryxuid);
            string XboxOneRep = new WebClient().DownloadString(config.Global.PlayerSummaryXboxOneRep);
            string presenceState = new WebClient().DownloadString(config.Global.PlayerSummarypresenceState);
            string presenceText = new WebClient().DownloadString(config.Global.PlayerSummarypresenceText);
            string presenceDevices = new WebClient().DownloadString(config.Global.PlayerSummarypresenceDevices);
            string download = null;

            try
            {
                download = PlayerSummarydownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Profile Info");
                    return;
                }

                if (GamerScoreS == "") { GamerScoreS = "Unknown"; }
                if (profilepictures == "") { profilepictures = "Unknown"; }
                if (gamertag == "") { gamertag = "Unknown"; }
                if (XboxOneRep == "") { XboxOneRep = "Unknown"; }
                if (xuids == "") { xuids = "Unknown"; }
                if (presenceState == "") { presenceState = "Unknown"; }
                if (presenceText == "") { presenceText = "Unknown"; }
                if (presenceDevices == "") { presenceDevices = "Unknown"; }

                Embed.AddField("GamerScore:", GamerScoreS, true);
                Embed.AddField("XUID:", (xuids, "HEX:", Tools.StringToHex(xuids)), true);
                Embed.WithImageUrl(profilepictures);
                Embed.AddField("Gamertag:", gamertag, true);

                Embed.AddField("XboxOneRep:", XboxOneRep, true);
                Embed.AddField("Presence State:", presenceState, true);
                Embed.AddField("Presence Text:", presenceText, true);
                Embed.AddField("Presence Devices:", presenceDevices, true);
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("Presence")]
        public async Task PresenceCommand()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string Presencedownload = new WebClient().DownloadString(config.Global.Presencedownload + GetApikey);
            string presence = new WebClient().DownloadString(config.Global.presence + CPUKey);
            string download = null;

            try
            {

                download = Presencedownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Xbox Info");
                    return;
                }


                if (CPUKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your cpukey with : **{config.Global.prefix}link CPUKey**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                else
                {

                    if (CheckApiKey == "APIKEY Already in database")
                    {

                        Embed.AddField("Friends Presence(showing up to 30):", presence + "", true);
                        Embed.AddField("Find Complete list here:", $"[LiFeOfAGaMeR]({config.Global.presencelink + CPUKey})");
                        Embed.WithImageUrl(config.Global.EmbededImage);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }
                    else if (CheckApiKey == "Not Registered")
                    {
                        Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                        Embed.WithAuthor("Register yourself https://xbl.io then Message the bot to link key ");
                        Embed.WithFooter(config.Global.BotName);
                        Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }

                }
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }
        }

        [Command("GetFriendsPresence")]
        public async Task GetFriendsPresenceCommand(string gamertag)
        {
            /*Get API & CPU Key info*/
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

            /*Get XUID*/
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.XboxProfileSearch + gamertag, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");

            /*Download Friends Presence Info*/
            string Presencedownload = new WebClient().DownloadString(config.Global.GetFriendsPresencedownload + xuid + "/presence/" + "&ACHXUID=" + xuid + "&APIKEY=" + GetApikey);
            string presence = new WebClient().DownloadString(config.Global.GetFriendsPresence + xuid);
            string download = null;

            try
            {

                download = Presencedownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Xbox Info");
                    return;
                }


                if (CPUKey == "Not Registered")
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithAuthor("Register yourself");
                    Embed.WithFooter(config.Global.BotName);
                    Embed.WithDescription($"Sorry {Context.User.Mention} \n you need to link your cpukey with : **{config.Global.prefix}link CPUKey**.");
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());
                }
                else
                {

                    if (CheckApiKey == "APIKEY Already in database")
                    {
                        Embed.AddField("Friends GamerTag:", gamertag, true);
                        Embed.AddField("Friends Presence:", presence, true);
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
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }
        }

        [Command("RecentPlayers")]
        public async Task RecentPlayersdownload()
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string RecentPlayersdownload = new WebClient().DownloadString(config.Global.RecentPlayersdownload + GetApikey);
            string RecentPlayers = new WebClient().DownloadString(config.Global.RecentPlayers);
            string download = null;
            try
            {
                download = RecentPlayersdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Achievement Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (RecentPlayers == "") { RecentPlayers = "Unknown"; }

                    Embed.AddField("Recent Players:", RecentPlayers);
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
