using System;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
namespace stealthbot
{
    class Program
    {
        public DiscordSocketClient Client;
        public CommandService Commands;
        public IServiceProvider _services;

        static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        private Task Client_Log(LogMessage Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[{DateTime.Now.ToLocalTime()} AT {Message.Source}] - [{Message.Message}]");
            return null;
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync($"{config.Global.prefix}help");
            /*while (true)
            {
                System.Threading.Thread.Sleep(30000);
                await Client.SetGameAsync($"{config.Global.prefix}help");
                System.Threading.Thread.Sleep(30000);
                await Client.SetGameAsync("Silent's Hookah!");
                System.Threading.Thread.Sleep(30000);
                await Client.SetGameAsync("https://silentlive.gq");
                await Task.CompletedTask;
            }*/
            
        }

        public async Task Client_MessageReceived(SocketMessage MessageParam)
        {

            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix(config.Global.prefix, ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            if (Context.Message.Content.Contains($"{config.Global.prefix}link") || Context.Message.Content.Contains($"{config.Global.prefix}Link"))
            {
                await Context.Message.DeleteAsync();
            }

            var Result = await Commands.ExecuteAsync(Context, ArgPos, _services);

            if (!Result.IsSuccess)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[{DateTime.Now.ToLocalTime()} AT Commands] - Error in Reading a Command: {Context.Message.Content} | ERROR: {Result.ErrorReason}");
            }
        }


        public async Task MainAsync() {

            Client = new DiscordSocketClient(new DiscordSocketConfig { LogLevel = LogSeverity.Debug, MessageCacheSize = 250 });
            Commands = new CommandService(new CommandServiceConfig { CaseSensitiveCommands = false, DefaultRunMode = RunMode.Async, LogLevel = LogSeverity.Debug });

            _services = new ServiceCollection()
            .AddSingleton(Client)
            .AddSingleton(Commands)
            .BuildServiceProvider();

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            
            await Client.LoginAsync(TokenType.Bot, "OTI2MjUyODk4Mzk0OTgwNDAz.G2PeJJ.z-F0ohm343e8CmL03chwARxAggzuHwaPQSVUtU");
            await Client.StartAsync();
            await Task.Delay(-1);

        }
    }
}
