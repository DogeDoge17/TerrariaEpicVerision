using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaEpicVerision.Items
{
	public class Balls : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("balls"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("This is an aesthetic modded sword.");
		}

		public override void SetDefaults()
		{
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;	
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = 10000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtWall, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}