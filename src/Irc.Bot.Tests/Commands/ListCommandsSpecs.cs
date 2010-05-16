using System.Collections.Generic;
using ClassicIRCBot.Commands;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Commands.Specs.ListCommandsSpecifications
{
    public class When_listing_commands : ConcernFor<ListCommands>
    {
        private BotStub bot;
        private List<string> exceptedResult;

        protected override void Context()
        {
            bot = new BotStub();

            var testCommand = CommandStub.Make();
            var listCommands = new ListCommands(bot);

            this.exceptedResult = new List<string> { testCommand.GetKeyWord(), listCommands.GetKeyWord() };
        }

        protected override ListCommands CreateSubjectUnderTest()
        {
            var commands = new ListCommands(bot);

            return commands;
        }

        protected override void Because()
        {
            sut.Execute(MessageSender.Empty, new IrcMessage(null, "", ""), null);
        }

        [Test]
        public void Should_list_all_commands()
        {
            Assert.That(bot.Said.Count, Is.EqualTo(exceptedResult.Count));
            foreach (var foundCommand in bot.Said)
            {
                Assert.That(exceptedResult.Contains(foundCommand));
            }
        }
    }
}
