using System.Diagnostics;
using System.Threading;
using Irc;
using Irc.Bot.Infrastructure;
using StructureMap;

namespace IrcBot.GUI.Plugins
{
    public interface PluginLoader
    {
        void ReloadPlugins();
    }

    public class PluginLoaderImpl : PluginLoader
    {
        private readonly IContainer container;

        public PluginLoaderImpl(IContainer container)
        {
            this.container = container;
        }

        public void ReloadPlugins()
        {
            this.container.EjectAllInstancesOf<Command>();

            this.ClearEventHandlers();

            // We may end up in this method faster than we should (new dll may be still locked).
            Thread.Sleep(100);

            this.container.Configure(x =>
            {
                x.AddRegistry<CommandRegistry>();
                x.AddRegistry<HandlerRegistry>();
                x.AddRegistry<PluginRegistry>();
            });

            Debug.WriteLine(container.WhatDoIHave());
        }

        public void ClearEventHandlers()
        {
            var events = this.container.GetAllInstances<Event>();

            foreach (var @event in events)
            {
                var type = typeof(IrcEventHandler<>).MakeGenericType(@event.GetType());

                var method = this.container.GetType().GetMethod("EjectAllInstancesOf").MakeGenericMethod(type);
                method.Invoke(this.container, new object[] { });
            }
        }
    }
}
