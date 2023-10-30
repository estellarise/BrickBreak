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
        Vector2 Velocity = new Vector2(0,0); // <X,Y> direction vector
        float speed = 0;
        public Ball(Texture2D texture, Vector2 position) : base(texture, position)
        {
        }
    }
}
