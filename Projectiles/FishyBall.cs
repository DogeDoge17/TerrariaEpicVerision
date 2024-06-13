using System;
using System.Diagnostics;
using Microsoft.Build.Execution;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Chat.Commands;
using Terraria.ID;
using Terraria.ModLoader;

namespace TerrariaEpicVerision.Projectiles
{
    public class FishyBall : ModProjectile, ILocalizedModType
    {
        public new string LocalizationCategory => "Projectiles.Magic";

        public const int DoubleDamageTime = 90;
        public ref float BeamLength => ref Projectile.localAI[0];

        public bool mainBeam => Projectile.ai[0] == 0f;

        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.alpha = 255;
            Projectile.penetrate = 1;
            Projectile.MaxUpdates = 6;
            Projectile.timeLeft = 30 * Projectile.MaxUpdates; 
        }

        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            int life = (int)Math.Floor(target.lifeMax / 4.1698113208);
            if (life < target.lifeMax)
                target.life = life;
            else
                target.life = 70000;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) => target.AddBuff(BuffID.Wet, 600);
    }
}