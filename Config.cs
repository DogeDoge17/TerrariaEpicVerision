using Newtonsoft.Json;
using System.ComponentModel;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;

namespace TerrariaEpicVerision
{
    public class Config : ModConfig     
    {
        public static bool useLowRes = false;
        public static bool nanakoEnabled = true;

        public override ConfigScope Mode => ConfigScope.ClientSide;
        
        public bool _useLowRes = false;
        [DefaultValue(true)]
        public bool _nanakoEnabled = true;

        public override void OnChanged()
        {
            useLowRes = _useLowRes;
            nanakoEnabled = _nanakoEnabled;
            base.OnChanged();
        }
    }
}
