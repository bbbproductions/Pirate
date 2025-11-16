using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;
using Pirate.Content.Items;

namespace Pirate.Content.Items
{
    public class SturdyCrossbow : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.damage = 28;
            Item.knockBack = 3.5f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;
            Item.value = Item.buyPrice(silver: 90);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shoot = ProjectileID.WoodenArrowFriendly;
            Item.shootSpeed = 9f;
            Item.useAmmo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.GoldBar, 10)
                .AddIngredient(ModContent.ItemType<FlimsyCrossbow>())
                .AddTile(TileID.Anvils)
                .Register();

            CreateRecipe()
                .AddIngredient(ItemID.PlatinumBar, 10)
                .AddIngredient(ModContent.ItemType<FlimsyCrossbow>())
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
            damage += player.GetModPlayer<GlobalPlayer>().pirateDamage;
        }
    }
}