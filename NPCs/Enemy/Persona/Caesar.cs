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
namespace TerrariaEpicVerision.NPCs.Enemy.Persona
{
    public class Caesar : HighResNPC
    {
        public static Akihiko tempAki;
        public Akihiko akihiko;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Enemy/Persona/Caesar High Res");


        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Caesar"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            NPC.damage = 50;
            NPC.width = 77;
            NPC.height = 174;
            NPC.value = 0;
            NPC.lifeMax = 1;
            NPC.scale = 1;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.aiStyle = 22;
            NPC.knockBackResist = 0;
            NPC.defense = 0;
            NPC.noGravity = true;
            NPC.noTileCollide = true;

            source = new Rectangle(0, 0, 750, 1260);

            if (tempAki != null)
            {
                akihiko = tempAki;
                tempAki = null;
            }
        }
        public override void OnKill()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/PersonaBreak1",
                "TerrariaEpicVerision/Sounds/Akihiko/PersonaBreak2"
            };

            if(!killedSelf)
                SoundEngine.PlaySound(new SoundStyle(paths[Main.rand.Next(0, paths.Count)]), NPC.position);

            for (int d = 0; d < 5; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            }

            base.OnKill();
        }



        bool killedSelf;
        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {

            killedSelf = true;

            NPC.life = 0;

            if (akihiko != null)
            {
                akihiko.checkLife = true;
                akihiko.onlyKill = true;
                akihiko.hitTarg = target;
            }

            for (int d = 0; d < 5; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
            }
            base.OnHitPlayer(target, hurtInfo);
        }
        
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Akihiko Persona 3's Persona.")
            });
        }

        public override void AI()
        {
            if (akihiko != null)
            {
                if (akihiko.NPC.life <= 0)
                {
                    for (int d = 0; d < 5; d++)
                    {
                        Gore.NewGore(null, new Vector2(NPC.position.X + (NPC.width / 2), NPC.position.Y + (NPC.height / 2)), new Vector2(), GoreID.Smoke1, 1.5f);
                    }
                    NPC.life = 0;
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


            base.AI();
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0f;
        }
    }

}
