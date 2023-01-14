using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.IO;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace TerrariaEpicVerision
{
    public class Config : ModConfig
    {
        public static bool useLowRes = false;

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [Label("Use the orignal low resolution textures")]
        public bool _useLowRes = false;

        public override void OnChanged()
        {
            useLowRes = _useLowRes;
            base.OnChanged();
        }
    }
}
