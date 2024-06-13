using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent;
using Terraria;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace TerrariaEpicVerision.NPCs.TownNPC
{
    [AutoloadHead]
    public class Drake : HighResNPC
    {
        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/DrakeHIGHRES");
        public override Asset<Texture2D> shimmerLargeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/DrakeHIGHRES_Shimmer");
        public override Asset<Texture2D> shimmerTransformLargeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/DrakeHIGHRES_Shimmering");

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ShimmerTownTransform[NPC.type] = true;
            NPCID.Sets.ShimmerTownTransform[Type] = true;

            NPC.Happiness.SetBiomeAffection<JungleBiome>(AffectionLevel.Like);
            NPC.Happiness.SetBiomeAffection<SnowBiome>(AffectionLevel.Dislike);
            NPC.Happiness.SetBiomeAffection<DesertBiome>(AffectionLevel.Hate);

            NPC.Happiness.SetNPCAffection(NPCID.Princess, AffectionLevel.Love);
            NPC.Happiness.SetNPCAffection(NPCID.Angler, AffectionLevel.Like);
            NPC.Happiness.SetNPCAffection(NPCID.Clothier, AffectionLevel.Dislike);
            NPC.Happiness.SetNPCAffection(NPCID.Merchant, AffectionLevel.Dislike);
            NPC.Happiness.SetNPCAffection(NPCID.DD2Bartender, AffectionLevel.Hate);
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 28;
            NPC.height = 48;
            NPC.aiStyle = 7;
            NPC.damage = 10;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;

            source = new Rectangle(0, 0, 172, 296);

        }

        public override bool CanTownNPCSpawn(int numTownNPCs) => Main.npc.Any(npc => npc.type == NPCID.Princess) | Main.npc.Any(npc => npc.type == NPCID.Angler);
       
        public override bool CanGoToStatue(bool toKingStatue) => true;
        public override List<string> SetNPCNameList() => new List<string>() {"Drake", "Drizzy", "Aubrey", "Mr OVOXO", "Champagne Papi" };

        private static string PapiChat(string key, params object[] args) => Language.GetTextValue($"Mods.TerrariaEpicVerision.NPCs.LumberJack.Chat.{key}", args);
    }

    public class DrakeProfile : ITownNPCProfile
    {
        public int RollVariation() => 0;
        public string GetNameForVariant(NPC npc) => npc.getNewNPCName();

        public Asset<Texture2D> GetTextureNPCShouldUse(NPC npc)
        {
            if (npc.IsABestiaryIconDummy)
                return ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/Drake");

            if (npc.IsShimmerVariant)
            {
                return ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/Drake_Shimmer");
            }

            

            return ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/Drake");
        }

        public int GetHeadTextureIndex(NPC npc) => ModContent.GetModHeadSlot("TerrariaEpicVerision/NPCs/TownNPC/Drake_Head");
    }
}
