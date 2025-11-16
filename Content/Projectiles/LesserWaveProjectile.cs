using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pirate.Content.Projectiles
{
    public class LesserWaveProjectile : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 48;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 60;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
        }

        public override void AI()
        {
            // Give it a watery blue effect
            Lighting.AddLight(Projectile.Center, 0f, 0.3f, 0.8f);

            // Move like a wave
            Projectile.position += new Vector2(Projectile.velocity.X * 0.5f, 0f);

            // Flip the sprite depending on the direction it's moving
            if (Projectile.velocity.X > 0)
            {
                Projectile.spriteDirection = 1; // Faces right
            }
            else if (Projectile.velocity.X < 0)
            {
                Projectile.spriteDirection = -1; // Faces left
            }

            // Optional: rotate slightly with movement
            Projectile.rotation = Projectile.velocity.X * 0.05f;
        }
    }
}