using System;
using System.Collections.Generic;
using ClassicIRCBot.Commands;
using ClassicIRCBot.Infrastructure;
using Irc.Bot.Infrastructure;

namespace Irc.Bot.Commands
{
    public interface CommandFactory
    {
        Command MakeCommand(string commandKeyWord);
    }

    public class CommandFactoryImpl : CommandFactory
    {
        private readonly List<Command> commands;

        public CommandFactoryImpl(List<Command> commands)
        {
            this.commands = commands;
        }

        public Command MakeCommand(string commandKeyWord)
        {
            if (commands == null)
                return new EmptyCommand();

            foreach (var command in commands)
            {
                if (command.GetKeyWord() == commandKeyWord)
                    return command;
            }

            return new EmptyCommand();
        }

        private class EmptyCommand : Command
        {
            public void Execute(MessageSender executor, IrcMessage message, CommandParameters parameters)
            {
                return;
            }

            public string GetKeyWord()
            {
                return "emptycommand";
            }

            public SecurityLevelEnum GetRequiredSecurityLevel()
            {
                return SecurityLevelEnum.Public;
            }

            public string[] GetHelp()
            {
                throw new NotImplementedException();
            }
        }

        public List<Command> GetAllKnownCommands()
        {
            return commands ?? new List<Command>();
        }
    }


}