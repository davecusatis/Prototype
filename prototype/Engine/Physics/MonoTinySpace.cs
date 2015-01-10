using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using prototype.Engine.AI;

namespace prototype.Engine.MonoTinySpace
{
    class TCWorld
    {
        private const float TIMESTEP = 1 / 60f;
        private const float MINESCULE_TIME = 0.0000001f;

        public List<TCRectangle> RectangleList;
        
        public TCWorld()
        {
            RectangleList = new List<TCRectangle>();
        }

        public void MoveObjectAlongPath(Enemy p, ref Stack<Node> path)
        {
            // arbitrary - todo: fix
            if (path.Count > 3)
            {
                Node next;
                if (p.EnemyState == State.Moseying)
                {
                    next = path.Peek();
                }
                else
                {
                    next = path.Pop();
                }

                float X = next.Position.X - p.Position.X / Node.NODE_SIZE;
                float Y = (next.Position.Y - p.Position.Y / Node.NODE_SIZE);
                Vector2 vel = new Vector2(X, Y);

                if (!(p.Position.X <= (next.Position.X * Node.NODE_SIZE * 1.0001f) && p.Position.Y <= (next.Position.Y * Node.NODE_SIZE * 1.0001f)
                    && p.Position.X >= (next.Position.X * Node.NODE_SIZE * 0.9999f) && p.Position.Y >= (next.Position.Y * Node.NODE_SIZE * 0.9999f)))
                {
                    MoveObject(p, vel);
                }

                p.Position = next.Position * Node.NODE_SIZE;
                p.EnemyRect.Position = next.Position * Node.NODE_SIZE;

                if (vel.Y < 0)
                {
                    p.DirectionFacing = Direction.Up;
                }
                if (vel.X < 0)
                {
                    p.DirectionFacing = Direction.Left;
                }
                if (vel.Y > 0)
                {
                    p.DirectionFacing = Direction.Down;
                }
                if (vel.X > 0)
                {
                    p.DirectionFacing = Direction.Right;
                }

                //Console.Write("Moving enemy to: X = {0}, Y = {1}\n", p.Position.X, p.Position.Y);
                //p.Position = next.Position * 32;
                //p.EnemyRect.Position = next.Position * 32;
            }
            else
            {
                p.SearchState = EnemySearchState.Found;
            }
        }

        public void MoveObject(Enemy p, Vector2 velocity)
        {
            p.EnemyRect.Position = p.Position;
            p.EnemyRect.Velocity = velocity;
            double minT = TIMESTEP;
            float t = TIMESTEP;
            Vector2 mvA = new Vector2(0, 0);
            Vector2 mvB = new Vector2(0, 0);
            double ttc = 0.0;
            Vector2 minMV = new Vector2(0, 0);
            TCRectangle colRect = new TCRectangle();

            while (t > MINESCULE_TIME)
            {

                minMV = new Vector2(0, 0);
                minT = t;
                foreach (var rect in RectangleList)
                {
                    if (BoxToBoxCollide(p.EnemyRect, rect, t, ref mvA, ref mvB, ref ttc))
                    {
                        if (ttc < minT)
                        {
                            colRect = rect;
                            minT = ttc;
                            minMV = mvA;
                        }
                    }
                }

                minT -= MINESCULE_TIME;
                if (minT < 0) minT = 0;

                p.EnemyRect.Position += p.EnemyRect.Velocity * (float)minT;
                p.EnemyRect.Velocity += minMV;

                t -= (float)minT;
            }

            p.Position = p.EnemyRect.Position;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="velocity"></param>
        public void MoveObject(Player p, Vector2 velocity)
        {
            p.playerRect.Position = p.Position;
            p.playerRect.Velocity = velocity;
            double minT = TIMESTEP;
            float t = TIMESTEP;
            Vector2 mvA = new Vector2(0, 0);
            Vector2 mvB = new Vector2(0, 0);
            double ttc = 0.0;
            Vector2 minMV = new Vector2(0, 0);
            TCRectangle colRect = new TCRectangle();

            while (t > MINESCULE_TIME)
            {

                minMV = new Vector2(0, 0);
                minT = t;
                foreach (var rect in RectangleList)
                {
                    if (BoxToBoxCollide(p.playerRect, rect, t, ref mvA, ref mvB, ref ttc))
                    {
                        if (ttc < minT)
                        {
                            colRect = rect;
                            minT = ttc;
                            minMV = mvA;
                        }
                    }
                }

                minT -= MINESCULE_TIME;
                if (minT < 0) minT = 0;

                p.playerRect.Position += p.playerRect.Velocity * (float)minT;
                p.playerRect.Velocity += minMV;

                t -= (float)minT;
            }

            p.Position = p.playerRect.Position;
        }

        public void MoveObject(Particle p, Vector2 velocity)
        {
            p.ParticleRect.Position = p.position;
            p.ParticleRect.Velocity = velocity;
            double minT = TIMESTEP;
            float t = TIMESTEP;
            Vector2 mvA = new Vector2(0, 0);
            Vector2 mvB = new Vector2(0, 0);
            double ttc = 0.0;
            Vector2 minMV = new Vector2(0, 0);
            TCRectangle colRect = new TCRectangle();

            while (t > MINESCULE_TIME)
            {

                minMV = new Vector2(0, 0);
                minT = t;
                foreach (var rect in RectangleList)
                {
                    if (BoxToBoxCollide(p.ParticleRect, rect, t, ref mvA, ref mvB, ref ttc))
                    {
                        if (ttc < minT)
                        {
                            colRect = rect;
                            minT = ttc;
                            minMV = mvA;
                        }
                    }
                }

                minT -= MINESCULE_TIME;
                if (minT < 0) minT = 0;

                p.ParticleRect.Position += p.ParticleRect.Velocity * (float)minT;
                p.ParticleRect.Velocity += minMV;

                t -= (float)minT;
            }

            p.position = p.ParticleRect.Position;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rect"></param>
        public void AddRect(TCRectangle rect)
        {
            if (rect != null && rect != default(TCRectangle))
            {
                RectangleList.Add(rect);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="time"></param>
        /// <param name="mvA"></param>
        /// <param name="mvB"></param>
        /// <param name="ttc"></param>
        /// <returns></returns>
        public bool BoxToBoxCollide(TCRectangle a, TCRectangle b, double time, ref Vector2 mvA, ref Vector2 mvB, ref double ttc)
        {
            float leftTTC = 0;
            float rightTTC = 0;
            float upTTC = 0;
            float downTTC = 0;
            double minT;
            int shortestTime;
            bool collideY;
            bool collideX;
            double massRatio;
            Vector2 positionA, positionB;

            if (b.Velocity.X != a.Velocity.X)
            {
                collideX = true;
                rightTTC = -(b.Position.X - a.Position.X - a.Width) / (b.Velocity.X - a.Velocity.X);
                leftTTC = -(a.Position.X - b.Position.X - b.Width) / (a.Velocity.X - b.Velocity.X);
            }
            else
            {
                collideX = false;
            }

            if (b.Velocity.Y != a.Velocity.Y)
            {
                collideY = true;
                downTTC = -(b.Position.Y - a.Position.Y - a.Height) / (b.Velocity.Y - a.Velocity.Y);
                upTTC = -(a.Position.Y - b.Position.Y - b.Height) / (a.Velocity.Y - b.Velocity.Y);
            }
            else
            {
                collideY = false;
            }

            shortestTime = -1;
            minT = time;

            if (collideX)
            {
                if ((rightTTC >= 0) && (rightTTC < minT) && (a.Velocity.X > 0))
                {
                    positionA.Y = a.Position.Y + a.Velocity.Y * rightTTC;
                    positionB.Y = b.Position.Y + b.Velocity.Y * rightTTC;

                    if (((positionA.Y + a.Height) > positionB.Y)
                        && ((positionB.Y + b.Height) > positionA.Y))
                    {
                        shortestTime = 0;
                        minT = rightTTC;
                    }
                }

                if ((leftTTC >= 0) && (leftTTC < minT) && (a.Velocity.X < 0))
                {
                    positionA.Y = a.Position.Y + a.Velocity.Y * leftTTC;
                    positionB.Y = b.Position.Y + b.Velocity.Y * leftTTC;

                    if (((positionA.Y + a.Height) > positionB.Y)
                       && ((positionB.Y + b.Height) > positionA.Y))
                    {
                        shortestTime = 1;
                        minT = leftTTC;
                    }
                }
            }

            if (collideY)
            {
                if ((upTTC >= 0) && (upTTC < minT) && (a.Velocity.Y < 0))
                {
                    positionA.X = a.Position.X + a.Velocity.X * upTTC;
                    positionB.X = b.Position.X + b.Velocity.X * upTTC;

                    if (((positionA.X + a.Width) > positionB.X)
                        && ((positionB.X + b.Width) > positionA.X))
                    {
                        shortestTime = 2;
                        minT = upTTC;
                    }
                }


                if ((downTTC >= 0) && (downTTC < minT) && (a.Velocity.Y > 0))
                {
                    positionA.X = a.Position.X + a.Velocity.X * downTTC;
                    positionB.X = b.Position.X + b.Velocity.X * downTTC;

                    if (((positionA.X + a.Width) > positionB.X)
                        && ((positionB.X + b.Width) > positionA.X))
                    {
                        shortestTime = 3;
                        minT = downTTC;
                    }
                }
            }

            if (minT == time)
            {
                mvA = Vector2.Zero;
                mvB = Vector2.Zero;
                ttc = -1;
                return false;
            }

            switch (shortestTime)
            {
                case 0:
                    mvA = new Vector2(-a.Velocity.X, 0);
                    break;
                case 1:
                    mvA = new Vector2(-a.Velocity.X, 0);
                    break;
                case 2:
                    mvA = new Vector2(0, -a.Velocity.Y);
                    break;
                case 3:
                    mvA = new Vector2(0, -a.Velocity.Y);
                    break;
            }

            ttc = minT;
            return true;
        }



    }

    /// <summary>
    /// 
    /// </summary>
    public class TCRectangle
    {
        public Vector2 Position;
        public Vector2 Velocity;
        public int Width;
        public int Height;
        public double Mass;

        public TCRectangle()
        {
            Position = new Vector2(0, 0);
            Velocity = new Vector2(0, 0);
            Width = 0;
            Height = 0;
            Mass = 0.0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="mass"></param>
        public TCRectangle(Vector2 position, Vector2 velocity, int width, int height, double mass)
        {
            Position = position;
            Velocity = velocity;
            Width = width;
            Height = height;
            Mass = mass;
        }
    }
}
