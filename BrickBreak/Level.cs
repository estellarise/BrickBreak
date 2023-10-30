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
        Brick[] bricks;

        T[] InitializeArray<T>(int length) where T : new()
        {
            T[] array = new T[length];
            for (int i = 0; i < length; ++i)
            {
                array[i] = new T();
            }

            return array;
        }

        public Level(int length, int width)
        {
            this.length = length;
            this.width = width;
            //bricks = InitializeArray<GameObject>(200);
            bricks = new Brick[length];
        }

        /*
        public Brick[,] InitializeArray(int length,int width, Texture2D brickTexture)
        {
            Brick[,] array = new Brick[length,width];
            for (int i = 0; i < length; ++i)
            {
                for (int j = 0; j< width; ++j)
                {
                    array[i,j] = new Brick(brickTexture, new Vector2(0,0));
                }
            }

            return array;
        }
        

        public Level(int length, int width, Texture2D brickTexture)
        {
            //bricks = new Brick[200].Select(h => new Brick()).ToArray();
            //this.bricks = new Brick[length, width];
            this.bricks = InitializeArray(length, width, brickTexture);
        }
        */
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
            //bricks[i,j] = brick;
            bricks[i] = brick;  
        }

        public Brick getBrick(int i, int j)
        {
            //return bricks[i, j] ;
            return bricks[i];
        }
    }
}
