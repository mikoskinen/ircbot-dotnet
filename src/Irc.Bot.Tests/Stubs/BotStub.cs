using System;
using System.Collections.Generic;
using ClassicIRCBot.Commands;
using Irc.Bot.Entities;
using Irc.Bot.Infrastructure;
using Irc.Entities;
using Irc.Infrastructure;
using NUnit.Framework;

namespace Irc.Bot.Stubs
{
    public class BotStub : Bot
    {
        public List<string> Said;
        public string LastSaid;
        public string LastPart;
        public string LastSendToServer;
        public bool SendToServerCalled;

        public void SetChannels(List<Channel> channels)
        {
            throw new NotImplementedException();
        }

        public void ConnectToServer(Server server, Credentials credentials)
        {
            throw new NotImplementedException();
        }

        public void SayTo(string receiver, string message)
        {
            LastSaid = message;

            if (Said == null)
                Said = new List<string>();

            Said.Add(message);
        }

        public void JoinAllChannels()
        {
            throw new NotImplementedException();
        }

        public void SetConnectionStatusTo(bool status)
        {
            throw new NotImplementedException();
        }

        public Users GetAuthenticatedUsers()
        {
            var users = new Users();

            var user = new User("testuser", 10);
            user.HostMask = "testhostmask";
            users.AddUser(user);

            return users;
        }

        public IList<Command> GetAllCommands()
        {
            var commands = new List<Command>();
            var testCommand = CommandStub.Make();
            var listCommands = new ListCommands(new BotStub());

            commands.Add(testCommand);
            commands.Add(listCommands);

            return commands;
        }

        public void JoinChannel(string channel, string password)
        {
            throw new NotImplementedException();
        }

        public void DisconnectFromServer()
        {
            throw new NotImplementedException();
        }

        public void SendToServer(string message)
        {
            SendToServerCalled = true;
            LastSendToServer = message;
        }

        public Credentials GetCredentials()
        {
            throw new NotImplementedException();
        }

        public void PartChannel(string channel)
        {
            this.LastPart = channel;
        }

        public void SetCommands(IEnumerable<Command> commands)
        {
            throw new NotImplementedException();
        }
    }
}