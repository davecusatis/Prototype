using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace prototype.Engine
{
    class Atlas
    {
        public int FirstGid;
        public int Columns;
        public int Rows;
        public int ElementWidth;
        public int ElementHeight;
        public Texture2D Texture;

        public Atlas(int first, int elementWidth, int elementHeight, int rows, int columns, Texture2D texture)
        {
            FirstGid = first;
            ElementWidth = elementWidth;
            ElementHeight = elementHeight;
            Rows = rows;
            Columns = columns;
            Texture = AssetManager.removeTransparentBG(texture);
        }
    }
}
