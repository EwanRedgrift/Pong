using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(10, 170, 25, 25);
        Rectangle player2 = new Rectangle(580, 170, 25, 25);

        Rectangle ball = new Rectangle(295, 195, 10, 10);
        Rectangle topSide = new Rectangle(296, 195, 8, 1);
        Rectangle bottomSide = new Rectangle(296, 205, 8, 1);
        Rectangle leftSide = new Rectangle(294, 196, 1, 8);
        Rectangle rightSide = new Rectangle(305, 196, 1, 8);

        Rectangle divider = new Rectangle(300, 0, 10, 600);

        int turn = 1;

        int player1Score = 0;
        int player2Score = 0;

        int playerYSpeed = 6;
        int playerXSpeed = 6;
        float ballXSpeed = 5;
        float ballYSpeed;

        bool wPressed = false;
        bool sPressed = false;
        bool aPressed = false;
        bool dPressed = false;

        bool upPressed = false;
        bool downPressed = false;
        bool leftPressed = false;
        bool rightPressed = false;

        int ticks;

        Random random = new Random();

        SolidBrush blueBrush = new SolidBrush(Color.DodgerBlue);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Pen whitePen = new Pen(Color.White, 3);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = true;
                    break;
                case Keys.S:
                    sPressed = true;
                    break;
                case Keys.A:
                    aPressed = true;
                    break;
                case Keys.D:
                    dPressed = true;
                    break;
                case Keys.Up:
                    upPressed = true;
                    break;
                case Keys.Down:
                    downPressed = true;
                    break;
                case Keys.Left:
                    leftPressed = true;
                    break;
                case Keys.Right:
                    rightPressed = true;
                    break;
            }

        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wPressed = false;
                    break;
                case Keys.S:
                    sPressed = false;
                    break;
                case Keys.A:
                    aPressed = false;
                    break;
                case Keys.D:
                    dPressed = false;
                    break;
                case Keys.Up:
                    upPressed = false;
                    break;
                case Keys.Down:
                    downPressed = false;
                    break;
                case Keys.Left:
                    leftPressed = false;
                    break;
                case Keys.Right:
                    rightPressed = false;
                    break;
            }

        }

        private void gameTimer_Tick(object sender, EventArgs e)
        {

            //move ball
            ball.X += (int)ballXSpeed;
            ball.Y += (int)ballYSpeed;

            //check if it hits the top or bottom
            if (ball.Y <= 0 || ball.Y >= this.Height - ball.Height)
            {
                ballYSpeed *= -1;
            }

            //move player 1
            if (wPressed == true && player1.Y > 0)
            {
                player1.Y -= playerYSpeed;
            }

            if (sPressed == true && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerYSpeed;
            }

            if (aPressed == true && player1.X > 0)
            {
                player1.X -= playerXSpeed;
            }

            if (dPressed == true && player1.X < this.Width - player1.Width && player1.X + player1.Width <= this.Width / 2)
            {
                player1.X = player1.X + playerXSpeed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= playerYSpeed;
            }

            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerYSpeed;
            }

            if (leftPressed == true && player2.X > 0 && player2.X >= this.Width / 2 + divider.Width / 2)
            {
                player2.X -= playerXSpeed;
            }

            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerXSpeed;
            }

            if (player1.IntersectsWith(bottomSide))
            {
                ballXSpeed *= -1F;

                ballYSpeed *= -1F;
                ballYSpeed += -7F;

                ball.Y = player1.Y - ball.Width;
            }

            //Player 1 left side
            if (player1.IntersectsWith(leftSide))
            {
                ballYSpeed *= -1F;

                ballXSpeed *= -1F;
                ballXSpeed += 7F;

                ball.X = player1.X + player1.Width;

            }

            //player 1 left side
            if (player1.IntersectsWith(rightSide))
            {
                ballYSpeed *= -1F;

                ballXSpeed *= -1F;
                ballXSpeed += -7F;

                ball.X = player1.X - ball.Width;
            }

            //Player 2 right side
            if (player2.IntersectsWith(rightSide))
            {
                ballYSpeed *= -1F;

                ballXSpeed *= -1F;
                ballXSpeed += -7F;

                ball.X = player2.X - ball.Width;
            }

            //check if ball goes off left side of screen
            if (ball.X <= 0)
            {
                if (turn % 2 != 0)
                {
                    winLabel.Text = "Player 2 wins";
                }
                else
                {
                    winLabel.Text = "Player 1 wins";
                }

                gameTimer.Stop();
            }

            //check if ball goes off right side of screen
            if (ball.X >= this.Width)
            {
                if (turn % 2 != 0)
                {
                    player1Score++;
                    p1ScoreLabel.Text = $"{player1Score}";
                }
                else
                {
                    player2Score++;
                    p2ScoreLabel.Text = $"{player2Score}";
                }

                ballXSpeed = -10;

                turn++;
            }

            //if (ticks % 5 == 0)
            //{
            //    ballYSpeed = ballYSpeed * 0.925F;
            //    ballXSpeed = ballXSpeed * 0.925F;

            //    p1ScoreLabel.Text = ballXSpeed + "";
            //    p2ScoreLabel.Text = ballYSpeed + "";

            //}



            topSide.X = ball.X + 1;
            topSide.Y = ball.Y;

            bottomSide.X = ball.X + 1;
            bottomSide.Y = ball.Y + ball.Height - 1;

            leftSide.X = ball.X;
            leftSide.Y = ball.Y + 1;

            rightSide.X = ball.X + ball.Width - 1;
            rightSide.Y = ball.Y + 1;

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ticks++;


            e.Graphics.FillRectangle(blueBrush, player1);
            e.Graphics.FillRectangle(blueBrush, player2);
            e.Graphics.FillRectangle(whiteBrush, ball);



            e.Graphics.FillRectangle(blueBrush, topSide);
            e.Graphics.FillRectangle(blueBrush, bottomSide);
            e.Graphics.FillRectangle(blueBrush, leftSide);
            e.Graphics.FillRectangle(blueBrush, rightSide);

            e.Graphics.FillRectangle(blueBrush, divider);

            if (turn % 2 == 0)
            {
                e.Graphics.DrawRectangle(whitePen, player2);
            }
            else
            {
                e.Graphics.DrawRectangle(whitePen, player1);
            }
        }

    }
}
