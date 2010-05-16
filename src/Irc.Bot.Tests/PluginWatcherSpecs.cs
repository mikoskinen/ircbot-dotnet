using ClassicIRCBot.Tests.Stubs;
using IrcBot.GUI.Plugins;
using NUnit.Framework;

namespace Irc.Bot.Specs.FileSystemPluginWatcherSpecifications
{
    public abstract class PluginWatcherConcern : ConcernFor<FileSystemPluginWatcher>
    {
        protected PluginManagerStub manager = new PluginManagerStub();

        protected override FileSystemPluginWatcher CreateSubjectUnderTest()
        {
            var watcher = new FileSystemPluginWatcher(manager);

            return watcher; ;
        }
    }

    public class When_plugins_are_updated : PluginWatcherConcern
    {
        protected override void Because()
        {
            sut.ReactToChanges();
        }

        [Test]
        public void Should_ask_bot_handler_to_reload_commands()
        {
            Assert.That(manager.PluginReloadCount, Is.EqualTo(1));
        }
    }

    public class When_multiple_changes_happen_almost_instantly : PluginWatcherConcern
    {
        protected override void Because()
        {
            for (int i = 0; i < 5; i++)
            {
                sut.ReactToChanges();
            }
        }

        [Test]
        public void Should_only_once_ask_plugin_manager_to_reload_plugins()
        {
            Assert.That(manager.PluginReloadCount, Is.EqualTo(1));
        }
    }
}
