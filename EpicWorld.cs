using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.UI;
using TerrariaEpicVerision.NPCs.TownNPC;

namespace TerrariaEpicVerision
{
    public class EpicWorld : ModSystem
    {
        internal CatSelectionUI genieUI;
        private UserInterface genieUserInterface;
        public static bool genieSpawned = false;

        public override void OnWorldLoad()
        {
            if (!Main.dedServ)
            {
                genieUI = new CatSelectionUI();
                genieUI.Activate();
                genieUserInterface = new UserInterface();
                genieUserInterface.SetState(genieUI);
            }
        }

        public override void SaveWorldData(TagCompound tag)
        {
            var downed = new List<string>();
            if (genieSpawned) downed.Add("catGenie");            
            tag["downed"] = downed;
        }

        public override void NetSend(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = genieSpawned;           
            writer.Write(flags);
        }

        public override void NetReceive(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            genieSpawned = flags[0];
            // As mentioned in NetSend, BitBytes can contain 8 values. If you have more, be sure to read the additional data:
            // BitsByte flags2 = reader.ReadByte();
            // downed9thBoss = flags[0];
        }

        public override void LoadWorldData(TagCompound tag)
        {
            var downed = tag.GetList<string>("downed");
            genieSpawned = downed.Contains("catGenie");
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int MouseTextIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Mouse Text"));
            if (MouseTextIndex != -1)
            {
                layers.Insert(MouseTextIndex, new LegacyGameInterfaceLayer(
                    "TerrariaEpicVerision: Item Selector",
                    delegate
                    {
                        if (CatSelectionUI.visible)
                        {
                            genieUI.Draw(Main.spriteBatch);
                        }
                        return true;
                    },
                    InterfaceScaleType.UI)
                );
            }
        }

        public override void UpdateUI(GameTime gameTime)
       {
            if (genieUserInterface != null && CatSelectionUI.visible)
            {
                genieUserInterface.Update(gameTime);
            }           
        }
    }
}
