using System;
using System.Windows.Forms;
using Classic.IRCBot;
using IrcBot.GUI.Plugins;

namespace IrcBot.GUI
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var container = ContainerCreator.CreateContainer())
            {
                var botHandler = container.GetInstance<Handler>();
                var pluginWatcher = container.GetInstance<PluginWatcher>();
                pluginWatcher.Start();

                Application.Run(new FormMain(botHandler));
            }
        }


    }
}
