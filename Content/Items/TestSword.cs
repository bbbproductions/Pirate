using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Items
{
	public class TestSword : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = 40; // Lower damage than BeanSword
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 10; // Increased knockback
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("Wood", 10) // Different recipe for flavor
			recipe.AddIngredient(ItemID.Feather, 1); // Adds a thematic "wind" element
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}