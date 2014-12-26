using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace prototype.Engine.Collision
{
    class GenericCollision
    {
        public Direction CollisionDirection;
        public Rectangle CollisionRectangle;

        public GenericCollision(Direction collisionDir, Rectangle rect)
        {
            CollisionDirection = collisionDir;
            CollisionRectangle = rect;
        }
    }
}
