using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Projectiles
{
    public class ThrowingAxeProjectile : ModProjectile
    {

        public override void SetDefaults()
        {
            Projectile.width = 28;
            Projectile.height = 24;
            Projectile.aiStyle = 2;            
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.penetrate = 1;           
            Projectile.timeLeft = 300;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;
        }
    }
}