using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pirate.Content.Projectiles
{
    public class LesserWhirlpoolProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 30;
            Projectile.height = 30;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.aiStyle = 0;
            Projectile.penetrate = 3;    // pierces 3 enemies
            Projectile.timeLeft = 180;   // 3 seconds
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            // Spin like a whirlpool
            Projectile.rotation += 0.25f * Projectile.direction;

            // Slow down gradually (drag)
            Projectile.velocity *= 0.97f;

            // Water dust
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Water);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].scale = 1.2f;

            // Add blue water glow
            Lighting.AddLight(Projectile.Center, 0f, 0.4f, 0.6f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // EXTRA knockback (simulates whirlpool push)
            target.velocity += Projectile.DirectionTo(target.Center) * 7f;
        }
    }
}