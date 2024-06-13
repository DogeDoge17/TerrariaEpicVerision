using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrariaEpicVerision.Items.Fishing;

namespace TerrariaEpicVerision.Items
{
    public class AryanPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("balls"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            //Tooltip.SetDefault("Is this a " + '"' + "The Binding of Isaac" + '"' + " reference??");
        }


        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 9999;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.rare = ItemRarityID.Lime;
            Item.useStyle = ItemUseStyleID.EatFood;            
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.value = Item.buyPrice(0, 0, 52, 4);
        }


        //public override void SetDefaults()
        //{
        //    Item.width = 32;
        //    Item.height = 32;
        //    Item.useTime = 75;
        //    Item.useStyle = ItemUseStyleID.DrinkLiquid;
        //    Item.useTurn = true;
        //    Item.value = 1000;
        //    Item.rare = ItemRarityID.Lime;
        //    Item.UseSound = SoundID.Item3;
        //    Item.consumable = true;
        //    Item.maxStack = Item.CommonMaxStack;
        //}

        public override void AddRecipes()
        {
           CreateRecipe().
            AddIngredient<GermanFish>().
            AddIngredient(ItemID.Daybloom).
            AddIngredient(ItemID.Blinkroot).
            AddIngredient(ItemID.Sunflower,3).            
            AddTile(TileID.Bottles).
            Register();
        }

        public override bool? UseItem(Player player)
        {
            player.hairColor = new Color(223, 193, 80);
            player.skinColor = new Color(255, 143, 113);
            player.eyeColor = new Color(61, 255, 242);
            return true;
        }

    }
}
