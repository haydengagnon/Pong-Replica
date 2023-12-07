using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class MegaPong : Form
    {
        private const int SCREEN_WIDTH = 900;
        private const int SCREEN_HEIGHT = 900;
        private const int BALL_RADIUS = 10;
        private const int PADDLE_WIDTH = 20;
        private const int PADDLE_HEIGHT = 80;
        private const int PADDLE_MOVE_AMOUNT = 20;
        private int ballX = SCREEN_WIDTH / 2;
        private int ballY = SCREEN_HEIGHT / 2;
        private int ballXVelocity = 0;
        private int ballYVelocity = 0;
        private int p1PaddleVelocity = 0;
        private int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        private int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;        
        private int player3PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
        private int player4PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
        private int player1Lives = 5;
        private int player2Lives = 5;
        private int player3Lives = 5;
        private int player4Lives = 5;
        private int cpuMovement = 5;
        private int direction;
        private int cpumove;

        public MegaPong()
        {
            Random directionGenerator = new Random();
            direction = directionGenerator.Next(4);
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
            else if (direction == 2)
            {
                ballYVelocity = -5;
                ballXVelocity = 0;
            }
            else if (direction == 3)
            {
                ballYVelocity = 5;
                ballXVelocity = 0;
            }

            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ClientSize = new Size(SCREEN_WIDTH, SCREEN_HEIGHT);
            this.DoubleBuffered = true;
            this.BackColor = Color.Black;
            this.Paint += new PaintEventHandler(MegaPong_Paint);
            this.KeyDown += new KeyEventHandler(MegaPong_KeyDown);
            System.Windows.Forms.Timer gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
            gameTimer.Start();
        }

        private void MegaPong_Paint(object sender, PaintEventArgs e)
        {
            // Draw paddles and lives if player has remaining lives
            //player 1
            if (player1Lives > 0)
            {
                e.Graphics.FillRectangle(Brushes.Green, 0, player1PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);
                e.Graphics.DrawString(player1Lives.ToString(), Font, Brushes.White, 0, player1PaddleY + PADDLE_HEIGHT / 2);
            }

            //player 2
            if (player2Lives > 0)
            {
                e.Graphics.FillRectangle(Brushes.Red, SCREEN_WIDTH - PADDLE_WIDTH, player2PaddleY, PADDLE_WIDTH, PADDLE_HEIGHT);
                e.Graphics.DrawString(player2Lives.ToString(), Font, Brushes.White, SCREEN_WIDTH - PADDLE_WIDTH, player2PaddleY + PADDLE_HEIGHT / 2);
            }

            //player 3
            if (player3Lives > 0)
            {
                e.Graphics.FillRectangle(Brushes.Yellow, player3PaddleX, 0, PADDLE_HEIGHT, PADDLE_WIDTH);
                e.Graphics.DrawString(player3Lives.ToString(), Font, Brushes.Black, player3PaddleX, 0);
            }

            //player 4
            if (player4Lives > 0)
            {
                e.Graphics.FillRectangle(Brushes.Blue, player4PaddleX, SCREEN_HEIGHT - PADDLE_WIDTH, PADDLE_HEIGHT, PADDLE_WIDTH);
                e.Graphics.DrawString(player4Lives.ToString(), Font, Brushes.White, player4PaddleX, SCREEN_HEIGHT - PADDLE_WIDTH);
            }
            
            // Draw ball
            e.Graphics.FillEllipse(Brushes.White, ballX - BALL_RADIUS, ballY - BALL_RADIUS, BALL_RADIUS * 2, BALL_RADIUS * 2);

        }

        private void MegaPong_KeyDown(object sender, KeyEventArgs e)
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

            //Computer paddle movement
            if (player2PaddleY > ballY && player2PaddleY > 0)
            {
                player2PaddleY -= cpuMovement;
            }
            else if (player2PaddleY < ballY - PADDLE_HEIGHT && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                player2PaddleY += cpuMovement;
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

            if (player3PaddleX > ballX && player3PaddleX > 0)
            {
                player3PaddleX -= cpuMovement;
            }
            else if (player3PaddleX < ballX - PADDLE_HEIGHT && player3PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player3PaddleX += cpuMovement;
            }
            else if (player3PaddleX + 36 < ballX && player3PaddleX - 36 > ballX - PADDLE_HEIGHT && 
                player3PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT && player3PaddleX > 0 && ballXVelocity == 0)
            {
                Random cpumoveGenerator = new Random();
                cpumove = cpumoveGenerator.Next(2);
                if(cpumove == 0)
                {
                    player3PaddleX -= 10;
                }
                else if(cpumove == 1)
                {
                    player3PaddleX += 10;
                }
            }

            if (player4PaddleX > ballX && player4PaddleX > 0)
            {
                player4PaddleX -= cpuMovement;
            }
            else if (player4PaddleX < ballX - PADDLE_HEIGHT && player4PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT)
            {
                player4PaddleX += cpuMovement;
            }
            else if (player4PaddleX + 36 < ballX && player4PaddleX - 36 > ballX - PADDLE_HEIGHT && 
                player4PaddleX < SCREEN_WIDTH - PADDLE_HEIGHT && player4PaddleX > 0 && ballXVelocity == 0)
            {
                Random cpumoveGenerator = new Random();
                cpumove = cpumoveGenerator.Next(2);
                if(cpumove == 0)
                {
                    player4PaddleX -= 10;
                }
                else if(cpumove == 1)
                {
                    player4PaddleX += 10;
                }
            }

            // Check if ball hits player paddles
            if (player1Lives > 0)
            {
                if (ballX - BALL_RADIUS <= PADDLE_WIDTH && ballY >= player1PaddleY && ballY <= player1PaddleY + PADDLE_HEIGHT)
                {
                    ballXVelocity = -ballXVelocity;

                    if (ballY - 1 < player1PaddleY + PADDLE_HEIGHT && ballY > player1PaddleY + PADDLE_HEIGHT / 2) 
                    {
                        ballYVelocity++;
                    }
                    else if (ballY + 1 > player1PaddleY && ballY < player1PaddleY + PADDLE_HEIGHT /2)
                    {
                        ballYVelocity--;
                    }
                    if (ballXVelocity < 10)
                    {
                        ballXVelocity++;
                    }
                }
            }

            if (player2Lives > 0)
            {
                if (ballX + BALL_RADIUS >= SCREEN_WIDTH - PADDLE_WIDTH && ballY >= player2PaddleY && ballY <= player2PaddleY + PADDLE_HEIGHT)
                {
                    ballXVelocity = -ballXVelocity;
                    
                    if (ballY - 1 < player2PaddleY + PADDLE_HEIGHT && ballY > player2PaddleY + PADDLE_HEIGHT / 2) 
                    {
                        ballYVelocity++;
                    }
                    else if (ballY + 1 > player2PaddleY && ballY < player2PaddleY + PADDLE_HEIGHT / 2)
                    {
                        ballYVelocity--;
                    }
                    if (ballXVelocity > -10)
                    {
                        ballXVelocity--;
                    }
                }
            }

            if (player3Lives > 0)
            {
                if (ballY - BALL_RADIUS <= PADDLE_WIDTH && ballX >= player3PaddleX && ballX <= player3PaddleX + PADDLE_HEIGHT)
                {
                    ballYVelocity = -ballYVelocity;

                    if (ballX - 1 < player3PaddleX + PADDLE_HEIGHT && ballX > player3PaddleX + PADDLE_HEIGHT / 2)
                    {
                        ballXVelocity++;
                    }
                    else if (ballX + 1 > player3PaddleX && ballX < player3PaddleX + PADDLE_HEIGHT / 2)
                    {
                        ballXVelocity--;
                    }
                    
                    if (ballYVelocity < 10)
                    {
                        ballYVelocity++;
                    }
                }
            }

            if (player4Lives > 0)
            {
                if (ballY + BALL_RADIUS >= SCREEN_HEIGHT - PADDLE_WIDTH && ballX >= player4PaddleX && ballX <= player4PaddleX + PADDLE_HEIGHT)
                {
                    ballYVelocity = -ballYVelocity;

                    if (ballX - 1 < player4PaddleX + PADDLE_HEIGHT && ballX > player4PaddleX + PADDLE_HEIGHT / 2)
                    {
                        ballXVelocity++;
                    }
                    else if (ballX + 1 > player4PaddleX && ballX < player4PaddleX + PADDLE_HEIGHT / 2)
                    {
                        ballXVelocity--;
                    }

                    if (ballYVelocity > - 10)
                    {
                        ballYVelocity--;
                    }
                }
            }


            // Check if ball goes out of bounds
            if (player1Lives > 0)
            {
                if (ballX - BALL_RADIUS <= 0)
                {
                    player1Lives--;
                    ResetBall();
                }
            }
            else if (player1Lives == 0)
            {
                if (ballX - BALL_RADIUS <= 0)
                {
                    ballXVelocity = -ballXVelocity;
                }
            }
            
            if (player2Lives > 0)
            {
                if (ballX + BALL_RADIUS >= SCREEN_WIDTH)
                {
                    player2Lives--;
                    ResetBall();
                }
            }
            else if (player2Lives == 0)
            {
                if (ballX +BALL_RADIUS >= SCREEN_WIDTH)
                {
                    ballXVelocity = -ballXVelocity;
                }
            }

            if (player3Lives > 0)
            {
                if (ballY + BALL_RADIUS <= 0)
                {
                    player3Lives--;
                    ResetBall();
                }
            }
            else if (player3Lives == 0)
            {
                if (ballY + BALL_RADIUS <= 0)
                {
                    ballYVelocity = -ballYVelocity;
                }
            }

            if (player4Lives > 0)
            {
                if (ballY + BALL_RADIUS >= SCREEN_HEIGHT)
                {
                    player4Lives--;
                    ResetBall();
                }
            }
            else if (player4Lives == 0)
            {
                if (ballY + BALL_RADIUS >= SCREEN_HEIGHT)
                {
                    ballYVelocity = -ballYVelocity;
                }
            }

            // Redraw form
            this.Invalidate();
            if (player1Lives > 0 && player2Lives == 0 && player3Lives == 0 && player4Lives == 0)
            {
                MegaPongP1Win winner1 = new MegaPongP1Win();
                winner1.Show();
                player1Lives = 0;
                this.Close();
            }
            if (player2Lives > 0 && player1Lives == 0 && player3Lives == 0 && player4Lives == 0)
            {
                MegaPongP2Win winner2 = new MegaPongP2Win();
                winner2.Show();
                player2Lives = 0;
                this.Close();
            }
            if (player3Lives > 0 && player1Lives == 0 && player2Lives == 0 && player4Lives == 0)
            {
                MegaPongP3Win winner3 = new MegaPongP3Win();
                winner3.Show();
                player3Lives = 0;
                this.Close();
            }
            if (player4Lives > 0 && player1Lives == 0 && player2Lives == 0 && player3Lives == 0)
            {
                MegaPongP4Win winner4 = new MegaPongP4Win();
                winner4.Show();
                player2Lives = 0;
                this.Close();
            }
        }

        private void ResetBall()
        {
            ballX = SCREEN_WIDTH / 2;
            ballY = SCREEN_HEIGHT / 2;

            Random directionGenerator = new Random();
            direction = directionGenerator.Next(4);
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
            else if (direction == 2)
            {
                ballYVelocity = -5;
                ballXVelocity = 0;
            }
            else if (direction == 3)
            {
                ballYVelocity = 5;
                ballXVelocity = 0;
            }

            player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
            player3PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2;
            player4PaddleX = SCREEN_WIDTH / 2 - PADDLE_HEIGHT / 2; 
        }
    }
}