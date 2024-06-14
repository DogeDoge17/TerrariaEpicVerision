using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.Utils.Cil;
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
using TerrariaEpicVerision.Dusts;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using TerrariaEpicVerision.NPCs.Enemy.Persona;

//no I didn't resuse code... thanks for asking
namespace TerrariaEpicVerision.NPCs.Enemy
{
    public class Akihiko : HighResNPC
    {

        private int personaSummonChance = 75;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Akihiko High Res");


        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Akihiko Persona 3"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Main.npcFrameCount[Type] = 2;
            //largeImage = ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Akihiko High Res");
        }

        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 43;
            NPC.height = 97;
            NPC.value = 500;
            NPC.lifeMax = 200;
            NPC.scale = 1;
            NPC.HitSound = RandomHitNoise();
            NPC.DeathSound = RandomDeathNoise();
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.5f;
            NPC.defense = 15;

            NPC.frame = new Rectangle(0, 0, 43, 97);
            source = new Rectangle(0, 0, 465, 1048);


            //Banner = Item.NPCtoBanner(ModContent.NPCType<Ryuji>());
            //BannerItem = ModContent.ItemType<RyujiBanner>();
        }

        private SoundStyle RandomHitNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Hit1",
                "TerrariaEpicVerision/Sounds/Akihiko/Hit2",
                "TerrariaEpicVerision/Sounds/Akihiko/Hit3",
                "TerrariaEpicVerision/Sounds/Akihiko/Hit4"
            };

            return new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            checkLife = true;
            hitTarg = target;
            base.OnHitPlayer(target, hurtInfo);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("The Two-Fisted Protein Junkie.")
            });
        }

        private SoundStyle RandomDeathNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Death1",
                "TerrariaEpicVerision/Sounds/Akihiko/Death2",
                "TerrariaEpicVerision/Sounds/Akihiko/Death3"
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
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }


        //public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        //{
            
        //    base.OnHitByItem(player, item, damage, knockback, crit);
        //}

        //public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        //{
        //    for (int d = 0; d < 10; d++)
        //    {
        //        //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
        //        Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
        //    }


        //    base.OnHitByProjectile(projectile, damage, knockback, crit);
        //}

        private void PlayHitSound(Player target)
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Attack1",
                "TerrariaEpicVerision/Sounds/Akihiko/Attack2",
                "TerrariaEpicVerision/Sounds/Akihiko/Attack3",
                "TerrariaEpicVerision/Sounds/Akihiko/Attack4",
                "TerrariaEpicVerision/Sounds/Akihiko/Attack5",
                "TerrariaEpicVerision/Sounds/Akihiko/Attack7"
            };

            List<string> pathsKill = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Kill1",
                "TerrariaEpicVerision/Sounds/Akihiko/Kill2"
            };

            if (target.statLife <= 0)
                SoundEngine.PlaySound(new SoundStyle(pathsKill[Main.rand.Next(0, pathsKill.Count)]), NPC.position);
            else if (!onlyKill)
                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

        }

        bool PercentBool(int chance) { if (MathF.Floor(Main.rand.Next(0, 100)) < chance) { return true; } else { return false; } }


        private bool playedSpawnNoise = false;
        private float noiseTimer = Main.rand.Next(1, 10);


        private float revertSpriteTimer = 1;
        private float personaSummonTimer = 5;

        public bool checkLife = false;
        public bool onlyKill = false;
        public Player hitTarg;


        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;
            personaSummonTimer -= 1 * Time.deltaTime;
            revertSpriteTimer -= 1 * Time.deltaTime;


            if (!playedSpawnNoise)
            {
                List<string> paths = new List<string>()
                {
                    "TerrariaEpicVerision/Sounds/Akihiko/Spawn1"
                };

                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

                playedSpawnNoise = true;
            }

            if (checkLife)
            {
                PlayHitSound(hitTarg);
                checkLife = false;
            }

            if (revertSpriteTimer <= 0)
            {
                NPC.frame = new Rectangle(0, 0, 43, 97);

                source = new Rectangle(0, 0, 465, 1048);
            }


            if (personaSummonTimer <= 0)
            {
                personaSummonTimer = 5;

                if (PercentBool(personaSummonChance))
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona1",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona2",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona3",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona4",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona5",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona6",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona7",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona8",
                        "TerrariaEpicVerision/Sounds/Akihiko/Persona9"
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

                    revertSpriteTimer = 1;

                    NPC.frame = new Rectangle(0, 97, 43, 97);
                    source = new Rectangle(0, 1048, 465, 1048);

                    Caesar.tempAki = this;
                    NPC.NewNPC(null, (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<Caesar>());
                }
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


            if (noiseTimer <= 0)
            {
                if (NPC.life < (float)NPC.lifeMax * .40f)
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan1",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan2",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan3",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan4",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan5",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan6",
                        "TerrariaEpicVerision/Sounds/Akihiko/Moan7"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }
                else
                {
                    List<string> paths = new List<string>()
                    {
                        "TerrariaEpicVerision/Sounds/Akihiko/Idle1",
                        "TerrariaEpicVerision/Sounds/Akihiko/Idle2",
                        "TerrariaEpicVerision/Sounds/Akihiko/Idle3"
                    };

                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                }

                noiseTimer = Main.rand.Next(2, 10);
            }

            base.AI();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            if (Main.hardMode)
                npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return spawnInfo.Player.ZoneForest ? SpawnCondition.OverworldNightMonster.Chance * 0.1f : 0f;
        }
    }

}
