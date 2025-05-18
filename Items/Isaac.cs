using Microsoft.Xna.Framework;
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

namespace TerrariaEpicVerision.Items
{
	public class Isaac : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("balls"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
			//Tooltip.SetDefault("Is this a " + '"' + "The Binding of Isaac" + '"' + " reference??");
		}

		public override void SetDefaults()
		{
			//Item.DefaultToRangedWeapon(ProjectileID.GelBalloon,)

			Item.damage = 18;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 6;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.UseSound = new SoundStyle("TerrariaEpicVerision/Sounds/Isaac Shoot");
			Item.autoReuse = true;

			Item.shoot = ProjectileID.GelBalloon;
			Item.shootSpeed = 25;
			Item.ammo = AmmoID.None;
			Item.noMelee = true;
		}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips.FirstOrDefault(tooltip => tooltip.Name == "Speed" && tooltip.Mod == "Terraria").Text = "awesome Speed";
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.RottenChunk, 20);
			recipe.AddIngredient(ItemID.PurificationPowder, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}