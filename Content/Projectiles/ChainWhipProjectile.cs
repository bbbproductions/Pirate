using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Projectiles
{
	public class ChainWhipProjectile : ModProjectile
	{
		public override void SetStaticDefaults() {
			// This makes the projectile use whip collision detection and allows flasks to be applied to it.
			ProjectileID.Sets.IsAWhip[Type] = true;
		}

		public override void SetDefaults() {
			Projectile.DefaultToWhip();

			Projectile.WhipSettings.Segments = 20;
			Projectile.WhipSettings.RangeMultiplier = 0.5f;
		}

		private float Timer {
			get => Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}

		private float ChargeTime {
			get => Projectile.ai[1];
			set => Projectile.ai[1] = value;
		}

		// This method draws a line between all points of the whip, in case there's empty space between the sprites.
		private void DrawLine(List<Vector2> list) {
			Texture2D texture = TextureAssets.FishingLine.Value;
			Rectangle frame = texture.Frame();
			Vector2 origin = new Vector2(frame.Width / 2, 2);

			Vector2 pos = list[0];
			for (int i = 0; i < list.Count - 1; i++) {
				Vector2 element = list[i];
				Vector2 diff = list[i + 1] - element;

				float rotation = diff.ToRotation() - MathHelper.PiOver2;
				Color color = Lighting.GetColor(element.ToTileCoordinates(), Color.White);
				Vector2 scale = new Vector2(1, (diff.Length() + 2) / frame.Height);

				Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

				pos += diff;
			}
		}

                public override bool PreDraw(ref Color lightColor) {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);
            Main.DrawWhip_WhipBland(Projectile, list);

            return false;
        }
	}
}