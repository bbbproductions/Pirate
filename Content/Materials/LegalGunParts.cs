using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Materials
{
    public class LegalGunParts : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.maxStack = 999;
            Item.value = Item.buyPrice(silver: 10);
            Item.rare = ItemRarityID.White;
            Item.useStyle = ItemUseStyleID.Swing; 
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.consumable = false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddRecipeGroup(RecipeGroupID.IronBar, 5) 
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}