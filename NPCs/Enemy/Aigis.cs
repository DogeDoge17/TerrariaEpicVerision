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
using TerrariaEpicVerision.Items.Aigis;
using TerrariaEpicVerision.NPCs.Enemy.Persona;

namespace TerrariaEpicVerision.NPCs.Enemy
{
    public class Aigis : HighResNPC
    {
        private int orgiaModeChance = 50;
        private float orgiaTimer;
        private float overheatingTimer;
        private float orgiaActivateTimer;
        private int originalDamage;
        private int orgiaDamage;
        private int overheatDamage;
        private int originalDefense;
        private int orgiaDefense;
        private int overheatDefense;

        private bool OrgiaMode { get { return NPC.ai[1] == 1; } set { NPC.ai[1] = value ? 1 : 0; } }
        private bool Overheating { get { return NPC.ai[1] == 2; } set { NPC.ai[1] = value ? 2 : 0 ; } }

        private int personaSummonChance = 100;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Aigis High Res");


        public override void SetStaticDefaults()
        {            
            Main.npcFrameCount[Type] = 2;
        }

        public override void SetDefaults()
        {
            NPC.damage = 30;
            NPC.width = 43;
            NPC.height = 97;
            NPC.value = 500;
            NPC.lifeMax = 250;
            NPC.scale = 0.9f;
            NPC.HitSound = RandomHitNoise();
            NPC.DeathSound = RandomDeathNoise();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.5f;
            NPC.defense = 17;
            NPC.Size = new Vector2(43, 97);

            NPC.frame = new Rectangle(0, 0, 43, 97);

            originalDamage = NPC.damage;
            originalDefense = NPC.defense;

            orgiaDamage = 120;//(int)MathF.Ceiling(NPC.damage * 1.5f);
            orgiaDefense = 0;

            overheatDamage = 2;
            overheatDefense = 2;

            source = new Rectangle(0, 0, 314, 804);

                       

            //Banner = Item.NPCtoBanner(ModContent.NPCType<Ryuji>());
            //BannerItem = ModContent.ItemType<RyujiBanner>();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Aigis (Aegis) from the hit games Persona 3, Persona 3 FES, Persona 3 Portable, Persona 3: Dancing in Moonlight, Megami Tensei Chaining Soul: Persona 3, Persona 4 Arena, Persona 4 Arena Ultimax, Aegis: The First Mission, Persona Q: Shadow of the Labyrinth, and Persona Q2: New Cinema Labyrinth."),
            });
        }



        private SoundStyle RandomHitNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/Hit1",
                "TerrariaEpicVerision/Sounds/Aigis/Hit2",
                "TerrariaEpicVerision/Sounds/Aigis/Hit3",
                "TerrariaEpicVerision/Sounds/Aigis/Hit4",
                "TerrariaEpicVerision/Sounds/Aigis/Hit5",
                "TerrariaEpicVerision/Sounds/Aigis/Hit6",
                "TerrariaEpicVerision/Sounds/Aigis/Hit7",
                "TerrariaEpicVerision/Sounds/Aigis/Hit8",
                "TerrariaEpicVerision/Sounds/Aigis/Hit9",
                "TerrariaEpicVerision/Sounds/Aigis/Hit10"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        private SoundStyle RandomDeathNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/Death1",
                "TerrariaEpicVerision/Sounds/Aigis/Death2",
                "TerrariaEpicVerision/Sounds/Aigis/Death3",
                "TerrariaEpicVerision/Sounds/Aigis/Death4"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            checkLife = true;
            hitTarg = target;
            base.OnHitPlayer(target, hurtInfo);
        }


        public override void OnKill()
        {
            for (int d = 0; d < 30; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<RoboGore>(), 1.5f);
            }

            base.OnKill();
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int d = 0; d < 3; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<RoboGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }
        
        private void PlayHitSound(Player target)
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/Attack1",
                "TerrariaEpicVerision/Sounds/Aigis/Attack2",
                "TerrariaEpicVerision/Sounds/Aigis/Attack3",
                "TerrariaEpicVerision/Sounds/Aigis/Attack4"
            };

            List<string> pathsKill = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/Kill1",
                "TerrariaEpicVerision/Sounds/Aigis/Kill2",
                "TerrariaEpicVerision/Sounds/Aigis/Kill3",
                "TerrariaEpicVerision/Sounds/Aigis/Kill4",
                "TerrariaEpicVerision/Sounds/Aigis/Kill5",
                "TerrariaEpicVerision/Sounds/Aigis/Kill6"
            };

            List<string> pathsLow = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/PlayerLow1"
            };

            if (target.statLife <= 0)
                SoundEngine.PlaySound(new SoundStyle(pathsKill[Main.rand.Next(0, pathsKill.Count)]), NPC.position);
            else if (target.statLife <= 50 && !onlyKill)
                SoundEngine.PlaySound(new SoundStyle(pathsLow[Main.rand.Next(0, pathsLow.Count)]), NPC.position);
            else if (!onlyKill)
                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
        }


        bool PercentBool(int chance) { if (MathF.Floor(Main.rand.Next(0, 100)) < chance) { return true; } else { return false; } }


        private bool playedSpawnNoise = false;
        private float noiseTimer = Main.rand.Next(1, 10);

        private float smokeTimer = (float)Main.rand.Next(1, 100) / 100;

        private bool saidOrgia;
        private bool saidOverheat;

        private float personaSummonTimer = 5;

        public bool checkLife = false;
        public bool onlyKill = false;
        public Player hitTarg;

        

        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;
            personaSummonTimer -= 1 * Time.deltaTime;

            NPC.ai[3] = Main.rand.NextFloat();

            for (int i = 0; i < NPC.ai.Length; i++)
            {
                Console.Write(i + ": " + NPC.ai[i] + "\t");
            }
            Console.WriteLine();

            if (checkLife)
            {
                PlayHitSound(hitTarg); 
                checkLife = false;
            }

            if (!playedSpawnNoise)
            {
                List<string> paths = new List<string>()
                {
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn1",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn2",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn3",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn4",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn5",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn6",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn7",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn8",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn9",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn10",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn11",
                    "TerrariaEpicVerision/Sounds/Aigis/Spawn12"
                };

                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                playedSpawnNoise = true;
            }


            //if (Main.LocalPlayer.position.X > NPC.position.X)
            //{
            //    NPC.spriteDirection = 1;
            //}
            //else
            //{
            //    NPC.spriteDirection = 0;
            //}
            NPC.spriteDirection = NPC.direction;

            //Console.WriteLine("Persona Summon Timer: " + MathF.Ceiling(personaSummonTimer));

            if (personaSummonTimer <= 0)
            {
                personaSummonTimer = 5;

                if (PercentBool(personaSummonChance))
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Aigis/Persona1",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona2",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona3",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona4",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona5",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona6",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona7",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona8",
                        "TerrariaEpicVerision/Sounds/Aigis/Persona9"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/PersonaSummon"), NPC.position);
                    for (int i = 0; i < 20; i++)
                    {
                        if (NPC.spriteDirection == 0)
                            Dust.NewDust(NPC.position, 20, 20, ModContent.DustType<SummonPersona>(), 5, 0.1f, -1, Color.White, .3f);
                        else
                            Dust.NewDust(NPC.position, 20, 20, ModContent.DustType<SummonPersona>(), -5, 0.1f, 1, Color.White, .3f);

                    }
                    if (!Main.dedServ)
                    {
                        //PallasAthena.orgiaStack = (byte)(OrgiaMode.ToInt() +1);                       
                        var entitySource = new EntitySource_Parent(Entity, "Spawning Aigis persona"); 
                        int slot = NPC.NewNPC(entitySource, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<PallasAthena>());                    
                        NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, slot);
                    }
                }
            }

            //Console.WriteLine("Orgia Mode: " + orgiaMode + " Overheating: " + overheating + " Orgia Activate Timer: " + MathF.Ceiling(orgiaActivateTimer) + " Orgia Timer: " + MathF.Ceiling(orgiaTimer) + " Said Orgia:" + saidOrgia + " Smoke Timer: " + smokeTimer);

            //Orgia Mode Stuff
            if (NPC.life < (float)NPC.lifeMax * .40f)
            {
                if (!OrgiaMode && !Overheating)
                {
                    orgiaActivateTimer -= 1 * Time.deltaTime;

                    if (orgiaActivateTimer <= 0)
                    {
                        if (PercentBool(orgiaModeChance))
                        {
                            OrgiaMode = true;

                            orgiaTimer = 10;

                            if (!saidOrgia)
                            {
                                List<string> paths = new List<string>() { "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn1", "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn2", "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn3", "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn4", "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn5", "TerrariaEpicVerision/Sounds/Aigis/OrgiaModeOn6" };

                                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);


                                saidOrgia = true;
                            }
                        }
                        else
                        {
                            //Console.WriteLine("Failed To activate");
                            orgiaActivateTimer = 5;
                        }
                    }
                }
                else if (OrgiaMode)
                {
                    orgiaTimer -= 1 * Time.deltaTime;

                    saidOrgia = false;

                    NPC.damage = orgiaDamage;
                    NPC.defense = orgiaDefense;

                    Lighting.AddLight(NPC.position, new Vector3(3, 1, 1));

                    NPC.frame = new Rectangle(0, 97, 43, 97);
                    source = new Rectangle(0, 804, 314, 826);


                    if (orgiaTimer <= 0)
                    {
                        OrgiaMode = false;
                        Overheating = true;
                        overheatingTimer = 5;
                    }
                }
                else if (Overheating)
                {
                    overheatingTimer -= 1 * Time.deltaTime;
                    smokeTimer -= 1 * Time.deltaTime;

                    saidOrgia = false;

                    if (!saidOverheat)
                    {
                        List<string> paths = new List<string>() { "TerrariaEpicVerision/Sounds/Aigis/Overheat1", "TerrariaEpicVerision/Sounds/Aigis/Overheat2", "TerrariaEpicVerision/Sounds/Aigis/Overheat3", "TerrariaEpicVerision/Sounds/Aigis/Overheat4", "TerrariaEpicVerision/Sounds/Aigis/Overheat5", "TerrariaEpicVerision/Sounds/Aigis/Overheat6", "TerrariaEpicVerision/Sounds/Aigis/Overheat7" };

                        SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                        saidOverheat = true;
                    }

                    NPC.frame = new Rectangle(0, 0, 43, 97);
                    source = new Rectangle(0, 0, 314, 804);


                    NPC.damage = overheatDamage;
                    NPC.defense = overheatDefense;

                    if (smokeTimer <= 0)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.Smoke, 0, 0, 0, Color.White, 1.5f);
                        smokeTimer = (float)Main.rand.Next(1, 100) / 100;
                    }

                    if (overheatingTimer <= 0)
                    {
                        Overheating = false;
                        saidOrgia = false;
                        saidOverheat = false;
                        orgiaActivateTimer = 5;
                    }
                }
                else
                {
                    NPC.frame = new Rectangle(0, 0, 43, 97);
                    source = new Rectangle(0, 0, 314, 804);

                    saidOverheat = false;
                    saidOrgia = false;

                    NPC.damage = originalDamage;
                    NPC.defense = originalDefense;

                }
            }


            if (noiseTimer <= 0)
            {
                if (NPC.life < (float)NPC.lifeMax * .40f)
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Aigis/Moan1",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan2",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan3",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan4",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan5",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan6",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan7",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan8",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan9",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan10",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan11",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan12",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan13",
                        "TerrariaEpicVerision/Sounds/Aigis/Moan14",
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }
                else
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Aigis/Idle1",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle2",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle3",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle4",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle5",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle6",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle7",
                        "TerrariaEpicVerision/Sounds/Aigis/Idle8"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }

                noiseTimer = Main.rand.Next(2, 10);
            }

            base.AI();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.IronBar, 1, 1, 5));
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<GunBarrel>(), 70, 1, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneForest ? SpawnCondition.OverworldNightMonster.Chance * 0.07f : 0f;
        }
    }
}
