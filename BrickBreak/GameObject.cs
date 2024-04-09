using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using System.ComponentModel.Design;

namespace BrickBreak
{
    internal class GameObject
    {
        public Texture2D Texture;
        public Rectangle Bounds;
        public float Speed;
        public Vector2 Direction = new Vector2(0, 0);

        public GameObject(Texture2D texture, Rectangle rectangle, float speed = 0)
        {
            this.Texture = texture;
            this.Bounds = rectangle;
            this.Speed = speed;
        }

        // can make bool instead to vary collision effect
        // in fact, prob should
        public virtual bool collidesWith(GameObject other)
        {
            // check vertical collision
            // this obj center b/w left and right of the other one
            if (this.Bounds.Center.X < other.Bounds.Right && this.Bounds.Center.X > other.Bounds.Left)
            { 
                // this obj collides other from bottom
                // other bottom b/w this top and bottom
                if (this.Bounds.Top < other.Bounds.Bottom && this.Bounds.Bottom > other.Bounds.Bottom)
                {
                    //this.Bounds.Y = other.Bounds.Bottom;
                    this.Direction.Y = 1;
                    //Debug.WriteLine("Bounce Down");
                    return true;
                }
                // this obj collides other from top
                //other top b/w this top and bottom
                if (this.Bounds.Top < other.Bounds.Top && this.Bounds.Bottom > other.Bounds.Top)
                {
                    //this.Bounds.Y = other.Bounds.Top - this.Bounds.Height;
                    this.Direction.Y = -1;
                    //Debug.WriteLine("Bounce Up");
                    return true;
                }
            } 
            // check hz collision
            // this obj center b/w top and bottom of other one
            if (this.Bounds.Center.Y < other.Bounds.Bottom && this.Bounds.Center.Y > other.Bounds.Top)
            {
                if (this.Bounds.Left  < other.Bounds.Right && this.Bounds.Right > other.Bounds.Right)
                {
                    //this.Bounds.X = other.Bounds.Right;
                    this.Direction.X = 1;
                    //Debug.WriteLine("Bounce Right");
                    return true;
                }
                else if (this.Bounds.Right > other.Bounds.Left && this.Bounds.Left < other.Bounds.Left)
                {
                    //this.Bounds.X = other.Bounds.Left - this.Bounds.Width;
                    this.Direction.X = -1;
                    //Debug.WriteLine("Bounce Left");
                    return true;
                }
            }
            return false;
        }

        public Rectangle getBounds() { return Bounds; } 
        public void setBounds(Rectangle bounds) { Bounds= bounds; }
        public Texture2D getTexture() { return Texture;}
        public void setTexture(Texture2D tex) { Texture = tex;} 
        public Vector2 getDirection()
        {
            return Direction;
        }

        public void setDirection(Vector2 direction)
        {
            Direction = direction;
        }
        public float getSpeed()
        {
            return Speed;
        }

        public void setSpeed(float speed)
        {
            Speed = speed;
        }

 
    }

    
}