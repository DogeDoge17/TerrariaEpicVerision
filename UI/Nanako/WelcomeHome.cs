using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
                MyUI.Activate();
            }

            base.Load();
        }

        public override void Unload()
        {
            MyUI = null;

            base.Unload();
        }

        private GameTime _lastUpdateUiGameTime;

        public override void UpdateUI(GameTime gameTime)
        {
            if (!Config.nanakoEnabled)
            {
                if (MyInterface?.CurrentState != null)
                    HideMyUI();
                return;
            }

            //Console.WriteLine(Vector3.Distance(new Vector3(Main.LocalPlayer.oldPosition.X, Main.LocalPlayer.oldPosition.Y, 0), new Vector3(Main.spawnTileX * 16, Main.spawnTileY * 16, 0)));
            //Main.LocalPlayer.moveSpeed = -0.4f;
            Rectangle mouseRect = new(Main.mouseX, Main.mouseY, 0, 0);
            if (Vector3.Distance(new Vector3(Main.LocalPlayer.oldPosition.X, Main.LocalPlayer.oldPosition.Y, 0), new Vector3(Main.spawnTileX * 16, Main.spawnTileY * 16, 0)) < 879.75f && !mouseRect.Intersects(new Rectangle(0, 0, 1152, 220))) /*(IsWithin((int)Main.LocalPlayer.oldPosition.X, (Main.spawnTileX - 55) * 16, (Main.spawnTileX + 55) * 16) && IsWithin((int)Main.LocalPlayer.oldPosition.Y, (Main.spawnTileY - 55) * 16, (Main.spawnTileY + 55) * 16))*/ //(Enumerable.Range((Main.spawnTileX - 30) * 16, (Main.spawnTileX + 30) * 16).Contains((int)Main.LocalPlayer.oldPosition.X))//((Main.LocalPlayer.oldPosition.X <= (Main.spawnTileX + 30) * 16 && Main.clientPlayer.oldPosition.X >= (Main.spawnTileX - 30) * 16))
            {
                hideTimer -= 1 * Time.deltaTime;
                if (!alreadyPlayed && playAgainTimer < 0)
                {
                    SoundEngine.PlaySound(new SoundStyle("TerrariaEpicVerision/Sounds/Welcome Home"));

                    playAgainTimer = playAgainInterval;

                    alreadyPlayed = true;
                    ShowMyUI();

                }

                if (hideTimer < 0)
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
                if (mouseRect.Intersects(new Rectangle(0, 0, 1152, 220))) HideMyUI();

                MyInterface.Update(gameTime);
            }


        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int mouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (mouseTextIndex != -1)
            {
                layers.Insert(mouseTextIndex, new LegacyGameInterfaceLayer(
                    "TerrariaEpicVerision: Welcome Home",
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

        public override void OnInitialize()
        {
            UIPanel panel = new UIPanel(null, null, 0, 4);
            panel.Width.Set(1152, 0);
            panel.Height.Set(220, 0);

            UIImage image = new UIImage(ModContent.Request<Texture2D>("TerrariaEpicVerision/UI/Nanako/WelcomeHome"), new Rectangle(0, 0, 1152, 220));
            image.Width = panel.Width;
            image.Height = panel.Height;
            image.SetImage(ModContent.Request<Texture2D>("TerrariaEpicVerision/UI/Nanako/WelcomeHome"), new Rectangle(0, 0, 1152, 220));
            panel.Append(image);

            Append(panel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
