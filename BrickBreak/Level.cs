using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace BrickBreak
{

    internal class Level
    {
        int length;
        int width;
        Brick[,] bricks;

        public Level(int length, int width)
        {
            this.length = length;
            this.width = width;
            bricks = new Brick[length,width];
        }
              
        public int getLength()
        {
            return length; 
        }

        public int getWidth()
        {
            return width;
        }
        public void setBrick(int i, int j, Brick brick)
        {
            bricks[i,j] = brick;
        }

        public Brick getBrick(int i, int j)
        {
            return bricks[i, j] ;
        }
    }
}
