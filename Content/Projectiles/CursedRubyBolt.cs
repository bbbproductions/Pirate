using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pirate.Content.Projectiles
{
    public class CursedRubyBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.aiStyle = 1; // simple bullet AI
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        public override void AI()
        {
            // Red glow
            Lighting.AddLight(Projectile.Center, 0.8f, 0.1f, 0.1f);

            // Red dust trail
            int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch);
            Main.dust[dust].noGravity = true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            // Burn for 4 seconds
            target.AddBuff(BuffID.OnFire, 240);
        }
    }
}