using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Commands.Specs.SayToSpecifications
{
    public abstract class SayToConcern : ConcernFor<SayTo>
    {
        protected BotStub bot= new BotStub();
        protected CommandParameters parameters;

        protected override SayTo CreateSubjectUnderTest()
        {
            return new SayTo(bot);
        }

        protected override void Because()
        {
            sut.Execute(MessageSender.Empty, IrcMessage.Empty, parameters);
        }
    }

    public class When_saying_nothing : SayToConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
        }

        [Test]
        public void Should_not_say_anything()
        {
            Assert.That(bot.Said, Is.Null);
        }
    }

    public class When_saying_something : SayToConcern
    {
        protected override void Context()
        {
            this.parameters = new CommandParameters();
            parameters.AddParameter("hello");
            parameters.AddParameter("to");
            parameters.AddParameter("you");
        }

        [Test]
        public void Should_get_said_message_from_parameters()
        {
            Assert.That(bot.LastSaid, Is.EqualTo("hello to you"));
        }
    }
}
