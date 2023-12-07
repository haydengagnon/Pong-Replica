using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace PongGame
{
    public partial class LevelFive : Form
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
        private int debuffX = 485;
        private int debuff1Y = 105;
        private int debuff2Y = 285;
        private int debuff3Y = 465;
        private int debuffWidth = 30;
        private int debuffHeight = 30;
        private bool debuffed = false;
        private int direction;
        private int cpuTeleport;
        private int teleportDirection;
        private int cpumove;

        public LevelFive()
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

            e.Graphics.FillRectangle(Brushes.Red, debuffX, debuff1Y, debuffWidth, debuffHeight);
            e.Graphics.FillRectangle(Brushes.Red, debuffX, debuff2Y, debuffWidth, debuffHeight);
            e.Graphics.FillRectangle(Brushes.Red, debuffX, debuff3Y, debuffWidth, debuffHeight);

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
            if (player2PaddleY > ballY && player2PaddleY > 0)
            {
                player2PaddleY -= 4;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += 4;
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

            // Check if ball touches or passses through debuff zones
            if (ballX + BALL_RADIUS > debuffX && ballX - BALL_RADIUS < debuffX + debuffWidth
                && ballY + BALL_RADIUS > debuff1Y && ballY - BALL_RADIUS < debuff1Y + debuffHeight)
            {
                debuffed = true;
            }
            if (ballX + BALL_RADIUS > debuffX && ballX - BALL_RADIUS < debuffX + debuffWidth
                && ballY + BALL_RADIUS > debuff2Y && ballY - BALL_RADIUS < debuff2Y + debuffHeight)
            {
                debuffed = true;
            }
            if (ballX + BALL_RADIUS > debuffX && ballX - BALL_RADIUS < debuffX + debuffWidth
                && ballY + BALL_RADIUS > debuff3Y && ballY - BALL_RADIUS < debuff3Y + debuffHeight)
            {
                debuffed = true;
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
                if (debuffed == false)
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
                else if (debuffed == true)
                {
                    Random teleport = new Random();
                    Random ballDirection = new Random();
                    cpuTeleport = teleport.Next(4);
                    teleportDirection = ballDirection.Next(3);
                    if (cpuTeleport == 0)
                    {
                        player2PaddleY = 0;
                        ballY = player2PaddleY + PADDLE_HEIGHT / 2;
                        if (teleportDirection == 0)
                        {
                            ballYVelocity -= 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 1)
                        {
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 2)
                        {
                            ballYVelocity += 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                    }

                    else if (cpuTeleport == 1)
                    {
                        player2PaddleY = 200 - PADDLE_HEIGHT;
                        ballY = player2PaddleY + PADDLE_HEIGHT / 2;
                        if (teleportDirection == 0)
                        {
                            ballYVelocity -= 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 1)
                        {
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 2)
                        {
                            ballYVelocity = +3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                    }

                    else if (cpuTeleport == 2)
                    {
                        player2PaddleY = 400 - PADDLE_HEIGHT;
                        ballY = player2PaddleY + PADDLE_HEIGHT / 2;
                        if (teleportDirection == 0)
                        {
                            ballYVelocity -= 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 1)
                        {
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 2)
                        {
                            ballYVelocity += 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                    }

                    else if (cpuTeleport == 3)
                    {
                        player2PaddleY = 600 - PADDLE_HEIGHT;
                        ballY = player2PaddleY + PADDLE_HEIGHT / 2;
                        if (teleportDirection == 0)
                        {
                            ballYVelocity -= 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 1)
                        {
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                        else if (teleportDirection == 2)
                        {
                            ballYVelocity += 3;
                            ballXVelocity = -ballXVelocity;
                            debuffed = false;
                        }
                    }
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
                StreamWriter sw = new StreamWriter("Campaign/Unlocked.txt",true);
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
        }
    }
}