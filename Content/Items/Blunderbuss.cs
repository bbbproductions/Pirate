using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Pirate.Content.Materials;

namespace Pirate.Content.Items
{
    public class Blunderbuss : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 62;
            Item.height = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 45;
            Item.useAnimation = 45;
            Item.damage = 35;
            Item.knockBack = 6f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = false;
            Item.shoot = ProjectileID.Bullet;
            Item.shootSpeed = 12f;
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("IronBar", 6);
            recipe.AddIngredient(ModContent.ItemType<LegalGunParts>(), 1)
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }

        // Add bullet spread
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
                           Vector2 position, Vector2 velocity, int type, int damage, float knockBack)
        {
            int numberProjectiles = 3; // spread of 3 bullets
            float rotation = MathHelper.ToRadians(10); // 10 degree spread
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f)));
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockBack, player.whoAmI);
            }
            return false; // prevents default single bullet
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