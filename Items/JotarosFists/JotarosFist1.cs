//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Text.RegularExpressions;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.Audio;
//using Terraria.DataStructures;
//using Terraria.GameContent.Bestiary;
//using Terraria.GameContent.ItemDropRules;
//using Terraria.ID;
//using Terraria.GameContent.Creative;
//using Terraria.IO;
//using Terraria.ModLoader;
//using Terraria.ModLoader.Utilities;
//using TerrariaEpicVerision.Dusts;
//using TerrariaEpicVerision.Gores;
//using TerrariaEpicVerision.Items;

//namespace TerrariaEpicVerision.Items.JotarosFists
//{
//	public class JotarosFist1 : ModItem
//	{
//		public override void SetStaticDefaults()
//		{
//		    DisplayName.SetDefault("Jotaro's Fist Tier 1"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
//			Tooltip.SetDefault("Ora");

//			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
//		}

//		public override void SetDefaults()
//		{
//			Item.damage = 33;
//			Item.DamageType = DamageClass.Melee;
//			Item.width = 32;
//			Item.height = 32;
//			Item.useTime = 12;
//			Item.useAnimation = 12;	
//			//Item.useStyle = ItemUseStyleID.Rapier;
//			Item.useStyle = ItemUseStyleID.Shoot;
//			Item.knockBack = 6;
//			Item.value = 10000;
//			Item.rare = ItemRarityID.Green;
//			Item.UseSound = SoundID.Item1;
//			Item.autoReuse = true;
//			Item.useTurn = true;
//		}
		 
//		public override void AddRecipes()
//		{
//			Recipe recipe1 = CreateRecipe();
//			recipe1.AddIngredient(ItemID.FallenStar, 20);
//			recipe1.AddIngredient(ItemID.RottenChunk, 20);
//			recipe1.AddTile(TileID.DemonAltar);
//			recipe1.Register();

//			Recipe recipe2 = CreateRecipe();
//			recipe2.AddIngredient(ItemID.FallenStar, 20);
//			recipe2.AddIngredient(ItemID.Vertebrae, 20);
//			recipe2.AddTile(TileID.DemonAltar);
//			recipe2.Register();
//		}
//	}
//}