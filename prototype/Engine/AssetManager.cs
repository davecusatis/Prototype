using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

using TiledSharp;

namespace prototype.Engine
{
    static class AssetManager
    {
        /// <summary>
        /// Utility function used to remove the transparent color from Atlases. By default color set to (255, 0, 255) - magenta
        /// </summary>
        /// <param name="tex"></param>
        /// <returns></returns>
        public static Texture2D removeTransparentBG(Texture2D tex)
        {

            Color[] texData = new Color[tex.Height * tex.Width];
            tex.GetData<Color>(texData);
            for (int j = 0; j < texData.Length; j++)
            {
                if (texData[j].R == 255 && texData[j].G == 0 && texData[j].B == 255)
                {
                    //texData[j].A = (byte)0;
                    texData[j] = Color.Transparent;
                }
            }
            tex.SetData<Color>(texData);

            return tex;
        }
    }

}
