using System.Collections.Generic;
using ClassicIRCBot.Commands;
using Irc.Bot.Infrastructure;
using Irc.Entities;
using Irc.Infrastructure;
using NUnit.Framework;
using Rhino.Mocks;

namespace Irc.Bot.Specs.ClassicIrcBotSpecifications
{
    public abstract class BotConcern : ConcernFor<ClassicIrcBot>
    {
        private PrivateMessageParser parser;
        protected Connection connection = MockRepository.GenerateMock<Connection>();

        protected override ClassicIrcBot CreateSubjectUnderTest()
        {
            return new ClassicIrcBot(connection);
        }
    }

    public class When_getting_all_known_commands_and_no_know_commands_are_available : BotConcern
    {
        private IList<Command> result;

        protected override void Because()
        {
            this.result = sut.GetAllCommands();
        }

        [Test]
        public void Should_return_empty_collection()
        {
            Assert.That(result, Is.Empty);
        }
    }

    public class When_getting_all_known_commands : BotConcern
    {
        private IList<Command> result;
        private List<Command> commands;

        protected override void Context()
        {
            this.commands = new List<Command> { new Help(this.sut) };
        }

        protected override ClassicIrcBot CreateSubjectUnderTest()
        {
            var bot = base.CreateSubjectUnderTest();
            bot.SetCommands(commands);
        
            return bot;
        }

        protected override void Because()
        {
            this.result = sut.GetAllCommands();
        }

        [Test]
        public void Should_get_all_commands()
        {
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.TypeOf(typeof(Help)));
        }
    }

    public class When_connecting_to_server_with_credentials : BotConcern
    {
        private Credentials credentials;

        protected override void Because()
        {
            credentials = new Credentials("testIdent", "testNick");
            sut.ConnectToServer(new Server("", 0, ""), credentials);
        }

        [Test]
        public void Should_crendentials_be_later_retriavable()
        {
            Assert.That(sut.GetCredentials(), Is.EqualTo(credentials));
        }
    }

    public class When_parting_channel : BotConcern
    {
        protected override void Because()
        {
            sut.PartChannel("#testchannel");
        }

        [Test]
        public void Should_send_correctly_formatted_part_message_to_server()
        {
            connection.AssertWasCalled(x => x.SendToServer("PART #testchannel"));
        }
    }
}
