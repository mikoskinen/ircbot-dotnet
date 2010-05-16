using Classic.IRCBot;

namespace IrcBot.GUI
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormMain : System.Windows.Forms.Form
	{
	    private readonly Handler handler;
        private System.Windows.Forms.Button buttonConnect;

		public FormMain()
		{
			InitializeComponent();
		}

	    public FormMain(Handler handler)
	    {
	        this.handler = handler;
            InitializeComponent();
	    }

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.buttonConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(12, 12);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(112, 24);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(488, 81);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormMain";
            this.Text = "ClassiC IRC-Bot";
            this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
		    this.buttonConnect.Enabled = false;
            this.handler.CreateAndConnectBots();
		}

	}
}
