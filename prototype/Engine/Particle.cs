using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using prototype.Engine.MonoTinySpace;

namespace prototype.Engine
{
    public class Particle
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public float angle { get; set; }
        public float angularVelocity {get; set;}
        public Color color { get; set; }
        public float size { get; set; }
        public int timeToLive { get; set; }
        public TCRectangle ParticleRect;

        public Particle(Texture2D tex, Vector2 pos, Vector2 vel, float ang, float angVel, Color c, float sz, int ttl)
        {
            texture = tex;
            position = pos;
            velocity = vel;
            angle = ang;
            angularVelocity = angVel;
            color = c;
            size = sz;
            timeToLive = ttl;
            ParticleRect = new TCRectangle(pos, vel, tex.Width, tex.Height, 1);
        }

        public void Update()
        {
            timeToLive--;
            position += velocity;
            
            angle += angularVelocity;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
            Vector2 origin = new Vector2(texture.Width / 2, texture.Height / 2);

            spriteBatch.Draw(texture, position, sourceRect, color, angle, origin, size, SpriteEffects.None, 0);
        }
    }
}
