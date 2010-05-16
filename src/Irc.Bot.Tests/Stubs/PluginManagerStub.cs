using IrcBot.GUI.Plugins;

namespace ClassicIRCBot.Tests.Stubs
{
    public class PluginManagerStub : PluginLoader
    {
        public int PluginReloadCount;

        public void ReloadPlugins()
        {
            this.PluginReloadCount += 1;
        }
    }
}
