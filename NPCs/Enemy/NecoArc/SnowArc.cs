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
    public class SnowArc : ANeco
    {
        public override string BestiaryInfo => "A Neco-Arc that lives in the Snow.";

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.expertMode || Main.masterMode)
            {
                switch (Main.rand.Next(0, 3))
                {
                    case 1:
                        target.AddBuff(BuffID.Frozen, 100, true, false);
                        break;
                    case 2:
                        target.AddBuff(BuffID.Chilled, 100, true, false);
                        break;
                }
            }
            base.ModifyHitPlayer(target, ref modifiers);
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.ZoneSnow)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.6f;
            }


            return 0f;
        }
    }

}
