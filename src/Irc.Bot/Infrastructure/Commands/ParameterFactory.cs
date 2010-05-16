namespace ClassicIRCBot.Infrastructure
{
    public interface CommandParametersFactory
    {
        CommandParameters CreateParametersFromMessage(string completeMessage);
    }

    public class ParameterFactory : CommandParametersFactory
    {
        public CommandParameters CreateParametersFromMessage(string completeMessage)
        {
            var words = completeMessage.Split(' ');
            if (words.Length <= 1)
            {
                return new CommandParameters();
            }

            var parameters = new CommandParameters();
            for (var i = 1; i < words.Length; i++)
            {
                parameters.AddParameter(words[i]);
            }

            return parameters;
        }
    }
}
