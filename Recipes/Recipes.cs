using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrariaEpicVerision.Items.Placeable;
using Terraria.Localization;

namespace TerrariaEpicVerision.Recipes
{
    public class Recipes : ModSystem
    {

        public static RecipeGroup HerbRecipeGroup;

        public override void AddRecipeGroups()
        {         
            HerbRecipeGroup = new RecipeGroup(() => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue($"Mods.TerrariaEpicVerision.RecipeGroup.Herb")}",
                ItemID.Daybloom, ItemID.DaybloomSeeds, ItemID.Moonglow, ItemID.MoonglowSeeds, ItemID.Blinkroot, ItemID.BlinkrootSeeds, ItemID.Waterleaf, ItemID.WaterleafSeeds, ItemID.Deathweed, ItemID.DeathweedSeeds, ItemID.Shiverthorn, ItemID.ShiverthornSeeds);

            RecipeGroup.RegisterGroup("Herb", HerbRecipeGroup);
        }

        public override void AddRecipes()
        {
            Recipe.Create(ItemID.MeatGrinder)
                .AddIngredient(ItemID.Gel, 112)
                .AddTile(TileID.Anvils)
                .AddRecipeGroup(RecipeGroupID.IronBar, 43)
                .AddCondition(Condition.Hardmode)
                .Register();

            Recipe.Create(ItemID.StoneBlock)
                .AddIngredient<StonePlatform>(2)
                .Register();

            Recipe.Create(ItemID.DirtBlock)
                .AddIngredient<DirtPlatform>(2)
                .Register();

            Recipe.Create(ItemID.Emerald)
                .AddIngredient(ItemID.Sapphire, 2)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe.Create(ItemID.Sapphire)
                .AddIngredient(ItemID.Emerald, 2)
                .AddTile(TileID.Anvils)
                .Register();

            Recipe.Create(ItemID.TruffleWorm)
                .AddIngredient(ItemID.Worm)
                .AddIngredient(ItemID.GlowingMushroom, 10)
                .AddTile(TileID.DemonAltar)
                .AddCondition(Condition.Hardmode)
                .Register();

            Recipe.Create(ItemID.HerbBag).AddRecipeGroup("Herb", 10).AddTile(TileID.DemonAltar).Register();

            Recipe.Create(ItemID.Cloud, 4).AddIngredient(ItemID.BottledWater).AddCondition(Condition.InUnderworld).Register();

            Recipe.Create(ItemID.WaterBucket).AddIngredient(ItemID.EmptyBucket).AddCondition(Condition.NearWater).Register();
            Recipe.Create(ItemID.WaterBucket).AddIngredient(ItemID.EmptyBucket).AddIngredient(ItemID.BottledWater).Register();

            Recipe.Create(ItemID.HoneyBucket).AddIngredient(ItemID.EmptyBucket).AddCondition(Condition.NearHoney).Register();
            Recipe.Create(ItemID.HoneyBucket).AddIngredient(ItemID.EmptyBucket).AddIngredient(ItemID.BottledHoney).Register();

            Recipe.Create(ItemID.LavaBucket).AddIngredient(ItemID.EmptyBucket).AddCondition(Condition.NearLava).Register();

            Recipe.Create(ItemID.TurtleShell).AddIngredient(ItemID.Turtle,10).AddTile(TileID.MeatGrinder).Register();

            if (ModLoader.TryGetMod("FargowiltasSouls", out Mod Souls))
            {
                Souls.TryFind("SharpshootersEssence", out ModItem sharp);
                Recipe.Create(sharp.Type, 1).AddIngredient(ItemID.PainterPaintballGun)
                .AddIngredient(ItemID.IceBow)
                .AddIngredient(ItemID.BloodRainBow)
                .AddIngredient(ItemID.Harpoon)
                .AddIngredient(ItemID.PhoenixBlaster)
                .AddIngredient(ItemID.BeesKnees)
                .AddIngredient(ItemID.HellwingBow)
                .AddIngredient(ItemID.RangerEmblem)
                .AddIngredient(ItemID.HallowedBar, 5)

                .AddTile(TileID.TinkerersWorkbench)
                .Register();
            }

        }
       
    }
}
