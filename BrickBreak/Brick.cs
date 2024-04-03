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

        public Brick(Texture2D texture, Rectangle rect, float speed = 0): base(texture, rect, speed)
        {

        }
        public bool Exists()
        {
            return exists;
        }

    }
}
