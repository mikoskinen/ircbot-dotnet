using System;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Classic.IRCBot
{
	/// <summary>
	/// IrcBot-class
	/// Can connect to server and join channel
	/// </summary>
	/// 

	public class IrcBot
	{
		#region Variables
		public static StreamWriter writer;
		protected string LogFileName;
		protected FileStream file;
		protected System.Windows.Forms.RichTextBox TextBox;
		protected NetworkStream stream;
		protected TcpClient irc;
		protected	StreamReader reader;
		protected StreamWriter sw;
		protected PingSender ping;
		protected bool Connected;

		#endregion

		#region Properties
		/// <summary>
		/// ServerAddress-property
		/// </summary>
		public static string serverAddress = "mediatraffic.fi.quakenet.org";
		public string ServerAddress
		{
			get
			{
				return serverAddress;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				serverAddress = value;
			}
		}
		/// <summary>
		/// BotNick-property
		/// </summary>
		protected static string botNick = "botti";
		public string BotNick
		{
			get
			{
				return botNick;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botNick = value;
			}
		}
		/// <summary>
		/// BotUser-property
		/// </summary>
		protected static string botUser = "USER Bottii 0 * :Botti";
		public string BotUser
		{
			get
			{
				return botUser;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botUser = value;
			}
		}
		/// <summary>
		/// BotChannel-property
		/// </summary>
		protected static string botChannel = "#botchannel";
		public string BotChannel
		{
			get
			{
				return botChannel;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botChannel = value;
			}
		}

		/// <summary>
		/// Bot's Quit-message property
		/// </summary>
		protected static string botQuitMessage = "I'm gone";
		public string BotQuitMessage
		{
			get
			{
				return botQuitMessage;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botQuitMessage = value;
			}
		}

		/// <summary>
		/// BotLogFileName-property
		/// </summary>
		protected static string botLogFileName = "botlog.log";
		public string BotLogFileName
		{
			get
			{
				return botLogFileName;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botLogFileName = value;
			}
		}

		/// <summary>
		/// BotLog-property
		/// </summary>
		protected static bool botLog = true;
		public bool BotLog
		{
			get
			{
				return botLog;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				botLog = value;
			}
		}


		/// <summary>
		/// ServerPort-property
		/// </summary>
		protected static int serverPort = 6667;
		public int ServerPort
		{
			get
			{
				return serverPort;
			}
			set
			{
				// Voidaan tarkistaa kelpoisuus
				serverPort = value;
			}
		}

	

		#endregion Private Fields

		#region Constructers
		/// <summary>
		/// IrcBot-constructor without parameters
		/// </summary>
		public IrcBot()
		{
			this.ServerAddress = "mediatraffic.fi.quakenet.org";
			this.ServerPort = 6667;
			this.BotChannel = "#kakerock";
			this.BotNick = "ClassicBot";
			this.BotUser = "USER ClassicBot";
			this.TextBox = null;
			this.Connected = false;
		}

		/// <summary>
		/// IrcBot-constructor with parameters
		/// </summary>
		/// <param name="address">ServerAddress</param>
		/// <param name="port">ServerPort</param>
		/// <param name="channel">BotChannel</param>
		/// <param name="nick">BotNick</param>
		/// <param name="user">BotUser</param>
		/// <param name="textbox">TextBox</param>
		public IrcBot(string address, int port, string channel, string nick, string user, System.Windows.Forms.RichTextBox textbox)
		{
			this.ServerAddress = address;
			this.ServerPort = port;
			this.BotChannel = channel;
			this.BotNick = nick;
			this.BotUser = user;
			this.TextBox = textbox;
			this.Connected = false;
		}

		/// <summary>
		/// IrcBot-constructor with parameters
		/// </summary>
		/// <param name="address">ServerAddress</param>
		/// <param name="port">ServerPort</param>
		/// <param name="channel">BotChannel</param>
		/// <param name="nick">BotNick</param>
		/// <param name="user">BotUser</param>
		/// <param name="textbox">TextBox</param>
		public IrcBot(string address, int port, string channel, string nick, string user, System.Windows.Forms.RichTextBox textbox, string quitmessage, bool log, string logfilename)
		{
			this.ServerAddress = address;
			this.ServerPort = port;
			this.BotChannel = channel;
			this.BotNick = nick;
			this.BotUser = user;
			this.TextBox = textbox;
			this.Connected = false;
			this.BotQuitMessage = quitmessage;
			this.BotLog = true;
			this.BotLogFileName = logfilename;
		}

		#endregion

		#region Methods
		/// <summary>
		/// Method that returns server's address
		/// </summary>
		/// <returns>String: ServerAddress</returns>
		public string getServerAddress()
		{
			return serverAddress;
		}
		/// <summary>
		/// Method that disconnects bot from server
		/// </summary>
		public void Disconnect()
		{
			if (this.TextBox != null)
				this.TextBox.AppendText ("Disconnected\n");
			writer.WriteLine("QUIT :{0}", this.BotQuitMessage);
			ping.Abort();
			writer.Flush();
			writer.Close ();
			reader.Close ();
			sw.Close();
			irc.Close ();
		}

		/// <summary>
		/// Method that joins a channel
		/// </summary>
		/// <param name="channel">Channel name (+password)</param>
		/// <returns></returns>
		public void JoinChannel(string channel)
		{
			writer.WriteLine ("JOIN " + channel);
			writer.Flush (); 
		}

		/// <summary>
		/// Method that connects to irc-server
		/// </summary>
		public void Connect()
		{
			string inputLine;

			if (this.BotLog)
			{
				LogFileName = this.BotLogFileName;
				file = new FileStream(LogFileName, FileMode.Create, FileAccess.Write);
				sw = new StreamWriter(file);
			}

			try
			{
				// Connecting to server
				if (this.TextBox != null)
					this.TextBox.AppendText("Connecting to server\n");
				irc = new TcpClient(ServerAddress, ServerPort);
				stream = irc.GetStream();
				reader = new StreamReader(stream);
				writer = new StreamWriter(stream);

				ping = new PingSender();
				ping.Start();

				writer.WriteLine (BotUser);
				writer.Flush ();
				writer.WriteLine ("NICK " + BotNick);
				writer.Flush ();
				writer.WriteLine ("JOIN " + BotChannel);
				writer.Flush (); 

				while (true)
				{                     
					// Oleskellaan kanavalla
					while ( (inputLine = reader.ReadLine () ) != null )
					{
						if (this.BotLog)
							sw.Write (inputLine + "\n");
						// Ei kirjoiteta turhaa tietoa textboxiin
						if (this.TextBox != null && inputLine.IndexOf("PONG") == -1 )
							this.TextBox.AppendText(inputLine + "\n");

						// Käsitellään serveriltä tuleva viesti

						if (inputLine.StartsWith ("PING"))
						{
							int index = inputLine.LastIndexOf(":");
							int numero = Int32.Parse(inputLine.Substring(index+1));
							if (this.TextBox != null)
								this.TextBox.AppendText("PONG :" +numero + "\n");
							writer.WriteLine("PONG :" + numero);
							writer.Flush();
							Thread.Sleep (1000);
						}

						// Tarkistetaan päästiinkö serverille. Jos kyllä, joinataan kanavalla
						if (inputLine.IndexOf("PONG") != -1 && this.Connected == false)
						{
							if (this.TextBox != null)
								this.TextBox.AppendText("Connected to server\n");
							this.JoinChannel(this.BotChannel);
							if (this.TextBox != null)
								this.TextBox.AppendText("Joined channel\n");

							this.Connected = true;
							Thread.Sleep(200);
						}

						if (inputLine.IndexOf("hello") != -1)
						{
							writer.WriteLine("PRIVMSG {0} : moi!", this.BotChannel);
							writer.Flush();
							Thread.Sleep(200);
						}

					}

					// Suljetaan streamit
					writer.Close ();
					reader.Close ();
					if (this.BotLog)
						sw.Close();
					irc.Close ();
				}
			}
			catch (Exception e)
			{
				if (this.TextBox != null)
				{
					this.TextBox.AppendText("Connection problem");
					this.TextBox.AppendText(e.ToString() + "\n");
				}
				Thread.Sleep(5000);
			}
		}

		#endregion
	}


}
