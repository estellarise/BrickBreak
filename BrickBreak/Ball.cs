using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreak
{
    internal class Ball : GameObject
    {
        public Ball(Texture2D texture, Rectangle rect, float speed = 0): base(texture, rect, speed)
        {
        }
    }
}
