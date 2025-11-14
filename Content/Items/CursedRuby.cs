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
    public class CursedRuby : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.mana = 12;
            Item.damage = 20;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<Projectiles.CursedRubyBolt>();
            Item.shootSpeed = 12f;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.buyPrice(silver: 50);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Ruby, 1)
                .AddRecipeGroup(ItemID.SilverBar, 5)
                .AddTile(TileID.DemonAltar)
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