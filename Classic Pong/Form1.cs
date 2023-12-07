using System;
using System.Drawing;
using System.Windows.Forms;

namespace PongGame
{
    public partial class Form1 : Form
    {
        public const int SCREEN_WIDTH = 1000;
        public const int SCREEN_HEIGHT = 600;
        public const int BALL_RADIUS = 10;
        public const int PADDLE_WIDTH = 20;
        public const int PADDLE_HEIGHT = 80;
        public const int PADDLE_MOVE_AMOUNT = 8;
        public int ballX = SCREEN_WIDTH / 2;
        public int ballY = SCREEN_HEIGHT / 2;
        public int ballXVelocity;
        public int ballYVelocity = 0;
        public int p1PaddleVelocity = 0;
        public int p2PaddleVelocity = 0;
        public int player1PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        public int player2PaddleY = SCREEN_HEIGHT / 2 - PADDLE_HEIGHT / 2;
        public int player1Score = 0;
        public int player2Score = 0;
        private int direction;

        public Form1()
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
            this.KeyPreview = true;
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

            // Draw scores
            e.Graphics.DrawString(player1Score.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 - 50, 10);
            e.Graphics.DrawString(player2Score.ToString(), Font, Brushes.White, SCREEN_WIDTH / 2 + 30, 10);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // use escape key to return to home
            if (e.KeyCode == Keys.Escape)
            {
                FormHome gohome = new FormHome();
                gohome.Show();
                this.Close();
            }

            // Move player paddles
            if (e.KeyCode == Keys.W && player1PaddleY > 0)
            {
                p1PaddleVelocity -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.S && player1PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                p1PaddleVelocity += PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.Up && player2PaddleY > 0)
            {
                p2PaddleVelocity -= PADDLE_MOVE_AMOUNT;
            }
            else if (e.KeyCode == Keys.Down && player2PaddleY < SCREEN_HEIGHT - PADDLE_HEIGHT)
            {
                p2PaddleVelocity += PADDLE_MOVE_AMOUNT;
            }
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // Update ball position
            ballX += ballXVelocity;
            ballY += ballYVelocity;

            // Update Paddle Position
            player1PaddleY += p1PaddleVelocity;
            player2PaddleY += p2PaddleVelocity;
            if (p1PaddleVelocity > 0)
            {
                p1PaddleVelocity--;
            }
            else if (p1PaddleVelocity < 0)
            {
                p1PaddleVelocity++;
            }

            if (p2PaddleVelocity > 0)
            {
                p2PaddleVelocity--;
            }
            else if (p2PaddleVelocity < 0)
            {
                p2PaddleVelocity++;
            }

            // Check if paddle hits top or bottom of screen
            if(player1PaddleY <= 0 || player1PaddleY + PADDLE_HEIGHT >= SCREEN_HEIGHT)
            {
                p1PaddleVelocity = -p1PaddleVelocity;
            }

            if(player2PaddleY <= 0 || player2PaddleY + PADDLE_HEIGHT >= SCREEN_HEIGHT)
            {
                p2PaddleVelocity = -p2PaddleVelocity;
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

            // Game over screen
            this.Invalidate();
            if (player1Score == 5)
            {
                Player1Win winner1 = new Player1Win();
                winner1.Show();
                player1Score++;
                this.Close();
            }
            if (player2Score == 5)
            {
                Player2Win winner2 = new Player2Win();
                winner2.Show();
                player2Score++;
                this.Close();
            }
        }

        // Reset game after a player scores
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
        }
    }
}