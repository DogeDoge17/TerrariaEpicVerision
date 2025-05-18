using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Gores;


namespace TerrariaEpicVerision.NPCs.Enemy.NecoArc
{
    public class WetArc : ANeco
    {
        public override string BestiaryInfo => "A Neco-Arc that got wet in the rain :(.";

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.raining && spawnInfo.Player.ZoneOverworldHeight && !spawnInfo.Player.ZoneSnow)
            {
                return (float)SpawnCondition.OverworldDayRain.Chance * 0.4f;
            }

            return 0f;
        }
    }

}
