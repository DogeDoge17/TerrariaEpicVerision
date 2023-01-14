//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.Audio;
//using Terraria.DataStructures;
//using Terraria.Enums;
//using Terraria.GameContent.ItemDropRules;
//using Terraria.ID;
//using Terraria.IO;
//using Terraria.ModLoader;
//using Terraria.ModLoader.Utilities;
//using Terraria.ObjectData;
//using TerrariaEpicVerision.Gores;
//using TerrariaEpicVerision.NPCs.Enemy;

//namespace TerrariaEpicVerision.Items
//{
//    public class RyujiBannerTile : ModTile
//    {
//		public override void SetStaticDefaults()
//		{
//			Main.tileFrameImportant[Type] = true;
//			Main.tileNoAttach[Type] = true;
//			Main.tileLavaDeath[Type] = true;
//			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
//			TileObjectData.newTile.Height = 3;
//			TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
//			TileObjectData.newTile.StyleHorizontal = true;
//			TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
//			TileObjectData.newTile.StyleWrapLimit = 111;
//			TileObjectData.addTile(Type);
//			//.dustType = -1;
//			//disableSmartCursor = true;
//			ModTranslation name = CreateMapEntryName();
//			name.SetDefault("Banner");
//			AddMapEntry(new Color(13, 88, 130), name);
//		}

//		public override void KillMultiTile(int i, int j, int frameX, int frameY)
//		{
//			int style = frameX / 18;
//			string item;
//			switch (style)
//			{
//				case 0:
//					item = "SarcophagusBanner";
//					break;
//				case 1:
//					item = "OctopusBanner";
//					break;
//				default:
//					return;
//			}
//			Item.NewItem(null, i * 16, j * 16, 16, 48, ModContent.ItemType<RyujiBanner>());
//		}

//		public override void NearbyEffects(int i, int j, bool closer)
//		{
//			if (closer)
//			{
//				Player player = Main.LocalPlayer;
//				int style = Main.tile[i, j].TileFrameX / 18;
//				string type;
//				switch (style)
//				{
//					case 0:
//						type = "Sarcophagus";
//						break;
//					case 1:
//						type = "Octopus";
//						break;
//					default:
//						return;
//				}



//					player.AddBuff()
//				player.HasNPCBannerBuff(ModContent.NPCType<Ryuji>());
//				//player.hasBanner = true;
//			}
//		}

//		public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
//		{
//			if (i % 2 == 1)
//			{
//				spriteEffects = SpriteEffects.FlipHorizontally;
//			}
//		}
//	}
//}
