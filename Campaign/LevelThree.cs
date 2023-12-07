using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PongGame
{
    public partial class LevelThree : Form
    {
        private const int SCREEN_WIDTH = 1000;
        private const int SCREEN_HEIGHT = 600;
        private const int BALL_RADIUS = 10;
        private const int PADDLE_WIDTH = 20;
        private const int PADDLE_HEIGHT = 80;
        private int ballX = SCREEN_WIDTH / 2;
        private int ballY = SCREEN_HEIGHT / 2;
        //private int ballY = 200;
        //private int ballX = 800;
        private int ballXVelocity = 0;
        private int ballYVelocity = 0;
        private int p1PaddleVelocity = 0;
        private int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player1Score = 0;
        private int player2Score = 0;
        private int direction;
        private int cpumove;
        private int obstacleX = 300;
        private int obstacleWidth = 400;
        private int obstacleHeight = 40;
        private int obstacle1Y = 180;
        private int obstacle2Y = 380;

        public LevelThree()
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

            // Draw Obstacles
            e.Graphics.FillRectangle(Brushes.Gray, obstacleX, obstacle1Y, obstacleWidth, obstacleHeight);
            e.Graphics.FillRectangle(Brushes.Gray, obstacleX, obstacle2Y, obstacleWidth, obstacleHeight);

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
                ballXVelocity = 0;
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
            if (player2PaddleY > ballY && player2PaddleY > 0 && ballX > SCREEN_WIDTH / 2)
            {
                player2PaddleY -= 5;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT &&
                ballX > SCREEN_WIDTH / 2)
            {
                player2PaddleY += 5;
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
            if (ballY + BALL_RADIUS > obstacle1Y + 5 && ballY - BALL_RADIUS < obstacle1Y + obstacleHeight - 5)
            {
                if (ballX + BALL_RADIUS > obstacleX && ballX - BALL_RADIUS < obstacleX + obstacleWidth)
                {
                    ballXVelocity = - ballXVelocity;
                }
            }
            else if (ballY + BALL_RADIUS > obstacle1Y && ballY - BALL_RADIUS < obstacle1Y + obstacleHeight)
            {
                 if (ballX + BALL_RADIUS > obstacleX && ballX - BALL_RADIUS < obstacleX + obstacleWidth)
                 {
                    ballYVelocity = -ballYVelocity;
                 }
            }

            if (ballY + BALL_RADIUS > obstacle2Y + 5 && ballY - BALL_RADIUS < obstacle2Y + obstacleHeight - 5)
            {
                if (ballX + BALL_RADIUS > obstacleX && ballX - BALL_RADIUS < obstacleX + obstacleWidth)
                {
                    ballXVelocity = - ballXVelocity;
                }
            }
            else if (ballY + BALL_RADIUS > obstacle2Y && ballY - BALL_RADIUS < obstacle2Y + obstacleHeight)
            {
                 if (ballX + BALL_RADIUS > obstacleX && ballX - BALL_RADIUS < obstacleX + obstacleWidth)
                 {
                    ballYVelocity = -ballYVelocity;
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
            }

            // Redraw form
            this.Invalidate();
            if (player1Score == 5)
            {
                StreamWriter sw = new StreamWriter("Unlocked.txt",true);
                sw.WriteLine("3");
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
        }
    }
}