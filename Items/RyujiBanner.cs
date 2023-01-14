//using Microsoft.Xna.Framework;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.Audio;
//using Terraria.GameContent.ItemDropRules;
//using Terraria.ID;
//using Terraria.IO;
//using Terraria.ModLoader;
//using Terraria.ModLoader.Utilities;
//using TerrariaEpicVerision.Gores;
//using TerrariaEpicVerision.NPCs.Enemy;

//namespace TerrariaEpicVerision.Items
//{
//    public class RyujiBanner : ModItem
//    {
//		public override void SetStaticDefaults()
//		{
//		   DisplayName.SetDefault("Ryuji Persona 5 Banner"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
//			Tooltip.SetDefault("Nearby players get a bonus against: Ryuji Persona 5.");
//		}

//		public override void SetDefaults()
//		{
//			Item.width = 10;
//			Item.height = 10;
//			Item.maxStack = 99;
//			Item.consumable = true;
//			Item.useStyle = ItemUseStyleID.Swing;
//			Item.useTime = 10;
//			Item.useAnimation = 10;
//			Item.createTile = ModContent.TileType<RyujiBannerTile>();
//			Item.autoReuse = true;
//			Item.rare = ItemRarityID.Blue;
//		}

//		public override void AddRecipes()
//		{

//		}
//	}
//}
