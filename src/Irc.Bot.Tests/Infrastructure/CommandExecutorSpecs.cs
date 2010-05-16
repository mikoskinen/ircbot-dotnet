using ClassicIRCBot.Infrastructure;
using Irc.Bot.Commands;
using Irc.Bot.Events;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Infrastructure.Specs.CommandExecutorSpecifications
{
    public abstract class CommandExecutorConcern : ConcernFor<CommandExecutorImpl>
    {
        protected SecurityLevelCheckerStub securityChecker = new SecurityLevelCheckerStub();
        protected BotStub bot = new BotStub();
        protected PrivateMessageReceived args = new PrivateMessageReceived { Message = new IrcMessage(new MessageSender("", "", ""), "command", "") };
        protected CommandStub command = new CommandStub();

        protected override CommandExecutorImpl CreateSubjectUnderTest()
        {
            return new CommandExecutorImpl(bot, securityChecker);
        }

        protected override void Because()
        {
            sut.Execute(command, args.Message.Sender, args.Message, new CommandParameters());
        }
    }

    public class When_command_is_executed_with_enough_security_level : CommandExecutorConcern
    {
        protected override void Context()
        {
            this.securityChecker.SetReturnValue(true);
        }

        [Test]
        public void Should_execute_command()
        {
            Assert.That(command.Executed, Is.True);
        }
    }

    public class When_command_is_executed_without_enough_security_level : CommandExecutorConcern
    {
        protected override void Context()
        {
            this.securityChecker.SetReturnValue(false);
        }

        [Test]
        public void Should_not_execute_command()
        {
            Assert.That(command.Executed, Is.False);
        }
    }
}

