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
        private int bricksLeft;
        Random rnd;
        SpriteFont font;
        Text winText;
        Text loseText;
        Text resetText;
        
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
            // TODO: Add your initialization logic here
            // Load Textures
            Texture2D paddleTexture = Content.Load<Texture2D>("paddleBlue");
            Texture2D ballTexture = Content.Load<Texture2D>("ballBlue");
            Texture2D brickTexture = Content.Load<Texture2D>("element_blue_rectangle");
            LoadContent();

            font = Content.Load<SpriteFont>("BoxedFont");
            winText = new Text("You Win! :)", font);
            loseText = new Text("You lose... \n Press space to play again.", font);
            //resetText = new Text("Press space to play again.", font,);

            gameState = "ongoing";
            windowCenter= new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2);
            rnd = new Random();

            wall = new Rectangle(0,0, _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            score = 0;
            lives = 3;
            paddle = new Paddle(
                        paddleTexture,
                        new Rectangle(_graphics.PreferredBackBufferWidth / 2,
                            _graphics.PreferredBackBufferHeight - paddleTexture.Height, 
                            paddleTexture.Width, 
                            paddleTexture.Height
                        )
                     );

            ball = new Ball(
                    ballTexture, 
                    new Rectangle(
                        _graphics.PreferredBackBufferWidth / 2, 
                        _graphics.PreferredBackBufferHeight - paddleTexture.Height - ballTexture.Height,
                        ballTexture.Width,
                        ballTexture.Height 
                    )
                );

            ball.setSpeed(400f);
            paddle.setSpeed(1200f);
            board = new Level(8,6);

            //Populate level with bricks
            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    board.setBrick(
                            i,
                            j,
                            new Brick
                            (
                                brickTexture,
                                new Rectangle(
                                    10 + i * 64,
                                    j * 32,
                                    brickTexture.Width,
                                    brickTexture.Height
                                )
                            )
                    );
                    bricksLeft++;
                }
            }           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ;
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            var kstate = Keyboard.GetState();
            if (lives <= 0) { 
                gameState = "lost"; 
            }
            else if (bricksLeft == 0) { gameState = "won"; }

            // Start stopped ball
            if (ball.getDirection() == Vector2.Zero) // if ball is stopped
            {
                if(kstate.IsKeyDown(Keys.Space)) // press space to start it
                {
                    // choose one of two random directions: 45, 135 degrees 
                    if (gameState == "ongoing")
                    {
                        int xRnd = rnd.Next(0, 2); // 0 or 1
                        ball.setDirection(new Vector2(-1 + xRnd * 2, -1)); // <-1 or 1, -1>, ball always starts up
                    }
                    else
                    {
                        Initialize();
                    }
                }
            }

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
            
            // Collision Detection
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
                ball.Bounds.X = _graphics.PreferredBackBufferWidth / 2; 
                ball.Bounds.Y = _graphics.PreferredBackBufferHeight - paddle.Texture.Height - ball.Texture.Height;

                // lose a life
                lives -= 1;
            }

            ball.collidesWith(paddle);

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
                            bricksLeft--;
                        }
                    }
                }
            }

            // Update ball position
            ball.Bounds.Offset(
                    (int)(ball.getSpeed()* ball.Direction.X * (float) gameTime.ElapsedGameTime.TotalSeconds),
                    (int)(ball.getSpeed()* ball.Direction.Y * (float) gameTime.ElapsedGameTime.TotalSeconds)
                );
            
            // check game state
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);
            _spriteBatch.Begin();

            // Draw Information
            // Places text in center of the screen
            _spriteBatch.DrawString(font, "Score:" + score.ToString(), new Vector2(600, 0), Color.White,
                0, new Vector2 (0,0), 2.0f, SpriteEffects.None,0.5f);
            _spriteBatch.DrawString(font,"Lives:" + lives.ToString(), new Vector2(600, 30), Color.White,
                0, new Vector2 (0,0), 2.0f, SpriteEffects.None,0.5f);

            if (gameState == "won")
            {
                _spriteBatch.DrawString(font, winText.text, windowCenter, Color.White, 0, winText.position, 5.0f, SpriteEffects.None, 0.5f);
            }
            else if (gameState == "lost")
            {
                _spriteBatch.DrawString(font, loseText.text, windowCenter, Color.White, 0, loseText.position, 2.5f, SpriteEffects.None, 0.5f);
                //_spriteBatch.DrawString(font, resetText.text, windowCenter, Color.White, 0, resetText.position, 5.0f, SpriteEffects.None, 0.5f);
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