using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaEpicVerision.Items;

namespace TerrariaEpicVerision.GlobalNPCs
{
    public class PaintingShop : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Painter)
            {
                shop.Add(ModContent.ItemType<AigisStuffPainting>());
                shop.Add(ModContent.ItemType<RiseStuffPainting>());
                shop.Add(ModContent.ItemType<MakotoStuffPainting>());
                //shop.item[nextSlot].SetDefaults(ModContent.ItemType<AigisStuffPainting>());
                //shop.item[nextSlot].shopCustomPrice = 40000;
                //nextSlot++;

                //shop.item[nextSlot].SetDefaults(ModContent.ItemType<RiseStuffPainting>());
                //shop.item[nextSlot].shopCustomPrice = 40000;
                //nextSlot++;

                //shop.item[nextSlot].SetDefaults(ModContent.ItemType<MakotoStuffPainting>());
                //shop.item[nextSlot].shopCustomPrice = 40000;
                //nextSlot++;

                // We can use shopCustomPrice and shopSpecialCurrency to support custom prices and currency. Usually a shop sells an item for item.value. 
                // Editing item.value in SetupShop is an incorrect approach.
            }
            base.ModifyShop(shop);
        }
       
        //public override void SetupShop(int type, Chest shop, ref int nextSlot)
        //{
        //    if (type == NPCID.Painter)
        //    {
        //        shop.item[nextSlot].SetDefaults(ModContent.ItemType<AigisStuffPainting>());
        //        shop.item[nextSlot].shopCustomPrice = 40000;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ModContent.ItemType<RiseStuffPainting>());
        //        shop.item[nextSlot].shopCustomPrice = 40000;
        //        nextSlot++;

        //        shop.item[nextSlot].SetDefaults(ModContent.ItemType<MakotoStuffPainting>());
        //        shop.item[nextSlot].shopCustomPrice = 40000;
        //        nextSlot++;

        //        // We can use shopCustomPrice and shopSpecialCurrency to support custom prices and currency. Usually a shop sells an item for item.value. 
        //        // Editing item.value in SetupShop is an incorrect approach.



        //    }
        //}
    }
}
