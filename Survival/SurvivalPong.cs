using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Data.SQLite;

namespace PongGame
{
    public partial class SurvivalPong : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        private const int BALL_RADIUS = 10;
        private const int PADDLE_WIDTH = 20;
        private const int PADDLE_HEIGHT = 80;
        private int ballX = SCREEN_WIDTH / 2;
        private int ballY = SCREEN_HEIGHT / 2;
        private int ballXVelocity = -5;
        private int ballYVelocity = 0;
        private int p1PaddleVelocity = 0;
        private int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        public int player1Score = 0;
        private int player2Score = 0;
        private int cpumove = 1;
        private int direction;
        private int maxXVelocity = 10;
        SQLiteConnection sqlite_conn;

        public SurvivalPong()
        {
            InitializeComponent();
            Random directionGenerator = new Random();
            direction = directionGenerator.Next(2);
            if (direction == 0)
            {
                ballXVelocity = -5;
                ballYVelocity = 0;
            }
            else if(direction == 1)
            {
                ballXVelocity = 5;
                ballYVelocity = 0;
            }
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 10;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            gameTimer.Start();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // Draw paddles
            e.Graphics.FillRectangle(Brushes.Green, 0, player1PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);
            e.Graphics.FillRectangle(Brushes.Red, SCREEN_WIDTH - PADDLE_WIDTH, player2PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);

            // Draw ball
            e.Graphics.FillEllipse(Brushes.White, ballX - BALL_RADIUS, ballY - BALL_RADIUS, BALL_RADIUS * 2, BALL_RADIUS * 2);

            // Draw scores
            e.Graphics.DrawString(player1Score.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 - 50, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Move player paddles
            if (e.KeyCode == Keys.W && player1PaddleY > 0)
            {
                p1PaddleVelocity -= 8;
            }
            else if (e.KeyCode == Keys.S  && player1PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                p1PaddleVelocity += 8;
            }

            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Update ball position
            ballX += ballXVelocity;
            ballY += ballYVelocity;
        
            // Update Paddle Position
            player1PaddleY += p1PaddleVelocity;
            if (p1PaddleVelocity > 0)
            {
                p1PaddleVelocity--;
            }
            else if (p1PaddleVelocity < 0)
            {
                p1PaddleVelocity++;
            }


            // Check if paddle hits top or bottom of screen
            if(player1PaddleY <= 0 || player1PaddleY + PADDLE_HEIGHT >= SCREEN_HEIGHT)
            {
                p1PaddleVelocity = -p1PaddleVelocity;
            }

            // Computer moves paddle
            if (player2PaddleY > ballY && player2PaddleY > 0)
            {
                player2PaddleY -= 5;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += 5;
            }
            else if (player2PaddleY + 36 < ballY && player2PaddleY - 36 > ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT && player2PaddleY > 0 && ballYVelocity < 1)
            {
                Random cpumoveGenerator = new Random();
                cpumove = cpumoveGenerator.Next(2);
                if(cpumove == 0)
                {
                    player2PaddleY -= 10;
                }
                else if(cpumove == 1)
                {
                    player2PaddleY += 10;
                }
            }

            // Check if ball hits top or bottom of screen
            if (ballY - BALL_RADIUS <= 0 || ballY + BALL_RADIUS >= SCREEN_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;
            }

            // Check if ball hits player paddles
            if (ballX - BALL_RADIUS <= PADDLE_WIDTH && ballY >= player1PaddleY && ballY <= player1PaddleY + PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                if (ballXVelocity < maxXVelocity)
                {
                    ballXVelocity++;
                }
                if (ballY <= player1PaddleY + PADDLE_HEIGHT && ballY > player1PaddleY + PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY >= player1PaddleY && ballY < player1PaddleY + PADDLE_HEIGHT /2)
                {
                    ballYVelocity--;
                }
            }
            else if (ballX + BALL_RADIUS >= SCREEN_WIDTH - PADDLE_WIDTH && ballY >= player2PaddleY && ballY <= player2PaddleY + PADDLE_HEIGHT)
            {
                ballXVelocity = -ballXVelocity;
                if (ballXVelocity > -maxXVelocity)
                {
                    ballXVelocity--;
                }
                if (ballY <= player2PaddleY + PADDLE_HEIGHT && ballY > player2PaddleY + PADDLE_HEIGHT / 2) 
                {
                    ballYVelocity++;
                }
                else if (ballY >= player2PaddleY && ballY < player2PaddleY + PADDLE_HEIGHT /2)
                {
                    ballYVelocity--; 
                }
            }

            // Check if ball goes out of bounds
            if (ballX - BALL_RADIUS <= 0)
            {
                player2Score++;
                ResetBall();
            }
            else if (ballX + BALL_RADIUS >= SCREEN_WIDTH)
            {
                player1Score++;
                ResetBall();
                cpumove++;
                maxXVelocity++;
            }

            // Game over screen
            this.Invalidate();
            if (player2Score > 0)
            {
                ballXVelocity = 0;
                InsertData(sqlite_conn);
                OrderData(sqlite_conn);
                SurvivalGameOver p1score = new SurvivalGameOver(player1Score);
                p1score.Show();
                this.Close();
                player2Score = 0;
            }
        }

        private void ResetBall()
        {
            ballX = SCREEN_WIDTH / 2;
            ballY = SCREEN_HEIGHT / 2;
            Random directionGenerator = new Random();
            direction = directionGenerator.Next(2);
            if (direction == 0)
            {
                ballXVelocity = -5;
                ballYVelocity = 0;
            }
            else if(direction == 1)
            {
                ballXVelocity = 5;
                ballYVelocity = 0;
            }
            ballYVelocity = 0;
            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            p1PaddleVelocity = 0;
        }

        void InsertData(SQLiteConnection conn)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = "INSERT INTO scores (score) VALUES ($player1Score);";
            sqlite_cmd.Parameters.AddWithValue("$player1Score", player1Score);
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }

        void OrderData(SQLiteConnection conn)
        {
            sqlite_conn = new SQLiteConnection("Data Source=Survival/ScoreList.db");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = new SQLiteCommand(sqlite_conn);
            sqlite_cmd.CommandText = "SELECT rowid, score FROM scores ORDER BY score DESC";
            sqlite_cmd.ExecuteNonQuery();
            sqlite_conn.Close();
        }
    }
}