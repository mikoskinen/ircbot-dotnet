using Classic.IRCBot;
using Irc;
using Irc.Bot;
using Irc.Bot.Infrastructure;
using Irc.Infrastructure;
using IrcBot.GUI.Plugins;
using StructureMap;

namespace IrcBot.GUI
{
    public static class ContainerCreator
    {
        public static Container CreateContainer()
        {
            var container = new Container(
                x =>
                {
                    x.For<BotFactory>().Singleton().Use<BotFactoryImpl>();
                    x.For<Bot>().HybridHttpOrThreadLocalScoped().Use<ClassicIrcBot>();
                    
                    x.For<DataSource>().HybridHttpOrThreadLocalScoped().Use<BotDataSource>();
                    x.For<DataSourceConnection>().HybridHttpOrThreadLocalScoped().Use<XMLDataSourceConnection>();

                    x.For<PrivateMessageParser>().Use<PrivateMessageParserImpl>();

                    x.For<Connection>().HybridHttpOrThreadLocalScoped().Use<IrcConnection>();

                    x.For<Handler>().Singleton().Use<BotHandler>();
                    x.For<PluginWatcher>().Singleton().Use<FileSystemPluginWatcher>();
                    x.For<PluginLoader>().Singleton().Use<PluginLoaderImpl>();

                    x.AddRegistry<CommandManagementRegistry>();
                    x.AddRegistry<CommandRegistry>();
                    x.AddRegistry<HandlerRegistry>();
                    x.AddRegistry<EventRegistry>();
                    x.AddRegistry<PluginRegistry>();
                });

            container.AssertConfigurationIsValid();

            return container;
        }
    }
}
