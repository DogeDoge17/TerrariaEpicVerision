using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace TerrariaEpicVerision.Gores
{
    public class RoboGore : ModGore
    {
        public override void OnSpawn(Gore gore, IEntitySource source)
        {
            gore.sticky = false;
            gore.timeLeft = 1;
            gore.scale = 1;

            //if(gore.behindTiles)
            //Dust.NewDust(gore.position, (int)gore.scale, (int)gore.scale, 10, 0f, 0f, 20, Color.Red, gore.scale);

            base.OnSpawn(gore, source);
        }
    }
}
