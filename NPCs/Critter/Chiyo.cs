using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Gores;


namespace TerrariaEpicVerision.NPCs.Critter
{
    public class Chiyo : HighResNPC
    {

        //public static Texture2D realTexture;
        //public static Asset<Texture2D> realAsset;
        //static Texture2D smallTexture;

        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Critter/RealChiyo");


        public override void SetStaticDefaults()
        {
            DisplayName.Format("Chiyo-chan"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

            //FileStream filestream = File.OpenRead("TerrariaEpicVerision/NPCs/Critter/RealChiyo.png");
            //realTexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, filestream);

            //realTexture = OpenStream

            // UIImage image = new UIImage(ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Critter/RealChiyo"), new Rectangle(0, 0, 306, 816));

            //largeImage = ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Critter/RealChiyo");

            //realAsset = ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Critter/RealChiyo");
            //realTexture = (Texture2D)realAsset;
            //smallTexture = (Texture2D)ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/Critter/Chiyo");

            // HighResImage.SetNew(new ImageUI(realAsset, new Rectangle(0, 0, 306, 816)));

            //realTexture = Texture2D.FromStream(Main.graphics.GraphicsDevice, Mod.Conte("TerrariaEpicVerision/NPCs/Critter/RealChiyo.png"));
        }

        public override void SetDefaults()
        {
            NPC.damage = 0;
            NPC.width = 30;
            NPC.height = 81;
            NPC.value = 0;
            NPC.lifeMax = 10;
            NPC.scale = 1f;
            //NPC.targetRect = new Rectangle(0, 0, 30, 81);
            NPC.HitSound = new SoundStyle("TerrariaEpicVerision/Sounds/ChiyoDie");
            NPC.DeathSound = new SoundStyle("TerrariaEpicVerision/Sounds/ChiyoDie");
            NPC.aiStyle = 7;
            NPC.knockBackResist = 0.9f;
            NPC.friendly = true;
            NPC.townNPC = false;
            NPC.defense = 0;
            NPC.frame = new Rectangle(0, 0, 30, 80);
            source = new Rectangle(0, 0, 306, 816);

            base.SetDefaults();
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return true;
        }

        public override void OnKill()
        {
            for (int d = 0; d < 25; d++)
            {
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 1.5f);
            }

            base.OnKill();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("THE CREATURE"),
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.Center, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.HitEffect(hit);
        }


        public override void AI()
        {
            if (Main.LocalPlayer.position.X > NPC.position.X)
            {
                NPC.spriteDirection = 0;
            }
            else
            {
                NPC.spriteDirection = 1;
            }

            base.AI();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 1));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldDayGrassCritter.Chance * .3f;
        }
    }
}