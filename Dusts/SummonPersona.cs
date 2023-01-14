using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;


namespace TerrariaEpicVerision.Dusts
{
    public class SummonPersona : ModDust
    {
        public override void SetStaticDefaults()
        {
			//UpdateType = 110;
		    
			base.SetStaticDefaults();
        }

        public override void OnSpawn(Dust dust)
		{
			dust.noGravity = true;

			// Since the vanilla dust texture has all the dust in 1 file, we'll need to do some math.
			// If you want to use a vanilla dust texture, you can copy and paste it, changing the desiredVanillaDustTexture

 
			dust.frame = new Rectangle(0, 0, 157, 89);

			//dust.velocity = Vector2.Zero;
		}

		// This Update method shows off some interesting movement. Using customData assigned to a Player, we spiral around the Player while slowly getting closer. In practice, it looks like a vortex.
		public override bool Update(Dust dust)
		{


			if (dust.alpha == 1)
			{
				dust.rotation = 3.14159f;

				//dust.position = new Vector2(dust.position.X -.001f, dust.position.Y);
			}
			// Move the dust based on its velocity and reduce its size to then remove it, as the 'return false;' at the end will prevent vanilla logic.
			dust.position += dust.velocity;
			dust.scale -= 0.01f;

			if (dust.scale < 0.003f)
				dust.active = false;

			return false;
		}
	}
}
