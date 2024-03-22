using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace BrickBreak
{
    public class Game1 : Game
    {
        private Ball ball;
        private Brick paddle;
        private Level board;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Random rnd;
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
            Texture2D paddleTexture = Content.Load<Texture2D>("paddleBlu");
            Texture2D ballTexture = Content.Load<Texture2D>("ballBlue");
            Texture2D brickTexture = Content.Load<Texture2D>("element_blue_rectangle");

            rnd = new Random();
            paddle = new Brick
                (paddleTexture, _graphics.PreferredBackBufferWidth / 2, 
                    _graphics.PreferredBackBufferHeight - paddleTexture.Height,
                    paddleTexture.Width,
                    paddleTexture.Height
                );

            ball = new Ball
                (ballTexture, _graphics.PreferredBackBufferWidth / 2, 
                    _graphics.PreferredBackBufferHeight - paddleTexture.Height - ballTexture.Height,
                    ballTexture.Width,
                    ballTexture.Height 
                );
            ball.setSpeed(400f);
            paddle.setSpeed(1000f);
            board = new Level(5,5);

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
                                10 + i * 64,
                                j * 32,
                                brickTexture.Width,
                                brickTexture.Height
                            )
                    );
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

            // Start stopped ball
            if (ball.getDirection() == Vector2.Zero) // if ball is stopped
            {
                if(kstate.IsKeyDown(Keys.Space)) // press space to start it
                {
                    // choose one of two random directions: 45, 135 degrees 
                    int xRnd = rnd.Next(0, 2); // 0 or 1
                    ball.setDirection(new Vector2(-1 + xRnd * 2, -1)); // <-1 or 1, -1>, ball always starts up
                }
            }

            // Move paddle
            // Right paddle controller: Left, Right
            if (kstate.IsKeyDown(Keys.Left))
            {
                paddle.setX(
                    (int)(paddle.getX() - paddle.getSpeed() * (float) gameTime.ElapsedGameTime.TotalSeconds)
                );
            }
            
            if (kstate.IsKeyDown(Keys.Right))
            {
                paddle.setX(
                    (int) (paddle.getX() + paddle.getSpeed() * (float) gameTime.ElapsedGameTime.TotalSeconds)
                );
            }
            
            // Update ball position
            ball.setX(
                    (int) (ball.getX() + ball.getSpeed() * ball.getDirection().X * (float) gameTime.ElapsedGameTime.TotalSeconds)
            );
            ball.setY(
                    (int) (ball.getY() + ball.getSpeed() * ball.getDirection().Y * (float) gameTime.ElapsedGameTime.TotalSeconds)
                );

            // Collision Detection
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ball.Texture, ball.Bounds, Color.White);
            _spriteBatch.Draw(paddle.Texture, paddle.Bounds, Color.White);

            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    Brick currBrick = board.getBrick(i, j);
                    if (currBrick.Exists())
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