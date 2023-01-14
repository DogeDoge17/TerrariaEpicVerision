using IL.Terraria.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Content.Sources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Assets;
using Terraria.ModLoader.Utilities;
using Terraria.UI;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.UI;
using UIImage = TerrariaEpicVerision.UI.UIImage;

namespace TerrariaEpicVerision.NPCs
{
    public abstract class HighResNPC : ModNPC
    {
        public abstract Asset<Texture2D> largeImage { get; }

        public Rectangle source;

        public float scale = 0.05f;

        public bool autoScale = true;

        public bool forceHighRes = false;

        //public bool autoSource = true;

        public override void SetDefaults()
        {
            //if (source == null || autoSource == true)
            //    source = new Rectangle(0, 0, largeImage.Width(), largeImage.Height());

            base.SetDefaults();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (!Config.useLowRes || forceHighRes)
            {
                if (!autoScale)
                    spriteBatch.Draw(((Texture2D)largeImage), NPC.position - screenPos, source, drawColor, 0, new Vector2(0, 0), scale, RandomTools.IntToFlip(NPC.spriteDirection), 0);
                else
                    spriteBatch.Draw(((Texture2D)largeImage), NPC.position - screenPos, source, drawColor, 0, new Vector2(0, 0), (float)(NPC.width + NPC.height) / (float)(source.Width + source.Height), RandomTools.IntToFlip(NPC.spriteDirection), 0);

                // Debug.WriteLine((float)(NPC.width + NPC.height) / (float)(source.Width + source.Height) + " NPC: " + NPC.width + " " + NPC.height + " " + (NPC.width + NPC.height) + " Rect: " + source.Width + " " + source.Height + " " + (source.Width + source.Height));

                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class HighResImage : ModSystem
    {
        private UserInterface MyInterface;

        public static List<ImageUI> imageUIs = new List<ImageUI>();


        public override void Load()
        {
            if (!Main.dedServ)
            {
                MyInterface = new UserInterface();

                //MyUI = new ImageUI();
                //MyUI.Activate(); // Activate calls Initialize() on the UIState if not initialized, then calls OnActivate and then calls Activate on every child element
            }

            base.Load();
        }

        public static void SetNew(ImageUI imgUI)
        {
            imgUI.Activate();
            imageUIs.Add(imgUI);

        }


        public override void Unload()
        {
            //////////////////MyUI?.SomeKindOfUnload(); // If you hold data that needs to be unloaded, call it in OO-fashion
            imageUIs = null;

            base.Unload();
        }


        public override void UpdateUI(GameTime gameTime)
        {

            ShowMyUI();


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
                        if (Time.gameTime != null && MyInterface?.CurrentState != null)
                        {
                            MyInterface.Draw(Main.spriteBatch, Time.gameTime);
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
            foreach (var img in imageUIs)
                MyInterface?.SetState(img);
        }

        internal void HideMyUI()
        {
            MyInterface?.SetState(null);
        }


    }

    public class ImageUI : UIState
    {


        public UIImageFramed image;

        public UIPanel panel;

        public static bool Visible = true;

        Asset<Texture2D> texture;
        Rectangle size;

        public ImageUI(Asset<Texture2D> texture, Rectangle size)
        {
            this.texture = texture;
            this.size = size;
        }

        public override void OnInitialize() // 1
        {
            UIPanel panel = new UIPanel(null, null, 0, 4);
            panel.Width.Set(306, 0); // 3
            panel.Height.Set(816, 0); // 3

            UIImage image = new UIImage(texture, size);
            image.Width = panel.Width;
            image.Height = panel.Height;
            image.SetImage(texture, size);
            panel.Append(image);

            Append(panel); // 4
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

    }
}
