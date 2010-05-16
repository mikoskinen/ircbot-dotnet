using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc;
using Irc.Bot.Commands;
using Irc.Bot.EventHandlers;
using Irc.Bot.Events;
using Irc.Bot.Infrastructure;
using IrcBot.GUI.Properties;
using StructureMap.Configuration.DSL;

namespace IrcBot.GUI
{
    internal class CommandManagementRegistry : Registry
    {
        public CommandManagementRegistry()
        {

            For<IrcEventHandler<PrivateMessageReceived>>().Use<CommandHandler>().SetProperty(target =>
                                                                                                 {
                                                                                                     target.CommandCharacter = Settings.Default.CommandCharacter;
                                                                                                 });

            For<CommandParametersFactory>().Use<ParameterFactory>();
            For<SecurityLevelChecker>().Use<CommandSecurityChecker>();
            For<CommandExecutor>().Use<CommandExecutorImpl>();
            For<CommandFactory>().Use<CommandFactoryImpl>().EnumerableOf<Command>();
        }
    }
}