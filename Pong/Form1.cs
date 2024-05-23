/*

  ____  ____  ____       __ __   ___     __  __  _    ___  __ __ 
 /    ||    ||    \     |  |  | /   \   /  ]|  |/ ]  /  _]|  |  |
|  o  | |  | |  D  )    |  |  ||     | /  / |  ' /  /  [_ |  |  |
|     | |  | |    /     |  _  ||  O  |/  /  |    \ |    _]|  ~  |
|  _  | |  | |    \     |  |  ||     /   \_ |     \|   [_ |___, |
|  |  | |  | |  .  \    |  |  ||     \     ||  .  ||     ||     |
|__|__||____||__|\_|    |__|__| \___/ \____||__|\_||_____||____/ 
                                                                 
Air Hockey
By Ewan Redgrift
5/23/24

*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
{
    public partial class Form1 : Form
    {
        Rectangle player1 = new Rectangle(140, 175, 30, 30);
        Rectangle player2 = new Rectangle(550, 175, 30, 30);
        Rectangle ball = new Rectangle(280, 180, 20, 20);

        Rectangle p1Bottom = new Rectangle(148, 202, 14, 7);
        Rectangle p1Right = new Rectangle(137, 180, 7, 20);
        Rectangle p1Left = new Rectangle(166, 180, 7, 20);
        Rectangle p1Top = new Rectangle(149, 173, 14, 7);

        Rectangle p2Bottom = new Rectangle(557, 202, 14, 7);
        Rectangle p2Right = new Rectangle(548, 180, 7, 20);
        Rectangle p2Left = new Rectangle(576, 180, 7, 20);
        Rectangle p2Top = new Rectangle(557, 173, 14, 7);


        Rectangle divider = new Rectangle(295, 0, 10, 600);

        int player1Score = 0;
        int player2Score = 0;

        int playerSpeed = 7;
        float ballHitSpeed = 5;
        float ballXSpeed = 3;
        float ballYSpeed = 3;

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
        SolidBrush redBrush = new SolidBrush(Color.Red);
        SolidBrush whiteBrush = new SolidBrush(Color.White);

        Pen whitePen = new Pen(Color.White, 3);
        Pen lightBlueBrush = new Pen(Color.LightBlue, 3);
        Pen lightRedBrush = new Pen(Color.Pink, 3);

        SoundPlayer boomSound = new SoundPlayer(Properties.Resources.Boom);
        SoundPlayer coinSound = new SoundPlayer(Properties.Resources.Coin);
        public Form1()
        {
            InitializeComponent();

            ResetBall(); //Randomized ball direction at start
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
                case Keys.Escape:
                    if (gameTimer.Enabled == false)
                    {
                        ResetGame();
                    }
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
                boomSound.Play();
            }

            //move player 1
            if (wPressed && player1.Y > 0)
            {
                player1.Y -= playerSpeed;
                p1Bottom.Y -= playerSpeed;
                p1Right.Y -= playerSpeed;
                p1Top.Y -= playerSpeed;
                p1Left.Y -= playerSpeed;
            }

            if (sPressed && player1.Y < this.Height - player1.Height)
            {
                player1.Y += playerSpeed;
                p1Bottom.Y += playerSpeed;
                p1Right.Y += playerSpeed;
                p1Top.Y += playerSpeed;
                p1Left.Y += playerSpeed;
            }

            if (aPressed && player1.X > 0)
            {
                player1.X -= playerSpeed;
                p1Bottom.X -= playerSpeed;
                p1Right.X -= playerSpeed;
                p1Top.X -= playerSpeed;
                p1Left.X -= playerSpeed;
            }

            if (dPressed && player1.X < this.Width - player1.Width && player1.X + player1.Width <= (this.Width / 2 - divider.Width))
            {
                player1.X += playerSpeed;
                p1Bottom.X += playerSpeed;
                p1Right.X += playerSpeed;
                p1Top.X += playerSpeed;
                p1Left.X += playerSpeed;
            }

            //move player 2
            if (upPressed == true && player2.Y > 0)
            {
                player2.Y -= playerSpeed;
                p2Bottom.Y -= playerSpeed;
                p2Right.Y -= playerSpeed;
                p2Top.Y -= playerSpeed;
                p2Left.Y -= playerSpeed;
            }

            if (downPressed == true && player2.Y < this.Height - player2.Height)
            {
                player2.Y += playerSpeed;
                p2Bottom.Y += playerSpeed;
                p2Right.Y += playerSpeed;
                p2Top.Y += playerSpeed;
                p2Left.Y += playerSpeed;
            }

            if (leftPressed == true && player2.X > 0 && player2.X >= this.Width/2 + divider.Width)
            {
                player2.X -= playerSpeed;
                p2Bottom.X -= playerSpeed;
                p2Right.X -= playerSpeed;
                p2Top.X -= playerSpeed;
                p2Left.X -= playerSpeed;
            }

            if (rightPressed == true && player2.X < this.Width - player2.Width)
            {
                player2.X += playerSpeed;
                p2Bottom.X += playerSpeed;
                p2Right.X += playerSpeed;
                p2Top.X += playerSpeed;
                p2Left.X += playerSpeed;
            }

            //check if ball goes off left side of screen
            if (ball.X <= 0)
            {
                player2Score++;

                coinSound.Play();
                ResetBall();
            }

            //check if ball goes off right side of screen
            if (ball.X >= this.Width)
            {
                player1Score++;

                coinSound.Play();
                ResetBall();
            }


            BallPlayerCollision();

            if (ticks % 5 == 0)
            {
                ballYSpeed = ballYSpeed * 0.95F;
                ballXSpeed = ballXSpeed * 0.95F;
            }

            if (player1Score == 3)
            {
                winLabel.Text = $"Player 1 wins\n\nClick ESC to play again";
                gameTimer.Stop();
            }
            if (player2Score == 3)
            {
                winLabel.Text = $"Player 2 wins\n\nClick ESC to play again";
                gameTimer.Stop();
            }



            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            ticks++;

            //Drawing
            e.Graphics.FillEllipse(blueBrush, player1);
            e.Graphics.DrawEllipse(lightBlueBrush, player1);

            e.Graphics.FillEllipse(redBrush, player2);
            e.Graphics.DrawEllipse(lightRedBrush, player2);

            e.Graphics.FillEllipse(whiteBrush, ball);

            e.Graphics.FillRectangle(blueBrush, divider);

            //Score
            p1ScoreLabel.Text = $"{player1Score}";
            p2ScoreLabel.Text = $"{player2Score}";
        }

        private void BallPlayerCollision()
        {
            //p1
            if (ball.IntersectsWith(p1Left))
            {
                ballXSpeed += ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p1Top))
            {
                ballYSpeed += -ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p1Bottom))
            {
                ballYSpeed += ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p1Right))
            {
                ballXSpeed += -ballHitSpeed;
                boomSound.Play();
            }

            //p2
            if (ball.IntersectsWith(p2Left))
            {
                ballXSpeed += ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p2Top))
            {
                ballYSpeed += -ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p2Bottom))
            {
                ballYSpeed += ballHitSpeed;
                boomSound.Play();
            }
            if (ball.IntersectsWith(p2Right))
            {
                ballXSpeed += -ballHitSpeed;
                boomSound.Play();
            }
        }

        private void ResetBall()
        {
            int randDirection = random.Next(-1, 2);
            while (randDirection == 0)
            {
                randDirection = random.Next(-1, 2);
            }

            ballXSpeed = 3 * randDirection;

            randDirection = random.Next(-1, 2);
            while (randDirection == 0)
            {
                randDirection = random.Next(-1, 2);
            }

            ballYSpeed = 3 * randDirection;

            ball.X = 360;
            ball.Y = 195;
        }

        private void ResetGame()
        {
            Rectangle player1 = new Rectangle(140, 175, 30, 30);
            Rectangle player2 = new Rectangle(550, 175, 30, 30);
            Rectangle ball = new Rectangle(280, 180, 20, 20);

            Rectangle p1Bottom = new Rectangle(148, 202, 14, 7);
            Rectangle p1Right = new Rectangle(137, 180, 7, 20);
            Rectangle p1Left = new Rectangle(166, 180, 7, 20);
            Rectangle p1Top = new Rectangle(149, 173, 14, 7);

            Rectangle p2Bottom = new Rectangle(557, 202, 14, 7);
            Rectangle p2Right = new Rectangle(548, 180, 7, 20);
            Rectangle p2Left = new Rectangle(576, 180, 7, 20);
            Rectangle p2Top = new Rectangle(557, 173, 14, 7);

            player1Score = 0;
            player2Score = 0;

            ticks = 0;

            winLabel.Text = null;

            gameTimer.Start();
        }

    }
}
