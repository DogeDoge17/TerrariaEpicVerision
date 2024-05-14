using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.ObjectData;
using TerrariaEpicVerision.Dusts;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using TerrariaEpicVerision.NPCs.Enemy.Persona;
using TerrariaEpicVerision.Tiles;

namespace TerrariaEpicVerision.Items
{
	public class AigisStuffPainting : ModItem
	{
		public override void SetStaticDefaults()
		{
			//DisplayName.SetDefault("Aigis Persona Three Stuff painting");
			//Tooltip.SetDefault("im stuff");
		}

		public override void SetDefaults()
		{
			Item.width = 48;
			Item.height = 34;

            Item.buyPrice(1, 3, 0, 0);

            Item.consumable = true;
			Item.maxStack = 9999;
			Item.useAnimation = 15;
			Item.useTime = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTurn = true;
			Item.autoReuse = true;

			Item.createTile = ModContent.TileType<AigisStuffTile>();
		}

		public override bool CanUseItem(Player player)
		{

			return true;
		}
	}
}

