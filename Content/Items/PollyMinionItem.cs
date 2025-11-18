using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Pirate.Content.Buffs;
using Pirate.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace Pirate.Content.Items
{
    public class PollyMinionItem : ModItem
	{
		public override void SetStaticDefaults() {
			ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; // This lets the player target anywhere on the whole screen while using a controller
			ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

			ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; // The default value is 1, but other values are supported. See the docs for more guidance. 
		}

		public override void SetDefaults() {
			Item.damage = 8;
			Item.knockBack = 5f;
			Item.mana = 12; // mana cost
			Item.width = 32;
			Item.height = 32;
			Item.useTime = 36;
			Item.useAnimation = 36;
			Item.useStyle = ItemUseStyleID.Swing; // how the player's arm moves when using the item
			Item.value = Item.sellPrice(copper: 80);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item44; // What sound should play when using the item

			// These below are needed for a minion weapon
			Item.noMelee = true; // this item doesn't do any melee damage
			Item.DamageType = DamageClass.Summon; // Makes the damage register as summon. If your item does not have any damage type, it becomes true damage (which means that damage scalars will not affect it). Be sure to have a damage type
			Item.buffType = ModContent.BuffType<PollyMinionBuff>();
			// No buffTime because otherwise the item tooltip would say something like "1 minute duration"
			Item.shoot = ModContent.ProjectileType<PollyMinionProjectile>(); // This item creates the minion projectile
		}

		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback) {
			// Here you can change where the minion is spawned. Most vanilla minions spawn at the cursor position, limited by the gameplay range
			position = Main.MouseWorld;
			player.LimitPointToPlayerReachableArea(ref position);
		}

		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			// This is needed so the buff that keeps your minion alive and allows you to despawn it properly applies
			player.AddBuff(Item.buffType, 2);

			return true; // The minion projectile will be spawned by the game since we return true.
		}

		public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Feather, 8)
                .AddTile(TileID.WorkBenches)
                .Register();
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
			damage += player.GetModPlayer<GlobalPlayer>().pirateDamage;
		}
	}
}