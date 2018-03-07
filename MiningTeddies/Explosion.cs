using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MiningTeddies
{
    /// <summary>
    /// An animated explosion
    /// </summary>
    public class Explosion
    {
        #region Fields

        // object location
        Rectangle drawRectangle;

        // animation strip info
        Texture2D strip;
        int frameWidth;
        int frameHeight;

        // hard-coded animation info. There are better ways to do this,
        // we just don't know enough to use them yet
        const int FramesPerRow = 3;
        const int NumRows = 3;
        const int NumFrames = 9;

        // fields used to track and draw animations
        Rectangle sourceRectangle;
        int currentFrame;
        const int TotalFrameMilliseconds = 10;
        int elapsedFrameMilliseconds = 0;

        // playing or not
        bool playing = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a new explosion object
        /// </summary>
        /// <param name="strip">the strip for the explosion</param>
        /// <param name="x">the x location of the center of the explosion</param>
        /// <param name="y">the y location of the center of the explosion</param>
        public Explosion(Texture2D strip, int x, int y)
        {
            this.strip = strip;

            // initialize animation to start at frame 0
            currentFrame = 0;

            // calculate frame size
            frameWidth = strip.Width / FramesPerRow;
            frameHeight = strip.Height / NumRows;

            // set initial draw and source rectangles
            drawRectangle = new Rectangle(0, 0, frameWidth, frameHeight);
            sourceRectangle = new Rectangle(0, 0, frameWidth, frameHeight);

            // Immediately start playing.
            Play(x, y);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets whether or not the explosion animation is currently playing
        /// </summary>
        public bool Playing
        {
            get { return playing; }
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Updates the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="gameTime">the game time</param>
        public void Update(GameTime gameTime)
        {
            if (playing)
            {
                // check for advancing animation frame
                elapsedFrameMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedFrameMilliseconds > TotalFrameMilliseconds)
                {
                    // reset frame timer
                    elapsedFrameMilliseconds = 0;

                    // advance the animation
                    if (currentFrame < NumFrames - 1)
                    {
                        currentFrame++;
                        SetSourceRectangleLocation(currentFrame);
                    }
                    else
                    {
                        // reached the end of the animation
                        playing = false;
                    }
                }
            }
        }

        /// <summary>
        /// Draws the explosion. This only has an effect if the explosion animation is playing
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (playing)
            {
                spriteBatch.Draw(strip, drawRectangle, sourceRectangle, Color.White);
            }
        }

        /// <summary>
        /// Starts playing the animation for the explosion
        /// </summary>
        /// <param name="x">the x location of the center of the explosion</param>
        /// <param name="y">the y location of the center of the explosion</param>
        public void Play(int x, int y)
        {
            // reset tracking values
            playing = true;
            elapsedFrameMilliseconds = 0;
            currentFrame = 0;

            // set draw location and source rectangle
            drawRectangle.X = x - drawRectangle.Width / 2;
            drawRectangle.Y = y - drawRectangle.Height / 2;
            SetSourceRectangleLocation(currentFrame);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Sets the source rectangle location to correspond with the given frame
        /// </summary>
        /// <param name="frameNumber">the frame number</param>
        private void SetSourceRectangleLocation(int frameNumber)
        {
            // calculate X and Y based on frame number
            sourceRectangle.X = (frameNumber % FramesPerRow) * frameWidth;
            sourceRectangle.Y = (frameNumber / FramesPerRow) * frameHeight;
        }

        #endregion

    }
}
