using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Projectiles
{
	// The projectile shot by ExampleSentry.
	// The most important things needed for a projectile spawned by a sentry are:
	//		ProjectileID.Sets.SentryShot and Projectile.DamageType = DamageClass.Summon
	public class TestSentryShot : ModProjectile
	{
		public override void SetStaticDefaults() {
			// It is important that projectiles shot by sentries are in this set to properly work with effects that are triggered by sentry attacks.
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