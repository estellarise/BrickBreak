using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreak
{
    internal class Paddle:GameObject
    {
        public Paddle(Texture2D texture, Rectangle rect, float speed = 0): base(texture, rect, speed)
        {

        }

        public int BoundPaddleLR(int leftBound, int rightBound)
        {
            if (this.Bounds.Left <= leftBound)
            {
                return leftBound;
            }
            else if (this.Bounds.Right >= rightBound)
            {
                return rightBound - this.Bounds.Width;
            }
            else
            {
                return this.Bounds.Left;
            }

        }

    }
}
