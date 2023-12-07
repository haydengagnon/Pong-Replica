using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class FormHome : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;

        public FormHome()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;

            Label gameCenter = new Label();
            gameCenter.Text = "Welcome to my Pong Games";
            gameCenter.ForeColor = Color.White;
            gameCenter.BackColor = Color.Black;
            gameCenter.Font = new Font(gameCenter.Font.FontFamily, 48);
            gameCenter.Size = new Size(SCREEN_WIDTH, 100);
            gameCenter.TextAlign = ContentAlignment.MiddleCenter;
            gameCenter.BorderStyle = BorderStyle.None;
            this.Controls.Add(gameCenter);

            Label backKey = new Label();
            backKey.Text = "Hit Esc at any time to return to this menu";
            backKey.ForeColor = Color.Yellow;
            backKey.Font = new Font(backKey.Font.FontFamily, 20);
            backKey.Size = new Size(SCREEN_WIDTH, 100);
            backKey.Location = new Point(0, 60);
            backKey.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(backKey);

            Button survival = new Button();
            survival.Text = "Survival Mode";
            survival.ForeColor = Color.Black;
            survival.BackColor = Color.Green;
            survival.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 + 100);
            survival.Size = new Size(200, 60);
            survival.Click += new EventHandler(OnButtonClickedSurvival);
            this.Controls.Add(survival);

            Button pong = new Button();
            pong.Text = "Pong";
            pong.ForeColor = Color.Black;
            pong.BackColor = Color.Green;
            pong.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 - 60);
            pong.Size = new Size(200, 60);
            pong.Click += new EventHandler(OnButtonClickedPong);
            this.Controls.Add(pong);

            Button megaPong = new Button();
            megaPong.Text = "Mega Pong";
            megaPong.ForeColor = Color.Black;
            megaPong.BackColor = Color.Green;
            megaPong.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 + 20);
            megaPong.Size = new Size(200, 60);
            megaPong.Click += new EventHandler(OnButtonClickedMegaPong);
            this.Controls.Add(megaPong);

            Button highScores = new Button();
            highScores.Text = "High Scores";
            highScores.ForeColor = Color.Black;
            highScores.BackColor = Color.Green;
            highScores.Location = new Point(SCREEN_WIDTH / 2 + 120, SCREEN_HEIGHT / 2 + 100);
            highScores.Size = new Size(200, 60);
            highScores.Click += new EventHandler(OnButtonClickedHighScores);
            this.Controls.Add(highScores);

            Button campaign = new Button();
            campaign.Text = "Campaign";
            campaign.ForeColor = Color.Black;
            campaign.BackColor = Color.Green;
            campaign.Location = new Point(SCREEN_WIDTH / 2 - 100, SCREEN_HEIGHT / 2 - 140);
            campaign.Size = new Size(200, 60);
            campaign.Click += new EventHandler(OnButtonClickedCampaign);
            this.Controls.Add(campaign);
        }

        private void OnButtonClickedPong(object sender, EventArgs e)
        {
            // Open Form1.cs
            PongPlayerSelect ppselect = new PongPlayerSelect();
            ppselect.Show();
            this.Close();
        }

        private void OnButtonClickedMegaPong(object sender, EventArgs e)
        {
            MegaPong megaPongGame = new MegaPong();
            megaPongGame.Show();
            this.Close();
        }

        private void OnButtonClickedSurvival(object sender, EventArgs e)
        {
            SurvivalRules survivalRules = new SurvivalRules();
            survivalRules.Show();
            this.Close();
        }

        private void OnButtonClickedHighScores(object sender, EventArgs e)
        {
            HighScores scorelist = new HighScores();
            scorelist.Show();
            this.Close();
        }

        private void OnButtonClickedCampaign(object sender, EventArgs e)
        {
            CampaignHome levelSelect = new CampaignHome();
            levelSelect.Show();
            this.Close();
        }
    }
}
