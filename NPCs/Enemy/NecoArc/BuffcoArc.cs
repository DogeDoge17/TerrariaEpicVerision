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
    public class BuffcoArc : ANeco
    {
        public override string BestiaryInfo => "Super strong Neco-Arc.";

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.damage = 70;
            NPC.width = 45;
            NPC.height = 155;
            NPC.value = 1000;
            NPC.lifeMax = 1500;            
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.ZoneForest && !Main.raining)
                return SpawnCondition.OverworldNight.Chance * .09f;
            else
                return 0f;
        }
    }
}
