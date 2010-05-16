using ClassicIRCBot.EventHandlers;
using Irc.Bot.Infrastructure;
using StructureMap.Configuration.DSL;

namespace IrcBot.GUI
{
    internal class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            Scan(x =>
                     {
                         x.AssemblyContainingType<ChannelJoiner>();
                         x.AddAllTypesOf<Command>();
                     });
        }
    }
}