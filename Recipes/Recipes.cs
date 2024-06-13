using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrariaEpicVerision.Items.Placeable;

namespace TerrariaEpicVerision.Recipes
{
    public class Recipes : ModSystem
    {
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.MeatGrinder)
                .AddIngredient(ItemID.Gel, 112)
                .AddTile(TileID.Anvils)
                .AddRecipeGroup(RecipeGroupID.IronBar, 43)
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
                .Register(); 
        }
    }
}
