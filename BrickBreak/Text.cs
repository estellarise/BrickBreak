using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace BrickBreak
{
    internal class Text
    {
        public String text;
        public Vector2 position;
        public SpriteFont font;

        public Text(String text, SpriteFont font)
        {
            this.text = text;
            this.font = font;
            //this.position = position;
            this.position = font.MeasureString(text) / 2;
        }
    }
}
