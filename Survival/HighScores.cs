using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace PongGame
{
    public partial class HighScores : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        string topScoresQuery = "SELECT score FROM scores ORDER BY score DESC LIMIT 5";
        SQLiteConnection sqlite_conn;

        public HighScores()
        {
            InitializeComponent();
            QueryTopScores(topScoresQuery);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(GoHome_KeyDown);

            Label gameCenter = new Label();
            gameCenter.Text = "HIGH SCORES";
            gameCenter.ForeColor = Color.White;
            gameCenter.BackColor = Color.Black;
            gameCenter.Font = new Font(gameCenter.Font.FontFamily, 48);
            gameCenter.Size = new Size(SCREEN_WIDTH, 100);
            gameCenter.TextAlign = ContentAlignment.MiddleCenter;
            gameCenter.BorderStyle = BorderStyle.None;
            this.Controls.Add(gameCenter);

            Label listScores1 = new Label();
            listScores1.Text = Convert.ToString("Score:");
            listScores1.ForeColor = Color.Yellow;
            listScores1.BackColor = Color.Black;
            listScores1.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores1.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores1.Location = new Point(250, 100);
            listScores1.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores1);

            Label listScores2 = new Label();
            listScores2.Text = Convert.ToString("Score:");
            listScores2.ForeColor = Color.Yellow;
            listScores2.BackColor = Color.Black;
            listScores2.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores2.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores2.Location = new Point(250, 200);
            listScores2.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores2);

            Label listScores3 = new Label();
            listScores3.Text = Convert.ToString("Score:");
            listScores3.ForeColor = Color.Yellow;
            listScores3.BackColor = Color.Black;
            listScores3.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores3.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores3.Location = new Point(250, 300);
            listScores3.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores3);

            Label listScores4 = new Label();
            listScores4.Text = Convert.ToString("Score:");
            listScores4.ForeColor = Color.Yellow;
            listScores4.BackColor = Color.Black;
            listScores4.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores4.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores4.Location = new Point(250, 400);
            listScores4.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores4);

            Label listScores5 = new Label();
            listScores5.Text = Convert.ToString("Score:");
            listScores5.ForeColor = Color.Yellow;
            listScores5.BackColor = Color.Black;
            listScores5.Font = new Font(gameCenter.Font.FontFamily, 28);
            listScores5.Size = new Size(SCREEN_WIDTH / 2, 100);
            listScores5.Location = new Point(250, 500);
            listScores5.BorderStyle = BorderStyle.None;
            this.Controls.Add(listScores5);
        }

        void OrderData(SQLiteConnection conn)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = "SELECT score FROM scores ORDER BY score DESC LIMIT 5";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        void QueryTopScores(string txtQuery)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = txtQuery;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
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