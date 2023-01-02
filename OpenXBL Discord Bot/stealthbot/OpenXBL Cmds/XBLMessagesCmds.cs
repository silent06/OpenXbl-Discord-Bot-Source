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
    public class XBLMessagesCmds : ModuleBase<SocketCommandContext>
    {

        EmbedBuilder Embed = new EmbedBuilder();

        [Command("SendXboxMSG")]
        public async Task SendXboxMSG(string MSG, string GamerTag)
        {
            string xboxprofile = new WebClient().DownloadString(config.Global.xboxprofile + GamerTag); /*Download Gamer Profile save to local drive in json*/
            string xuid = new WebClient().DownloadString(config.Global.xuids);  /*Fetch XUID from new json file*/
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");/*Fetch CPUKEY*/
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);/*Get APIKey to submit to sendmsg link*/
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);/*Check if API is already register or not*/
            string sendmsg = new WebClient().DownloadString(config.Global.sendmsg + MSG + "&sendaconversationXUID=" + xuid + "&APIKEY=" + GetApikey);/*Send MSG*/

            try
            {

                if (CheckApiKey == "APIKEY Already in database")
                {


                    if (sendmsg == "success")
                    {

                        Embed.AddField("Message Status:", sendmsg, true);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }
                    else
                    {

                        Embed.AddField("Message Status:", "Failed!", true);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }

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
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("XboxMessages")]
        public async Task Conversationslist()
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string Conversationslistdownload = new WebClient().DownloadString(config.Global.Conversationslistdownload + GetApikey);
            string Conversations = new WebClient().DownloadString(config.Global.Conversations);
            string NumberOfConversations = new WebClient().DownloadString(config.Global.NumberOfConversations);
            string UnreadConversations = new WebClient().DownloadString(config.Global.UnreadConversations);

            string download = null;

            /*Split DownloadString*/
            string[] lines = Conversations.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );
            try
            {
                download = Conversationslistdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Xbox MSG Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (Conversations == "") { Conversations = "Unknown"; }

                    Embed.AddField("MSG1:", lines[0], true);
                    Embed.AddField("MSG2:", lines[1], true);
                    Embed.AddField("MSG3:", lines[2], true);
                    Embed.AddField("Total XboxMessages:", NumberOfConversations, true);
                    Embed.AddField("UnRead XboxMessages:", UnreadConversations, true);
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
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

        [Command("XboxMessageRequests")]
        public async Task XboxMessageRequestslist()
        {

            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);
            string XboxMessageRequestsdownload = new WebClient().DownloadString(config.Global.RequestsConversationslistdownload + GetApikey);
            string RequestsConversations = new WebClient().DownloadString(config.Global.RequestsConversations);
            string RequestsNumberOfConversations = new WebClient().DownloadString(config.Global.RequestsNumberOfConversations);
            string RequestsUnreadConversations = new WebClient().DownloadString(config.Global.RequestsUnreadConversations);

            string download = null;

            /*Split DownloadString*/
            string[] lines = RequestsConversations.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None
                );
            try
            {
                download = XboxMessageRequestsdownload;

                // check if download was successful
                if (download == null)
                {
                    await ReplyAsync("Failed to download Xbox MSG Info");
                    return;
                }

                if (CheckApiKey == "APIKEY Already in database")
                {
                    if (RequestsConversations == "") { RequestsConversations = "Unknown"; }

                    Embed.AddField("MSG1:", lines[0], true);
                    Embed.AddField("MSG2:", lines[1], true);
                    Embed.AddField("MSG3:", lines[2], true);
                    Embed.AddField("Total Requests:", RequestsNumberOfConversations, true);
                    Embed.AddField("UnRead Requests:", RequestsUnreadConversations, true);
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
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }

        }

    }
}
