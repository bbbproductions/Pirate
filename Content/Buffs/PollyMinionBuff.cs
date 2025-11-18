using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Pirate.Content.Items;
using Pirate.Content.Projectiles;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System;

namespace Pirate.Content.Buffs
{
    public class PollyMinionBuff : ModBuff
       {
        public override void SetStaticDefaults() {
            Main.buffNoSave[Type] = true; // This buff won't save when you exit the world
            Main.buffNoTimeDisplay[Type] = true; // The time remaining won't display on this buff
        }

         public override void Update(Player player, ref int buffIndex) {
               // If the minions exist reset the buff time, otherwise remove the buff from the player
              if (player.ownedProjectileCounts[ModContent.ProjectileType<PollyMinionProjectile>()] > 0) {
                 player.buffTime[buffIndex] = 18000;
             }
             else {
                 player.DelBuff(buffIndex);
                buffIndex--;
                }
        }
    }
}