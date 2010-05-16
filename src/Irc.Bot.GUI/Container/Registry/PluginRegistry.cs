using Irc;
using Irc.Bot.Infrastructure;
using StructureMap.Configuration.DSL;

namespace IrcBot.GUI
{
    internal class PluginRegistry : Registry
    {
        public PluginRegistry()
        {
            Scan( x =>
                      {
                          x.AssembliesFromPath(@".\plugins");
                          x.AddAllTypesOf<Command>();

                          x.ConnectImplementationsToTypesClosing(typeof(IrcEventHandler<>));
                      });
        }
    }
}