using System.Collections;
using System.Reflection;

namespace MinimumAmbientLightingUpdated
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    // HighLogic.CurrentGame.Parameters.CustomParams<MAL>().overallMin
    public class MAL1 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Day Mode"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "MinAmbience"; } }
        public override string DisplaySection { get { return "Minimum Ambient Lighting"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }

        [GameParameters.CustomFloatParameterUI("Default Minimum Ambient Light", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Used when 'Reset' button is pressed")]
        public float defaultValue = 20f;

        /***********************************************************/

        [GameParameters.CustomFloatParameterUI("Map View Day mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Map view")]
        public float mapMinDay = 20f;

        [GameParameters.CustomFloatParameterUI("Flight, Day mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Flight")]
        public float flightMinDay = 20f;

        [GameParameters.CustomFloatParameterUI("Editor, Day mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Minimum ambient lighting when in the Editor")]
        public float editorMinDay = 20f;

        [GameParameters.CustomFloatParameterUI("Space Center, Day mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Space Center")]
        public float spaceCenterMinDay = 20f;

        [GameParameters.CustomFloatParameterUI("Tracking Station, Day mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Tracking Station")]
        public float trackingStationMinDay = 20f;



        [GameParameters.CustomParameterUI("Use alternate skin")]
        public bool useAltSkin = false;

        [GameParameters.CustomParameterUI("Autoclose after time")]
        public bool autoClose = true;

        [GameParameters.CustomIntParameterUI("Seconds before autoclose", minValue = 2, maxValue = 30)]
        public int autoCloseTimeout = 5;

        public override void SetDifficultyPreset(GameParameters.Preset preset) { }

        public override bool Enabled(MemberInfo member, GameParameters parameters) { return true; }

        public override bool Interactible(MemberInfo member, GameParameters parameters) { return true; }

        public override IList ValidValues(MemberInfo member) { return null; }
    }

    public class MAL2 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Night Mode"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "MinAmbience"; } }
        public override string DisplaySection { get { return "Minimum Ambient Lighting"; } }
        public override int SectionOrder { get { return 2; } }
        public override bool HasPresets { get { return false; } }

        [GameParameters.CustomFloatParameterUI("Default Minimum Ambient Light", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Used when 'Reset' button is pressed")]
        public float defaultValue = 20f;

        /***********************************************************/

        [GameParameters.CustomFloatParameterUI("Map View Night mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Map view")]
        public float mapMinNight = 20f;

        [GameParameters.CustomFloatParameterUI("Flight Night mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Flight")]
        public float flightMinNight = 20f;

        [GameParameters.CustomFloatParameterUI("Editor, Night mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Minimum ambient lighting when in the Editor")]
        public float editorMinNight = 20f;

        [GameParameters.CustomFloatParameterUI("Space Center, Night mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Space Center")]
        public float spaceCenterMinNight = 20f;

        [GameParameters.CustomFloatParameterUI("Tracking Station, Night mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Tracking Station")]
        public float trackingStationMinNight = 20f;


        public override void SetDifficultyPreset(GameParameters.Preset preset) { }

        public override bool Enabled(MemberInfo member, GameParameters parameters) { return true; }

        public override bool Interactible(MemberInfo member, GameParameters parameters) { return true; }

        public override IList ValidValues(MemberInfo member) { return null; }
    }

    public class MAL3 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Stream Mode"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "MinAmbience"; } }
        public override string DisplaySection { get { return "Minimum Ambient Lighting"; } }
        public override int SectionOrder { get { return 3; } }
        public override bool HasPresets { get { return false; } }

        [GameParameters.CustomFloatParameterUI("Default Minimum Ambient Light", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Used when 'Reset' button is pressed")]
        public float defaultValue = 20f;

        [GameParameters.CustomFloatParameterUI("Map View Stream", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Map view")]
        public float mapMinStream = 20f;

        [GameParameters.CustomFloatParameterUI("Flight Stream", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in Flight")]
        public float flightMinStream = 20f;

        [GameParameters.CustomFloatParameterUI("EditorStream", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
              toolTip = "Minimum ambient lighting when in the Editor")]
        public float editorMinStream = 20f;

        [GameParameters.CustomFloatParameterUI("Space Center Stream", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Space Center")]
        public float spaceCenterMinStream = 20f;

        [GameParameters.CustomFloatParameterUI("Tracking Station, Stream mode", minValue = 0, maxValue = 100f, stepCount = 101, displayFormat = "F1",
            toolTip = "Minimum ambient lighting when in the Tracking Station")]
        public float trackingStationMinStream = 20f;


        public override void SetDifficultyPreset(GameParameters.Preset preset) { }

        public override bool Enabled(MemberInfo member, GameParameters parameters) { return true; }

        public override bool Interactible(MemberInfo member, GameParameters parameters) { return true; }

        public override IList ValidValues(MemberInfo member) { return null; }
    }
}