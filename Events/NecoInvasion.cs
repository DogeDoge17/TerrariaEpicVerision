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
//using Terraria.ModLoader.Default;
//using Terraria.ModLoader.Utilities;
//using TerrariaEpicVerision.NPCs.Enemy.NecoArc;

//namespace TerrariaEpicVerision.Events
//{
//    public class NecoInvasion : ModSystem
//    {

//        public bool invading = false;
//        private bool gaveOutMessageEnd = true;
//        private bool endStuff = true;

//        //public float timeLast = 10000;
//        //public float timeLeft = 10000;

//        readonly private float spawnInterval = 2f;
//        private float spawnTimer = 0.5f;

//        private bool playingMusic = false;

//        public override void UpdateUI(GameTime gameTime)
//        {
//            int chance = Main.rand.Next(0, 26);

//            if (!Main.dayTime && Main.time == 0 && !invading)
//            {


//                // Main.NewText("Attempted to start a neco invasion with a chance of: " + chance);

//                if (chance == 15 && !Main.slimeRain)
//                {
//                    //timeLeft = timeLast;
//                    endStuff = false;
//                    invading = true;
//                    gaveOutMessageEnd = false;
//                    Main.NewText("NECO INVASION HAS BEGAN UWA~!!");
//                }
//            }
//            else if (Main.dayTime)
//            {
//                if (!endStuff)
//                {
//                    Main.StartRain();

//                    endStuff = true;
//                }

//                invading = false;

//                if (!gaveOutMessageEnd)
//                {
//                    Main.NewText("Teh neco arc army has been defeated UwU~");

//                    gaveOutMessageEnd = true;
//                }
//            }

//            if (Main.slimeRain)
//            {
//                if (!endStuff)
//                {
//                    Main.StartRain();

//                    endStuff = true;
//                }

//                invading = false;

//                if (!gaveOutMessageEnd)
//                {
//                    Main.NewText("Teh Neco-Arc army has left.");

//                    gaveOutMessageEnd = true;
//                }
//            }

//            //timeLeft -= 1 * Time.deltaTime;

//            spawnTimer -= 1 * Time.deltaTime;

//            int xDist = Main.rand.Next(600, 1200);
//            int yDist = Main.rand.Next(0, 100);

//            int necoType = Main.rand.Next(0, 45);

//            if (Main.rand.Next(0, 2) == 1) xDist *= -1;
//            yDist *= -1;

//            int playerToSpawnOn = Main.rand.Next(0, Main.player.Length - 1);

//            if (invading)
//            {
//                if (!playingMusic)
//                {
//                    if (MusicLoader.MusicExists("TerrariaEpicVerision/Sounds/Music/Neco Invasion"))
//                    {
//                        Main.curMusic = MusicLoader.GetMusicSlot("TerrariaEpicVerision/Sounds/Music/Neco Invasion");
//                    }
//                    else
//                    {
//                        MusicLoader.AddMusic(this.Mod, "TerrariaEpicVerision/Sounds/Music/Neco Invasion");
//                        Main.curMusic = MusicLoader.GetMusicSlot("TerrariaEpicVerision/Sounds/Music/Neco Invasion");
//                    }

//                    playingMusic = true;
//                }

//                if (spawnTimer <= 0)
//                {
//                    if (necoType >= 0 && necoType < 15)
//                        NPC.NewNPC(null, (int)Main.LocalPlayer.position.X + xDist, (int)Main.LocalPlayer.position.Y + yDist, ModContent.NPCType<NecoArc>());
//                    else if (necoType >= 15 && necoType < 21)
//                        NPC.NewNPC(null, (int)Main.LocalPlayer.position.X + xDist, (int)Main.LocalPlayer.position.Y + yDist, ModContent.NPCType<BuffcoArc>());
//                    else if (necoType >= 21 && necoType < 30)
//                        NPC.NewNPC(null, (int)Main.LocalPlayer.position.X + xDist, (int)Main.LocalPlayer.position.Y + yDist, ModContent.NPCType<SnowArc>());
//                    else if (necoType >= 30 && necoType < 45)
//                        NPC.NewNPC(null, (int)Main.LocalPlayer.position.X + xDist, (int)Main.LocalPlayer.position.Y + yDist, ModContent.NPCType<JungleArc>());

//                    spawnTimer = spawnInterval;
//                }
//            }

//            base.UpdateUI(gameTime);
//        }
//    }
//}