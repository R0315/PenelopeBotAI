using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PenelopeBOT.Utilities;

namespace PenelopeBOT
{
    /// <summary>
    /// Entry point for the program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            await host.RunAsync();
        }

        /// <summary>
        /// Creates the host builder with the necessary services.
        /// </summary>
        /// <param name="args">The command-line arguments.</param>
        /// <returns>The configured host builder.</returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<ISecretsLibrary, SecretsLibrary>();
                    services.AddSingleton<KernelBuilder>();
                    services.AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
                    {
                        GatewayIntents = GatewayIntents.Guilds |
                                         GatewayIntents.GuildMessages |
                                         GatewayIntents.GuildMessageReactions |
                                         GatewayIntents.DirectMessages |
                                         GatewayIntents.MessageContent |
                                         GatewayIntents.GuildMembers
                    }));
                    services.AddSingleton<IHostedService, Bot>();
                });
    }
}
