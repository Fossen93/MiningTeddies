using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace MiningTeddies
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        //Sets the window size
        public const int WindowWidth = 800;
        public const int WindowHeight = 600;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Declaring fields for the mine sprite, and a list of mines
        private Texture2D mine;
        List<Mine> mines;

        //Declaring fields for the teddybear sprite, and a list of teddybears
        private Texture2D teddy;
        List<TeddyBear> teddys;

        //Declaring fields for the explosion sprite, and a list of explosions
        private Texture2D explosion;
        List<Explosion> explosions;

        //Create a random generator
        Random rand = new Random();

        //The spawn delay for the bears
        int spawnDelay;

        MouseState oldState = Mouse.GetState();




        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = WindowWidth;
            graphics.PreferredBackBufferHeight = WindowHeight;
            this.IsMouseVisible = true;

            
            
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load the mine sprite
            mine = this.Content.Load<Texture2D>(@"graphics\mine");
            mines = new List<Mine>();

            //Load the teddybear sprite
            teddy = this.Content.Load<Texture2D>(@"graphics\teddybear");
            teddys = new List<TeddyBear>();

            //Load the explosion sprite
            explosion = this.Content.Load<Texture2D>(@"graphics\explosion");
            explosions = new List<Explosion>();

            //sets the spawn delay between 1-3
            spawnDelay = (rand.Next(3) + 1);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //Removes all the inactive objects
            for (int i = teddys.Count - 1; i >= 0; i--)
            {
                if (!teddys[i].Active)
                    teddys.RemoveAt(i);
            }

            for (int i = mines.Count - 1; i >= 0; i--)
            {
                if (!mines[i].Active)
                    mines.RemoveAt(i);
            }

            for (int i = explosions.Count - 1; i >= 0; i--)
            {
                if (!explosions[i].Playing)
                    explosions.RemoveAt(i);
            }

            
            //Creats a mine when you press left mouse button
            MouseState newState = Mouse.GetState();
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                Mine m = new Mine(mine, newState.X, newState.Y);
                mines.Add(m);
            }

            oldState = newState;

            //Creats a teddy bear after the spawn delay time has finished
            if (gameTime.TotalGameTime.Seconds >= spawnDelay)
            {
                spawnDelay += ((rand.Next(3) + 1));

                double x = (rand.NextDouble() - 0.5);
                double y = (rand.NextDouble() - 0.5);
                Vector2 v = new Vector2((float)x, (float)y);
                TeddyBear t = new TeddyBear(teddy, v, rand.Next(WindowWidth), rand.Next(WindowHeight));
                teddys.Add(t);
            }

            //Updates the movement of the teddybear
            for (int i = 0; i < teddys.Count; i++)
            {
                teddys[i].Update(gameTime);
            }


            //Creats an explosion each time a teddybear hits a bomb
            if (teddys.Count > 0 && mines.Count > 0)
            {
                foreach (TeddyBear t in teddys)
                {
                    if (t.Active)
                    {
                        foreach (Mine m in mines)
                        {
                            if (m.Active)
                            {
                                Rectangle mineRect = m.CollisionRectangle;
                                if (t.CollisionRectangle.Intersects(mineRect))
                                {
                                    t.Active = false;
                                    m.Active = false;
                                    Explosion e = new Explosion(explosion, t.Location.X, t.Location.Y);
                                    explosions.Add(e);
                                }
                            }
                        }
                    }
                }
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            
            spriteBatch.Begin();

            //Paint all the object
            if (mines.Count > 0)
            {
                for(int i = 0; i < mines.Count; i++)
                {
                    mines[i].Draw(spriteBatch);
                }
            }

            if(teddys.Count > 0)
            {
                for (int i = 0; i < teddys.Count; i++)
                {
                    teddys[i].Draw(spriteBatch);
                }
            }

            if (explosions.Count > 0)
            {
                foreach (Explosion e in explosions)
                {
                    e.Draw(spriteBatch);
                    e.Update(gameTime);
                }
            }


            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
