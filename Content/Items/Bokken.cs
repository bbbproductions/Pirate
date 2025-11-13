using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Items
{
	public class Bokken : ModItem //A japanese wooden sword used for training
	{
		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Melee;
			Item.width = 25;
			Item.height = 40;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(copper: 20);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
		}

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Bokken");
			Tooltip.SetDefault("A japanese wooden sword used for training\n Why don’t pirates fight with bokkens at sea? Because they don’t want to board the wrong ship!);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.Wood, 7);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}