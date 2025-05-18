using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using TerrariaEpicVerision.Buffs;

namespace TerrariaEpicVerision.Items
{
    public class YestiGravityPotion : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Potions";
        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 20;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 18;
            Item.useTurn = true;
            Item.maxStack = 9999;
            Item.rare = ItemRarityID.Lime;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.UseSound = SoundID.Item3;
            Item.consumable = true;
            Item.buffType = ModContent.BuffType<YestiGravityBuff>();
            Item.buffTime = 28800;
            Item.value = Item.buyPrice(0, 2, 0, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.BottledWater).
                AddIngredient(ItemID.Prismite).
                AddTile(TileID.AlchemyTable).
                AddConsumeItemCallback(Recipe.ConsumptionRules.Alchemy).
                Register();


            if (ModLoader.TryGetMod("CalamityMod", out Mod cal))
            {
                cal.TryFind("BloodOrb", out ModItem bloodOrb);
                CreateRecipe().
                    AddIngredient(ItemID.BottledWater).
                    AddIngredient(bloodOrb).
                    AddTile(TileID.AlchemyTable).
                    Register().
                    DisableDecraft();
            }
        }
    }
}
