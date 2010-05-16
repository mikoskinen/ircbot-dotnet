using ClassicIRCBot.EventHandlers;
using Irc;
using Irc.Bot.EventHandlers;
using StructureMap.Configuration.DSL;

namespace IrcBot.GUI
{
    internal class HandlerRegistry : Registry
    {
        public HandlerRegistry()
        {
            Scan(x =>
                     {
                         x.AssemblyContainingType<Logger>();
                         x.AssemblyContainingType(typeof(Event));
                         x.ConnectImplementationsToTypesClosing(typeof(IrcEventHandler<>));
                         x.ExcludeType<CommandHandler>();
                     });
        }
    }
}