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
    public class ThunderStaff : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 50;
            Item.useAnimation = 25;
            Item.damage = 8;
            Item.knockBack = 4f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 12;
            Item.noMelee = true;
            Item.value = Item.buyPrice(silver: 90);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.ThunderBolt>();
            Item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(ItemID.SilverBar, 8)
                .AddIngredient(ItemID.RainCloud, 10)
                .AddTile(TileID.Anvils)
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
            damage += player.GetModPlayer<PiratePlayer>().pirateDamage;
        }
    }
}