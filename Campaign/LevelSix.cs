using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PongGame
{
    public partial class LevelSix : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        private const int BALL_RADIUS = 10;
        private const int PADDLE_WIDTH = 20;
        private const int PADDLE_HEIGHT = 80;
        private int ballX = SCREEN_WIDTH / 2;
        private int ballY = SCREEN_HEIGHT / 2;
        private int ballXVelocity = 0;
        private int ballYVelocity = 0;
        private int p1PaddleVelocity = 0;
        private int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player1Score = 0;
        private int player2Score = 0;
        private int direction;
        private int cpumove;
        private bool obstacleOneVisible = true;
        private bool obstacleTwoVisible = true;
        private int obstacleOneX = 300;
        private int obstacleTwoX = 625;
        private const int obstacleWidth = 75;
        private const int obstacleHeight = 200;
        private int obstacleOneY = 0;
        private int ObstacleTwoY = SCREEN_HEIGHT - obstacleHeight;

        public LevelSix()
        {
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
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1;
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

            // Draw obstacles
            if (obstacleOneVisible == true)
            {
                e.Graphics.FillRectangle(Brushes.Gray, obstacleOneX, obstacleOneY, obstacleWidth, obstacleHeight);
            }
            else if (obstacleOneVisible == false)
            {

            }

            if (obstacleTwoVisible == true)
            {
                e.Graphics.FillRectangle(Brushes.Gray, obstacleTwoX, ObstacleTwoY, obstacleWidth, obstacleHeight);
            }
            else if (obstacleTwoVisible == false)
            {

            }

            // Draw scores
            e.Graphics.DrawString(player1Score.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 - 50, 10);
            e.Graphics.DrawString(player2Score.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 + 30, 10);
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
                ballXVelocity = 0;
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
                player2PaddleY -= 6;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += 6;
            }
            else if (player2PaddleY + 36 < ballY && player2PaddleY - 36 > ballY - PADDLE_HEIGHT && 
                player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT && player2PaddleY > 0 && ballYVelocity == 0)
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
                if (ballXVelocity < 10)
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
                if (ballXVelocity > -10)
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

            // Check if ball hits obstacles
            if (ballX + BALL_RADIUS >= obstacleOneX && ballX - BALL_RADIUS <= obstacleOneX + obstacleWidth && ballY - BALL_RADIUS <= obstacleOneY + obstacleHeight - 5)
            {
                ballXVelocity = -ballXVelocity;
                obstacleOneVisible = false;
            }
            else if (ballX + BALL_RADIUS >= obstacleOneX && ballX - BALL_RADIUS <= obstacleOneX + obstacleWidth && ballY - BALL_RADIUS <= obstacleOneY + obstacleHeight && ballYVelocity < 0)
            {
                ballYVelocity = -ballYVelocity;
                obstacleOneVisible = false;
            }

            if (ballX + BALL_RADIUS >= obstacleTwoX && ballX - BALL_RADIUS <= obstacleTwoX + obstacleWidth && ballY + BALL_RADIUS >= ObstacleTwoY + 5)
            {
                ballXVelocity = -ballXVelocity;
                obstacleTwoVisible = false;
            }
            else if (ballX + BALL_RADIUS >= obstacleTwoX && ballX - BALL_RADIUS <= obstacleTwoX + obstacleWidth && ballY + BALL_RADIUS >= ObstacleTwoY && ballYVelocity > 0)
            {
                ballYVelocity = -ballYVelocity;
                obstacleTwoVisible = false;
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
            }

            // Redraw form
            this.Invalidate();
            if (player1Score == 5)
            {
                StreamWriter sw = new StreamWriter("Unlocked.txt",true);
                sw.WriteLine("5");
                sw.Close();
                ballXVelocity = 0;
                
                CampaignHome winner1 = new CampaignHome();
                winner1.Show();
                player1Score++;
                this.Close();
            }
            if (player2Score == 5)
            {
                CampaignHome winner2 = new CampaignHome();
                winner2.Show();
                player2Score++;
                this.Close();
                ballXVelocity = 0;
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
            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            p1PaddleVelocity = 0;
            obstacleOneVisible = true;
            obstacleTwoVisible = true;
        }
    }
}