using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Pirate.Content.Projectiles;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
	
	public class WoodenBallistaItem : ModItem
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Type] = true;
			ItemID.Sets.LockOnIgnoresCollision[Type] = true;
		}

		public override void SetDefaults() {
			Item.damage = 15;
			Item.DamageType = DamageClass.Summon;
			Item.sentry = true;
			Item.mana = 10;
			Item.width = 26;
			Item.height = 28;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.noMelee = true;
			Item.knockBack = 3;
			Item.value = Item.buyPrice(gold: 30);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item83;
			Item.shoot = ModContent.ProjectileType<WoodenBallista>();
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("Wood", 20);
			recipe.AddIngredient(ItemID.WoodenArrow, 25);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			bool canPlaceInAir = false;

			position = Main.MouseWorld;
			player.LimitPointToPlayerReachableArea(ref position);
			int halfProjectileHeight = (int)Math.Ceiling(ContentSamples.ProjectilesByType[type].height / 2f);

			if (!canPlaceInAir) {
				player.FindSentryRestingSpot(type, out int worldX, out int worldY, out int pushYUp);
				position = new Vector2(worldX, worldY - halfProjectileHeight);
			}
			else {
				position.Y -= halfProjectileHeight;
			}

			Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, Main.myPlayer, ai2: canPlaceInAir ? 0 : 1);

			player.UpdateMaxTurrets();

			return false;
		}

			public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var lineToChange = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
			if (lineToChange != null)
			{
				string[] split = lineToChange.Text.Split(' ');
				lineToChange.Text = split.First() + " pirate " + split.Last();
			}
		}

		public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
		{
			damage += player.GetModPlayer<PiratePlayer>().pirateDamage;
		}
	}
}