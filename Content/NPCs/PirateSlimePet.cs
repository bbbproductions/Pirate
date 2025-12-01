using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Terraria.GameContent.ItemDropRules;

namespace Pirate.Content.NPCs
{

    public class PirateSlimePet : ModNPC
    {
        private static readonly string[] names = { "Slippy", "Gloop", "Cap'n Goo" };

        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.TownCat];
            NPCID.Sets.IsTownPet[NPC.type] = true;
            NPCID.Sets.ActsLikeTownNPC[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 30;
            NPC.height = 24;
            NPC.friendly = true;
            NPC.townNPC = true;
            NPC.lifeMax = 250;
            NPC.dontTakeDamage = true;
            NPC.aiStyle = 7; 
            AIType = NPCID.TownCat;
            AnimationType = NPCID.TownCat;
        }

        public override void OnSpawn(IEntitySource source)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.GivenName = names[Main.rand.Next(names.Length)];
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.SyncNPC, number: NPC.whoAmI);
            }
        }

        public override bool CanChat() => false; // Pets don't talk
    }
}