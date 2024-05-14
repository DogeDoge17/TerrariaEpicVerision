using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaEpicVerision.Items.Aigis
{
    public class GunBarrel : ModItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("balls"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.
            //Tooltip.SetDefault("Is this a " + '"' + "The Binding of Isaac" + '"' + " reference??");

        }

        
        public override void SetDefaults()
        {
            //Item.DefaultToRangedWeapon(ProjectileID.GelBalloon,)         
            Item.width = 30;
            Item.height = 30;
            Item.value = 10000;
            Item.maxStack = 999;
            Item.rare = ItemRarityID.Gray;

        }
    }
}
