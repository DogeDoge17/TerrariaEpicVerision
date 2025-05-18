using TerrariaEpicVerision.Tiles;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using System.Collections.Generic;
using System.Linq;
using Terraria.DataStructures;
using Terraria.ObjectData;
using Microsoft.VisualBasic;
using ReLogic.OS;

namespace TerrariaEpicVerision.Projectiles
{
    public class EpicHouseProj : ModProjectile
    {
        public static bool[] InstaCannotDestroyT;
        public static bool[] DungeonTile;
        public static bool[] HardmodeOre;

        public static bool[] InstaCannotDestroyW;
        public static bool[] DungeonWall;

        public static HashSet<Rectangle> CannotDestroyRectangle = new HashSet<Rectangle>();

        public override void SetStaticDefaults()
        {
            SetFactory tileFactory = TileID.Sets.Factory;
            HardmodeOre = tileFactory.CreateBoolSet(false,
               TileID.Cobalt,
               TileID.Palladium,
               TileID.Mythril,
               TileID.Orichalcum,
               TileID.Adamantite,
               TileID.Titanium);

            DungeonTile = tileFactory.CreateBoolSet(false,
                TileID.BlueDungeonBrick,
                TileID.GreenDungeonBrick,
                TileID.PinkDungeonBrick);

            InstaCannotDestroyT = tileFactory.CreateBoolSet(false);

            SetFactory wallFactory = WallID.Sets.Factory;
            InstaCannotDestroyW = wallFactory.CreateBoolSet(false);

            DungeonWall = wallFactory.CreateBoolSet(false,
                WallID.BlueDungeonSlabUnsafe,
                WallID.BlueDungeonTileUnsafe,
                WallID.BlueDungeonUnsafe,
                WallID.GreenDungeonSlabUnsafe,
                WallID.GreenDungeonTileUnsafe,
                WallID.GreenDungeonUnsafe,
                WallID.PinkDungeonSlabUnsafe,
                WallID.PinkDungeonTileUnsafe,
                WallID.PinkDungeonUnsafe);

            base.SetStaticDefaults();
        }   


        public override void SetDefaults()
        {
            Projectile.width = 1;
            Projectile.height = 1;
            Projectile.timeLeft = 1;
        }

        public static void PlaceHouse(int x, int y, Vector2 position, int side, Player player)
        {
            int xPosition = (int)((side * -1) + x + position.X / 16.0f);
            int yPosition = (int)(y + position.Y / 16.0f);
            Tile tile = Main.tile[xPosition, yPosition];

            // Testing for blocks that should not be destroyed
            if (!OkayToDestroyTileAt(xPosition, yPosition, true))
                return;

            int wallType = WallID.Wood;
            int tileType = TileID.WoodBlock;
            int platformStyle = 0;

            if (player.ZoneDesert && !player.ZoneBeach)
            {
                wallType = WallID.Cactus;
                tileType = TileID.CactusBlock;
                platformStyle = 25;
            }
            else if (player.ZoneSnow)
            {
                wallType = WallID.BorealWood;
                tileType = TileID.BorealWood;
                platformStyle = 19;
            }
            else if (player.ZoneJungle)
            {
                wallType = WallID.RichMaogany;
                tileType = TileID.RichMahogany;
                platformStyle = 2;
            }
            else if (player.ZoneCorrupt)
            {
                wallType = WallID.Ebonwood;
                tileType = TileID.Ebonwood;
                platformStyle = 1;
            }
            else if (player.ZoneCrimson)
            {
                wallType = WallID.Shadewood;
                tileType = TileID.Shadewood;
                platformStyle = 5;
            }
            else if (player.ZoneBeach)
            {
                wallType = WallID.PalmWood;
                tileType = TileID.PalmWood;
                platformStyle = 17;
            }
            else if (player.ZoneHallow)
            {
                wallType = WallID.Pearlwood;
                tileType = TileID.Pearlwood;
                platformStyle = 3;
            }
            else if (player.ZoneGlowshroom)
            {
                wallType = WallID.Mushroom;
                tileType = TileID.MushroomBlock;
                platformStyle = 18;
            }
            else if (player.ZoneSkyHeight)
            {
                wallType = WallID.DiscWall;
                tileType = TileID.Sunplate;
                platformStyle = 22;
            }
            else if (player.ZoneUnderworldHeight)
            {
                wallType = WallID.ObsidianBrick;
                tileType = TileID.ObsidianBrick;
                platformStyle = 13;
            }

            if (x == 10 * side || x == 1 * side)
            {
                //dont act if the right tile already above (but DO replace a corner platform)
                if (y == -5 && tile.TileType == tileType)
                    return;

                //dont act on correct block above/below door, destroying them will break it
                if ((y == -4 || y == 0) && tile.TileType == tileType)
                    return;

                if ((y == -1 || y == -2 || y == -3) && (tile.TileType == TileID.ClosedDoor || tile.TileType == TileID.OpenDoor))
                    return;
            }
            else //for blocks besides those on the left/right edges where doors are placed, its okay to have platform as floor
            {
                //dont act if the right blocks already above
                if (y == -5 && (tile.TileType == TileID.Platforms || tile.TileType == tileType))
                    return;

                if (y == 0 && (tile.TileType == TileID.Platforms || tile.TileType == tileType))
                    return;
            }

            //doing it this way so the code still runs to place bg walls behind open door
            if (!((x == 9 * side || x == 2 * side) && (y == -1 || y == -2 || y == -3) && tile.TileType == TileID.OpenDoor))
                ClearEverything(xPosition, yPosition);

            // Spawn walls
            if (y != -5 && y != 0 && x != (10 * side) && x != (1 * side))
            {
                WorldGen.PlaceWall(xPosition, yPosition, wallType);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);
            }

            //platforms on top
            if (y == -5 && Math.Abs(x) >= 3 && Math.Abs(x) <= 5)
            {
                WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms, style: platformStyle);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, xPosition, yPosition, TileID.Platforms, platformStyle);
            }
            // Spawn border
            else if ((y == -5) || (y == 0) || (x == (10 * side) && ((y != -1) && (y != -2)&& (y != -3))) || (x == (1 * side) && y == -4))
            {
                WorldGen.PlaceTile(xPosition, yPosition, tileType);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);
            }
        }
       
        public static void PlaceFurniture(int x, int y, Vector2 position, int side, Player player)
        {
            int xPosition = (int)((side * -1) + x + position.X / 16.0f);
            int yPosition = (int)(y + position.Y / 16.0f);

            Tile tile = Main.tile[xPosition, yPosition];
            // Testing for blocks that should not be destroyed
            if (!OkayToDestroyTileAt(xPosition, yPosition, true))
                return;

            if (y == -1)
            {
                //Door
                if (Math.Abs(x) == 1 || Math.Abs(x) == 10)
                {
                    int placeStyle = 0;

                    if (player.ZoneDesert && !player.ZoneBeach)
                    {
                        placeStyle = 4;
                    }
                    else if (player.ZoneSnow)
                    {
                        placeStyle = 30;
                    }
                    else if (player.ZoneJungle)
                    {
                        placeStyle = 2;
                    }
                    else if (player.ZoneCorrupt)
                    {
                        placeStyle = 1;
                    }
                    else if (player.ZoneCrimson)
                    {
                        placeStyle = 10;
                    }
                    else if (player.ZoneBeach)
                    {
                        placeStyle = 29;
                    }
                    else if (player.ZoneHallow)
                    {
                        placeStyle = 3;
                    }
                    else if (player.ZoneGlowshroom)
                    {
                        placeStyle = 6;
                    }
                    else if (player.ZoneSkyHeight)
                    {
                        placeStyle = 9;
                    }
                    else if (player.ZoneUnderworldHeight)
                    {
                        placeStyle = 19;
                    }

                    var doe = WorldGen.PlaceTile(xPosition, yPosition, TileID.ClosedDoor, style: placeStyle, forced:true);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendTileSquare(-1, xPosition, yPosition - 2, 1, 3);
                }


                //if side == -1 then 6                
                int furnPos = side == 1 ? 6 : 6;                
             
                //Chair
                if (x == furnPos * side)
                {
                    int placeStyle = 0;

                    if (player.ZoneDesert && !player.ZoneBeach)
                    {
                        placeStyle = 6;
                    }
                    else if (player.ZoneSnow)
                    {
                        placeStyle = 30;
                    }
                    else if (player.ZoneJungle)
                    {
                        placeStyle = 3;
                    }
                    else if (player.ZoneCorrupt)
                    {
                        placeStyle = 2;
                    }
                    else if (player.ZoneCrimson)
                    {
                        placeStyle = 11;
                    }
                    else if (player.ZoneBeach)
                    {
                        placeStyle = 29;
                    }
                    else if (player.ZoneHallow)
                    {
                        placeStyle = 4;
                    }
                    else if (player.ZoneGlowshroom)
                    {
                        placeStyle = 9;
                    }
                    else if (player.ZoneSkyHeight)
                    {
                        placeStyle = 10;
                    }
                    else if (player.ZoneUnderworldHeight)
                    {
                        placeStyle = 16;
                    }

                    WorldGen.PlaceObject(xPosition, yPosition, TileID.Chairs, direction: side, style: placeStyle);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, xPosition, yPosition, TileID.Chairs, placeStyle);
                }

                //workbench
                furnPos = (side == 1) ? 7 : 8;

                if (x == (furnPos * side))
                {
                    int placeStyle = 0;

                    if (player.ZoneDesert && !player.ZoneBeach)
                    {
                        placeStyle = 5;
                    }
                    else if (player.ZoneSnow)
                    {
                        placeStyle = 23;
                    }
                    else if (player.ZoneJungle)
                    {
                        placeStyle = 2;
                    }
                    else if (player.ZoneCorrupt)
                    {
                        placeStyle = 1;
                    }
                    else if (player.ZoneCrimson)
                    {
                        placeStyle = 6;
                    }
                    else if (player.ZoneBeach)
                    {
                        placeStyle = 22;
                    }
                    else if (player.ZoneHallow)
                    {
                        placeStyle = 3;
                    }
                    else if (player.ZoneGlowshroom)
                    {
                        placeStyle = 7;
                    }
                    else if (player.ZoneSkyHeight)
                    {
                        placeStyle = 24;
                    }
                    else if (player.ZoneUnderworldHeight)
                    {
                        placeStyle = 14;
                    }

                    WorldGen.PlaceTile(xPosition, yPosition, TileID.WorkBenches, style: placeStyle);
                    if (Main.netMode == NetmodeID.Server)
                        NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, xPosition, yPosition, TileID.Tables, placeStyle);
                }
            }
            //Torch
            if (x == (7 * side) && y == -4)
            {
                WorldGen.PlaceTile(xPosition, yPosition, TileID.Torches, style: 5);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, xPosition, yPosition, TileID.Torches);
            }
        }

        public static void UpdateWall(int x, int y, Vector2 position, int side, Player player)
        {
            int xPosition = (int)((side * -1) + x + position.X / 16.0f);
            int yPosition = (int)(y + position.Y / 16.0f);

            WorldGen.SquareWallFrame(xPosition, yPosition);
            if (Main.netMode == NetmodeID.Server)
                NetMessage.SendTileSquare(-1, xPosition, yPosition, 1);
        }

        private int UpdateX(int x, float posX, int side) => (int)((side * -1) + x + posX / 16.0f);

        private void PlaceOutsidePlatforms(Vector2 position, Player player, int side)
        {
            int xPosition = UpdateX(-1 * side , position.X, side);
            int yPosition = (int)(0 + position.Y / 16.0f);
            int platformStyle = 0;

            if (player.ZoneDesert && !player.ZoneBeach)
            {
                platformStyle = 25;
            }
            else if (player.ZoneSnow)
            {
                platformStyle = 19;
            }
            else if (player.ZoneJungle)
            {
                platformStyle = 2;
            }
            else if (player.ZoneCorrupt)
            {
                platformStyle = 1;
            }
            else if (player.ZoneCrimson)
            {
                platformStyle = 5;
            }
            else if (player.ZoneBeach)
            {
                platformStyle = 17;
            }
            else if (player.ZoneHallow)
            {
                platformStyle = 3;
            }
            else if (player.ZoneGlowshroom)
            {
                platformStyle = 18;
            }
            else if (player.ZoneSkyHeight)
            {
                platformStyle = 22;
            }
            else if (player.ZoneUnderworldHeight)
            {
                platformStyle = 13;
            }



            for (int i = 0; i < 4; i++)
            {
                //xPosition = UpdateX(-2, position.X, side);
                switch (i)
                {
                    case 1:
                        xPosition = UpdateX(0, position.X, side);
                        break;
                    case 2:
                        xPosition = UpdateX(11*side, position.X, side);
                        break;
                    case 3:
                        xPosition = UpdateX(12 * side, position.X, side);
                        break;
                }

                //ClearEverything(xPosition, yPosition);
                if (!Main.tile[xPosition,yPosition].HasTile)
                WorldGen.PlaceTile(xPosition, yPosition, TileID.Platforms, style: platformStyle, forced: true);
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.TileManipulation, -1, -1, null, 1, xPosition, yPosition, TileID.Platforms, platformStyle);
            }


        }

        public override void OnKill(int timeLeft)
        {
            Vector2 position = Projectile.Center;
            SoundEngine.PlaySound(SoundID.Item14, position);
            Player player = Main.player[Projectile.owner];

            if (Main.netMode == NetmodeID.MultiplayerClient)
                return;

            if (player.Center.X < position.X)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int x = 11; x > -1; x--)
                    {
                        if (i != 2 && (x == 11 || x == 0))
                            continue;

                        for (int y = -6; y <= 1; y++)
                        {
                            if (i != 2 && (y == -6 || y == 1))
                                continue;

                            if (i == 0)
                            {
                                PlaceHouse(x, y, position, 1, player);
                            }
                            else if (i == 1)
                            {
                                PlaceFurniture(x, y, position, 1, player);
                            }
                            else
                            {
                                UpdateWall(x, y, position, 1, player);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int x = -11; x < 1; x++)
                    {
                        if (i != 2 && (x == -11 || x == 0))
                            continue;

                        for (int y = -6; y <= 1; y++)
                        {
                            if (i != 2 && (y == -6 || y == 1))
                                continue;

                            if (i == 0)
                            {
                                PlaceHouse(x, y, position, -1, player);
                            }
                            else if (i == 1)
                            {
                                PlaceFurniture(x, y, position, -1, player);
                            }
                        }
                    }
                }
            }

            if (player.Center.X < position.X)            
                PlaceOutsidePlatforms(position, player, 1);            
            else
                PlaceOutsidePlatforms(position, player, -1);            
        }

      

        internal static void DestroyChest(int x, int y)
        {
            int chestType = 1;

            int chest = Chest.FindChest(x, y);
            if (chest != -1)
            {
                for (int i = 0; i < 40; i++)
                {
                    Main.chest[chest].item[i] = new Item();
                }

                Main.chest[chest] = null;

                if (Main.tile[x, y].TileType == TileID.Containers2)
                {
                    chestType = 5;
                }

                if (Main.tile[x, y].TileType >= TileID.Count)
                {
                    chestType = 101;
                }
            }

            for (int i = x; i < x + 2; i++)
            {
                for (int j = y; j < y + 2; j++)
                {
                    Main.tile[i, j].TileType = 0;
                    //Main.tile[i, j].sTileHeader = 0;
                    Main.tile[i, j].TileFrameX = 0;
                    Main.tile[i, j].TileFrameY = 0;
                }
            }

            if (Main.netMode != NetmodeID.SinglePlayer)
            {
                if (chest != -1)
                {
                    NetMessage.SendData(MessageID.ChestUpdates, -1, -1, null, chestType, x, y, 0f, chest, Main.tile[x, y].TileType);
                }

                NetMessage.SendTileSquare(-1, x, y, 3);
            }
        }

        internal static Point16 FindChestTopLeft(int x, int y, bool destroy)
        {
            Tile tile = Main.tile[x, y];
            if (TileID.Sets.BasicChest[tile.TileType])
            {
                TileObjectData data = TileObjectData.GetTileData(tile.TileType, 0);
                x -= tile.TileFrameX / 18 % data.Width;
                y -= tile.TileFrameY / 18 % data.Height;

                if (destroy)
                {
                    DestroyChest(x, y);
                }

                return new Point16(x, y);
            }

            return Point16.NegativeOne;
        }

        internal static void ClearEverything(int x, int y, bool sendData = true)
        {
            FindChestTopLeft(x, y, true);

            Tile tile = Main.tile[x, y];
            bool hadLiquid = tile.LiquidAmount != 0;
            WorldGen.KillTile(x, y, noItem: true);
            tile.ClearEverything();

            //tile.lava(false);
            //tile.honey(false);

            if (Main.netMode == NetmodeID.Server)
            {
                if (hadLiquid)
                    NetMessage.sendWater(x, y);
                if (sendData)
                    NetMessage.SendTileSquare(-1, x, y, 1);
            }
        }

        public static bool OkayToDestroyTile(Tile tile)
        {
            if (tile == null)
            {
                return false;
            }
            bool noDungeon = !NPC.downedBoss3 && (DungeonWall[tile.WallType] || DungeonTile[tile.TileType]);

            bool noHMOre = HardmodeOre[tile.TileType] && !NPC.downedMechBossAny;
            bool noChloro = tile.TileType == TileID.Chlorophyte && !(NPC.downedMechBoss1 && NPC.downedMechBoss2 && NPC.downedMechBoss3);
            bool noLihzahrd = (tile.TileType == TileID.LihzahrdBrick || tile.WallType == WallID.LihzahrdBrickUnsafe) && !NPC.downedGolemBoss;
            bool noAbyss = false;

            if (ModLoader.TryGetMod("CalamityMod", out Mod calamity))
            {
                if (calamity.TryFind("AbyssGravel", out ModTile gravel) && calamity.TryFind("Voidstone", out ModTile voidstone))
                    noAbyss = tile.TileType == gravel.Type || tile.TileType == voidstone.Type;
            }

            if (noDungeon || noHMOre || noChloro || noLihzahrd || noAbyss || TileBelongsToMagicStorage(tile) ||
                InstaCannotDestroyT[tile.TileType] ||
                InstaCannotDestroyW[tile.WallType])
                return false;

            return true;
        }
        public static bool OkayToDestroyTileAt(int x, int y, bool bypassVanillaCanPlace = false) // Testing for blocks that should not be destroyed
        {
            if (!WorldGen.InWorld(x, y))
                return false;
            Tile tile = Main.tile[x, y];
            if (tile == null)
            {
                return false;
            }
            if (CannotDestroyRectangle != null && CannotDestroyRectangle.Any())
            {
                foreach (Rectangle rect in CannotDestroyRectangle)
                {
                    if (rect.Contains(x * 16, y * 16))
                    {
                        return false;
                    }
                }
            }
            Rectangle area = new(x, y, 3, 3);
            if (!bypassVanillaCanPlace && GenVars.structures != null && !GenVars.structures.CanPlace(area))
            {
                return false;
            }

            return OkayToDestroyTile(tile);
        }

        public static bool TileBelongsToMagicStorage(Tile tile)
        {
            return ModLoader.TryGetMod("MagicStorage", out Mod res) && TileLoader.GetTile(tile.TileType)?.Mod == ModLoader.GetMod("MagicStorage");
        }

    }
}