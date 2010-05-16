using System.Collections.Generic;
using Irc.Bot.Entities;
using Irc.Bot.Infrastructure;
using Irc.Entities;
using Irc.Infrastructure;

namespace Irc.Bot
{
    public interface Bot
    {
        void SetChannels(List<Channel> channels);
        void ConnectToServer(Server server, Credentials credentials);
        void SayTo(string receiver, string message);
        void JoinAllChannels();
        Users GetAuthenticatedUsers();
        IList<Command> GetAllCommands();
        void JoinChannel(string channel, string password);
        void DisconnectFromServer();
        void SendToServer(string message);
        Credentials GetCredentials();
        void PartChannel(string channel);
        void SetCommands(IEnumerable<Command> commands);
    }
}