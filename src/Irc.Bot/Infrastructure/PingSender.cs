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
		private System.IO.StreamWriter writer;
		private string serverAddress;

		private Thread pingSender;
		// Empty constructor makes instance of Thread
		public PingSender (ref System.IO.StreamWriter writer, ref string address)
		{
			pingSender = new Thread (new ThreadStart (this.Run) );
			this.serverAddress = address;
			this.writer = writer;
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
			try
			{
				while (true)
				{
					this.writer.WriteLine (PING + this.serverAddress);
					this.writer.Flush ();
					Thread.Sleep (15000);
				}
			} 
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
			}
		}	
	}
}
