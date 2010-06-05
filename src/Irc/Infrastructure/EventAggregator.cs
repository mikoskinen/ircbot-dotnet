using System;
using Irc.Infrastructure;
using StructureMap;

namespace Irc
{
    public interface EventAggregator
    {
        void Raise<T>(T args) where T : Event;
    }

    public class ContainerEventAggregator : EventAggregator
    {
        private readonly IContainer container;
        private readonly EventDispatcher dispatcher;

        public ContainerEventAggregator(IContainer container, EventDispatcher dispatcher)
        {
            this.container = container;
            this.dispatcher = dispatcher;
        }

        public void Raise<T>(T args) where T : Event
        {
            var eventType = GetEventType(args);

            var eventHandlers = container.GetAllInstances(eventType);
            if (eventHandlers == null) return;

            foreach (var handler in eventHandlers)
            {
                //InvokeHandler(args, handler);
                this.dispatcher.Dispatch(args, handler);
            }
        }

        private Type GetEventType<T>(T args)
        {
            return typeof(IrcEventHandler<>).MakeGenericType(args.GetType());
        }

        //private void InvokeHandler<T>(T args, object handler)
        //{
        //    var method = handler.GetType().GetMethod("Handle", new[] { args.GetType() });
        //    method.Invoke(handler, new object[] { args });
        //}
    }
}