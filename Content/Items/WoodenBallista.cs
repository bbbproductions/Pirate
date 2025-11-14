using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Pirate.Content.Projectiles;

namespace Pirate.Content.Items
{
    public class WoodenBallista : ModItem
    {
       public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.noMelee = true;

            Item.damage = 12;
            Item.knockBack = 3f;
            Item.DamageType = DamageClass.Summon;
            Item.mana = 0;

            Item.rare = ItemRarityID.White;
            Item.value = Item.buyPrice(silver: 20);

            Item.UseSound = SoundID.Item44;

            Item.shoot = ModContent.ProjectileType<WoodenBallistaTurret>();
            Item.shootSpeed = 0f;

            Item.sentry = true;   // *** IMPORTANT! ***
        }
       
       public override bool? UseItem(Player player)
{
    Vector2 spawnPos = Main.MouseWorld;
    int projWidth = 32;
    int projHeight = 14;

    // Start a little above the mouse
    spawnPos.Y -= projHeight;

    // Cast downward to find the first **standable solid tile**
    bool foundGround = false;
    for (int i = 0; i < 50; i++) // check max 50 tiles down
    {
        int tileX = (int)((spawnPos.X + projWidth / 2) / 16f);
        int tileY = (int)((spawnPos.Y + projHeight) / 16f);

        if (tileY >= Main.maxTilesY) break;

        Tile tile = Main.tile[tileX, tileY];

        // Only solid tiles, NOT platforms (tileSolidTop), and tiles that players can stand on
        if (tile.HasTile && Main.tileSolid[tile.TileType] && !Main.tileSolidTop[tile.TileType])
        {
            // Snap projectile so its bottom sits **on top of this tile**
            spawnPos.Y = tileY * 16f - projHeight;
            foundGround = true;
            break;
        }

        spawnPos.Y += 1f; // move down 1 pixel at a time
    }

    if (!foundGround)
        return false; // no valid ground, cancel spawn

    // Spawn the sentry projectile
    Projectile.NewProjectile(
        player.GetSource_ItemUse(Item),
        spawnPos,
        Vector2.Zero,
        ModContent.ProjectileType<WoodenBallistaTurret>(),
        Item.damage,
        Item.knockBack,
        player.whoAmI
    );

    return true;
}

    public override bool Shoot(Player player,
        Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
        Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        return false; // IMPORTANT: this prevents default spawning
    }

       

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 20)
                .AddIngredient(ItemID.WoodenArrow, 25)
                .AddTile(TileID.WorkBenches)
                .Register();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			var lineToChange = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
			if(lineToChange != null)
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