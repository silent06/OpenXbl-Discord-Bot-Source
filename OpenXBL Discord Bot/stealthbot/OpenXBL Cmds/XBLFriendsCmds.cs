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
    public class XBLFriendsCmds : ModuleBase<SocketCommandContext>
    {
        
        EmbedBuilder Embed = new EmbedBuilder();

        [Command("AddFriend")]
        public async Task AddFriendCommand(string AddFriend)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.XboxProfileSearch + AddFriend, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");
            ClientInfo Client = new ClientInfo();
            bool Exists = mysql.GetClientData(CPUKey, ref Client);
            // OpenXBL getxbldata = new OpenXBL();
            EmbedBuilder Embed = new EmbedBuilder();
            try
            {
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
                        string AddFriendWebRequest = new WebClient().DownloadString(config.Global.AddFriend + xuid + "/" + "&APIKEY=" + GetApikey);
                        Embed.AddField("Friend Added:", AddFriend, true);
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
                await Context.Channel.SendMessageAsync(ex.Message);
            }
        }

        [Command("RemoveFriend")]
        public async Task RemoveCommand(string RemoveFriend)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.XboxProfileSearch + RemoveFriend, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");

            ClientInfo Client = new ClientInfo();
            bool Exists = mysql.GetClientData(CPUKey, ref Client);

            EmbedBuilder Embed = new EmbedBuilder();
            try
            {
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
                        string AddFriendWebRequest = new WebClient().DownloadString(config.Global.RemoveFriend + xuid + "/" + "&APIKEY=" + GetApikey);
                        Embed.AddField("Friend Removed:", RemoveFriend, true);
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
            catch (Exception)
            {
                //await Context.Channel.SendMessageAsync(ex.Message);
                await ReplyAsync("server is offline");
            }
        }


        /*Need to make friendlist php Web Html*/
        [Command("Requestfriendslist")]
        public async Task friendslistdownload(string gamertag)
        {
            /*Get Keys*/
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);


            /*Get XUID*/
            var httpResponse = OpenXblHttp.RestClient.makeRequestAsync(config.Global.XboxProfileSearch + gamertag, "null", GetApikey, config.Global.httpRequestA = true);
            string json = OpenXblHttp.RestClient.strResponseValue;
            var doc = JsonDocument.Parse(json);
            var xuid = doc.RootElement.GetProperty("profileUsers")[0].GetProperty("hostId");

            /*Get Friend Info*/
            string friendslistdownload = new WebClient().DownloadString(config.Global.friendslistdownload + xuid + "&APIKEY=" + GetApikey);
            string friendslist = new WebClient().DownloadString(config.Global.friendslist);
            string NumberOfFriends = new WebClient().DownloadString(config.Global.friendsNumberOfFriends);

            string download = null;

            try
            {
                download = friendslistdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download friendslist Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (friendslist == "") { friendslist = "Unknown"; }
                    Embed.AddField("Your Friends List(Shows up to 75):", friendslist, true);
                    Embed.AddField("Total Friends", NumberOfFriends, true);
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
            catch (Exception)
            {
                await ReplyAsync("server is offline");
                //await Context.Channel.SendMessageAsync(ex.Message);/*Use for Debugging*/
            }

        }

        [Command("friendslist")]
        public async Task friendslistdownload()
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string friendslistdownload = new WebClient().DownloadString(config.Global.Userfriendslistdownload + GetApikey);
            string friendslist = new WebClient().DownloadString(config.Global.Userfriendslist);
            string NumberOfFriends = new WebClient().DownloadString(config.Global.NumberOfFriends);

            string download = null;

            try
            {
                download = friendslistdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download friendslist Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (friendslist == "") { friendslist = "Unknown"; }
                    Embed.AddField("Current Friend List(Shows up to 75):", friendslist, true);
                    Embed.AddField("Total Friends", NumberOfFriends, true);
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
            catch (Exception)
            {
                await ReplyAsync("server is offline");
                //await Context.Channel.SendMessageAsync(ex.Message);/*Use for Debugging*/
            }

        }
    }
}
