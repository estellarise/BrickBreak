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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ball = new Ball(Content.Load<Texture2D>("ballBlue"), new Vector2(10,10));
            paddle = new Brick(Content.Load<Texture2D>("paddleBlu"),
                               new Vector2(_graphics.PreferredBackBufferHeight, 0)
                                );
            //board = new Level(5, 5, Content.Load<Texture2D>("element_blue_rectangle"));
            board = new Level(5,5);
            for (int i = 0; i < board.getLength(); ++i)
            {
                board.setBrick(
                        i,
                        //j, 
                        0,
                        new Brick(
                            Content.Load<Texture2D>("element_blue_rectangle"),
                            new Vector2(10+i*64,0*32) 
                            //new Vector2(0, 0)
                        )
                );
            }
            /*
            for (int i = 0; i < board.getLength(); ++i)
            {
                //for (int j = 0; j < board.getWidth(); ++j)
                //{
                    board.setBrick(
                        i,
                        //j, 
                        0,
                        new Brick(
                            Content.Load<Texture2D>("element_blue_rectangle"),
                            //new Vector2(10+i*64,j*32), 
                            new Vector2(0,0)
                        ) 
                    );
                //}
            }
            */
            

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
            //Console.WriteLine("Hello World");
            // Console.WriteLine(board.getBrick(1,2).Position.ToString());
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(ball.Texture, ball.Position, Color.White);
            _spriteBatch.Draw(paddle.Texture, paddle.Position, Color.White);


            for (int i = 0; i < board.getLength(); ++i)
            {
                for (int j = 0; j < board.getWidth(); ++j)
                {
                    _spriteBatch.Draw(board.getBrick(i,j).Texture, board.getBrick(i,j).Position, Color.White);
                }
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}