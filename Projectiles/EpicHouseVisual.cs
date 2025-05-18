using Microsoft.Xna.Framework;
using Newtonsoft.Json.Linq;
using System;
using Terraria;
using Terraria.ModLoader;
using TerrariaEpicVerision.Items;

namespace TerrariaEpicVerision.Projectiles
{
    public class EpicHouseVisual : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 160;
            Projectile.height = 96;
            Projectile.timeLeft = 10;
            Projectile.tileCollide = false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 mouse = Main.MouseWorld;

            if (player.position.X > mouse.X)
            {
                Projectile.position.X = ((((int)Math.Round(mouse.X) - Projectile.width - 24) + 16 / 2) / 16) * 16; //mouse.X - Projectile.width+8;
            } 
            else
            {
                Projectile.position.X = ((((int)Math.Round(mouse.X) - 40) + 16 / 2) / 16) * 16; //mouse.X - 32;
            }
            Projectile.position.Y = ((((int)Math.Round(mouse.Y) - Projectile.height + 8 + 16) + 16 / 2) / 16) * 16;//mouse.Y - Projectile.height + 8 + 16;

            Projectile.timeLeft++;

            if (player.HeldItem.type != ModContent.ItemType<EpicHouse>())
            {
                Projectile.Kill();
            }

            Projectile.hide = Projectile.owner != Main.myPlayer;
        }
    }
}