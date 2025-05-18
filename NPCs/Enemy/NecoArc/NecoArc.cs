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
    public class NecoArc : ANeco
    {
        public override string BestiaryInfo { get => "Funny silly cat thingy."; }


        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!Main.hardMode && spawnInfo.Player.ZoneForest && !Main.raining)
            {
                return SpawnCondition.OverworldNightMonster.Chance * .5f;
            }
            else if (spawnInfo.Player.ZoneForest && !Main.raining)
                return SpawnCondition.OverworldNightMonster.Chance * .3f;

            return 0f;
        }
    }

}
