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
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.UI;

namespace TerrariaEpicVerision.UI.Nanako
{
    public class WelcomeHome : ModSystem
    {
        internal UserInterface MyInterface;
        internal WelcomeHomeUI MyUI;

        private bool alreadyPlayed = true;
        private float hideTimer = 3f;
        private float playAgainTimer = 0;
        private float playAgainInterval = 60f;

        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                MyUI = new WelcomeHomeUI();
                MyUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            }

            base.Load();
        }

        public override void Unload()
        {
            //MyUI?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            MyUI = null;

            base.Unload();
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            //Console.WriteLine(Vector3.Distance(new Vector3(Main.LocalPlayer.oldPosition.X, Main.LocalPlayer.oldPosition.Y, 0), new Vector3(Main.spawnTileX * 16, Main.spawnTileY * 16, 0)));

            if (Vector3.Distance(new Vector3(Main.LocalPlayer.oldPosition.X, Main.LocalPlayer.oldPosition.Y, 0), new Vector3(Main.spawnTileX * 16, Main.spawnTileY * 16, 0)) < 879.75f) /*(IsWithin((int)Main.LocalPlayer.oldPosition.X, (Main.spawnTileX - 55) * 16, (Main.spawnTileX + 55) * 16) && IsWithin((int)Main.LocalPlayer.oldPosition.Y, (Main.spawnTileY - 55) * 16, (Main.spawnTileY + 55) * 16))*/ //(Enumerable.Range((Main.spawnTileX - 30) * 16, (Main.spawnTileX + 30) * 16).Contains((int)Main.LocalPlayer.oldPosition.X))//((Main.LocalPlayer.oldPosition.X <= (Main.spawnTileX + 30) * 16 && Main.clientPlayer.oldPosition.X >= (Main.spawnTileX - 30) * 16))
            {
                hideTimer -= 1 * Time.deltaTime;
                if (!alreadyPlayed && playAgainTimer < 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/Welcome Home"));

                    playAgainTimer = playAgainInterval;

                    alreadyPlayed = true;
                    ShowMyUI();

                }

                if(hideTimer < 0)
                {
                    HideMyUI();
                }
            }
            else
            {
                hideTimer = 5;
                alreadyPlayed = false;

                playAgainTimer -= 1 * Time.deltaTime;

                HideMyUI();
            }

            _lastUpdateUiGameTime = gameTime;
            if (MyInterface?.CurrentState != null)
            {
                MyInterface.Update(gameTime);
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "MyMod: MyInterface",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);
                        }
                        return true;
                    },
                       InterfaceScaleType.UI));
            }
        }

        public bool IsWithin(int value, int minimum, int maximum)
        {
            return value >= minimum && value <= maximum;
        }

        internal void ShowMyUI()
        {
            MyInterface?.SetState(MyUI);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
        }


    }

    internal class WelcomeHomeUI : UIState
    {


        //public UIImageFramed image;

        //public UIPanel panel;

        public static bool Visible = true;

        public override void OnInitialize() // 1
        {
            UIPanel panel = new UIPanel(null, null, 0, 4);
            panel.Width.Set(1152, 0); // 3
            panel.Height.Set(220, 0); // 3

            UIImage image = new UIImage(ModContent.Request<Texture2D>("TerrariaEpicVerision/UI/Nanako/WelcomeHome"), new Rectangle(0, 0, 1152, 220));
            image.Width = panel.Width;
            image.Height = panel.Height;
            image.SetImage(ModContent.Request<Texture2D>("TerrariaEpicVerision/UI/Nanako/WelcomeHome"), new Rectangle(0, 0, 1152, 220));
            panel.Append(image);

            Append(panel); // 4
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
