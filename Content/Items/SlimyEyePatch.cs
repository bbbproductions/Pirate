using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Pirate.Content.Items
{

    public class SlimyEyePatch : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 0;
            Item.rare = ItemRarityID.Blue;
            Item.vanity = true;
            Item.accessory = true;
        }
    }
}