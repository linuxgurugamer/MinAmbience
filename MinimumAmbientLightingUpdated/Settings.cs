using System.Collections;
using System.Reflection;

namespace MinimumAmbientLightingUpdated
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    // HighLogic.CurrentGame.Parameters.CustomParams<MAL>().overallMin
    public class MAL : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "MinAmbience"; } }
        public override string DisplaySection { get { return "Minimum Ambient Lighting Upd"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }

        [GameParameters.CustomFloatParameterUI("Default Minimum Ambient Light", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Used when 'Reset' button is pressed")]
        public float defaultValue = 20f;

        [GameParameters.CustomFloatParameterUI("Map View", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Map view")]
        public float mapMin = 20f;

        [GameParameters.CustomFloatParameterUI("Flight", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Flight")]
        public float flightMin = 20f;

        [GameParameters.CustomFloatParameterUI("Editor", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Minimum ambient lighting when in the Editor")]
        public float editorMin = 20f;

        [GameParameters.CustomFloatParameterUI("Space Center", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Space Center")]
        public float spaceCenterMin = 20f;

        [GameParameters.CustomFloatParameterUI("Tracking Station", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Tracking Station")]
        public float trackingStationMin = 20f;


        [GameParameters.CustomParameterUI("Use alternate skin")]
        public bool useAltSkin = false;

        [GameParameters.CustomParameterUI("Autoclose after time")]
        public bool autoClose = true;

        [GameParameters.CustomIntParameterUI("Seconds before autoclose", minValue = 2, maxValue = 30)]
        public int autoCloseTimeout = 5;

#if false
        [GameParameters.CustomIntParameterUI("Base Transparency", minValue = 0, maxValue = 255)]
        public int baseTransparency = 255;
#endif

        public override void SetDifficultyPreset(GameParameters.Preset preset)
        {

        }

        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {

            return true;
        }


        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {

            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }

}