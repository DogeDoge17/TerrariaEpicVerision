using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaEpicVerision.Projectiles;
using TerrariaEpicVerision.Tiles;

namespace TerrariaEpicVerision.Items
{
    public class EpicHouse : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("InstaHouse");
            // Tooltip.SetDefault("Places an NPC house instantly");
            Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 32;
            Item.maxStack = 99;
            Item.consumable = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.value = Item.buyPrice(0, 0, 3);
            Item.createTile = ModContent.TileType<EpicHouseTile>();
        }

        public override void HoldItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.ownedProjectileCounts[ModContent.ProjectileType<EpicHouseVisual>()] < 1)
            {
                Vector2 mouse = Main.MouseWorld;
                Projectile.NewProjectile(player.GetSource_ItemUse(Item), mouse, Vector2.Zero, ModContent.ProjectileType<EpicHouseVisual>(), 0, 0, player.whoAmI);
            }
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup("Wood", 60)
                .AddIngredient(ItemID.Torch)
                .AddTile(TileID.WorkBenches)
                .Register();

            if (ModLoader.TryGetMod("Fargowiltas", out Mod Mutant))
            {
                Mutant.TryFind("AutoHouse", out ModItem autoHouse);
                CreateRecipe()
                    .AddIngredient(autoHouse.Type)
                    .AddRecipeGroup("Wood", 10)                    
                    .AddTile(TileID.WorkBenches)
                    .Register();
            }

        }
    }
}