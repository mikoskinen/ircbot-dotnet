using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Commands.Specs.SendToServerSpecifications
{
    public abstract class SendToServerConcern : ConcernFor<SendToServer>
    {
        protected BotStub bot=new BotStub();
        protected MessageSender commandExecutor = MessageSender.Empty;
        protected IrcMessage message = IrcMessage.Empty;
        protected CommandParameters parameters;

        protected override SendToServer CreateSubjectUnderTest()
        {
            return new SendToServer(bot);
        }

        protected override void Because()
        {
            sut.Execute(commandExecutor, message, parameters);
        }
    }

    public class When_command_is_executed_without_parameters : SendToServerConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
        }

        [Test]
        public void Should_not_send_anything_to_server()
        {
            Assert.That(bot.SendToServerCalled, Is.False);
        }
    }

    public class When_command_is_executed_with_parameters : SendToServerConcern
    {

        private const string expectedResult = "MODE #mychannel +o anotherUser";

        protected override void Context()
        {
            this.parameters = new CommandParameters();
            parameters.AddParameter("MODE");
            parameters.AddParameter("#mychannel");
            parameters.AddParameter("+o");
            parameters.AddParameter("anotherUser");
        }


        [Test]
        public void Should_sent_the_parameters_to_server_in_one_sentence()
        {
            Assert.That(bot.SendToServerCalled, Is.True);
            Assert.That(bot.LastSendToServer, Is.EqualTo(expectedResult));
        }
    }
}
