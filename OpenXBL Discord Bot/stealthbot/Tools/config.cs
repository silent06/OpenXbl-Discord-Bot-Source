using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace stealthbot
{


    class config
    {
        internal static class Global
        {
            internal static bool httpRequestA;
            internal static bool debug;

            //Bot Color
            internal static int RGB1 = 32;
            internal static int RGB2 = 82;
            internal static int RGG3 = 91;

            //Bot Settings
            internal static string BotName = "OpenXbl";
            internal static string prefix = "$";

            /*Your VPS URL*/
            internal static string VPSString = "http://" + OpenXBL.VPS + "/";

            /*OpenXbl URL strings*/
            internal static string CheckXBLAccount = "account";
            internal static string XboxProfileSearch = "friends/search?gt=";
            internal static string GetScreenshots = "dvr/screenshots";
            internal static string ClubOwned = "clubs/owned";
            internal static string ClubSearch = "clubs/find?q=";
            internal static string ClubCreate = "clubs/create";
            internal static string ClubSummary = "clubs/";

            /*OpenXbl Api Key*/
            internal static string OpebXblApiToken = "";

            /*Univesal stuff*/
            internal static string CPUKey = VPSString + "XBLIO/stealth/getcpukey.php?discordid=";
            internal static string CheckApiKey = VPSString + "XBLIO/stealth/getApiKey.php?CPUKEY=";
            internal static string GetApikey = VPSString + "XBLIO/stealth/getApiKey.php?APIKEY=";

            /*Embeded Images*/
            internal static string EmbededImage = "https://images.pexels.com/photos/3165335/pexels-photo-3165335.jpeg?cs=srgb&dl=pexels-lucie-liz-3165335.jpg&fm=jpg";

            /*ActivityFeed*/
            internal static string ActivityFeedDL = VPSString + "XBLIO/xbox.php?ActivityFeed=activity/feed&APIKEY=";
            internal static string ActivityFeedUserString = VPSString + "XBLIO/activity/?ActivityFeed&ACHXUID=";
            internal static string NumberoFPosts = VPSString + "XBLIO/activity/stats.php?NumberoFActivity&ACHXUID=";

            /*ActivityHistory*/
            internal static string ActivityHistoryDL = VPSString + "XBLIO/xbox.php?Activityhistory=activity/history&APIKEY=";
            internal static string ActivityHistoryUserString = VPSString + "XBLIO/activity/?ActivityHistory&ACHXUID=";
            internal static string NumberoFPostsh = VPSString + "XBLIO/activity/stats.php?NumberoFHistory&ACHXUID=";
              

            /*GameClips*/
            internal static string ClipListdownload = VPSString + "XBLIO/xbox.php?gameclips=dvr/gameclips";
            internal static string ClipUserString = VPSString + "XBLIO/gameclips/?gameclips";

            /*GameClips of Friends*/
            internal static string GameclipsByXUIDdownload = VPSString + "XBLIO/xbox.php?friendsgameclips=dvr/gameclips/?xuid=";
            internal static string GameclipsByXUIDUserString = VPSString + "XBLIO/gameclips/?gameclipsByXUID&ACHXUID=";

            /*AddFriend RemoveFriend*/
            internal static string AddFriend = VPSString + "XBLIO/xbox.php?AddFriend=friends/add/";
            internal static string RemoveFriend = VPSString + "XBLIO/xbox.php?RemoveFriend=friends/remove/";
            
            /*GetUserByXuid*/
            internal static string GetUserByXuid = VPSString + "XBLIO/xbox.php?userprofile=account/";
            internal static string GamerScoreS = VPSString + "XBLIO/xuidSearch.php?gamerscore";
            internal static string profilepictures = VPSString + "XBLIO/xuidSearch.php?profilepicture";
            internal static string gamertag = VPSString + "XBLIO/xuidSearch.php?gamertag";
            internal static string AccountTier = VPSString + "XBLIO/xuidSearch.php?AccountTier";
            internal static string XboxOneRep = VPSString + "XBLIO/xuidSearch.php?XboxOneRep";
            internal static string Bio = VPSString + "XBLIO/xuidSearch.php?Bio";

            /*XboxProfile*/
            internal static string xboxprofile = VPSString + "XBLIO/xbox.php?downloadinfo=friends/search?gt=";
            internal static string xboxprofileGamerScore = VPSString + "XBLIO/xbox.php?gamerscore";
            internal static string xuids = VPSString + "XBLIO/xbox.php?xuid";
            internal static string xboxprofileprofilepictures = VPSString + "XBLIO/xbox.php?profilepicture";
            internal static string xboxprofileGamertag = VPSString + "XBLIO/xbox.php?gamertag";
            internal static string xboxprofileAccountTier = VPSString + "XBLIO/xbox.php?AccountTier";
            internal static string xboxprofileXboxOneRep = VPSString + "XBLIO/xbox.php?XboxOneRep";
            internal static string xboxprofileBio = VPSString + "XBLIO/xbox.php?Bio";
            internal static string tenurelevel = VPSString + "XBLIO/xbox.php?tenurelevel";

            /*GetAchievementStats*/
            internal static string GetAchievementStatsD = VPSString + "XBLIO/xbox.php?achievementstats=achievements/stats/";
            internal static string GetAchievementStatsUserString = VPSString + "XBLIO/ach/?achievementstats&CPUKEYForStats=";

            /*GetPlayerAchievementTitle*/
            internal static string GetPlayerAchievementTitle = VPSString + "XBLIO/xbox.php?SpecificGameAchievements=/achievements/title/";
            internal static string GetPlayerUserString = VPSString + "XBLIO/ach/?achievementsSpecificlist";

            /*Requestfriendslist*/
            internal static string friendslistdownload = VPSString + "XBLIO/xbox.php?anotherfriendlist=friends?xuid=";
            internal static string friendslist = VPSString + "XBLIO/friendslist.php?anotherfriendlist";
            internal static string friendsNumberOfFriends = VPSString + "XBLIO/friendslist.php?FNumberOfFriends";

            /*friendslist from ApiKey*/
            internal static string Userfriendslistdownload = VPSString + "XBLIO/xbox.php?friendlist=friends&APIKEY=";
            internal static string Userfriendslist = VPSString + "XBLIO/friendslist.php?friendsList";
            internal static string NumberOfFriends = VPSString + "XBLIO/friendslist.php?NumberOfFriends";

            /*Send Msg*/
            internal static string sendmsg = VPSString + "XBLIO/xbox.php?sendaconversation=";

            /*Xbox Messages*/
            internal static string Conversationslistdownload = VPSString + "XBLIO/xbox.php?conversations=conversations&APIKEY=";
            internal static string Conversations = VPSString + "XBLIO/conversation.php?conversations";
            internal static string NumberOfConversations = VPSString + "XBLIO/conversation.php?NumberOfconversations";
            internal static string UnreadConversations = VPSString + "XBLIO/conversation.php?UnreadConversations";

            /*Xbox Message Requests*/
            internal static string RequestsConversationslistdownload = VPSString + "XBLIO/xbox.php?conversationsrequests=conversations/requests&APIKEY=";
            internal static string RequestsConversations = VPSString + "XBLIO/conversationRequests.php?conversationsrequests";
            internal static string RequestsNumberOfConversations = VPSString + "XBLIO/conversationRequests.php?NumberOfconversations";
            internal static string RequestsUnreadConversations = VPSString + "XBLIO/conversationRequests.php?UnreadConversations";

            /*Player Summary*/
            internal static string PlayerSummarydownload = VPSString + "XBLIO/xbox.php?playersummary=player/summary&APIKEY=";
            internal static string PlayerSummaryGamerScoreS = VPSString + "XBLIO/playersummary.php?gamerscore";
            internal static string PlayerSummaryprofilepictures = VPSString + "XBLIO/playersummary.php?profilepicture";
            internal static string PlayerSummarygamertag = VPSString + "XBLIO/playersummary.php?gamertag";
            internal static string PlayerSummaryxuid = VPSString + "XBLIO/playersummary.php?xuid";
            internal static string PlayerSummaryXboxOneRep = VPSString + "XBLIO/playersummary.php?XboxOneRep";
            internal static string PlayerSummarypresenceState = VPSString + "XBLIO/playersummary.php?presenceState";
            internal static string PlayerSummarypresenceText = VPSString + "XBLIO/playersummary.php?presenceText";
            internal static string PlayerSummarypresenceDevices = VPSString + "XBLIO/playersummary.php?presenceDevices";

            /*Presence*/
            internal static string Presencedownload = VPSString + "XBLIO/xbox.php?presence=presence&APIKEY=";
            internal static string presence = VPSString + "XBLIO/presence/presence.php?presenceList&CPUKEYForStats=";
            internal static string presencelink = VPSString + "XBLIO/presence/site/?presenceList&CPUKEYForStats=";

            /*GetFriendsPresence*/
            internal static string GetFriendsPresencedownload = VPSString + "XBLIO/xbox.php?Multipeople=";
            internal static string GetFriendsPresence = VPSString + "XBLIO/presence/multipresence.php?Multipeople&ACHXUID=";

            /*Achievements*/
            internal static string Achievementdownload = VPSString + "XBLIO/xbox.php?achievements=/achievements";
            internal static string AchievementsUserString = VPSString + "XBLIO/ach/?achievements";


            /*GetPlayerAchievement*/
            internal static string GetPlayerAchievementListdownload = VPSString + "XBLIO/xbox.php?achievementslist=achievements/player/";
            internal static string UserXUIDString = VPSString + "XBLIO/ach/?achievementslist&ACHXUID=";

            /*GetAchievementTitleHistory*/
            internal static string GetAchievementTitleHistorydownload = VPSString + "XBLIO/xbox.php?gameachievementshistory=/achievements/";
            internal static string lastUnlock = VPSString + "XBLIO/ach/history.php?gameachievementshistory&lastUnlock";
            internal static string titleId = VPSString + "XBLIO/ach/history.php?gameachievementshistory&titleId";
            internal static string titleType = VPSString + "XBLIO/ach/history.php?gameachievementshistory&titleType";
            internal static string GetAchievementTitleName = VPSString + "XBLIO/ach/history.php?gameachievementshistory&name";
            internal static string earnedAchievements = VPSString + "XBLIO/ach/history.php?gameachievementshistory&earnedAchievements";
            internal static string currentGamerscore = VPSString + "XBLIO/ach/history.php?gameachievementshistory&currentGamerscore";
            internal static string maxGamerscore = VPSString + "XBLIO/ach/history.php?gameachievementshistory&maxGamerscore";
            internal static string rarityCategory1 = VPSString + "XBLIO/ach/history.php?gameachievementshistory&rarityCategory1";
            internal static string isRarestCategory = VPSString + "XBLIO/ach/history.php?gameachievementshistory&isRarestCategory";
            internal static string rarityCategory2 = VPSString + "XBLIO/ach/history.php?gameachievementshistory&rarityCategory2";
            internal static string isRarestCategory2 = VPSString + "XBLIO/ach/history.php?gameachievementshistory&isRarestCategory2";
            internal static string totalOfUnlocks2 = VPSString + "XBLIO/ach/history.php?gameachievementshistory&totalOfUnlocks2";

            /*GetPlayersACHGame*/
            internal static string achievementsanotherplayersgamedownload = VPSString + "XBLIO/xbox.php?achievementsanotherplayersgame=achievements/player/";
            internal static string GetPlayersUserXUIDString = VPSString + "XBLIO/achievementsanotherplayersgame/?achievementsanotherplayersgame&ACHXUID=";

            /*RecentPlayers*/
            internal static string RecentPlayersdownload = VPSString + "XBLIO/xbox.php?recentplayers=recent-players&APIKEY=";
            internal static string RecentPlayers = VPSString + "XBLIO/recentplayers.php?recentplayers";

            /*ReserveClub*/
            internal static string resverClub = VPSString + "XBLIO/xbox.php?reserveClub=clubs/reserve&clubName=";


            /*ClubsIOwn*/
            internal static string ClubsIOwndownload = VPSString + "XBLIO/xbox.php?clubsowned=clubs/owned&ACHXUID=";
            internal static string ClubUserXUIDString = VPSString + "XBLIO/clubs/?clubsowned&ACHXUID=";

            /*ClubSummary*/
            internal static string ClubSummaryDL = VPSString + "XBLIO/xbox.php?clubs=clubs/";
            internal static string ClubSummaryUserString = VPSString + "XBLIO/clubs/?clubsummary&ACHXUID=";
        }
        
        public struct hookah
        {
            public string profileUsers { get; set; }
            public string settings { get; set; }
            public string value { get; set; }

        };

    }

}
