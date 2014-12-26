using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
namespace prototype.Engine.Collision
{
    class Body
    {
        public Vector2 Velocity;   // should be float?
        public Vector2 Position;
        public Rectangle BodyRectangle;
        public bool Colliding;

        public Body(Vector2 velocity, Vector2 position, Rectangle rect, bool isColliding = false)
        {
            Velocity = velocity;
            Position = position;
            BodyRectangle = rect;
            Colliding = isColliding;
        }
        
        private bool IsColliding(Body b1, Body b2)
        {
            Colliding = b1.BodyRectangle.Intersects(b2.BodyRectangle);
            return Colliding;
        }

        public Rectangle GetCollisionRect(Body b1, Body b2)
        {
            if (IsColliding(b1, b2))
            {
                return Rectangle.Intersect(b1.BodyRectangle, b2.BodyRectangle);
            }

            return default(Rectangle);
        }
    }
}
