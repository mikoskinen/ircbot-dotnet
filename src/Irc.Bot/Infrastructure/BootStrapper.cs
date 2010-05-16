//using ClassicIRCBot.Commands;
//using ClassicIRCBot.EventHandlers;
//using Irc;
//using Irc.Events;

//namespace ClassicIRCBot.Infrastructure
//{
//    public static class BootStrapper
//    {
//        static BootStrapper()
//        {
//            ConfigureContainer();
//        }

//        private static void ConfigureContainer()
//        {
            
//        }
//    }

//    interface BootStrapperTask
//    {
//        void Execute();
//    }

//    class RegisterEventHandlers : BootStrapperTask
//    {
//        public void Execute()
//        {
//            EventSource.Register<PingReceived>(new PongSender(this));
//            EventSource.Register<ConnectedToServer>(new ConnectedStatusSetter(this));
//            EventSource.Register<ConnectedToServer>(new ChannelJoiner(this));
//        }
//    }

//    class RegisterCommands : BootStrapperTask
//    {
//        public void Execute()
//        {
//            RegisterCommand(new ListCommands());
//            RegisterCommand(new ListAuthenticatedUsers());
//            RegisterCommand(new AuthenticateUser(this._dataSource));
//            RegisterCommand(new JoinChannel());
//        }

//        private void RegisterCommand(Command command)
//        {
//            command.Functions = this;
//            command.CommandCharacter = this.ScriptChararacter;
//            command.ParametersFactory = new CommandParametersFactory();

//            EventSource.Register<MessageReceived>(command);
//        }
//    }
//}
