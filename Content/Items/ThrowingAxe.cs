using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
    public class ThrowingAxe : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Swing;   
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.damage = 14;                     
            Item.knockBack = 5f;                    
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;                     
            Item.consumable = true;                 
            Item.maxStack = 999;                   
            Item.shoot = ModContent.ProjectileType<Projectiles.ThrowingAxeProjectile>();
            Item.shootSpeed = 9f;                   
            Item.value = Item.buyPrice(silver: 15);
            Item.rare = ItemRarityID.White;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)                           
                .AddRecipeGroup(RecipeGroupID.IronBar, 2) 
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