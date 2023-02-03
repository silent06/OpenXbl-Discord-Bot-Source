using Discord;
using Discord.Commands;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;
using Discord.Net;
using Discord.WebSocket;
using stealthbot;

namespace stealthbot
{
    public class helpinfo: ModuleBase<SocketCommandContext>
    {
        [RequireContext(ContextType.Guild)]
        [RequireUserPermission(GuildPermission.Administrator)]


        [Command("GenerateRandomCPUKEY"), Summary("GenerateRandomCPUKEY info")]
        public async Task GenerateRandomCPUKEY()
        {
            int CPUKEY = 32;
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.AddField("CPUKEY:", (Tools.GetRandomHexNumber(CPUKEY)));
            Embed.WithDescription($"{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("ReserveClub"), Summary("ReserveClub info")]
        public async Task ReserveClubhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ReserveClub ClubName(For clubs with spaces use apostrophes) {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetPlayersACHGame"), Summary("GetPlayersACHGame info")]
        public async Task GetPlayersACHGamehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetPlayersACHGame gamertag titleid(For gamertags with spaces use apostrophes) {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetAchievementTitleHistory"), Summary("GetAchievementTitleHistory info")]
        public async Task GetAchievementTitleHistoryhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetAchievementTitleHistory titleID {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
        [Command("GetPlayerAchievementTitle"), Summary("GetPlayerAchievementTitle info")]
        public async Task GetPlayerAchievementTitlehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetPlayerAchievementTitle titleID {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
        [Command("GetPlayerAchievement"), Summary("GetPlayerAchievement info")]
        public async Task GetPlayerAchievementhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetPlayerAchievement gamertag(For gamertags with spaces use apostrophes) {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetFriendsPresence"), Summary("GetFriendsPresence info")]
        public async Task GetFriendsPresencehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetFriendsPresence Gamertag {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("SendXboxMSG"), Summary("SendXboxMSG info")]
        public async Task SendXboxMSGhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}SendXboxMSG MSG Gamertag(Message should be an apostrophe) {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("Requestfriendslist"), Summary("Requestfriendslist info")]
        public async Task Requestfriendslisthelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}Requestfriendslist gamertag {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("RemoveFriend"), Summary("RemoveFriend info")]
        public async Task RemoveFriendhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}RemoveFriend gamertag {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("AddFriend"), Summary("AddFriend info")]
        public async Task AddFriendhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}AddFriend gamertag {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("XboxProfile"), Summary("XboxProfile info")]
        public async Task XboxProfilehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}XboxProfile GamerTag(For gamertags with spaces use apostrophes) {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetUserByXuid"), Summary("GetUserByXuid info")]
        public async Task GetUserByXuidhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetUserByXuid XUID {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("DeleteGameClip"), Summary("DeleteGameClip info")]
        public async Task DeleteGameCliphelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}DeleteGameClip ClipId {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GameclipsByGamerTag"), Summary("info")]
        public async Task GameclipsByXUIDhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GameclipsByXUID GamerTag {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetAchievementStats"), Summary("GetAchievementStats info")]
        public async Task GetAchievementStatshelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetAchievementStats titleId {Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("GetFriendsAchStats"), Summary("GetFriendsAchStats info")]
        public async Task GetFriendsAchStatshelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}GetFriendsAchStats titleId FulldiscordName(they must be registered in Database){Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("ClubCreate"), Summary("ClubCreate info")]
        public async Task ClubCreatehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ClubCreate ClubName Clubtype{Context.User.Mention}.");
            Embed.AddField("ClubCreate Instructions:", "There are 3 Club-types: Hidden, Private, Open. Hidden is 3, Private 2, Open 1, So club type should be enter as an integer of 1, 2 or 3");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("ClubsSummary"), Summary("ClubsSummary info")]
        public async Task ClubsSummaryhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ClubsSummary clubId{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
        
        [Command("ClubSearch"), Summary("ClubSearch info")]
        public async Task ClubSearchhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ClubSearch ClubName{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }
        
        [Command("ClubDelete"), Summary("ClubDelete info")]
        public async Task ClubDeletehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ClubDelete ClubName{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("ClubInvite"), Summary("ClubInvite info")]
        public async Task ClubInvitehelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}ClubInvite ClubName GamerTag{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("SingleClubSearch"), Summary("SingleClubSearch info")]
        public async Task SingleClubSearchhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}SingleClubSearch ClubName{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("PostActivityFeed"), Summary("PostActivityFeed info")]
        public async Task PostActivityFeedhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use Enter {config.Global.prefix}PostActivityFeed message{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("ChangeApiKey"), Summary("ChangeApiKey info")]
        public async Task ChangeApiKeyFeedhelp()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription($"To use whisper to the bot {config.Global.prefix}ChangeApiKey ApiKey{Context.User.Mention}.");
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("help"), Summary("help info")]
        public async Task help()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(config.Global.RGB1, config.Global.RGB2, config.Global.RGG3);
            Embed.WithDescription(
                $"Commands: " +
                $"\n{config.Global.prefix}account, " +
                $"\n{config.Global.prefix}RandomGamerTag, " +
                $"\n{config.Global.prefix}Screenshots, " +
                $"\n{config.Global.prefix}GameCLips, " +
                $"\n{config.Global.prefix}DeleteGameClip, " +
                $"\n{config.Global.prefix}GameclipsByGamerTag, " +
                $"\n{config.Global.prefix}presence, " +
                $"\n{config.Global.prefix}GetUserByXuid, " +
                $"\n{config.Global.prefix}XboxProfile, " +
                $"\n{config.Global.prefix}AddFriend, " +
                $"\n{config.Global.prefix}RemoveFriend, " +
                $"\n{config.Global.prefix}Requestfriendslist, " +
                $"\n{config.Global.prefix}friendslist, " +
                $"\n{config.Global.prefix}SendXboxMSG, " +
                $"\n{config.Global.prefix}XboxMessages, " +
                $"\n{config.Global.prefix}XboxMessageRequests, " +
                $"\n{config.Global.prefix}PlayerSummary, " +
                $"\n{config.Global.prefix}GetFriendsPresence, " +
                $"\n{config.Global.prefix}Achievements, " +
                $"\n{config.Global.prefix}GetPlayerAchievement, " +
                $"\n{config.Global.prefix}GetAchievementStats, " +
                $"\n{config.Global.prefix}GetPlayerAchievementTitle, " +
                $"\n{config.Global.prefix}GetAchievementTitleHistory, " +
                $"\n{config.Global.prefix}GetPlayersACHGame, " +
                $"\n{config.Global.prefix}GetFriendsAchStats, " +
                $"\n{config.Global.prefix}RecentPlayers, " +
                $"\n{config.Global.prefix}ReserveClub, " +
                $"\n{config.Global.prefix}ClubDelete, " +
                $"\n{config.Global.prefix}ClubCreate, " +
                $"\n{config.Global.prefix}ClubsIOwn, " +
                $"\n{config.Global.prefix}ClubsSummary, " +
                $"\n{config.Global.prefix}ClubSearch, " +
                $"\n{config.Global.prefix}ClubInvite, " +
                $"\n{config.Global.prefix}SingleClubSearch, " +
                $"\n{config.Global.prefix}ActivityFeed, " +
                $"\n{config.Global.prefix}ActivityHistory, " +
                $"\n{config.Global.prefix}PostActivityFeed, " +
                $"\n{config.Global.prefix}AddCPUKEY, " +
                $"\n{config.Global.prefix}AddApiKey,  " +
                $"\n{config.Global.prefix}link, " +
                $"\n{config.Global.prefix}DeleteApiKey, " +
                $"\n{config.Global.prefix}ChangeApiKey " +
                $"\n{Context.User.Mention}."
                ); 
            Embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            Embed.AddField("Need CPUKEY? To use bot?", $"Enter **{config.Global.prefix}GenerateRandomCPUKEY**", true);
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

    }
}
