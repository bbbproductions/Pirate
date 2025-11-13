using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
    public class WroughtCutter : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 25;
            Item.useAnimation = 25;
            Item.damage = 12;                
            Item.knockBack = 5.5f;            
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.buyPrice(silver: 20);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Wood, 10)
                .AddRecipeGroup(RecipeGroupID.IronBar, 6) 
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