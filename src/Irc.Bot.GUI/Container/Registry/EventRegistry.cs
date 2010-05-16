using Irc;
using Irc.Infrastructure;
using StructureMap.Configuration.DSL;

namespace IrcBot.GUI
{
    public class EventRegistry : Registry
    {
        public EventRegistry()
        {
            Scan(x =>
                     {
                         x.AssemblyContainingType(typeof(Event));
                         x.AddAllTypesOf<MessageBasedEvent>();
                         x.AddAllTypesOf<Event>();
                     });

            For<EventFactory>().HybridHttpOrThreadLocalScoped().Use<IrcEventFactory>();
            For<EventAggregator>().HybridHttpOrThreadLocalScoped().Use<ContainerEventAggregator>();
        }
    }
}