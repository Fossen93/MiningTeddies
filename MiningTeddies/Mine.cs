using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MiningTeddies
{
    class Mine
    {
        #region Fields

        // Graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle;

        // Whether or not the mine is active
        bool active = true;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a mine
        /// </summary>
        /// <param name="sprite">the sprite for the mine</param>
        /// <param name="x">the x location of the center of the mine</param>
        /// <param name="y">the y location of the center of the mine</param>
        public Mine(Texture2D sprite, int x, int y)
        {
            // Save the sprite away.
            this.sprite = sprite;

            // Calculate our drawing region.
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the location of the mine
        /// </summary>
        public Point Location
        {
            get { return drawRectangle.Center; }
        }

        /// <summary>
        /// Gets the collision rectangle for the mine
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets and sets whether or not the mine is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Sets the x location of the center of the mine
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
        /// Sets the y location of the center of the mine
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
        /// Draws the mine
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
    }
}
