using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Dusts;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using TerrariaEpicVerision.NPCs.Enemy.Persona;
using TerrariaEpicVerision.Projectiles;

//no I didn't resuse code... thanks for asking
namespace TerrariaEpicVerision.NPCs.Enemy
{
    public class Goku : HighResNPC
    {

        private int ssjChance = 10;
        private bool ssj = false;
        private float ssjTimer;
        private float ssjActivateTimer;
        private int originalDamage;
        private int ssjDamage;
        private int originalDefense;
        private int ssjDefense;

        private int personaSummonChance = 100;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Goku High Res");

        


        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Son Goku"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

            Main.npcFrameCount[Type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.damage = 30;
            NPC.width = 47;
            NPC.height = 92;
            NPC.value = 1500;
            NPC.lifeMax = 250;
            NPC.scale = 0.9f;
            NPC.HitSound = RandomHitNoise();
            NPC.DeathSound = RandomDeathNoise();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.5f;
            NPC.defense = 17;
            NPC.Size = new Vector2(47, 93);

            NPC.frame = new Rectangle(0, 0, 47, 93);

            originalDamage = NPC.damage;
            originalDefense = NPC.defense;

            ssjDamage = 300;//(int)MathF.Ceiling(NPC.damage * 1.5f);
            ssjDefense = 40;

            source = new Rectangle(0, 0, 400, 791);


            //Banner = Item.NPCtoBanner(ModContent.NPCType<Ryuji>());
            //BannerItem = ModContent.ItemType<RyujiBanner>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Yeah, okay, but you can't beat Goku. smh"),
            });
        }



        private SoundStyle RandomHitNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Goku/Hit1",
                "TerrariaEpicVerision/Sounds/Goku/Hit2",
                "TerrariaEpicVerision/Sounds/Goku/Hit3",
                "TerrariaEpicVerision/Sounds/Goku/Hit4",
                "TerrariaEpicVerision/Sounds/Goku/Hit5"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        private SoundStyle RandomDeathNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Goku/Death1",
                "TerrariaEpicVerision/Sounds/Goku/Death2",
                "TerrariaEpicVerision/Sounds/Goku/Death3",
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Goku/Attack1",
                "TerrariaEpicVerision/Sounds/Goku/Attack2",
                "TerrariaEpicVerision/Sounds/Goku/Attack3",
                "TerrariaEpicVerision/Sounds/Goku/Attack4"
            };

            List<string> pathsKill = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Goku/Kill1",
                "TerrariaEpicVerision/Sounds/Goku/Kill2",
                "TerrariaEpicVerision/Sounds/Goku/Kill3",
                "TerrariaEpicVerision/Sounds/Goku/Kill4"
            };

            Console.WriteLine(target.statLife);

            if (target.statLife + 20 <= hurtInfo.Damage)
                SoundEngine.PlaySound(new SoundStyle(pathsKill[Main.rand.Next(0, pathsKill.Count)]), NPC.position);
            else
                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

            base.OnHitPlayer(target, hurtInfo);
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }


        bool PercentBool(int chance) { if (MathF.Floor(Main.rand.Next(0, 100)) < chance) { return true; } else { return false; } }


        private bool playedSpawnNoise = false;
        private float noiseTimer = Main.rand.Next(1, 10);


        private float ssjLoop = 0;


        private bool saidSSJ;

        private float kamehamehaTimeout;

        private int direction;

        int the = 60;


        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;

            if (!playedSpawnNoise)
            {
                List<string> paths = new List<string>()
                {
                    "TerrariaEpicVerision/Sounds/Goku/Spawn1",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn2",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn3",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn4",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn5",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn6",
                    "TerrariaEpicVerision/Sounds/Goku/Spawn7"
                };

                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                playedSpawnNoise = true;
            }


            if (Main.LocalPlayer.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 1;
                direction = 1;
            }
            else
            {
                NPC.spriteDirection = 0;
                direction = -1;
            }


            //Console.WriteLine("Orgia Mode: " + orgiaMode + " Overheating: " + overheating + " Orgia Activate Timer: " + MathF.Ceiling(orgiaActivateTimer) + " Orgia Timer: " + MathF.Ceiling(orgiaTimer) + " Said Orgia:" + saidOrgia + " Smoke Timer: " + smokeTimer);

            kamehamehaTimeout -= 1 * Time.deltaTime;

            if (kamehamehaTimeout <= 0)
            {
                //SoundEngine.PlaySound(SoundID.Coins, NPC.position);
                //Projectile.NewProjectile(null, NPC.Center, new Vector2(10 * direction, 0), ModContent.ProjectileType<Kamehameha>(), 50, 2, 255, direction);

                kamehamehaTimeout = 2;

                NPC.knockBackResist = 1f;
                NPC.aiStyle = -1;
            }
            else if (the >= 0)
            {
                the--;
                Console.WriteLine(the);
            }
            else if (the <= 0)
            {

                the = 300;

                NPC.knockBackResist = 0.5f;
                NPC.aiStyle = 3;
            }

            //Super Saiyan Mode Stuff
            if (NPC.life < (float)NPC.lifeMax * .40f)
            {
                if (!ssj)
                {
                    ssjActivateTimer -= 1 * Time.deltaTime;

                    if (ssjActivateTimer <= 0)
                    {
                        if (PercentBool(ssjChance))
                        {
                            ssj = true;

                            ssjTimer = 10;

                            if (!saidSSJ)
                            {
                                List<string> paths = new List<string>() { "TerrariaEpicVerision/Sounds/Goku/SSJEnter1", "TerrariaEpicVerision/Sounds/Goku/SSJEnter2" };

                                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                                SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/Goku/Aura Burst"), NPC.position);
                                SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/Goku/SSJ Noise"), NPC.position);
                                ssjLoop = 1.995f;

                                saidSSJ = true;
                            }
                        }
                        else
                        {
                            Console.WriteLine("Failed To activate");
                            ssjActivateTimer = 5;
                        }
                    }
                }
                else if (ssj)
                {
                    saidSSJ = false;

                    NPC.damage = ssjDamage;
                    NPC.defense = ssjDefense;

                    ssjLoop -= 1 * Time.deltaTime;

                    if (ssjLoop <= 0)
                    {
                        SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/Goku/SSJ Noise"), NPC.position);
                        ssjLoop = 1.995f;
                    }

                    Lighting.AddLight(NPC.position, new Vector3(1, 1, 3));

                    NPC.frame = new Rectangle(0, 97, 43, 97);
                    source = new Rectangle(0, 810, 400, 772);

                }
                else
                {
                    NPC.frame = new Rectangle(0, 0, 47, 93);
                    source = new Rectangle(0, 0, 400, 791);
                }

                if (noiseTimer <= 0)
                {
                    if (NPC.life < (float)NPC.lifeMax * .40f)
                    {
                        List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Goku/Moan1",
                        "TerrariaEpicVerision/Sounds/Goku/Moan2",
                        "TerrariaEpicVerision/Sounds/Goku/Moan3",
                        "TerrariaEpicVerision/Sounds/Goku/Moan4",
                        "TerrariaEpicVerision/Sounds/Goku/Moan5",
                        "TerrariaEpicVerision/Sounds/Goku/Moan6",
                        "TerrariaEpicVerision/Sounds/Goku/Moan7"
                    };

                        SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                    }
                    else
                    {
                        List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Goku/Idle1",
                        "TerrariaEpicVerision/Sounds/Goku/Idle2",
                        "TerrariaEpicVerision/Sounds/Goku/Idle3",
                        "TerrariaEpicVerision/Sounds/Goku/Idle4",
                        "TerrariaEpicVerision/Sounds/Goku/Idle5",
                        "TerrariaEpicVerision/Sounds/Goku/Idle6"
                    };

                        SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                    }

                    noiseTimer = Main.rand.Next(2, 10);
                }

                base.AI();
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 1, 1, 5));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode)
                return spawnInfo.Player.ZoneForest ? SpawnCondition.OverworldDaySlime.Chance * 0.1f : 0f;
            else
                return 0;
        }
    }
}
