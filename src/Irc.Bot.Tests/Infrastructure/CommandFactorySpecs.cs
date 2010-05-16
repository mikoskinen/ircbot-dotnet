using System;
using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Commands;
using NUnit.Framework;
using System.Collections.Generic;

namespace Irc.Bot.Infrastructure.Specs.CommandFactorySpecifications
{
    public abstract class CommandFactoryConcern : ConcernFor<CommandFactoryImpl>
    {
        private List<Command> commands;

        protected override void Context()
        {
            var command1 = new TestCommandStub1();
            var command2 = new TestCommandStub2();
            this.commands = new List<Command> {command1, command2};
        }

        protected override CommandFactoryImpl CreateSubjectUnderTest()
        {
            var factory = new CommandFactoryImpl(commands);
            return factory;
        }
    }

    public class When_making_command_by_its_key_word : CommandFactoryConcern
    {
        private Command result;

        protected override void Because()
        {
            result = sut.MakeCommand("test");
        }

        [Test]
        public void Should_make_correct_command()
        {
            Assert.That(result, Is.TypeOf(typeof(TestCommandStub1)));
        }
    }

    public class When_asked_for_known_commands : CommandFactoryConcern
    {
        private List<Command> result;

        protected override void Because()
        {
            result = sut.GetAllKnownCommands();
        }

        [Test]
        public void Should_return_all_known_commands()
        {
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0].GetKeyWord(), Is.EqualTo("test"));
            Assert.That(result[1].GetKeyWord(), Is.EqualTo("test2"));
        }
    }

    public class TestCommandStub1 : Command
    {
        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            throw new NotImplementedException();
        }

        public string GetKeyWord()
        {
            return "test";
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            throw new NotImplementedException();
        }

        public string[] GetHelp()
        {
            throw new NotImplementedException();
        }
    }


    public class TestCommandStub2 : Command
    {
        public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            throw new NotImplementedException();
        }

        public string GetKeyWord()
        {
            return "test2";
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            throw new NotImplementedException();
        }

        public string[] GetHelp()
        {
            throw new NotImplementedException();
        }
    }
}
