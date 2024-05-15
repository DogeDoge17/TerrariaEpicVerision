using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace TerrariaEpicVerision.Items
{
    internal class Opium : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("balls"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            //Tooltip.SetDefault("Is this a " + '"' + "The Binding of Isaac" + '"' + " reference??");
        }

        public override void SetDefaults()
        {            
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 75;
            Item.useStyle = ItemUseStyleID.DrinkLong;
            Item.useTurn = true;
            Item.value = 1000;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.NPCDeath6;

            Item.consumable = true;
            Item.maxStack = Item.CommonMaxStack;
            Item.potion = true;
            Item.healLife = 400;
        }       
       
        public override void OnConsumeItem(Player player)
        {
           // player.AddBuff(BuffID.PotionSickness, 700);
            player.AddBuff(BuffID.Confused, 700);
            player.AddBuff(BuffID.Weak, 30);
            player.AddBuff(BuffID.BrokenArmor, 700);
            player.AddBuff(BuffID.Bleeding, 700);
            player.AddBuff(BuffID.Slow, 60);
            player.AddBuff(BuffID.Darkness, 700);
           // player.Heal(400);

            for (int d = 0; d < 3; d++)
            {

                //static int 	NewGore (IEntitySource source, Vector2 Position, Vector2 Velocity, int Type, float Scale=1f) 	
                //  Gore.NewGore(NPC.position, NPC.width, NPC.height, 10, 0f, 0f, 20, Color.Red, 1.5f);
                Gore.NewGore(null, new Vector2(player.HandPosition.Value.X, player.HandPosition.Value.Y), new Vector2(), GoreID.Smoke1, .5f);
            }            
        }

    }
}
