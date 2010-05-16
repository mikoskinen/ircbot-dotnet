using Irc.Events;
using Irc.Stubs;
using NUnit.Framework;
using StructureMap;

namespace Irc.Infrastructure.Specs.EventAggregatorSpecifications
{
    public abstract class EventAggregatorConcern : ConcernFor<ContainerEventAggregator>
    {
        protected IContainer container = new Container();
        protected ConnectedToServerHandlerStub connectedToServerHandler = new ConnectedToServerHandlerStub();
        protected HandlerStub handlerStub = new HandlerStub();

        protected override ContainerEventAggregator CreateSubjectUnderTest()
        {
            return new ContainerEventAggregator(container);
        }
    }

    public class When_incoming_event_is_of_type_message_based_event_interface : EventAggregatorConcern
    {
        private MessageBasedEvent messageBasedEvent;

        protected override void Context()
        {
            container = new Container(x =>
            {
                x.For<IrcEventHandler<ServerMessageReceived>>().Add(handlerStub);
                x.For<IrcEventHandler<ConnectedToServer>>().Add(connectedToServerHandler);
            });

            this.messageBasedEvent = (MessageBasedEvent)new ServerMessageReceived();
        }

        protected override void Because()
        {
            sut.Raise(messageBasedEvent);
        }

        [Test]
        public void Should_correct_handler_handle_the_event()
        {
            Assert.That(handlerStub.Handled, Is.True);
            Assert.That(connectedToServerHandler.Handled, Is.False);
        }
    }

    public class When_incoming_event_is_of_type_event_interface : EventAggregatorConcern
    {
        private Event connectedToServerEvent;

        protected override void Context()
        {
            container = new Container(x =>
            {
                x.For<IrcEventHandler<ServerMessageReceived>>().Add(handlerStub);
                x.For<IrcEventHandler<ConnectedToServer>>().Add(connectedToServerHandler);
            });

            this.connectedToServerEvent = (Event)new ConnectedToServer();
        }

        protected override void Because()
        {
            sut.Raise(connectedToServerEvent);
        }

        [Test]
        public void Should_correct_handler_handle_the_event()
        {
            Assert.That(handlerStub.Handled, Is.False);
            Assert.That(connectedToServerHandler.Handled, Is.True);
        }
    }

    public class When_incoming_event_is_of_concrete_type : EventAggregatorConcern
    {
        private ConnectedToServer connectedToServerEvent;

        protected override void Context()
        {
            container = new Container(x =>
            {
                x.For<IrcEventHandler<ServerMessageReceived>>().Add(handlerStub);
                x.For<IrcEventHandler<ConnectedToServer>>().Add(connectedToServerHandler);
            });

            this.connectedToServerEvent = new ConnectedToServer();
        }

        protected override void Because()
        {
            sut.Raise(connectedToServerEvent);
        }

        [Test]
        public void Should_correct_handler_handle_the_event()
        {
            Assert.That(handlerStub.Handled, Is.False);
            Assert.That(connectedToServerHandler.Handled, Is.True);
        }
    }

    public class When_incoming_event_is_of_type_event_interface_and_handler_has_many_implementations : EventAggregatorConcern
    {
        private Event connectedToServerEvent;
        private MultiHandlerStub multiHandler;

        protected override void Context()
        {
            multiHandler = new MultiHandlerStub();

            container = new Container(x =>
            {
                x.For<IrcEventHandler<ConnectedToServer>>().Add(multiHandler);
            });

            this.connectedToServerEvent = (Event)new ConnectedToServer();
        }

        protected override void Because()
        {
            sut.Raise(connectedToServerEvent);
        }

        [Test]
        public void Should_correct_handler_method_handle_the_event()
        {
            Assert.That(multiHandler.HandledPingReceived, Is.False);
            Assert.That(multiHandler.HandledConnectedToServer, Is.True);
        }
    }
}
