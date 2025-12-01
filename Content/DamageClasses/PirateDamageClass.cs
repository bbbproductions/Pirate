// using Terraria;
// using Terraria.ModLoader;

// namespace Pirate.Content.DamageClasses
// {
//     public class PirateDamage : DamageClass
// {
//     public override void SetStaticDefaults()
//     {
//         // This makes your damage behave more like a class with its own stats
//         ClassName.SetDefault("pirate damage");
//     }

//     // These define how your class interacts with vanilla stats

//     public override StatModifier GetDamageModifier(Player player)
//     {
//         return player.GetModPlayer<PiratePlayer>().pirateDamage;
//     }

//     public override StatModifier GetKnockbackModifier(Player player)
//     {
//         return player.GetModPlayer<PiratePlayer>().pirateKnockback;
//     }

//     public override StatModifier GetCritChance(Player player)
//     {
//         return player.GetModPlayer<PiratePlayer>().pirateCrit;
//     }
// }
// }