using Classic.IRCBot;

namespace Irc.Bot.Stubs
{
    public class BotHandlerStub : Handler
    {
        public int PluginReloadCount;

        public void CreateAndConnectBots()
        {
            
        }

        public void ReloadPlugins()
        {
            PluginReloadCount +=1;
        }
    }
}