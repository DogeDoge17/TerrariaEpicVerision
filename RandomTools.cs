using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerrariaEpicVerision
{
    public class RandomTools
    {
        public static SpriteEffects IntToFlip(int direction)
        {
            if(direction > 0)
                return SpriteEffects.FlipHorizontally;
            else
                return SpriteEffects.None;
        }
    }
}
