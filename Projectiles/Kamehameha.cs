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
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.GameContent.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using TerrariaEpicVerision.Dusts;
using TerrariaEpicVerision.Gores;
using TerrariaEpicVerision.Items;
using TerrariaEpicVerision.NPCs.Enemy.Persona;

namespace TerrariaEpicVerision.Projectiles
{
    public class Kamehameha : ModProjectile
    {
        //public override string Texture => "Terraria/Projectile_" + ProjectileID.LastPrism;


        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Kamehameha"); // By default, capitalization in classnames will add spaces to the display name. You can customize the display name here by uncommenting this line.

        }

        public override void SetDefaults()
        {
            Projectile.width = 64;
            Projectile.height = 64;
            Projectile.timeLeft = 300;
            Projectile.penetrate = -1;
            Projectile.hostile = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.damage = 100;

            Projectile.frame = 2;
        }

        public override void AI()
        {


            base.AI();
        }


        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float point = 0f;
            Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
            return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Projectile.Center, endPoint, 4f, ref point);
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D projectileTexture = TextureAssets.Projectile[Projectile.type].Value;

            //Vector2 endPoint = Main.npc[(int)Projectile.ai[1]].Center;
            //Vector2 unit = endPoint - Projectile.Center;
            //float length = unit.Length();

            //Console.WriteLine("endPoint: " + endPoint.X + " " + endPoint.Y + " unit: " + unit.X + " " + unit.Y + " length: " + length);

            //unit.Normalize();
            //for (float k = 0; k <= 10; k++)
            //{
            //    Vector2 drawPos = Projectile.Center + unit * k - Main.screenPosition;
            if (Projectile.ai[0] == -1)
                Main.spriteBatch.Draw(projectileTexture, Projectile.position, null, new Color(), 0, new Vector2(2, 2), 1f, SpriteEffects.None, 0f);
            else
                Main.spriteBatch.Draw(projectileTexture, Projectile.position, new Rectangle(0, 64, 64, 64), new Color(), 0, new Vector2(2, 2), 1f, SpriteEffects.FlipHorizontally, 0f);

            //}
            return false;
        }


    }
}
