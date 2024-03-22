using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace BrickBreak
{
    internal class Brick:GameObject
    {
        bool exists = true;
        //float xVelocity = 0; // for paddle
        //float speed = 0; // for paddle

        public Brick(Texture2D texture, int xPosition, int yPosition, int width, int height): base(texture, xPosition, yPosition, width, height)
        {
        }
        public bool Exists()
        {
            return exists;
        }

        /*
        public float getSpeed()
        {
            return speed;
        }

        public void setSpeed(float brickSpeed)
        {
            speed = brickSpeed;
        }
        public float getXVelocity()
        {
            return xVelocity;
        }
        */

    }
}
