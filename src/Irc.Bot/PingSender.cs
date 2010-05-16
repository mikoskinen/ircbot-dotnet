using System;
using System.Threading;


namespace Classic.IRCBot
{
	/// <summary>
	/// Class that sends PING to irc server every 15 seconds
	/// </summary>
	public class PingSender
	{
		static string PING = "PING :";
		private Thread pingSender;
		// Empty constructor makes instance of Thread
		public PingSender ()
		{
			pingSender = new Thread (new ThreadStart (this.Run) );
		} 


		// Starts the thread
		public void Start ()
		{
			pingSender.Start ();
		}

		// Kills the thread
		public void Abort ()
		{
			pingSender.Abort();
		}



		// Send PING to irc server every 15 seconds
		public void Run ()
		{
			while (true)
			{
				IrcBot.writer.WriteLine (PING + IrcBot.serverAddress);
				IrcBot.writer.Flush ();
				Thread.Sleep (15000);
			}
		}	
	}
}
