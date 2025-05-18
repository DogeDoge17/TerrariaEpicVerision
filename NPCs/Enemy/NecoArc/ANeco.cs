using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria;
using Terraria.ModLoader;
using TerrariaEpicVerision.Gores;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.GameContent.Bestiary;

namespace TerrariaEpicVerision.NPCs.Enemy.NecoArc
{
    public abstract class ANeco : ModNPC
    {
        public abstract string BestiaryInfo { get; }

        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 30;
            NPC.height = 46;
            NPC.value = 100;
            NPC.lifeMax = 60;
            NPC.scale = 1;
            NPC.HitSound = RandomNecoHit();
            NPC.DeathSound = RandomNecoDeath();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.9f;
        }
        private SoundStyle RandomNecoHit()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/NecoHit1",
                "TerrariaEpicVerision/Sounds/NecoHit2",
                "TerrariaEpicVerision/Sounds/NecoHit3"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }
        private SoundStyle RandomNecoDeath()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/NecoDeath1"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement(BestiaryInfo)
            });
        }

     

        public override void OnKill()
        {
            for (int d = 0; d < 25; d++)
            {             
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 1.5f);
            }

            base.OnKill();
        }
        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int d = 0; d < 10; d++)
            {
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }

        private float noiseTimer = Main.rand.Next(1, 10);
        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;

            NPC.spriteDirection = -NPC.direction;

            if (noiseTimer <= 0)
            {
                if (NPC.life < (float)NPC.lifeMax * .40f)
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/NecoMoan1",
                        "TerrariaEpicVerision/Sounds/NecoMoan2",
                        "TerrariaEpicVerision/Sounds/NecoMoan3"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }
                else
                {
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/NecoHit"), NPC.position);
                }

                noiseTimer = Main.rand.Next(2, 10);
            }

            base.AI();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.FlinxFur, 50));
        }
    }
}
