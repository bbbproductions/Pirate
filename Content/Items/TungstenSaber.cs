using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
    public class TungstenSaber : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 42;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 18;
            Item.knockBack = 4.5f;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.buyPrice(silver: 60);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 15)
                .AddIngredient(ItemID.TungstenBar, 7)
                .AddTile(TileID.Anvils)
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