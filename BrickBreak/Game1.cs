using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.Threading.Tasks.Sources;
using static System.Net.Mime.MediaTypeNames;

namespace BrickBreak
{
    public class Game1 : Game
    {
        private Ball ball;
        private Paddle paddle;
        private Level board;
        private int score;
        private int lives;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Rectangle wall;
        private String gameState;
        private int brickCount;
        Random rnd;
        SpriteFont font;
        Text winText;
        Text loseText;
        Text gameTitle;
        Text resetText;
        Text beginText;
        
        Vector2 windowCenter;
        //Texture2D paddleTexture;
        //Texture2D ballTexture;
        //Texture2D brickTexture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            LoadContent();
            gameState = "ongoing";
            score = 0;
            lives = 3;

            // reset # of bricks remaining since win condition is tracked this way
            // will be counted upward as bricks are added
            // eventually will adapt to different level shapes,
            // so multiplying columns by rows for brickCount won't be sufficient
            brickCount = 0;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D paddleTexture = Content.Load<Texture2D>("paddleBlue");
            Texture2D ballTexture = Content.Load<Texture2D>("ballBlue");
            Texture2D brickTexture = Content.Load<Texture2D>("element_blue_rectangle");

            //load text
            font = Content.Load<SpriteFont>("BoxedFont");
            winText = new Text("You Win! :) \n Play again? Press space!", font);
            loseText = new Text("You lose... \n Press space to play again.", font);
            gameTitle = new Text("Breakout!", font);
            //beginText = new Text("Press space to begin!", font);
            //resetText = new Text("Press space to play again.", font,);

            //load Game Objects
            wall = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            paddle = new Paddle
                (
                        paddleTexture,
                        new Rectangle((_graphics.PreferredBackBufferWidth - paddleTexture.Width) / 2,
                            _graphics.PreferredBackBufferHeight - paddleTexture.Height, 
                            paddleTexture.Width, 
                            paddleTexture.Height
                        )
                 );

            ball = new Ball
                (
                    ballTexture, 
                    new Rectangle(
                        (_graphics.PreferredBackBufferWidth - ballTexture.Width) / 2, 
                        _graphics.PreferredBackBufferHeight - paddleTexture.Height - ballTexture.Height,
                        ballTexture.Width,
                        ballTexture.Height 
                    )
                );
            ball.setSpeed(400f);
            paddle.setSpeed(1000f);

            //Populate level with bricks
            board = new Level(10,4); // columns x rows, fix later
            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    board.setBrick(
                            i,
                            j,
                            // brick texture is 64 x 32
                            new Brick
                            (
                                brickTexture,
                                new Rectangle(
                                    75 + i * 68,
                                    50 + j * 36,
                                    brickTexture.Width,
                                    brickTexture.Height
                                )
                            )
                    );
                    brickCount++;
                }
            }           

            //load game tools and traits
            windowCenter= new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            rnd = new Random();
            ;
        }

        protected override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || kstate.IsKeyDown(Keys.Escape))
                Exit();
            if (brickCount == 0)
            {
                gameState = "won";
                ball.Direction = new Vector2(0, 0);
                ball.Bounds.X = paddle.Bounds.Center.X - ball.Bounds.Width / 2;
                ball.Bounds.Y = _graphics.PreferredBackBufferHeight - paddle.Texture.Height - ball.Texture.Height;
            }
            else if (lives <= 0) { gameState = "lost"; }

            // Move paddle
            // Right paddle controller: Left, Right
            if (kstate.IsKeyDown(Keys.Left))
            {
                paddle.Bounds.X -= (int)(paddle.getSpeed() * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            
            if (kstate.IsKeyDown(Keys.Right))
            {
                paddle.Bounds.X += (int)(paddle.getSpeed() * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            // bound paddle
            paddle.Bounds.X = paddle.BoundPaddleLR(0, _graphics.PreferredBackBufferWidth);

            // Start stopped ball
            if (ball.getDirection() == Vector2.Zero) // if ball is stopped
            {
                // Center ball on paddle while stopped so it moves with the paddle
                ball.Bounds.X = paddle.Bounds.Center.X - ball.Bounds.Width / 2;
                ball.Bounds.Y = _graphics.PreferredBackBufferHeight - paddle.Texture.Height - ball.Texture.Height;

                if(kstate.IsKeyDown(Keys.Space)) // press space to start it
                {
                    // choose one of two random directions: 45, 135 degrees 
                    if (gameState == "ongoing")
                    {
                        int xRnd = rnd.Next(0, 2); // 0 or 1
                        ball.setDirection(new Vector2(-1 + xRnd * 2, -1)); // <-1 or 1, -1>, ball always starts up
                    }
                    else //reset game to beginning
                    {
                        Initialize();
                    }
                }
            }
            else //if ball is not stopped
            {
                // Update ball position
                ball.Bounds.Offset(
                        (int)(ball.getSpeed()* ball.Direction.X * (float) gameTime.ElapsedGameTime.TotalSeconds),
                        (int)(ball.getSpeed()* ball.Direction.Y * (float) gameTime.ElapsedGameTime.TotalSeconds)
                );
            }
            
            // Collision Detection against Wall (add into an object or function later?)
            if (ball.Bounds.X < wall.Left)
            {
                ball.Direction.X = 1; 
            }
            if (ball.Bounds.X > wall.Right)
            {
                ball.Direction.X = -1;
            }
            if (ball.Bounds.Y < wall.Top) // remember top of screen is 0, bottom of screen is +, so Y must increase to bounce down
            {
                ball.Direction.Y = 1;
            }
            if (ball.Bounds.Y > wall.Bottom)
            {
                // reset ball to starting pt
                ball.Direction = new Vector2(0, 0);

                // lose a life
                lives -= 1;
            }

            ball.collidesWith(paddle); //changes directions upon hitting paddle

            // Check collision with bricks
            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    Brick currBrick = board.getBrick(i, j);
                    if (currBrick.Exists)
                    {
                        if (ball.collidesWith(currBrick))
                        {
                            currBrick.Exists = false;
                            score += 10;
                            brickCount--;
                            Debug.WriteLine(brickCount.ToString());
                        }
                    }
                }
            }


            // check game state
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(new Color(34,34,34));
            _spriteBatch.Begin();

            // Draw Game Information
            _spriteBatch.DrawString(font, "Score: " + score.ToString(), new Vector2(10, 0), Color.Goldenrod,
                0, new Vector2 (0,0), 2.0f, SpriteEffects.None,0.5f);
            _spriteBatch.DrawString(font,"Lives: " + lives.ToString(), new Vector2(630, 0), Color.Goldenrod,
                0, new Vector2 (0,0), 2.0f, SpriteEffects.None,0.5f);
            _spriteBatch.DrawString(font, gameTitle.text, new Vector2(windowCenter.X, 25), Color.LightGoldenrodYellow, 0, gameTitle.position, 2.0f, SpriteEffects.None, 0.5f);

            // Draw win and lose announcements
            if (gameState == "won")
            {
                _spriteBatch.DrawString(font, winText.text, windowCenter, Color.White, 0, winText.position, 2.5f, SpriteEffects.None, 0.5f);
            }
            else if (gameState == "lost")
            {
                _spriteBatch.DrawString(font, loseText.text, windowCenter, Color.White, 0, loseText.position, 2.5f, SpriteEffects.None, 0.5f);
            }

            _spriteBatch.Draw(ball.Texture, ball.Bounds, Color.White);
            _spriteBatch.Draw(paddle.Texture, paddle.Bounds, Color.White);

            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    Brick currBrick = board.getBrick(i, j);
                    if (currBrick.Exists)
                    {
                        _spriteBatch.Draw(currBrick.Texture, currBrick.Bounds, Color.White);
                    }
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}