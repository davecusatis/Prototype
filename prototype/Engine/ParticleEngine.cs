using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using prototype.Engine.MonoTinySpace;

namespace prototype.Engine
{
    public class ParticleEngine
    {
        private Random random;
        private List<Particle> particles;
        private List<Texture2D> textures;
        private int totalParticles = 5;
        private TCWorld World;
        public Vector2 emitterLocation { get; set; }

        public ParticleEngine(List<Texture2D> texs, Vector2 loc, TCWorld world)
        {
            emitterLocation = loc;
            this.textures = texs;
            this.particles = new List<Particle>();
            World = world;
            random = new Random();
        }

        public Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = emitterLocation;
           // Vector2 velocity = new Vector2(1f * (float)(random.NextDouble() * 2 - 1),
           //                              1f * (float)(random.NextDouble() * 2 - 1));
            Vector2 velocity = new Vector2(1f * (float)(random.NextDouble() * 2 - 1),
                                            -2f);
            float angle = 0;
            float angleVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = 10 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angleVelocity, color, size, ttl);
        }

        public Particle GenerateNewParticle(Direction dir, float size, int timeToLive)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = emitterLocation;
            Vector2 velocity = new Vector2(0,0);
            switch (dir)
            {
                case Direction.Up:
                    velocity = new Vector2(0, -5);
                    break;
                case Direction.Down:
                    velocity = new Vector2(0, 5);
                    break;
                case Direction.Left:
                    velocity = new Vector2(-5, 0);
                    break;
                case Direction.Right:
                    velocity = new Vector2(5, 0);
                    break;
            }


            return new Particle(texture, position, velocity, 0, 0, Color.White, size, timeToLive);

        }
        public void Update()
        {
            for (int i = 0; i < totalParticles; i++)
            {
                particles.Add(GenerateNewParticle());
            }

            for (int i = 0; i < particles.Count; i++ )
            {
                particles[i].Update();
                if (particles[i].timeToLive <= 0)
                {
                    particles.RemoveAt(i);
                    i--;

                }
            }
        }

        public void Add(Particle p)
        {
            particles.Add(p);
        }

        public void UpdateBullets()
        {
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update();
                if (particles[i].timeToLive <= 0)
                {
                    particles.RemoveAt(i);
                    i--;

                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
            spriteBatch.End();

        }

        public void Draw(SpriteBatch spriteBatch, Vector3 camera)
        {
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, null, null, null, null, Matrix.CreateTranslation(camera));
            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Draw(spriteBatch);
            }
            spriteBatch.End();

        }
    }
}
