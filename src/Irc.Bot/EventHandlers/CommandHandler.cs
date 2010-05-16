using ClassicIRCBot.Infrastructure;
using Irc.Bot.Commands;
using Irc.Bot.Events;

namespace Irc.Bot.EventHandlers
{
    public class CommandHandler : IrcEventHandler<PrivateMessageReceived>
    {
        private readonly CommandFactory factory;
        private readonly CommandParametersFactory parametersFactory;
        private readonly CommandExecutor executor;
        public string CommandCharacter { get; set; }

        public CommandHandler(CommandFactory factory, CommandParametersFactory parametersFactory, CommandExecutor executor)
        {
            this.factory = factory;
            this.parametersFactory = parametersFactory;
            this.executor = executor;

            this.CommandCharacter = "";
        }

        public void Handle(PrivateMessageReceived args)
        {
            var message = args.Message.Message;

            var skipHandling = ShouldSkipHandling(message);
            if (skipHandling)
                return;

            var commandKeyWord = GetCommandKeyWordFromMessage(message);
            var command = factory.MakeCommand(commandKeyWord);

            var parameters = parametersFactory.CreateParametersFromMessage(message);

            executor.Execute(command, args.Message.Sender, args.Message, parameters);
        }

        private bool ShouldSkipHandling(string message)
        {
            if (ScriptCharacterInUse())
            {
                return !DoesMessageContainScriptCharacter(message);
            }

            return false;
        }

        private bool ScriptCharacterInUse()
        {
            return !string.IsNullOrEmpty(CommandCharacter);
        }

        private bool DoesMessageContainScriptCharacter(string message)
        {
            if (message.IndexOf(this.CommandCharacter) != 0)
                return false;

            return true;
        }

        private string GetCommandKeyWordFromMessage(string messageFromExecutor)
        {
            var words = messageFromExecutor.Split(' ');
            var commandWord = words[0];

            if (ScriptCharacterInUse())
                commandWord = this.StripKeyCharacter(commandWord);

            return commandWord;
        }

        private string StripKeyCharacter(string commandWord)
        {
            return commandWord.Substring(1);
        }
    }
}