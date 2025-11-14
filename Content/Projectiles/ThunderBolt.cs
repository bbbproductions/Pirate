using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Pirate.Content.Projectiles
{
    public class ThunderBolt : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 6;       // Thin beam
            Projectile.height = 6;
            Projectile.aiStyle = 0;    // Custom
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 5; // Infinite until time runs out
            Projectile.timeLeft = 60; // 3 seconds (60 ticks = 1 sec)
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.alpha = 255;     // Start invisible, we’ll fade in
        }

       public override void AI()
{
    // Add some blue-white lighting
    Lighting.AddLight(Projectile.Center, 0.3f, 0.5f, 1f);

    // Direction of the beam
    Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.UnitX);

    // Check ahead for solid tiles, stop if hit
    float maxDistance = 480f; // 30 blocks × 16 pixels
    float step = 8f;
    for (float i = 0; i <= maxDistance; i += step)
    {
        Vector2 checkPos = Projectile.Center + direction * i;
        if (Collision.SolidCollision(checkPos, 8, 8))
        {
            // Stop at collision
            Projectile.position = checkPos - direction * 8f;
            Projectile.Kill();
            return;
        }

        // Sparkly lightning dust along the beam
        if (Main.rand.NextBool(10))
        {
            int dust = Dust.NewDust(checkPos, 2, 2, DustID.Electric, 0f, 0f, 100, default, 1.2f);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity = Vector2.Zero;
        }
    }

    // Move the beam forward
    Projectile.position += Projectile.velocity;

    // Damage enemies in a straight path
    // Rectangle beamHitbox = new Rectangle(
    //     (int)Projectile.Center.X - 4,
    //     (int)Projectile.Center.Y - 4,
    //     480,
    //     8);

    // for (int i = 0; i < Main.maxNPCs; i++)
    // {
    //     NPC npc = Main.npc[i];
    //     if (npc.active && !npc.friendly && !npc.dontTakeDamage)//&& npc.Hitbox.Intersects(beamHitbox)
    //     {
    //         NPC.HitInfo hitInfo = new NPC.HitInfo
    //         {
    //             Damage = Projectile.damage,
    //             Knockback = Projectile.knockBack,
    //             HitDirection = Projectile.direction,
    //             Crit = false
    //         };

    //         npc.StrikeNPC(hitInfo);
    //     }
    // }
}
    }
}