using System;
using System.Collections.Generic;
using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Commands;
using Irc.Bot.EventHandlers;
using Irc.Bot.Events;
using NUnit.Framework;

namespace Irc.Bot.Infrastructure.Specs.CommandHandlerSpecifications
{
    public abstract class CommandHandlerConcern : ConcernFor<CommandHandler>
    {
        protected CommandFactoryStub factory;
        protected PrivateMessageReceived args = new PrivateMessageReceived { Message = new IrcMessage(new MessageSender("", "", ""), "command", "") };
        protected CommandExecutorStub executor;

        protected override CommandHandler CreateSubjectUnderTest()
        {
            this.factory = new CommandFactoryStub();
            this.executor = new CommandExecutorStub();
            return new CommandHandler(factory, new ParameterFactory(), executor);
        }
    }

    public class When_trying_to_handle_event : CommandHandlerConcern
    {

        protected override void Because()
        {
            sut.Handle(args);
        }

        [Test]
        public void Should_try_to_find_the_correct_command_by_keyword()
        {
            Assert.That(factory.LastMakeCallTo, Is.EqualTo("command"));
        }

        [Test]
        public void Should_try_to_execute_the_found_command()
        {
            Assert.That(executor.executed, Is.True);
        }
    }

    public class When_handling_command_execution_with_parameters : CommandHandlerConcern
    {
        protected override void Context()
        {
            args = new PrivateMessageReceived
            {
                Message = new IrcMessage(new
                    MessageSender("", "", ""), "command parameter1 parameter2", "")
            };

        }

        protected override void Because()
        {
            sut.Handle(args);
        }

        [Test]
        public void Should_parameters_be_available_in_execution()
        {
            Assert.That(executor.ExecutionParameters.Count, Is.EqualTo(2));
            Assert.That(executor.ExecutionParameters[0], Is.EqualTo("parameter1"));
            Assert.That(executor.ExecutionParameters[1], Is.EqualTo("parameter2"));
        }

        [Test]
        public void Should_skip_the_parameters_when_making_command()
        {
            Assert.That(factory.LastMakeCallTo, Is.EqualTo("command"));
        }
    }

    public class When_handling_command_execution_and_script_character_is_in_use : CommandHandlerConcern
    {
        protected override void Context()
        {
            args = new PrivateMessageReceived
            {
                Message = new IrcMessage(new
                    MessageSender("", "", ""), "#command parameter1 parameter2", "")
            };
        }

        protected override void Because()
        {
            sut.CommandCharacter = "#";
            sut.Handle(args);
        }

        [Test]
        public void Should_try_to_make_the_command_without_script_character()
        {
            Assert.That(factory.LastMakeCallTo, Is.EqualTo("command"));
        }
    }

    public class When_message_comes_with_only_script_character : CommandHandlerConcern
    {
        protected override void Context()
        {
            args = new PrivateMessageReceived
            {
                Message = new IrcMessage(new
                    MessageSender("", "", ""), "#", "")
            };
        }

        protected override void Because()
        {
            sut.CommandCharacter = "#";
            sut.Handle(args);
        }

        [Test]
        public void Should_skip_handling()
        {
            Assert.That(factory.LastMakeCallTo, Is.EqualTo(""));
        }
    }

    public class When_message_comes_without_script_character_when_script_character_is_set : CommandHandlerConcern
    {

        protected override void Context()
        {
            args = new PrivateMessageReceived
            {
                Message = new IrcMessage(new
                    MessageSender("", "", ""), "command parameter1 parameter2", "")
            };
        }

        protected override void Because()
        {
            sut.CommandCharacter = "#";
            sut.Handle(args);
        }

        [Test]
        public void Should_skip_handling()
        {
            Assert.That(factory.LastMakeCallTo, Is.EqualTo(""));
        }
    }

    public class CommandFactoryStub : CommandFactory
    {
        public string LastMakeCallTo = "";
        public CommandStub TestCommand;

        public Command MakeCommand(string command)
        {
            LastMakeCallTo = command;

            this.TestCommand = new CommandStub();
            return this.TestCommand;
        }

        public List<Command> GetAllKnownCommands()
        {
            return new List<Command>();
        }
    }

    public class CommandStub : Command
    {
        public bool Executed = false;
        private SecurityLevelEnum securityLevel = SecurityLevelEnum.Public;

        public void Execute(MessageSender source, IrcMessage message, CommandParameters parameters)
        {
            Executed = true;
        }

        public string GetKeyWord()
        {
            throw new NotImplementedException();
        }

        public SecurityLevelEnum GetRequiredSecurityLevel()
        {
            return securityLevel;
        }

        public string[] GetHelp()
        {
            throw new NotImplementedException();
        }

        public void SetSecurityLevel(SecurityLevelEnum level)
        {
            this.securityLevel = level;
        }
    }

    public class CommandExecutorStub : CommandExecutor
    {
        public CommandParameters ExecutionParameters;
        public bool executed = false;

        public void Execute(Command command, MessageSender executor, IrcMessage message, CommandParameters parameters)
        {
            this.executed = true;
            this.ExecutionParameters = parameters;
            command.Execute(executor, message, parameters);

        }
    }
}
