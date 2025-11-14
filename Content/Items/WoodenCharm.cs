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
    public class WoodenCharm : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 24;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 6;
            Item.knockBack = 2f;
            Item.DamageType = DamageClass.Magic;
            Item.mana = 2;
            Item.noMelee = true;
            Item.value = Item.buyPrice(copper: 75);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item20;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.WoodenCharmBolt>();
            Item.shootSpeed = 8f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 12)
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