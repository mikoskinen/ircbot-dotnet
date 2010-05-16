using System;
using System.IO;
using Irc.Bot.Helpers;

namespace IrcBot.GUI.Plugins
{
    public interface PluginWatcher
    {
        void Start();
    }

    public class FileSystemPluginWatcher : PluginWatcher
    {
        private FileSystemWatcher watcher;
        private DateTime lastReactHappened;
        private TimeSpan containerUpdateTimeDelay;
        private PluginLoader loader;

        public FileSystemPluginWatcher(PluginLoader loader)
        {
            this.loader = loader;
        }

        public void Start()
        {
            this.ActivatePluginWatching();
            this.containerUpdateTimeDelay = 2.Seconds();
        }

        private void ActivatePluginWatching()
        {
            this.watcher = new FileSystemWatcher(@"C:\dev\projects\ClassicIRCBot\src\IrcBot.GUI\bin\Debug\plugins");

            watcher.Created += (sender, e) => ReactToChanges();
            watcher.Deleted += (sender, e) => ReactToChanges();
            watcher.Changed += (sender, e) => ReactToChanges();

            watcher.IncludeSubdirectories = false;
            watcher.EnableRaisingEvents = true;
        }

        public void ReactToChanges()
        {
            if (DidWeJustReactToChange())
                return;

            lastReactHappened = DateTime.Now;

            this.loader.ReloadPlugins();
        }

        private bool DidWeJustReactToChange()
        {
            var timeDifference = DateTime.Now - lastReactHappened;
            return TimeSpan.Compare(timeDifference, containerUpdateTimeDelay) < 1;
        }
    }
}