using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using prototype.Engine;
using prototype.Engine.MonoTinySpace;
namespace prototype
{
    
    class Player
    {
        public Texture2D PlayerTexture;
        public Vector2 Position;
        public ParticleEngine particleEngine;
        public Direction directionFacing;
        public TCRectangle playerRect;
        public bool Active;
        public int Health;

        public int Width
        {
            get{ return PlayerTexture.Width; }
        }
        public int Height
        {
            get { return PlayerTexture.Height; }
        }

        public void Initialize(Texture2D texture, Texture2D bullet, Vector2 pos, ContentManager content)
        {
            List<Texture2D> bulletList = new List<Texture2D>();
            bulletList.Add(bullet);
            playerRect = new TCRectangle(pos, Vector2.Zero, texture.Width, texture.Height, 1);
            PlayerTexture = AssetManager.removeTransparentBG(texture);
            Position = pos;
            particleEngine = new ParticleEngine(bulletList, pos);
            Active = true;
            Health = 100;
        }
       
        public void Update()
        {
            particleEngine.emitterLocation = Position;
            particleEngine.UpdateBullets();
        }

        public void Draw(SpriteBatch spriteBatch) 
        {
            spriteBatch.Draw(PlayerTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void DrawBullets(SpriteBatch spriteBatch, Vector3 camera)
        {
            particleEngine.Draw(spriteBatch, camera);
        }

        public void Shoot()
        {
            
            particleEngine.Add(particleEngine.GenerateNewParticle(directionFacing, 0.5f, 70));
        }

    }
}
