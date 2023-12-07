using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class PongPlayerSelect : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;

        public PongPlayerSelect()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.KeyDown += new KeyEventHandler(PSelect_KeyDown);

            Label gameCenter = new Label();
            gameCenter.Text = "Please select the number of players";
            gameCenter.ForeColor = Color.White;
            gameCenter.BackColor = Color.Black;
            gameCenter.Font = new Font(gameCenter.Font.FontFamily, 42);
            gameCenter.Size = new Size(SCREEN_WIDTH, 100);
            gameCenter.TextAlign = ContentAlignment.MiddleCenter;
            gameCenter.BorderStyle = BorderStyle.None;
            this.Controls.Add(gameCenter);

            Label controls = new Label();
            controls.Text = "Controls \n Player 1: up = w | down = s \n Player 2: up = up arrow | down = down arrow";
            controls.ForeColor = Color.Yellow;
            controls.BackColor = Color.Black;
            controls.Font = new Font(controls.Font.FontFamily, 28);
            controls.Size = new Size(SCREEN_WIDTH, 150);
            controls.TextAlign = ContentAlignment.MiddleCenter;
            controls.BorderStyle = BorderStyle.None;
            controls.Location = new Point(0, 400);
            this.Controls.Add(controls);

            Button oneplayer = new Button();
            oneplayer.Text = "1 Player";
            oneplayer.ForeColor = Color.Black;
            oneplayer.BackColor = Color.Green;
            oneplayer.Location = new Point(SCREEN_WIDTH / 2 - 100, 200);
            oneplayer.Size = new Size(200, 60);
            oneplayer.Click += new EventHandler(OnButtonClickedSingle);
            this.Controls.Add(oneplayer);

            Button twoplayer = new Button();
            twoplayer.Text = "2 Players";
            twoplayer.ForeColor = Color.Black;
            twoplayer.BackColor = Color.Green;
            twoplayer.Location = new Point(SCREEN_WIDTH / 2 - 100, 260);
            twoplayer.Size = new Size(200, 60);
            twoplayer.Click += new EventHandler(OnButtonClickedDouble);
            this.Controls.Add(twoplayer);
        }

        private void OnButtonClickedSingle(object sender, EventArgs e)
        {
            // Open Singleplayer
            PongOnePlayer sinlgeplayer = new PongOnePlayer();
            sinlgeplayer.Show();
            this.Close();
        }

        private void OnButtonClickedDouble(object sender, EventArgs e)
        {
            //Open 2 players
            Form1 form1 = new Form1();
            form1.Show();
            this.Close();
        }

        private void PSelect_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }
        }
    }
}