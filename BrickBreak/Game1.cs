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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            rnd = new Random();
            paddle = new Brick(Content.Load<Texture2D>("paddleBlu"), Vector2.Zero);                               
            ball = new Ball(Content.Load<Texture2D>("ballBlue"), Vector2.Zero);
            ball.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                               _graphics.PreferredBackBufferHeight - paddle.Texture.Height - ball.Texture.Height);
            ball.setSpeed(400f);
            paddle.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2,
                                          _graphics.PreferredBackBufferHeight - paddle.Texture.Height);
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
                            new Brick(
                                Content.Load<Texture2D>("element_blue_rectangle"),
                                new Vector2(10 + i * 64, j * 32)
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
            // TODO: Add your update logic here
            
            var kstate = Keyboard.GetState();

            if (ball.getVelocity() == Vector2.Zero) // if ball is stopped
            {
                if(kstate.IsKeyDown(Keys.Space)) // press space to start it
                {
                    // choose one of two random directions: 45, 135 degrees 
                    int xRnd = rnd.Next(0, 2); // 0 or 1
                    ball.setVelocity(new Vector2(-1+xRnd * 2, -1)); // <-1 or 1, -1>, ball always starts up
                }
            }
            // Right paddle controller: Left, Right
            if (kstate.IsKeyDown(Keys.Left))
            {
                paddle.Position.X -= paddle.getSpeed()*(float)gameTime.ElapsedGameTime.TotalSeconds;//XVelocity is linear, not vector2
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                paddle.Position.X += paddle.getSpeed() * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            ball.Position += ball.getSpeed()*(float)gameTime.ElapsedGameTime.TotalSeconds * ball.getVelocity();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.LightGoldenrodYellow);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ball.Texture, ball.Position, Color.White);
            _spriteBatch.Draw(paddle.Texture, paddle.Position, Color.White);

            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    Brick currBrick = board.getBrick(i, j);
                    if (currBrick.Exists())
                    {
                        _spriteBatch.Draw(currBrick.Texture, currBrick.Position, Color.White);
                    }
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}