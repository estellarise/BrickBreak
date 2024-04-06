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

        //public GameObject(Texture2D texture, int xPosition, int yPosition, int width, int height)
        public GameObject(Texture2D texture, Rectangle rectangle, float speed = 0)
        {
            this.Texture = texture;
            //this.Bounds = new Rectangle(xPosition, yPosition, width, height);
            this.Bounds = rectangle;
            this.Speed = speed;
        }

        // can make bool instead to vary collision effect
        // in fact, prob should
        public virtual void collidesWith(GameObject other)
        {
            // check vertical collision
            // this obj center b/w left and right of the other one
            if (this.Bounds.Center.X <= other.Bounds.Right && this.Bounds.Center.X >= other.Bounds.Left)
            { 
                // this obj collides other from bottom
                if (this.Bounds.Top <= other.Bounds.Bottom && this.Bounds.Bottom >= other.Bounds.Bottom)
                {
                    //this.Bounds.Y = other.Bounds.Bottom;
                    this.Direction.Y = 1;
                    Debug.WriteLine("Bounce Down");
                }
                // this obj collides other from top
                else if (this.Bounds.Bottom >= other.Bounds.Top && this.Bounds.Top <= other.Bounds.Bottom)
                {
                    //this.Bounds.Y = other.Bounds.Top - this.Bounds.Height;
                    this.Direction.Y = -1;
                    Debug.WriteLine("Bounce Up");
                }
            } 
            // check hz collision
            // this obj center b/w top and bottom of other one
            if (this.Bounds.Center.Y <= other.Bounds.Bottom && this.Bounds.Center.Y >= other.Bounds.Top)
            {
                if (this.Bounds.Left  <= other.Bounds.Right)
                {
                    //this.Bounds.X = other.Bounds.Right;
                    this.Direction.X = 1;
                }
                else if (this.Bounds.Right >= other.Bounds.Left)
                {
                    //this.Bounds.X = other.Bounds.Left - this.Bounds.Width;
                    this.Direction.X = -1;
                }
            }
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