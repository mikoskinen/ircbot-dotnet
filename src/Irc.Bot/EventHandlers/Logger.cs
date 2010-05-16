using System;
using System.IO;
using Irc;
using Irc.Bot.Events;
using Irc.Events;

namespace ClassicIRCBot.EventHandlers
{
    public class Logger : IrcEventHandler<ServerMessageReceived>, IrcEventHandler<PrivateMessageReceived>
    {
        private readonly string logFilePath;

        public Logger()
        {
            this.logFilePath = "botlog.log";
        }

        public void Handle(PrivateMessageReceived args)
        {
            this.Handle(args.Message.OriginalMessage);
        }

        public void Handle(ServerMessageReceived args)
        {
            this.Handle(args.Message);
        }

        private void Handle(string message)
        {
            lock (this)
            {
                try
                {
                    if (string.IsNullOrEmpty(message))
                        return;

                    if (IsPongMessage(message))
                        return;

                    Log(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

        private bool IsPongMessage(string message)
        {
            return message.IndexOf("PONG") >= 0;
        }

        private void Log(string message)
        {
            var fi = new FileStream(logFilePath, FileMode.Append, FileAccess.Write);
            var sw = new StreamWriter(fi);

            var logMessage = this.CreateLogMessage(message);
            sw.WriteLine(logMessage);

            sw.Flush();
            sw.Close();
        }

        private string CreateLogMessage(string message)
        {
            var logMessage = string.Format("[{0}] {1}", DateTime.Now.ToString(), message);
            return logMessage;
        }


    }
}
