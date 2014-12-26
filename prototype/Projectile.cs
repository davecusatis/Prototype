using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace prototype
{
    class Projectile
    {
        public Texture2D projectileTexture;
        public Vector2 position;
        public bool active = false;
        public int damage = 10;
        public Direction direction;

        public Projectile(Direction dir, Vector2 pos)
        {
            Initialize(dir, pos);
        }
        public Projectile(Texture2D texture)
        {
            projectileTexture = texture;
        }
        public void Initialize(Direction dir, Vector2 pos)
        {
            direction = dir;
            position = pos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!projectileTexture.Equals(null))
            {
                spriteBatch.Draw(projectileTexture, new Microsoft.Xna.Framework.Rectangle((int)position.X, (int)position.Y, projectileTexture.Width, projectileTexture.Height), Color.White);

            }
            else
            {
                spriteBatch.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleStrip, 0, 0, 4, 0, 1);
            }
        }


    }
}
