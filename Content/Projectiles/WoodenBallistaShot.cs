using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Projectiles
{
	public class WoodenBallistaBolt : ModProjectile
	{
		public override void SetStaticDefaults() {
			ProjectileID.Sets.SentryShot[Type] = true;
		}

		public override void SetDefaults() {
			Projectile.width = 16;
			Projectile.height = 16;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.timeLeft = 600;
		}
	}
}