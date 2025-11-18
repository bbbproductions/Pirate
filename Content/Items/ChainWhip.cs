using Pirate.Content.Buffs;
using Pirate.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items;

	public class ChainWhip : ModItem
	{
		public override string Texture => "Pirate/Content/Items/ChainWhip";

		public override void SetDefaults() {
			// Call this method to quickly set some of the properties below.
			//Item.DefaultToWhip(ModContent.ProjectileType<ExampleWhipProjectileAdvanced>(), 20, 2, 4);

			Item.DamageType = DamageClass.SummonMeleeSpeed;
			Item.damage = 16;
			Item.knockBack = 1.7f;
			Item.rare = ItemRarityID.White;

			Item.shoot = ModContent.ProjectileType<ChainWhipProjectile>();
			Item.shootSpeed = 7;

			Item.useStyle = ItemUseStyleID.Swing;
			Item.useTime = 30;
			Item.useAnimation = 30;
			Item.UseSound = SoundID.Item152;
			Item.noMelee = true;
			Item.noUseGraphic = true;
		}

		public override void AddRecipes() {
            {    Recipe recipe = CreateRecipe();
                recipe.AddRecipeGroup(RecipeGroupID.IronBar, 10);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
		}

		// Makes the whip receive melee prefixes
		public override bool MeleePrefix() {
			return true;
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