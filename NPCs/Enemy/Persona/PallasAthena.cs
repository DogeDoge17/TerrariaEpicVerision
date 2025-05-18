using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;
using Terraria.Social.WeGame;
using Steamworks;
using Terraria.DataStructures;
using System;

namespace TerrariaEpicVerision.NPCs.Enemy.Persona
{
    public class PallasAthena : HighResNPC
    {
        public NPC aigisNpc = null;

        //public static Aigis tempAigis;



        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Persona/PallasAthena High Res");

        private bool OrgiaMode => aigisNpc != null && (aigisNpc.ai[1] == 1);
        
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Athena"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            

            
            NPC.width = 152;
            NPC.height = 101;
            NPC.value = 0;
            NPC.lifeMax = 1;
            NPC.scale = 1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.aiStyle = 22;
            NPC.knockBackResist = 0;
            NPC.defense = 0;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            source = new Rectangle(0, 0, 750, 600);

            //try
            //{
            //    if (tempAigis != null)
            //    {
            //        aigis = tempAigis;
            //        tempAigis = null;
            //    }
            //}
            //catch
            //{
            //    aigis = null;
            //}

        }
        public override void OnKill()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Aigis/PersonaBreak1",
                "TerrariaEpicVerision/Sounds/Aigis/PersonaBreak2"
            };

            if (!killedSelf)
                if (aigisNpc == null)
                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);
                else
                    SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), aigisNpc.position);


            for (int d = 0; d < 5; d++)                         
               Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            

            base.OnKill();
        }

        bool killedSelf;
        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            killedSelf = true;

            NPC.life = 0;


            for (int d = 0; d < 5; d++)                        
                Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            

            base.ModifyHitPlayer(target, ref modifiers);
        }

  
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Aigis Persona 3's Persona.")
            });
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is NPC npc && npc.type == ModContent.NPCType<Aigis>())
            {
                aigisNpc = npc;                
            }

            base.OnSpawn(source);
            
        }

        public override void AI()
        {

            if (!OrgiaMode)
                NPC.damage = 50;
            else NPC.damage = 0x96;

            for (int i = 0; i < NPC.ai.Length; i++)
            {
                Console.Write(i + ": " + NPC.ai[i] + "\t");
            }
            Console.WriteLine();
            if (aigisNpc != null)
            {
                if(aigisNpc.life <= 0)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
                    }
                    
                    NPC.life = 0;         
                }
            }

            NPC.spriteDirection = NPC.direction;

            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo) => 0f;
    }

}
