using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
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
using TerrariaEpicVerision.Items;

//no I didn't resuse code... thanks for asking
namespace TerrariaEpicVerision.NPCs.Enemy
{
    public class Ryuji : HighResNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ryuji Persona 5"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }


        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Ryuji High Res");


        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 45;
            NPC.height = 115;
            NPC.value = 500;
            NPC.lifeMax = 150;
            NPC.scale = 1;
            NPC.HitSound = RandomRyujiHit();
            NPC.DeathSound = RandomRyujiDeath();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.5f;
            NPC.defense = 14;

            source = new Rectangle(0, 0, 324, 866);

            //Banner = Item.NPCtoBanner(ModContent.NPCType<Ryuji>());
            //BannerItem = ModContent.ItemType<RyujiBanner>();
        }

        private SoundStyle RandomRyujiHit()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit1",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit2",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit3",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit4",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit5",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit6",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiHit7"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        private SoundStyle RandomRyujiDeath()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiDeath1",
                "TerrariaEpicVerision/Sounds/Ryuji/RyujiDeath2",
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void OnKill()
        {
            for (int d = 0; d < 50; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
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

                new FlavorTextBestiaryInfoElement("Silly goober that questions if something is real.")
            });
        }

        /*
        public override void ModifyHitPlayer(Player target, ref int damage, ref bool crit)
        {
            if (Main.expertMode || Main.masterMode)
            {
                int ballHurt = Main.rand.Next(0, 2);

                if (ballHurt == 1)
                {
                    target.AddBuff(BuffID. , 100, true, false);
                }
            }
            base.ModifyHitPlayer(target, ref damage, ref crit);
        }
        */

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.OnHitByItem(player, item, damage, knockback, crit);
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }


            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }

        private bool playedSpawnNoise = false;
        private float noiseTimer = Main.rand.Next(1, 10);
        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;

            if (!playedSpawnNoise)
            {

                List<string> paths = new List<string>()
                {
                    "TerrariaEpicVerision/Sounds/Ryuji/RyujiSpawn1",
                    "TerrariaEpicVerision/Sounds/Ryuji/RyujiSpawn2"
                };

                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                playedSpawnNoise = true;
            }


            if (Main.LocalPlayer.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 1;
            }
            else
            {
                NPC.spriteDirection = 0;
            }

            if (noiseTimer <= 0)
            {
                if (NPC.life < (float)NPC.lifeMax * .40f)
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiMoan1",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiMoan2",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiMoan3",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiMoan4",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiMoan5"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }
                else
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiIdle1",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiIdle2",
                        "TerrariaEpicVerision/Sounds/Ryuji/RyujiIdle3"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }

                noiseTimer = Main.rand.Next(2, 10);
            }

            base.AI();
        }


        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneForest ? SpawnCondition.OverworldNightMonster.Chance * .2f : 0f;
        }
    }

}
