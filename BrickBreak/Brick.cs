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
        public bool Exists = true;

        public Brick(Texture2D texture, Rectangle rect, float speed = 0): base(texture, rect, speed)
        {

        }

        //overriding for one instance is okay
        //derive ErasableGameObject from GameObject if multiple erasable objects are needed
        //then override in ErasableGameObject 
        public override void collidesWith(GameObject other) { 
            base.collidesWith(other);
            this.Exists = false;
            // [add points]
        }

    }
}
