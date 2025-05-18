using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;

namespace TerrariaEpicVerision.NPCs.TownNPC
{
    //    internal class GenieCat : HighResNPC
    //    {
    //        private static Profiles.StackedNPCProfile NPCProfile;

    //        public override Asset<Texture2D> largeImage => ModContent.Request<Texture2D>("TerrariaEpicVerision/NPCs/TownNPC/GenieCatHIGHRES");

    //        public override void Load()
    //        {
    //        }

    //        public override void SetStaticDefaults()
    //        {
    //            Main.npcFrameCount[Type] = 25; // The amount of frames the NPC has

    //            NPCID.Sets.ExtraFramesCount[Type] = 9; // Generally for Town NPCs, but this is how the NPC does extra things such as sitting in a chair and talking to other NPCs.
    //            NPCID.Sets.AttackFrameCount[Type] = 4;
    //            NPCID.Sets.DangerDetectRange[Type] = 700; // The amount of pixels away from the center of the npc that it tries to attack enemies.
    //            NPCID.Sets.PrettySafe[Type] = 300;
    //            NPCID.Sets.AttackType[Type] = 1; // Shoots a weapon.
    //            NPCID.Sets.AttackTime[Type] = 60; // The amount of time it takes for the NPC's attack animation to be over once it starts.
    //            NPCID.Sets.AttackAverageChance[Type] = 30;
    //            NPCID.Sets.HatOffsetY[Type] = 4; // For when a party is active, the party hat spawns at a Y offset.
    //            NPCID.Sets.ShimmerTownTransform[NPC.type] = true; // This set says that the Town NPC has a Shimmered form. Otherwise, the Town NPC will become transparent when touching Shimmer like other enemies.

    //            //This sets entry is the most important part of this NPC. Since it is true, it tells the game that we want this NPC to act like a town NPC without ACTUALLY being one.
    //            //What that means is: the NPC will have the AI of a town NPC, will attack like a town NPC, and have a shop (or any other additional functionality if you wish) like a town NPC.
    //            //However, the NPC will not have their head displayed on the map, will de-spawn when no players are nearby or the world is closed, and will spawn like any other NPC.
    //            NPCID.Sets.ActsLikeTownNPC[Type] = true;

    //            // This prevents the happiness button
    //            NPCID.Sets.NoTownNPCHappiness[Type] = true;

    //            //To reiterate, since this NPC isn't technically a town NPC, we need to tell the game that we still want this NPC to have a custom/randomized name when they spawn.
    //            //In order to do this, we simply make this hook return true, which will make the game call the TownNPCName method when spawning the NPC to determine the NPC's name.
    //            //NPCID.Sets.SpawnsWithCustomName[Type] = true;

    //            // Connects this NPC with a custom emote.
    //            // This makes it when the NPC is in the world, other NPCs will "talk about him".
    //            //NPCID.Sets.FaceEmote[Type] = ModContent.EmoteBubbleType<ExampleBoneMerchantEmote>();

    //            //The vanilla Bone Merchant cannot interact with doors (open or close them, specifically), but if you want your NPC to be able to interact with them despite this,
    //            //uncomment this line below.
    //            //NPCID.Sets.AllowDoorInteraction[Type] = true;

    //            // Influences how the NPC looks in the Bestiary
    //            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
    //            {
    //                Velocity = 1f, // Draws the NPC in the bestiary as if its walking +1 tiles in the x direction
    //                Direction = 1 // -1 is left and 1 is right. NPCs are drawn facing the left by default but ExamplePerson will be drawn facing the right
    //            };

    //            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

    //            NPCProfile = new Profiles.StackedNPCProfile(
    //                new Profiles.DefaultNPCProfile(Texture, -1)
    //            );
    //        }

    //        public override void SetDefaults()
    //        {
    //            NPC.friendly = true; // NPC Will not attack player
    //            NPC.width = 18;
    //            NPC.height = 40;
    //            NPC.aiStyle = -1;
    //            NPC.damage = 10;
    //            NPC.defense = 15;
    //            NPC.lifeMax = 500;
    //            NPC.HitSound = SoundID.NPCHit1;
    //            NPC.DeathSound = SoundID.NPCDeath1;
    //            NPC.knockBackResist = 0f;
    //        }

    //        //Make sure to allow your NPC to chat, since being "like a town NPC" doesn't automatically allow for chatting.
    //        public override bool CanChat() => true;


    //        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
    //        {
    //            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
    //            bestiaryEntry.Info.AddRange([
    //				// Sets the preferred biomes of this town NPC listed in the bestiary.
    //				// With Town NPCs, you usually set this to what biome it likes the most in regards to NPC happiness.
    //				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Desert,

    //				// Sets your NPC's flavor text in the bestiary.
    //				new FlavorTextBestiaryInfoElement("A mystical cat that gives a many gifts."),

    //				// You can add multiple elements if you really wanted to
    //				// You can also use localization keys (see Localization/en-US.lang)
    //				new FlavorTextBestiaryInfoElement("Mods.ExampleMod.Bestiary.ExampleBoneMerchant")
    //            ]);
    //        }



    //        public override ITownNPCProfile TownNPCProfile()
    //        {
    //            return NPCProfile;
    //        }

    //        public override float SpawnChance(NPCSpawnInfo spawnInfo)
    //        {
    //            //If any player is underground and has an example item in their inventory, the example bone merchant will have a slight chance to spawn.
    //            if (spawnInfo.Player.ZoneDesert && !EpicWorld.genieSpawned)
    //            {
    //                return 0.34f;
    //            }

    //            //Else, the example bone merchant will not spawn if the above conditions are not met.
    //            return 0f;
    //        }        

    //        public override string GetChat()
    //        {
    //            // WeightedRandom<string> chat = new WeightedRandom<string>();

    //            //// These are things that the NPC has a chance of telling you when you talk to it.
    //            //chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue1"));
    //            //chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue2"));
    //            //chat.Add(Language.GetTextValue("Mods.ExampleMod.Dialogue.ExampleBoneMerchant.StandardDialogue3"));
    //            return "You have been chosen to select a gift of your choice. Please make your request now human."; // chat is implicitly cast to a string.
    //        }

    //        public override void SetChatButtons(ref string button, ref string button2)
    //        { // What the chat buttons are when you open up the chat UI
    //            button = "Choose";//Language.GetTextValue("LegacyInterface.28"); //This is the key to the word "Shop"
    //        }

    //        public override void OnChatButtonClicked(bool firstButton, ref string shopName)
    //        {
    //            if (firstButton)
    //            {
    //                if (!CatSelectionUI.visible) CatSelectionUI.timeStart = Main.GameUpdateCount;
    //                CatSelectionUI.visible = true;
    //            }           
    //        }
    //    }

    class CatSelectionUI : UIState
{
    public UIPanel CatPanel;
    public static bool visible = false;
    public static uint timeStart;

    public override void OnInitialize()
    {
        CatPanel = new();
        CatPanel.SetPadding(0);
        CatPanel.Left.Set(575f, 0f);
        CatPanel.Top.Set(275f, 0f);
        CatPanel.Width.Set(385f, 0f);
        CatPanel.Height.Set(190f, 0f);
        CatPanel.BackgroundColor = new(232, 228, 100);

        UIText text = new("Pharaoh's set");
        text.Left.Set(35, 0f);
        text.Top.Set(10, 0f);
        text.Width.Set(60, 0f);
        text.Height.Set(22, 0f);
        CatPanel.Append(text);

        UIText text2 = new("Sandstorm in a Bottle");
        text2.Left.Set(35, 0f);
        text2.Top.Set(40, 0f);
        text2.Width.Set(120, 0f);
        text2.Height.Set(22, 0f);
        CatPanel.Append(text2);

        UIText text21 = new("Flying Carpet");
        text21.Left.Set(35, 0f);
        text21.Top.Set(70, 0f);
        text21.Width.Set(100, 0f);
        text21.Height.Set(22, 0f);
        CatPanel.Append(text21);


        Asset<Texture2D> vanityTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_848");
        UIImageButton vanityButton = new(vanityTexture);
        vanityButton.Left.Set(10, 0f);
        vanityButton.Top.Set(10, 0f);
        vanityButton.Width.Set(22, 0f);
        vanityButton.Height.Set(22, 0f);
        //vanityButton.OnLeftClick += new MouseEvent(PlayButtonClicked1);
        CatPanel.Append(vanityButton);

        Asset<Texture2D> sandstormTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_857");
        UIImageButton bottleButton = new(sandstormTexture);
        bottleButton.Left.Set(10, 0f);
        bottleButton.Top.Set(40, 0f);
        bottleButton.Width.Set(22, 0f);
        bottleButton.Height.Set(22, 0f);
        // bottleButton.OnLeftClick += new MouseEvent(PlayButtonClicked2);
        CatPanel.Append(bottleButton);

        Asset<Texture2D> carpetTexture = ModContent.Request<Texture2D>("Terraria/Images/Item_934");
        UIImageButton carpetButton = new(carpetTexture);
        carpetButton.Left.Set(10, 0f);
        carpetButton.Top.Set(70, 0f);
        carpetButton.Width.Set(22, 0f);
        carpetButton.Height.Set(22, 0f);
        //carpetButton.OnLeftClick += new MouseEvent(PlayButtonClicked3);
        CatPanel.Append(carpetButton);


        Asset<Texture2D> buttonDeleteTexture = ModContent.Request<Texture2D>("Terraria/Images/UI/ButtonDelete");
        UIImageButton closeButton = new(buttonDeleteTexture);
        closeButton.Left.Set(350, 0f);
        closeButton.Top.Set(10, 0f);
        closeButton.Width.Set(22, 0f);
        closeButton.Height.Set(22, 0f);
        //closeButton.OnLeftClick += new MouseEvent(CloseButtonClicked);
        CatPanel.Append(closeButton);
        Append(CatPanel);
    }

}

}