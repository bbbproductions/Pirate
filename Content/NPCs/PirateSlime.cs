using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.GameContent.ItemDropRules;
using Pirate.Content.Items;

namespace Pirate.Content.NPCs
{

    public class PirateSlime : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
        }

        public override void SetDefaults()
        {
            NPC.width = 32;
            NPC.height = 24;
            NPC.aiStyle = 1;
            AIType = NPCID.BlueSlime;
            AnimationType = NPCID.BlueSlime; 

            // Purple slime stats
            NPC.damage = 18;
            NPC.defense = 10;
            NPC.lifeMax = 90;
            NPC.knockBackResist = 0.9f;

            NPC.value = 50f;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
        }

            public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            // Ocean pre-hardmode only
            if (!Main.hardMode && spawnInfo.Player.ZoneBeach)
                return 0.15f; // Similar to Purple Slime rarity

            return 0f;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<SlimyEyePatch>(), 3)); 
            // 1/3 chance
        }

       public override void AI()
{
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];

                if (!proj.active)
                    continue;

                // Check if this is a gold coin
                if (proj.type != ProjectileID.GoldCoin)
                    continue;

                // Check collision with NPC
                if (proj.Hitbox.Intersects(NPC.Hitbox))
                {
                    TurnIntoTownSlime();
                    proj.Kill(); // remove the coin
                }
            }
        }

        private void TurnIntoTownSlime()
        {
            int newNPC = NPC.NewNPC(NPC.GetSource_Death(), (int)NPC.Center.X, (int)NPC.Center.Y,
                ModContent.NPCType<PirateSlimePet>());

            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.SyncNPC, number: newNPC);
            }

            NPC.active = false;
        }
    }
}