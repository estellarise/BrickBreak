using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreak
{

    internal class Level
    {
        int length;
        int width;
        Brick[,] bricks;

        /*Brick[,] InitializeArray<Brick>(int length,int width) where Brick : new()
        {
            Brick[,] array = new Brick[length,width];
            for (int i = 0; i < length; ++i)
            {
                for (int j = 0; j< width; ++j)
                {
                    //array[i,j] = new Brick(,new Vector2(), true);
                    continue; // FIX
                }
            }

            return array;
        }
        */

        public Level(int length, int width)
        {
            //bricks = new Brick[200].Select(h => new Brick()).ToArray();
            this.bricks = new Brick[length, width];
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
