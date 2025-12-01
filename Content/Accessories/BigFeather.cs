using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Accessories
{
    public class BigFeather : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddRecipeGroup("Wood", 10); // Add Recipe group will add any type of wood.
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.15f;
            player.GetModPlayer<PiratePlayer>().dashFeather = true;
            player.GetModPlayer<PiratePlayer>().dashAccessoryEquipped = true;
        }
    }
}