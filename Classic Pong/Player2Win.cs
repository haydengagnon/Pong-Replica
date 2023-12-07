using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class Player2Win : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;

        public Player2Win()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            Label winner = new Label();
            winner.Text = "Player 2 Wins";
            winner.ForeColor = Color.Red;
            winner.BackColor = Color.Black;
            winner.Font = new Font(winner.Font.FontFamily, 48);
            winner.Size = new Size(SCREEN_WIDTH, 100);
            winner.TextAlign = ContentAlignment.MiddleCenter;
            winner.BorderStyle = BorderStyle.None;
            this.Controls.Add(winner);

            Button home = new Button();
            home.Text = "Return to Menu";
            home.ForeColor = Color.Black;
            home.BackColor = Color.Green;
            home.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 - 30);
            home.Size = new Size(200, 60);
            home.Click += new EventHandler(OnButtonClickedHome);
            this.Controls.Add(home);

            Button replay = new Button();
            replay.Text = "Play Again";
            replay.ForeColor = Color.Black;
            replay.BackColor = Color.Green;
            replay.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 + 30);
            replay.Size = new Size(200, 60);
            replay.Click += new EventHandler(OnButtonClickedReplay);
            this.Controls.Add(replay);
        }

        private void OnButtonClickedHome(object sender, EventArgs e)
        {
            FormHome home = new FormHome();
            home.Show();
            this.Close();
        }

        private void OnButtonClickedReplay(object sender, EventArgs e)
        {
            Form1 replay = new Form1();
            replay.Show();
            this.Close();
        }
    }
}