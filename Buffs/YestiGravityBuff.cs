using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace TerrariaEpicVerision.Buffs
{
    public class YestiGravityBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.pvpBuff[Type] = true;
            Main.buffNoSave[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {            
            if (((float)((player.position.Y / 16f - (60f + 10f * (Math.Pow(Main.maxTilesX / 4200f, 2)))) / (Main.worldSurface / 6.0))) < 1f)
            {
                player.gravity = Player.defaultGravity;
                if (player.wet)
                {
                    if (player.honeyWet)
                        player.gravity = 0.1f;
                    else if (player.merman)
                        player.gravity = 0.3f;
                    else
                        player.gravity = 0.2f;
                }
            }
        }
    }
}

