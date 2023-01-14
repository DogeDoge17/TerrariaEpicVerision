/*using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using Terraria.GameContent.Bestiary;


namespace TerrariaEpicVerision.Bosses
{
    public class Monstro : ModNPC
    {
        private float DeltaTime = 0.16f;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monstro The Binding of Isaac"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
        }

        public override void SetDefaults()
        {
            NPC.damage = 25;
            NPC.width = 166;
            NPC.height = 152;
            NPC.value = 500;
            NPC.lifeMax = 2500;
            NPC.scale = 1;
            NPC.HitSound = RandomHitNoise();
            NPC.DeathSound = RandomDeathNoise();
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.knockBackResist = 0;
            NPC.defense = 25;
            NPC.boss = true;
            NPC.lavaImmune = true;
            Music = MusicID.Boss2;

        }

        private SoundStyle RandomHitNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Hit1"
            };

            return new SoundStyle();//new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        private SoundStyle RandomDeathNoise()
        {
            List<string> paths = new List<string>()
            {
                "TerrariaEpicVerision/Sounds/Akihiko/Death1"
            };

            return new SoundStyle(); // new SoundStyle(paths[Main.rand.Next(0, paths.Count)]);
        }

        public override void OnKill()
        {
            for (int d = 0; d < 50; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, NPC.position, new Vector2(), ModContent.GoreType<NecoGore>(), 1.5f);
            }

            base.OnKill();
        }

        public override void OnHitByItem(Player player, Item item, int damage, float knockback, bool crit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.position, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.OnHitByItem(player, item, damage, knockback, crit);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,

                new FlavorTextBestiaryInfoElement("Monstro from the hit game The Binding of Isaac Rebirth and The Binding of Isaac.")
            });
        }

        public override void OnHitByProjectile(Projectile projectile, int damage, float knockback, bool crit)
        {
            for (int d = 0; d < 10; d++)
            {
                //Dust.NewDust(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 0.5f);
                Gore.NewGore(null, NPC.position, new Vector2(), ModContent.GoreType<NecoGore>(), 0.5f);
            }

            base.OnHitByProjectile(projectile, damage, knockback, crit);
        }

        private float spitOutTimer = 10f;

        public override void AI()
        {
            Player targetedPlayer = Main.player[NPC.target];

            NPC.TargetClosest(true);

            if (NPC.target < 0 || NPC.target == 255 || targetedPlayer.dead || !targetedPlayer.active)
            {
                NPC.noGravity = true;

                NPC.TargetClosest(false);
                NPC.velocity.Y = NPC.velocity.Y - 1f;
            }




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

        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;

            base.BossLoot(ref name, ref potionType);

        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {

            if (Main.masterMode || Main.expertMode)
            {
                npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MonstroTreasureBag>(), 1));
            }
            else
            {
                npcLoot.Add(ItemDropRule.Common(ItemID.Bone, 1));

            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return 0;
        }
    }

    public class MonstroTreasureBag : ModItem
    {
        public override int BossBagNPC => ModContent.NPCType<Monstro>();

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag (Monstro)"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("<right> to open ");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = 10000;
            Item.rare = ItemRarityID.Cyan;
            Item.consumable = true;
            Item.maxStack = 999;
            Item.expert = true;
        }

        public override void OpenBossBag(Player player)
        {
           // player.QuickSpawnItem(null, ItemID.Heart);

            base.OpenBossBag(player);
        }
    }

    public class MonstrosTooth : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Monstro's Tooth"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            Tooltip.SetDefault("Summons Monstro");
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.value = 10000;
            Item.rare = ItemRarityID.LightRed;
            Item.consumable = true;
            Item.maxStack = 20;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.useStyle = ItemUseStyleID.HoldUp;

            base.SetStaticDefaults();
        }

        public override bool CanUseItem(Player player)
        {
            return !NPC.AnyNPCs(ModContent.NPCType<Monstro>());

            //return base.CanUseItem(player);
        }
        public override bool? UseItem(Player player)
        {
            SoundEngine.PlaySound(SoundID.Roar, player.position);

            NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<Monstro>());

            return true;

            //return base.UseItem(player);
        }


    }

}*/