using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pirate.Content.Items;

namespace Pirate.Content.Projectiles
{
    public class WoodenBallistaTurret : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.sentry = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.penetrate = -1;
            Projectile.timeLeft = Projectile.SentryLifeTime; // Default sentry duration
            Projectile.friendly = false;         // canNOT hit enemies
            Projectile.hostile = false;         // does NOT hurt player
            Projectile.tileCollide = true;      // collides with world tiles
            Projectile.penetrate = -1;          // lasts until destroyed
            Projectile.damage = 0;              // <- contact damage is 0
            Projectile.DamageType = DamageClass.Summon; // damage from arrows, not touch
        }

        public override void AI()
        {

            Player player = Main.player[Projectile.owner];
            player.UpdateMaxTurrets();   // <- THIS MAKES IT COUNT

            // Stay still
            Projectile.velocity = Vector2.Zero;

            // Target nearest enemy
            NPC target = null;
            float distance = 600f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this))
                {
                    float d = Vector2.Distance(npc.Center, Projectile.Center);
                    if (d < distance)
                    {
                        distance = d;
                        target = npc;
                    }
                }
            }

            // No target = no shoot
            if (target == null)
                return;

            // Fire every 45 ticks (~0.75 seconds)
            Projectile.ai[0]++;
            if (Projectile.ai[0] >= 45)
            {
                Projectile.ai[0] = 0;

                // Bad accuracy (±15 degrees)
                float spread = MathHelper.ToRadians(15);
                Vector2 shootDir = Projectile.DirectionTo(target.Center)
                    .RotatedBy(Main.rand.NextFloat(-spread, spread));

                // Fire wooden arrow projectile
                Projectile.NewProjectile(
                    Projectile.GetSource_FromAI(),
                    Projectile.Center,
                    shootDir * 12f,
                    ProjectileID.WoodenArrowFriendly,
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner
                );
            }
        }
    }
}