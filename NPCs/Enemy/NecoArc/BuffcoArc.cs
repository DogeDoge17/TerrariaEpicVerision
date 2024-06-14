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
    public class BuffcoArc : ModNPC
    {
        public override void SetStaticDefaults()
        {
         //   DisplayName.SetDefault("Buff Neco Arc"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            NPC.damage = 70;
            NPC.width = 45;
            NPC.height = 155;
            NPC.value = 1000;
            NPC.lifeMax = 1500;
            NPC.HitSound = RandomNecoHit();
            NPC.DeathSound = RandomNecoDeath();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.9f;
        }

        private SoundStyle RandomNecoHit()
        {
            List<string> paths = new List<string>()
            {
               // "TerrariaEpicVerision/Sounds/NecoHit",
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

        public override void OnKill()
        {
            for (int d = 0; d < 50; d++)
            {

                //static int NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 1.5f);
            }

            base.OnKill();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Super strong Neco-Arc.")
            });
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }
        

        private float noiseTimer = Main.rand.Next(1, 10);
        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;


            //if (Main.LocalPlayer.position.X > NPC.position.X)
            //{
            //    NPC.spriteDirection = 0;
            //}
            //else
            //{
            //    NPC.spriteDirection = 1;
            //}

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

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]));
                }
                else
                {
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/NecoHit"));
                }

                noiseTimer = Main.rand.Next(2, 10);
            }

            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.ZoneForest && !Main.raining)
                return SpawnCondition.OverworldNight.Chance * .09f;
            else
                return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            // This is where we add item drop rules, here is a simple example:
            npcLoot.Add(ItemDropRule.Common(ItemID.FlinxFur, 10));
        }
    }
}
