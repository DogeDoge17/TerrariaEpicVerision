using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using UIImage = TerrariaEpicVerision.UI.UIImage;

namespace TerrariaEpicVerision.NPCs
{
    public abstract class HighResNPC : ModNPC
    {
        public abstract Asset<Texture2D> largeImage { get; }
        public virtual Asset<Texture2D> shimmerLargeImage { get; } = null;
        public virtual Asset<Texture2D> shimmerTransformLargeImage { get; } = null;

        public Rectangle source;       

        public int activeFrame;

        public float scale = 0.05f;

        public bool autoScale = true;

        public bool forceHighRes = false;

        public override void SetDefaults()
        {                
            base.SetDefaults();
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {            
            if (!Config.useLowRes || forceHighRes)
            {
                Asset<Texture2D> text;
                float leScale;
                if (autoScale)
                    leScale = (float)(NPC.width + NPC.height) / (float)(source.Width + source.Height);
                else
                    leScale = scale;


                if ((NPCID.Sets.ShimmerTownTransform[NPC.type] == true || NPCID.Sets.ShimmerTownTransform[Type] == true) && NPC.IsShimmerVariant)
                    text = shimmerLargeImage;
                else if ((NPCID.Sets.ShimmerTownTransform[NPC.type] == true || NPCID.Sets.ShimmerTownTransform[Type] == true) && NPC.shimmering)
                    text = shimmerTransformLargeImage;
                else
                    text = largeImage;

                spriteBatch.Draw(((Texture2D)text), NPC.position - screenPos, source, drawColor, 0, new Vector2(0, 0), leScale, RandomTools.IntToFlip(NPC.spriteDirection), 0);

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
