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
    public class JungleArc : ANeco
    {
        public override string BestiaryInfo => "A Neco-Arc that lives in the jungle.";

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Vine, 5, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Stinger, 3, 1, 3));
            npcLoot.Add(ItemDropRule.Common(ItemID.Moonglow, 20));

            if(NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3)
                    npcLoot.Add(ItemDropRule.Common(ItemID.ChlorophyteOre, 20,1,5));

            base.ModifyNPCLoot(npcLoot);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => spawnInfo.Player.ZoneJungle ? SpawnCondition.UndergroundJungle.Chance * 0.5f : 0;
    }

}
