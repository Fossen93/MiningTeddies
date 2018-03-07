using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiningTeddies
{
    /// <summary>
    /// A teddy bear
    /// </summary>
    class TeddyBear
    {
        #region Fields

        // graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle;

        // velocity information
        Vector2 velocity;

        // whether or not the teddy bear is active
        bool active = true;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a teddy bear
        /// </summary>
        /// <param name="sprite">the sprite for the teddy bear</param>
        /// <param name="x">the x location of the center of the teddy bear</param>
        /// <param name="y">the y location of the center of the teddy bear</param>
        public TeddyBear(Texture2D sprite, Vector2 velocity, int x, int y)
        {
            // Save the sprite away.
            this.sprite = sprite;

            // Save our velocity vector.
            this.velocity = velocity;

            // Calculate our drawing region.
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the teddy bear
        /// </summary>
        public Point Location
        {
            get { return drawRectangle.Center; }
        }

        /// <summary>
        /// Gets the collision rectangle for the teddy bear
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets and sets whether or not the teddy bear is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Sets the x location of the center of the teddy bear
        /// </summary>
        private int X
        {
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                // clamp to keep in range
                if (drawRectangle.Left < 0)
                {
                    drawRectangle.X = 0;
                }
                else if (drawRectangle.Right > Game1.WindowWidth)
                {
                    drawRectangle.X = Game1.WindowWidth - drawRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Sets the y location of the center of the teddy bear
        /// </summary>
        private int Y
        {
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                // clamp to keep in range
                if (drawRectangle.Top < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if (drawRectangle.Bottom > Game1.WindowHeight)
                {
                    drawRectangle.Y = Game1.WindowHeight - drawRectangle.Height;
                }
            }
        }

        #endregion

        #region Public methods
        /// <summary>
        /// Updates the teddy bear's location, bouncing if necessary
        /// </summary>
        /// <param name="gameTime">game time</param>
        public void Update(GameTime gameTime)
        {
            // move the teddy bear
            drawRectangle.X += (int)(gameTime.ElapsedGameTime.Milliseconds * velocity.X);
            drawRectangle.Y += (int)(gameTime.ElapsedGameTime.Milliseconds * velocity.Y);

            // bounce as necessary
            BounceTopBottom();
            BounceLeftRight();
        }

        /// <summary>
        /// Draws the teddy bear
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Bounces the teddy bear off the top and bottom window borders if necessary
        /// </summary>
        private void BounceTopBottom()
        {
            if (drawRectangle.Top < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if (drawRectangle.Bottom > Game1.WindowHeight)
            {
                // bounce off bottom
                drawRectangle.Y = Game1.WindowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }
        /// <summary>
        /// Bounces the teddy bear off the left and right window borders if necessary
        /// </summary>
        private void BounceLeftRight()
        {
            if (drawRectangle.Left < 0)
            {
                // bounce off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if (drawRectangle.Right > Game1.WindowWidth)
            {
                // bounce off right
                drawRectangle.X = Game1.WindowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
        }

        #endregion
    }
}
