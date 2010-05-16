using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Commands.Specs.HelpSpecifications
{
    public abstract class HelpConcern : ConcernFor<Help>
    {
        protected readonly BotStub bot = new BotStub();
        protected CommandParameters parameters;

        protected override Help CreateSubjectUnderTest()
        {
            var help = new Help(bot);
            return help;
        }

        protected override void Because()
        {
            sut.Execute(MessageSender.Empty, null, parameters);
        }
    }

    public class When_executing_help_with_parameter_of_known_command : HelpConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
            parameters.AddParameter("test");
        }

        [Test]
        public void Should_display_help_from_command()
        {
            Assert.That(bot.Said.Count, Is.EqualTo(2));
        }
    }

    public class When_executing_help_without_parameters : HelpConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
        }

        [Test]
        public void Should_display_general_help()
        {
            Assert.That(bot.Said[0], Is.EqualTo("Usage: help commandname"));
        }
    }

    public class When_executing_help_with_parameter_of_unknown_command : HelpConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
            parameters.AddParameter("unknowncommand");
        }

        [Test]
        public void Should_display_error()
        {
            Assert.That(bot.Said[0], Is.EqualTo("Unknown command unknowncommand"));
        }
    }
    

    
}
