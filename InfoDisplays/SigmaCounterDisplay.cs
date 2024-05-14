//using ExampleMod.Common.Players;
//using ExampleMod.Content.Items.Accessories;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.Enums;
using Terraria.ID;

namespace TerrariaEpicVerision.InfoDisplays
{
    /// <summary>
    /// InfoDisplay that is coupled with <seealso cref="ExampleInfoAccessory"/> and <seealso cref="ExampleInfoDisplayPlayer"/> to show
    /// off how to add a new info accessory (such as a Radar, Lifeform Analyzer, etc.)
    /// </summary>
    public class SigmaCounterDisplay : InfoDisplay
    {
        public static Color RedInfoTextColor => new(255, 19, 19, Main.mouseTextColor);

        public static string[] betaCucks = new string[]
        {
            "GemBunnyAmber",
            "GemBunnyAmethyst",
            "Bunny",
            "BunnySlimed",
            "GoldBunny",
            "TownBunny",
            "GemBunnyTopaz",
            "PartyBunny",
            "GemBunnyDiamond",
            "BunnyXmas",
            "GemBunnySapphire",
            "GemBunnyRuby",
            "GemBunnyEmeral",
            "Owl",
            "Bird",
            "Squirrel",
            "GemSquirrelAmber",
            "SquirrelRed",
            "Goldfish",
            "BlueJay",
            "RedBird",
            "BlueBird"
        };


        // By default, the vanilla circular outline texture will be used. 
        // This info display has a square icon instead of a circular one, so we need to use a custom outline texture instead of the vanilla outline texture.
        // You will only need to use a custom hover texture if your info display icon doesn't perfectly match the shape that vanilla info displays use
        public override string HoverTexture => Texture + "_Hover";

        // This dictates whether or not this info display should be active
        public override bool Active()
        {
            return Main.LocalPlayer.accThirdEye;
        }

        // Here we can change the value that will be displayed in the game
        public override string DisplayValue(ref Color displayColor, ref Color displayShadowColor)
        {
            // Counting how many minions we have
            // This is the value that will show up when viewing this display in normal play, right next to the icon
            int killCount = 0;
                        
            for (int i = 0; i < betaCucks.Length; i++)
            {
                killCount += Main.BestiaryTracker.Kills.GetKillCount(betaCucks[i]);
            }
               
            bool noInfo = killCount == 0;
            if (noInfo)
            {
                // If "No minions" will be displayed, grey out the text color, similar to DPS Meter or Radar
                displayColor = InactiveInfoTextColor;
            }
    
            return !noInfo ? $"{killCount} sigma points" : "Not sigma…";
        }
    }

}