using UnityEngine;
using ToolbarControl_NS;

namespace MinimumAmbientLightingUpdated
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(MinimumAmbientLightingUpdated.MODID, MinimumAmbientLightingUpdated.MODNAME);
        }
    }
}