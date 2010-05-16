using System;
using System.Collections;
using NUnit.Framework;

namespace Irc.Stubs
{
    public class ManualEventAggregator : EventAggregator
    {
        [ThreadStatic]
        private static ArrayList handlers;

        public Event LastIncomingEvent;

        public  void Register<T>(IrcEventHandler<T> handler) where T : Event
        {
            if (handlers == null)
                handlers = new ArrayList();

            handlers.Add(handler);
        }

        public void Raise<T>(T args) where T : Event
        {
            LastIncomingEvent = args;

            if (handlers == null) return;

            var type = typeof(IrcEventHandler<>).MakeGenericType(args.GetType());
            var isTypeMethod = typeof(ManualEventAggregator).GetMethod("IsType").MakeGenericMethod(type);

            foreach (var handler in handlers)
            {
                if (!((bool)isTypeMethod.Invoke(null, new[] { handler }))) continue;

                var method = handler.GetType().GetMethod("Handle", new[] { args.GetType() });
                method.Invoke(handler, new object[] { args });
            }
        }

        public static bool IsType<T>(object candidant)
        {
            try
            {
                var castedObject = (T)candidant;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}