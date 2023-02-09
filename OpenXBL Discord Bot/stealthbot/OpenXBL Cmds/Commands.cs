using Discord;
using Discord.Commands;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Discord.Net;
using Discord.WebSocket;

namespace stealthbot
{
    public class Commands : ModuleBase<SocketCommandContext>
    {

        [Command("ChangeTrigger")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task ChangeTrigger([Remainder] string NewTrigger)
        {
            if (string.IsNullOrWhiteSpace(NewTrigger))
            {
                return;
            }

            config.Global.prefix = NewTrigger;
            await Context.Channel.SendMessageAsync($"**{Context.User}** Trigger Changed to: **{config.Global.prefix}** ");

        }

        [Command(".ban")]
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]
        public async Task BanAsync(IGuildUser user, [Remainder] string reason)
        {
            if (string.IsNullOrWhiteSpace(reason))
            {
                return;
            }

            var allBans = await Context.Guild.GetBansAsync();
            bool isBanned = allBans.Select(b => b.User).Where(u => u.Username == user.Username).Any();

            if (!isBanned)
            {
                var targetHighest = (user as SocketGuildUser).Hierarchy;
                var senderHighest = (Context.User as SocketGuildUser).Hierarchy;

                if (targetHighest < senderHighest)
                {
                    await Context.Guild.AddBanAsync(user);

                    await Context.Channel.SendMessageAsync($"**{Context.User}** Has Banned **{user.Username}** for ```{reason}```");

                    var dmChannel = await user.GetOrCreateDMChannelAsync();
                    await dmChannel.SendMessageAsync($"You were banned from **{Context.Guild.Name}** for ```{reason}```");
                }
            }
        }

        [Command("link"), Summary("register")]
        public async Task LinkCommand(string CPUKey)
        {
            try
            {
                EmbedBuilder Embed = new EmbedBuilder();

                ClientInfo Client = new ClientInfo();
                bool Exists = mysql.GetClientData(CPUKey, ref Client);

                if (Exists)
                {
                    if (Client.Discordid == "0")
                    {
                        Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                        Embed.WithFooter(config.Global.BotName);
                        if (CPUKey.Length != 32)
                        {
                            Embed.WithAuthor("Wrong CPUKey Length");
                            Embed.WithDescription($"Your CPU Key needs to be 32 characters long {Context.User.Mention}.");
                        }
                        else
                        {
                            Embed.WithAuthor("Succesfully Registered");
                            mysql.SaveUserData(Context.User.ToString(), Context.User.Mention, CPUKey);
                            Embed.AddField("Get your OpenXbl APIKey here:", "https://xbl.io", true);
                            Embed.WithDescription($"You have successfully linked the CPUKey to {Context.User.Mention}.\n Dont forget to link your API_KEY with : **{config.Global.prefix}AddApiKey API_KEY**.");
                        }
                    }
                    else
                    {

                        Embed.WithAuthor("CPUKey is already linked");
                        Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                        Embed.WithFooter(config.Global.BotName);

                        if (CPUKey.Length != 32)
                        {
                            Embed.WithAuthor("Wrong CPUKey Length");
                            Embed.WithDescription($"Your CPU Key needs to be 32 characters long {Context.User.Mention}.");
                        }
                        else
                        {
                            Embed.WithAuthor("CPUKey is already linked");
                            Embed.WithDescription($"CPUKey is already registered by another user {Context.User.Mention}.");
                        }
                    }

                }
                else
                {
                    Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                    Embed.WithFooter(config.Global.BotName);
                    if (CPUKey.Length != 32)
                    {
                        Embed.WithAuthor("Wrong CPUKey Length");
                        Embed.WithDescription($"Your CPU Key needs to be 32 characters long {Context.User.Mention}.");
                    }
                    else
                    {
                        Embed.WithAuthor("CPUKey Error");
                        Embed.WithDescription($"This CPUKey dosent exist on our database {Context.User.Mention}.");
                    }
                }
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }
        }

        [Command("ChangeApiKey")]
        public async Task ChangeApiKeyCommand(string ApiKey)
        {
            /*Get Cpukey & APIKey*/
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

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
                        mysql.ChangeXBLKey(CPUKey, ApiKey);
                        Embed.WithDescription($"{Context.User.Mention} Your ApiKey was Changed.");
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
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "Unable to Change Apikey :*(");/*Use for Debugging*/
            }
        }

        [Command("DeleteApiKey")]
        public async Task DeleteApiKeyCommand()
        {
            /*Get Cpukey & APIKey*/
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string GetApikey = new WebClient().DownloadString(config.Global.GetApikey + CPUKey);

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
                    mysql.DeleteXBLKey(CPUKey, GetApikey);
                    Embed.WithDescription($"{Context.User.Mention} Your ApiKey has been deleted.");
                    Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                    await Context.Channel.SendMessageAsync("", false, Embed.Build());

                }
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "Unable to Delete ApiKey :*(");/*Use for Debugging*/
            }
        }

        [Command("AddApiKey")]
        public async Task AddApiKeyCommand(string AddApiKey)
        {
            string CPUKey = new WebClient().DownloadString(config.Global.CPUKey + "<@!" + Context.User.Id + ">");
            string CheckApiKey = new WebClient().DownloadString(config.Global.CheckApiKey + CPUKey);

            OpenXBL getxbldata = new OpenXBL();
            getxbldata.APIKEY = AddApiKey;
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

                        Embed.AddField("Your Already in the Database! Only 1 ApiKey at a time. thanks!", getxbldata.APIKEY, true);
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());

                    }
                    else if (CheckApiKey == "Not Registered")
                    {
                        mysql.AddXBLkey(CPUKey, ref getxbldata);
                        Embed.AddField("API_KEY Added:", AddApiKey, true);
                        Embed.AddField("Type ", $"{config.Global.prefix}Account to confirm your registry completion!", true);
                        Embed.WithThumbnailUrl(Context.User.GetAvatarUrl() ?? Context.User.GetDefaultAvatarUrl());
                        await Context.Channel.SendMessageAsync("", false, Embed.Build());
                    }                 

                }
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "Unable to Add ApiKey!");/*Use for Debugging*/
            }
        }

        [Command("AddCPUKEY"), Summary("AddCPUKEY")]
        public async Task AddCPUKEYCommand(string CPUKey)
        {
            try
            {
                EmbedBuilder Embed = new EmbedBuilder();
                ClientInfo Client = new ClientInfo();
                bool HasDiscordId = mysql.GetFriendsCPUKey(Context.User.ToString(), ref Client);         

                if (!HasDiscordId)
                {
                    bool Exists = mysql.GetClientData(CPUKey, ref Client);
                    if (!Exists)
                    {
                        Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                        Embed.WithFooter(config.Global.BotName);
                        if (CPUKey.Length != 32)
                        {
                            Embed.WithAuthor("Wrong CPUKey Length");
                            Embed.WithDescription($"Your CPU Key needs to be 32 characters long {Context.User.Mention}.");
                        }
                        else
                        {
                            Embed.WithAuthor("Succesfully Added");
                            mysql.AddCPUKEY(CPUKey);
                            Embed.WithDescription($"You have successfully Added the CPUKey to {Context.User.Mention}.");
                        }

                    }
                    else
                    {
                        Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
                        Embed.WithFooter(config.Global.BotName);
                        if (CPUKey.Length != 32)
                        {
                            Embed.WithAuthor("Wrong CPUKey Length");
                            Embed.WithDescription($"Your CPU Key needs to be 32 characters long {Context.User.Mention}.");
                        }
                        else
                        {
                            Embed.WithAuthor("CPUKey Error");
                            Embed.WithDescription($"This CPUKey Already exist on our database {Context.User.Mention}.");
                        }

                    }
                }
                else
                {
                    Embed.WithAuthor("Error");
                    Embed.WithDescription($"Your Already Registered Fucker!! {Context.User.Mention}.");

                }
                await Context.Channel.SendMessageAsync("", false, Embed.Build());
            }
            catch (Exception ex)
            {
                await Context.Channel.SendMessageAsync(config.Global.debug ? ex.Message : "server is offline");/*Use for Debugging*/
            }
        }

    }
}
