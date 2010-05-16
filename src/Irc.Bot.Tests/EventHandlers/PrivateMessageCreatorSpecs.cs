using System;
using ClassicIRCBot.EventHandlers;
using Irc.Events;
using Irc.Infrastructure;
using Irc.Stubs;
using NUnit.Framework;
using Rhino.Mocks;

namespace Irc.EventHandlers.Specs.PongSenderSpecifications.Specs.PrivateMessageCreatorSpecifications
{
    public class PrivateMessageCreatorConcern : ConcernFor<PrivateMessageCreator>
    {
        private Bot.Bot bot;
        protected PrivateMessageParser parser;
        protected Credentials credentials;
        protected ServerMessageReceived serverMessageReceived;

        protected override void Context()
        {
            this.credentials = new Credentials("ident", "name");

            this.bot = MockRepository.GenerateStub<Bot.Bot>();
            bot.Stub(x => x.GetCredentials()).Return(credentials);

            this.parser = MockRepository.GenerateMock<PrivateMessageParser>();

        }

        protected override PrivateMessageCreator CreateSubjectUnderTest()
        {
            return new PrivateMessageCreator(bot, parser, new ManualEventAggregator());
        }
    }

    public class When_handling_private_message_creation : PrivateMessageCreatorConcern
    {
        protected override void Because()
        {
            serverMessageReceived = new ServerMessageReceived { Message = "PRIVMSG hello hello" };

            sut.Handle(serverMessageReceived);
        }

        [Test]
        public void Should_use_bots_credentials_as_receiver()
        {
            parser.AssertWasCalled(x => x.Parse(serverMessageReceived.Message, credentials.Name));
        }
    }

    public class When_receiving_message_which_is_not_private : PrivateMessageCreatorConcern
    {
        protected override void Because()
        {
            serverMessageReceived = new ServerMessageReceived { Message = "not private message" };

            sut.Handle(serverMessageReceived);
        }

        [Test]
        public void Should_not_parse_the_message()
        {
            parser.AssertWasNotCalled(x => x.Parse(serverMessageReceived.Message, credentials.Name));
        }
    }
}
