﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

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