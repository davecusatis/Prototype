using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace prototype.Engine
{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set;}
        public int CurrentFrame;
        public int TotalFrames;
        public int FrameCtr;
        public AnimatedSprite(Texture2D texture, int rows, int cols)
        {
            Texture = texture;
            Rows = rows;
            Columns = cols;
            CurrentFrame = rows * cols;
            TotalFrames = rows * cols;
            FrameCtr = 10;
        }

        public void Update()
        {
            CurrentFrame--;
            if(CurrentFrame == 0)
            {
                CurrentFrame = TotalFrames;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;

            int row = (int)((float)CurrentFrame / (float)Columns);
            int column = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location, int index)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = index;
            int column = CurrentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
