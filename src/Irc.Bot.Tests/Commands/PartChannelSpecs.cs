using System;
using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Stubs;
using NUnit.Framework;

namespace Irc.Bot.Commands.Specs.PartChannelSpecs
{
    public abstract class PartChannelConcern : ConcernFor<PartChannel>
    {
        protected CommandParameters parameters;
        protected Exception result;
        protected BotStub bot;

        protected override PartChannel CreateSubjectUnderTest()
        {
            this.bot = new BotStub();
            return new PartChannel(bot);
        }
    }

    public class When_executing_part_channel_command_with_one_parameter : PartChannelConcern
    {

        protected override void Context()
        {
            parameters = new CommandParameters();
            parameters.AddParameter("#testchannel");
        }

        protected override void Because()
        {
            sut.Execute(new MessageSender("", "", ""), null, parameters);
        }

        [Test]
        public void Should_ask_bot_to_part_channel()
        {
            Assert.That(bot.LastPart, Is.EqualTo("#testchannel"));
        }
    }

    public class When_executing_part_channel_command_without_parameters : PartChannelConcern
    {
        protected override void Context()
        {
            parameters = new CommandParameters();
        }

        protected override void Because()
        {
            this.result = Assert.Catch<ParameterException>(() => sut.Execute(new MessageSender("", "", ""), null, parameters));
        }

        [Test]
        public void Should_throw()
        {
            Assert.That(result, Is.TypeOf(typeof(ParameterException)));
        }
    }

    public class When_executing_part_channel_command_with_more_than_one_parameter : PartChannelConcern
    {
        protected override void Context()
        {
            parameters = new CommandParameters();
            parameters.AddParameter("parameter one");
            parameters.AddParameter("parameter two");
        }

        protected override void Because()
        {
            this.result = Assert.Catch<ParameterException>(() => sut.Execute(new MessageSender("", "", ""), null, parameters));
        }

        [Test]
        public void Should_throw()
        {
            Assert.That(result, Is.TypeOf(typeof(ParameterException)));
        }
    }



}
