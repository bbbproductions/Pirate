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
    public class LesserWhirlpool : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.mana = 14;
            Item.damage = 22;
            Item.knockBack = 9f;
            Item.DamageType = DamageClass.Magic;
            Item.noMelee = true;

            Item.shoot = ModContent.ProjectileType<Projectiles.LesserWhirlpoolProjectile>();
            Item.shootSpeed = 10f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(silver: 90);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Book)
                .AddIngredient(ItemID.SharkFin)
                .AddIngredient(ItemID.WaterBucket, 3)
                .AddTile(TileID.Bookcases)
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