using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using prototype.Engine;
using prototype.Engine.MonoTinySpace;
using Microsoft.Xna.Framework.Content;
namespace prototype
{
    public enum State
    {
        Idle, Active
    }

    class Enemy
    {
        public Texture2D EnemyTexture;
        public Vector2 Position;
        public ParticleEngine ParticleEngine;
        public Direction DirectionFacing;
        public int Health;
        public TCRectangle EnemyRect;
        private State EnemyState;
        private float EnemyMoveSpeed;

        public int Width
        {
            get 
            { 
                return EnemyTexture.Width; 
            }
        }
        public int Height
        {
            get
            {
                return EnemyTexture.Height;
            }
        }

        public Enemy(Texture2D enemyTexture, Vector2 position, ContentManager Content)
        {
            EnemyTexture = AssetManager.removeTransparentBG(enemyTexture);
            Position = position;
            EnemyState = State.Idle;
            EnemyRect = new TCRectangle(position, Vector2.Zero, EnemyTexture.Width, EnemyTexture.Height, 1);

            List<Texture2D> particleTextures = new List<Texture2D>();
            particleTextures.Add(Content.Load<Texture2D>("red"));
            particleTextures.Add(Content.Load<Texture2D>("darkorange"));
            particleTextures.Add(Content.Load<Texture2D>("orange"));
            ParticleEngine = new ParticleEngine(particleTextures, new Vector2((float)((13.5f)) * 32, (float)((22.5f)) * 32));


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(EnemyTexture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public void Shoot()
        {
            ParticleEngine.Add(ParticleEngine.GenerateNewParticle(DirectionFacing, 0.5f, 70));
        }

        public void Update()
        {
            float maxDist = 10 * EnemyMoveSpeed;

            if(EnemyState == State.Idle)
            {
                
            }
        }
    }
}
