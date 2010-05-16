using System.Threading;
using Irc.Bot;
using Irc.Bot.Entities;
using Irc.Bot.Infrastructure;
using Irc.Entities;
using Irc.Infrastructure;

namespace Classic.IRCBot
{
    public interface Handler
    {
        void CreateAndConnectBots();
    }

    /// <summary>
    /// BotHandler manages as many bots as needed
    /// </summary>
    public class BotHandler : Handler
    {
        private readonly BotFactory botFactory;
        private readonly DataSource dataSource;

        public BotHandler(BotFactory botFactory, DataSource dataSource)
        {
            this.botFactory = botFactory;
            this.dataSource = dataSource;
        }

        public void CreateAndConnectBots()
        {
            this.SetUpBots();
        }

        /// <summary>
        /// Configures and creates the bots
        /// </summary>
        private void SetUpBots()
        {
            // First we figure out in what networks we need a bot
            var networks = dataSource.GetNetworks();
            
            // Then we create the needed bots
            for (int n = 0; n < networks.Count; n++)
            {
                var network = (Network)networks[n];

                var channels = this.dataSource.GetChannels(network.NetworkID);
                if (channels.Count <= 0)
                    continue;

                ThreadStart starter = () => this.CreateBotToServer(network);

                new Thread(starter).Start();
            }
        }

        private void CreateBotToServer(Network network)
        {
            var bot = botFactory.Make();

            var channels = this.dataSource.GetChannels(network.NetworkID);
            bot.SetChannels(channels);

            var server = network.ReturnFirstServer();
            this.ConnectBot(bot, server);
        }

        private void ConnectBot(Bot bot, Server server)
        {
            var credentials = new Credentials("USER Bottii 0 * :Botti", "IrcBotNM");
            bot.ConnectToServer(server, credentials);
        }

    }
}

