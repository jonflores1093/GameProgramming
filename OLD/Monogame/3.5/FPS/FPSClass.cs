using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPS
{
    class FPSClass
    {

        SpriteBatch spriteBatch;

        float CumulativeFrameTime;
        int NumFrames;
        int FramesPerSecond;
        SpriteFont Font;

        public FPSClass(Game game)
        {
            spriteBatch = new SpriteBatch(game.GraphicsDevice);
            Font = game.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void Draw(GameTime gameTime)
        {

            CumulativeFrameTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds / 1000f;
            NumFrames++;
            if (CumulativeFrameTime >= 1f)
            {
                FramesPerSecond = NumFrames;
                NumFrames = 0;
                CumulativeFrameTime -= 1f;
            }
            spriteBatch.Begin();
            spriteBatch.DrawString(Font, "FPS Class: " + FramesPerSecond.ToString(), new Vector2(0, 0), Color.Black);
            spriteBatch.End();
            
        }
    }
}
