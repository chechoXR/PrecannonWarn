using PreCannon.Entities;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;
using System.Timers;
using System.Numerics;
using PreCannonWarn.Properties;

namespace PreCannon
{
    public partial class Form1 : Form
    {
        private System.Timers.Timer timer;
        private ElapsedEventHandler elapsedEventHandler;
        private System.Media.SoundPlayer player;
        public Form1()
        {
            InitializeComponent();
            this.ClientSize = new System.Drawing.Size(350, 380);
            this.Size = (this.ClientSize);
            this.Name = "Precannon Warn";
            this.Text = "Precannon Warn";
        }

        private void InitializeComponent()
        {
            
            this.player = new System.Media.SoundPlayer(Resources.notification1);
            player.Play();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.sync = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.PrecannonAdviser = new System.Windows.Forms.Label();
            this.Author = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // sync
            // 
            this.sync.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sync.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sync.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.sync.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.sync.Location = new System.Drawing.Point(12, 82);
            this.sync.Name = "sync";
            this.sync.Size = new System.Drawing.Size(326, 182);
            this.sync.TabIndex = 0;
            this.sync.Text = "Sincronizar tiempo";
            this.sync.UseVisualStyleBackColor = true;
            this.sync.Click += new System.EventHandler(this.button1_Click);
            // 
            // Time
            // 
            this.Time.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Time.AutoSize = true;
            this.Time.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Time.Location = new System.Drawing.Point(-1, 291);
            this.Time.MinimumSize = new System.Drawing.Size(350, 18);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(350, 30);
            this.Time.TabIndex = 1;
            this.Time.Text = "Sin datos";
            this.Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.Time.Click += new System.EventHandler(this.Time_Click);
            // 
            // PrecannonAdviser
            // 
            this.PrecannonAdviser.AllowDrop = true;
            this.PrecannonAdviser.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.PrecannonAdviser.Enabled = false;
            this.PrecannonAdviser.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.PrecannonAdviser.Location = new System.Drawing.Point(-1, 9);
            this.PrecannonAdviser.Name = "PrecannonWarn";
            this.PrecannonAdviser.Size = new System.Drawing.Size(350, 54);
            this.PrecannonAdviser.TabIndex = 2;
            this.PrecannonAdviser.Text = "Precannon Warn";
            this.PrecannonAdviser.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.PrecannonAdviser.UseCompatibleTextRendering = true;
            // 
            // Author
            // 
            this.Author.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Author.AutoSize = true;
            this.Author.Location = new System.Drawing.Point(271, 369);
            this.Author.Name = "Author";
            this.Author.Size = new System.Drawing.Size(81, 15);
            this.Author.TabIndex = 3;
            this.Author.Text = "By: ChechoXR";
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(350, 384);
            this.Controls.Add(this.Author);
            this.Controls.Add(this.PrecannonAdviser);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.sync);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //HTTP Request to know current game time
                var handler = new HttpClientHandler();
                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };

                CheckForIllegalCrossThreadCalls = false;

                HttpClient client = new HttpClient(handler);
                this.sync.Enabled = false;                      
                this.Time.Text = "Buscando información...";
                HttpResponseMessage request = await client.GetAsync("https://127.0.0.1:2999/liveclientdata/gamestats");

                HttpResponseMessage response = request;
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                GameStats stats = Newtonsoft.Json.JsonConvert.DeserializeObject<GameStats>(jsonResponse);
               
                this.sync.Enabled = true;
                
                //Relaci�n timestamp vs tiempo de partida
                DateTime timestamp = DateTime.Now;
              

                if (this.timer == null)
                    this.timer = new System.Timers.Timer();
                else
                    this.timer.Elapsed -= this.elapsedEventHandler;

                double interval = 1000;                                
                timer.Interval = interval;
                timer.AutoReset = true;
                
                
                this.elapsedEventHandler= ((s, e) => 
                { 
                    //Check time in list
                    List<string> PrecannonWaves = new List<string>();
                    PrecannonWaves.Add("2:00");
                    PrecannonWaves.Add("3:30");
                    PrecannonWaves.Add("5:00");
                    PrecannonWaves.Add("6:30");
                    PrecannonWaves.Add("8:00");
                    PrecannonWaves.Add("9:30");
                    PrecannonWaves.Add("11:00");
                    PrecannonWaves.Add("12:30");
                    PrecannonWaves.Add("14:00");
                    PrecannonWaves.Add("15:30");
                    PrecannonWaves.Add("17:00");
                    PrecannonWaves.Add("18:30");
                    PrecannonWaves.Add("20:00");



                    DateTime currentTime = DateTime.Now;
                    var TotalGameEllapsedSeconds = stats.gameTime + (currentTime - timestamp).TotalSeconds;
                    TotalGameEllapsedSeconds += 1;
                    int min2 = (int)TotalGameEllapsedSeconds / 60;
                    int seg2 = (int)TotalGameEllapsedSeconds % 60;

                    string tiempoPartida = min2+ ":" + seg2.ToString("D2");
                    
                    this.Time.Text = "Tiempo: " + tiempoPartida;


                    if (PrecannonWaves.Contains(tiempoPartida))
                        this.player.Play();
                    


                });

                timer.Elapsed += elapsedEventHandler;
                timer.Start();



            }
            catch (Exception ex)
            {
                this.sync.Enabled = true;
                
                if(this.elapsedEventHandler != null)
                    this.timer.Elapsed -= this.elapsedEventHandler;

                this.Time.Text = "No encuentro el juego :c";
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.InnerException);
                Debug.WriteLine(ex.StackTrace);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Time_Click(object sender, EventArgs e)
        {

        }
    }
}