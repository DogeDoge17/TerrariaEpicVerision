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
    public class SnowArc : ModNPC
    {

        public override void SetStaticDefaults()
        {
           // DisplayName.//SetDefault("Snow Neco Arc"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 30;
            NPC.height = 46;
            NPC.value = 100;
            NPC.lifeMax = 300;
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,

                new FlavorTextBestiaryInfoElement("A Neco-Arc that lives in the Snow.")
            });
        }

        public override void OnKill()
        {
            for (int d = 0; d < 25; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 1.5f);
            }

            base.OnKill();
        }

        public override void OnHitByItem(Player player, Item item, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }
            base.OnHitByItem(player, item, hit, damageDone);
        }

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

     
        public override void OnHitByProjectile(Projectile projectile, NPC.HitInfo hit, int damageDone)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.OnHitByProjectile(projectile, hit, damageDone);
        }

        private float snowTimer = (float)Main.rand.Next(1, 100) / 100;
        private float noiseTimer = Main.rand.Next(1, 10);

        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;

            snowTimer -= 1 * Time.deltaTime;

            if (Main.LocalPlayer.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 0;
            }
            else
            {
                NPC.spriteDirection = 1;
            }


            if (snowTimer <= 0)
            {
                snowTimer = (float)Main.rand.Next(1, 100) / 100;
                Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Snow, 1f, 1f, 1, Color.White, 1);
            }

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

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.ZoneSnow)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.7f;
            }


            return 0f;
        }
    }

}
