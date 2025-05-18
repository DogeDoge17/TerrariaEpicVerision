using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaEpicVerision.Items;

namespace TerrariaEpicVerision.GlobalNPCs
{
    public class ShopMods : GlobalNPC
    {

        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            int items2Ad = 2;
            int item2Add = 0;

            if (shopName == "AlchemistNPCLite/Brewer/Thorium/RG/Redemption")
            {
                int firstPylon = -1;


                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i] != null)
                    {
                        if (items[i].Name.Contains("Pylon"))
                            firstPylon = i;
                        continue;
                    }

                    switch (items2Ad)
                    {
                        case 1:
                            item2Add = ModContent.ItemType<YestiGravityPotion>();
                            break;
                        case 2:
                            item2Add = ModContent.ItemType<AryanPotion>();
                            break;
                    }
                    items2Ad--;

                    if (firstPylon != -1)
                    {
                        items[i] = items[firstPylon];                     
                        items[firstPylon] = new Item(item2Add);
                                       
                    }
                    else
                        items[i] = new Item(item2Add);

                    if(items2Ad == 0)
                        break;
                    else
                    {
                        firstPylon = items.Select((ITEM, index) => new { ITEM, index }).Where(x => x.ITEM.Name.Contains("Pylon")).Select(x => (int?)x.index).FirstOrDefault() ?? -1;
                    }
                }
            }



            base.ModifyActiveShop(npc, shopName, items);
        }

        public override void ModifyShop(NPCShop shop)
        {


            //if (shop.NpcType == NPCID.Painter)
            //{
            //    shop.Add(ModContent.ItemType<AigisStuffPainting>());
            //    shop.Add(ModContent.ItemType<RiseStuffPainting>());
            //    shop.Add(ModContent.ItemType<MakotoStuffPainting>());
            //}
            //else if (shop.NpcType == NPCID.Painter)
            //{
            //    shop.Add(ModContent.ItemType<AigisStuffPainting>());
            //    shop.Add(ModContent.ItemType<RiseStuffPainting>());
            //    shop.Add(ModContent.ItemType<MakotoStuffPainting>());
            //}
   


            switch (shop.NpcType)
            {
                case NPCID.Painter:
                    shop.Add(ModContent.ItemType<AigisStuffPainting>());
                    shop.Add(ModContent.ItemType<RiseStuffPainting>());
                    shop.Add(ModContent.ItemType<MakotoStuffPainting>());
                    
                    break;
                case NPCID.Merchant:
                    shop.Add(ItemID.CanOfWorms);
                    break;
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
