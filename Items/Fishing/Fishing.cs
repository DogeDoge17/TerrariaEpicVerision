using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace TerrariaEpicVerision.Items.Fishing
{
    public class Fishing : ModPlayer
    {
        public override void CatchFish(FishingAttempt attempt, ref int itemDrop, ref int npcSpawn, ref AdvancedPopupRequest sonar, ref Vector2 sonarPosition)
        {
            bool water = !attempt.inHoney && !attempt.inLava;

            
           
            if ((Player.ZoneCrimson || Player.ZoneCorrupt) && water)
            {
                if (attempt.uncommon && Main.rand.NextBool(7))
                {
                    itemDrop = ModContent.ItemType<GermanFish>();
                }
            }

            base.CatchFish(attempt, ref itemDrop, ref npcSpawn, ref sonar, ref sonarPosition);
        }
    }
}
