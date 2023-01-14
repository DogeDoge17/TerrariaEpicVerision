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

namespace TerrariaEpicVerision.NPCs.Enemy
{
    public class Osaka : HighResNPC
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Osaka"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Osaka High Res");


        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 36;
            NPC.height = 78;
            NPC.value = 100;
            NPC.lifeMax = 75;
            NPC.scale = .9f; 
            NPC.HitSound = new SoundStyle("TerrariaEpicVerision/Sounds/THESAKER");
            NPC.DeathSound = new SoundStyle("TerrariaEpicVerision/Sounds/THESAKER");
            NPC.aiStyle = 3;
            NPC.knockBackResist = 0.9f;

            source = new Rectangle(0, 0, 394, 796);
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

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Also goes by Ayumu Kasuga.\nThe Saker."),
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


        private float noiseTimer = Main.rand.Next(1, 10);
        public override void AI()
        {
            noiseTimer -= 1 * Time.deltaTime;


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
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/THESAKER"), NPC.position);
                }
                else
                {
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/THESAKER"), NPC.position);
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
            return SpawnCondition.OverworldNightMonster.Chance * .3f;

        }
    }

}
