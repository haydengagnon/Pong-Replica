using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PongGame
{
    public partial class LevelSeven : Form
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
        private const int obstacle1X = 250;
        private const int obstacle1Y = 200;
        private const int obstacle2X = 700;
        private const int obstacle2Y = 200;
        private const int obstacle3X = 350;
        private const int obstacle3Y = 50;
        private const int obstacle4X = 350;
        private const int obstacle4Y = 500;
        private const int obstacleLongWidth = 300;
        private const int obstacleLongHeight = 50;
        private const int obstacleTallWidth = 50;
        private const int obstacleTallHeight = 200;

        public LevelSeven()
        {
            Random directionGenerator = new Random();
            direction = directionGenerator.Next(4);
            if (direction == 0)
            {
                ballXVelocity = -5;
                ballYVelocity = 3;
            }
            else if(direction == 1)
            {
                ballXVelocity = -5;
                ballYVelocity = -3;
            }
            else if(direction == 2)
            {
                ballXVelocity = 5;
                ballYVelocity = 3;
            }
            else if(direction == 3)
            {
                ballXVelocity = 5;
                ballYVelocity = -3;
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
            e.Graphics.FillRectangle(Brushes.Gray, obstacle1X, obstacle1Y, obstacleTallWidth, obstacleTallHeight);
            e.Graphics.FillRectangle(Brushes.Gray, obstacle2X, obstacle2Y, obstacleTallWidth, obstacleTallHeight);
            e.Graphics.FillRectangle(Brushes.Gray, obstacle3X, obstacle3Y, obstacleLongWidth, obstacleLongHeight);
            e.Graphics.FillRectangle(Brushes.Gray, obstacle4X, obstacle4Y, obstacleLongWidth, obstacleLongHeight);

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
            if (ballY - BALL_RADIUS < 0 || ballY + BALL_RADIUS > SCREEN_HEIGHT)
            {
                ballYVelocity = -ballYVelocity;
            }

            // Check if ball hits obstacles
            if (ballX + BALL_RADIUS + ballXVelocity > obstacle1X && ballX - BALL_RADIUS + ballXVelocity <= obstacle1X + obstacleTallWidth && ballY + BALL_RADIUS >= obstacle1Y && ballY - BALL_RADIUS <= obstacle1Y + obstacleTallHeight)
            {
                ballXVelocity = -ballXVelocity;
            }
            else if (ballX + BALL_RADIUS >= obstacle1X && ballX - BALL_RADIUS <= obstacle1X + obstacleTallWidth && ballY + BALL_RADIUS + ballYVelocity >= obstacle1Y && ballY - BALL_RADIUS + ballYVelocity <= obstacle1Y + obstacleTallHeight)
            {
                ballYVelocity = -ballYVelocity;
            }

            if (ballX + BALL_RADIUS + ballXVelocity >= obstacle2X && ballX - BALL_RADIUS + ballXVelocity <= obstacle2X + obstacleTallWidth && ballY + BALL_RADIUS >= obstacle2Y && ballY - BALL_RADIUS <= obstacle2Y + obstacleTallHeight)
            {
                ballXVelocity = -ballXVelocity;
            }
            else if (ballX + BALL_RADIUS >= obstacle2X && ballX - BALL_RADIUS <= obstacle2X + obstacleTallWidth && ballY + BALL_RADIUS + ballYVelocity >= obstacle2Y && ballY - BALL_RADIUS + ballYVelocity <= obstacle2Y + obstacleTallHeight)
            {
                ballYVelocity = -ballYVelocity;
            }

            if (ballX + BALL_RADIUS + ballXVelocity >= obstacle3X  && ballX - BALL_RADIUS + ballXVelocity <= obstacle3X + obstacleLongWidth  && ballY + BALL_RADIUS >= obstacle3Y && ballY - BALL_RADIUS <= obstacle3Y + obstacleLongHeight)
            {
                ballXVelocity = -ballXVelocity;
            }
            else if (ballX + BALL_RADIUS >= obstacle3X && ballX - BALL_RADIUS <= obstacle3X + obstacleLongWidth && ballY + BALL_RADIUS + ballYVelocity >= obstacle3Y && ballY - BALL_RADIUS + ballYVelocity <= obstacle3Y + obstacleLongHeight)
            {
                ballYVelocity = -ballYVelocity;
            }

            if (ballX + BALL_RADIUS + ballXVelocity >= obstacle4X && ballX - BALL_RADIUS + ballXVelocity <= obstacle4X + obstacleLongWidth && ballY + BALL_RADIUS >= obstacle4Y && ballY - BALL_RADIUS <= obstacle4Y + obstacleLongHeight)
            {
                ballXVelocity = -ballXVelocity;
            }
            else if (ballX + BALL_RADIUS >= obstacle4X && ballX - BALL_RADIUS <= obstacle4X + obstacleLongWidth && ballY + BALL_RADIUS + ballYVelocity >= obstacle4Y && ballY - BALL_RADIUS + ballYVelocity <= obstacle4Y + obstacleLongHeight)
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
                sw.WriteLine("7");
                sw.Close();
                ballXVelocity = 0;
                
                CampaignHome winner1 = new CampaignHome();
                winner1.Show();
                player1Score++;
                this.Close();
                ballXVelocity = 0;
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
                ballYVelocity = 3;
            }
            else if(direction == 1)
            {
                ballXVelocity = -5;
                ballYVelocity = -3;
            }
            else if(direction == 2)
            {
                ballXVelocity = 5;
                ballYVelocity = 3;
            }
            else if(direction == 3)
            {
                ballXVelocity = 5;
                ballYVelocity = -3;
            }
            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            p1PaddleVelocity = 0;
        }
    }
}