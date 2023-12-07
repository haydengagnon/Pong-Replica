using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class SurvivalRules : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;

        public SurvivalRules()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.KeyDown += new KeyEventHandler(GoHome_KeyDown);

            Label title = new Label();
            title.Text = "Welcome to Survival Mode";
            title.ForeColor = Color.White;
            title.BackColor = Color.Black;
            title.Font = new Font(title.Font.FontFamily, 48);
            title.Size = new Size(SCREEN_WIDTH, 100);
            title.TextAlign = ContentAlignment.MiddleCenter;
            title.BorderStyle = BorderStyle.None;
            this.Controls.Add(title);

            Label rules = new Label();
            rules.Text = "Score as many times as possible \n without getting scored on.";
            rules.ForeColor = Color.Yellow;
            rules.BackColor = Color.Black;
            rules.Font = new Font(rules.Font.FontFamily, 28);
            rules.Size = new Size(SCREEN_WIDTH, 150);
            rules.TextAlign = ContentAlignment.MiddleCenter;
            rules.Location = new Point(0, 100);
            this.Controls.Add(rules);

            Label controls = new Label();
            controls.Text = "Controls \n up = w | down = s";
            controls.ForeColor = Color.Yellow;
            controls.BackColor = Color.Black;
            controls.Font = new Font(controls.Font.FontFamily, 28);
            controls.Size = new Size(SCREEN_WIDTH, 150);
            controls.TextAlign = ContentAlignment.MiddleCenter;
            controls.BorderStyle = BorderStyle.None;
            controls.Location = new Point(0, 400);
            this.Controls.Add(controls);

            Button play = new Button();
            play.Text = "Play Survival";
            play.ForeColor = Color.Black;
            play.BackColor = Color.Green;
            play.Size = new Size (200, 60);
            play.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2);
            play.Click += new EventHandler(OnButonClickedPlaySurvival);
            this.Controls.Add(play);
        }

        private void OnButonClickedPlaySurvival(object sender, EventArgs e)
        {
            SurvivalPong survivalGame = new SurvivalPong();
            survivalGame.Show();
            this.Close();
        }

        private void GoHome_KeyDown(object sender, KeyEventArgs e)
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