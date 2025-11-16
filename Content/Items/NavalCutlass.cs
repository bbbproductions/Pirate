using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System.Linq;

namespace Pirate.Content.Items
{
    public class NavalCutlass : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 46;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.damage = 20;
            Item.knockBack = 5.5f;
            Item.scale = 1.1f;
            Item.DamageType = DamageClass.Melee;
            Item.value = Item.buyPrice(silver: 50);
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item1;
        }

        public class NavalCutlassGlobalNPC : GlobalNPC
        {
            public override void ModifyShop(NPCShop shop)
            {
                if (shop.NpcType == NPCID.Merchant)
                {
                    shop.Add(new Item(ModContent.ItemType<NavalCutlass>()));

                }
            }
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
            damage += player.GetModPlayer<GlobalPlayer>().pirateDamage;
        }
    }
}
//Sprite: needs to have the png size reajusted as well as some polishing