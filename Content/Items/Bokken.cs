using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
	public class Bokken : ModItem //A japanese wooden sword used for training
	{
		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 50;
			Item.useTime = 15;
			Item.useAnimation = 15;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8;
			Item.value = Item.buyPrice(copper: 20);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item1;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddRecipeGroup("Wood", 7);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
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

//Sprite: needs polishing as well as a bit wider