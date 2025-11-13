using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
    public class Blunderbuss : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 50;
            Item.height = 22;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 45;                
            Item.useAnimation = 45;
            Item.damage = 35;               
            Item.knockBack = 6f;
            Item.DamageType = DamageClass.Ranged;
            Item.noMelee = true;              
            Item.value = Item.buyPrice(gold: 1);
            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item36;    
            Item.autoReuse = false;
            Item.shoot = ProjectileID.Bullet;  
            Item.shootSpeed = 12f;            
            Item.useAmmo = AmmoID.Bullet;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.IronBar, 6)
                .AddIngredient(ModContent.ItemType<GunParts>(), 1)
                .AddTile(TileID.Anvils)
                .Register();
        }

        // Add bullet spread
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source, 
                                   Terraria.Projectile[] projectiles, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 3; 
            float rotation = MathHelper.ToRadians(10); 
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectiles[0].velocity.X, projectiles[0].velocity.Y)
                    .RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1f)));
                Projectile.NewProjectile(source, player.Center, perturbedSpeed, type, damage, knockBack, player.whoAmI);
            }
            return false; 
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