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
        bool exists;
        float xVelocity = 0; // for paddle
        float speed = 0; // for paddle

        public Brick(Texture2D texture, Vector2 position, bool exists) : base(texture, position)
        {
            this.exists = exists;
        }

    }
}
