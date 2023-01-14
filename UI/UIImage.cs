using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.UI.Elements;
using ReLogic.Content;

namespace TerrariaEpicVerision.UI
{
    public class UIImage : UIImageFramed
    {
        public UIImage(Asset<Texture2D> texture, Rectangle frame) : base(texture, frame)
        {
        }
        

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            
        }
    }
}
