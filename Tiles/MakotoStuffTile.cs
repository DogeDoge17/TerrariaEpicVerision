using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using ReLogic.Content;
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
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.ObjectData;
using TerrariaEpicVerision.Dusts;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using TerrariaEpicVerision.NPCs.Enemy.Persona;
namespace TerrariaEpicVerision.Tiles
{
    public class MakotoStuffTile : ModTile
    {
        public Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/Tiles/MakotoStuffTileBIG");
        public Rectangle source = new Rectangle(0,0,2369, 2309);



        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileID.Sets.FramesOnKillWall[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
            TileObjectData.addTile(Type);

            AddMapEntry(new Color(120, 85, 60), Language.GetText("Makoto Stuff Painting"));
            DustType = 7;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(new EntitySource_TileBreak(i, j), i * 16, j * 16, 32, 32, ModContent.ItemType<MakotoStuffPainting>());
        }

        //int c = 9;
        //public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        //{
        //    if (!Config.useLowRes)
        //    {
        //        if (c == 9)
        //        {
        //            spriteBatch.Draw(((Texture2D)largeImage), new Vector2(i * 16 - (int)Main.screenPosition.X +192, j * 16 - (int)Main.screenPosition.Y + 192), source, Color.White, 0, new Vector2(0, 0), (float)(48 + 48) / (float)(source.Width + source.Height), SpriteEffects.None, 0);
        //            c = 0;
        //        }
        //        c++;
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}
    }
}



