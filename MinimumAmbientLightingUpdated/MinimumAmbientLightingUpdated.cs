using KSP.UI.Screens;
using UnityEngine;
using ToolbarControl_NS;
using ClickThroughFix;


namespace MinimumAmbientLightingUpdated
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class MinimumAmbientLightingUpdated : MonoBehaviour
    {

        private static Color centerAmbient;

        private bool showSlider;

        private float minAmbient;

        private Rect windowPos;

        private static ToolbarControl toolbarControl;

#if false
        private CanvasGroup trans;
        private float effectiveTransparency;
#endif
        private void Start()
        {
            Object.DontDestroyOnLoad(this);

            centerAmbient = RenderSettings.ambientLight;
            ResizeWindow();
            InitializeToolbarButton();
#if false
            effectiveTransparency = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().baseTransparency;
            trans = GetComponent<CanvasGroup>();
            trans.alpha = effectiveTransparency;
#endif
        }

        const string _appImageFile = "MinimumAmbientLightingUpdated/PluginData/ToolbarIcon";
        public const string MODID = "MinAmb";
        public const string MODNAME = "Minimum Ambient Lighting";

        private void InitializeToolbarButton()
        {
            toolbarControl = this.gameObject.AddComponent<ToolbarControl>();
            toolbarControl.AddToAllToolbars(
                OnAppToggleOn, OnAppToggleOff,
                ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.MAPVIEW | ApplicationLauncher.AppScenes.SPACECENTER | ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.TRACKSTATION | ApplicationLauncher.AppScenes.VAB,
                MODID,
                "MainButton",
                _appImageFile,
                _appImageFile,
                _appImageFile,
                _appImageFile,
                MODNAME
                );
        }


        double lastTimeAccessed;

        private void OnAppToggleOn()
        {
            showSlider = true;
            lastTimeAccessed = Time.realtimeSinceStartup;
        }

        private void OnAppToggleOff()
        {
            showSlider = false;
        }

        private void OnGUI()
        {
            if (showSlider)
            {
                if (!HighLogic.CurrentGame.Parameters.CustomParams<MAL>().useAltSkin)
                    GUI.skin = HighLogic.Skin;
                windowPos = ClickThruBlocker.GUILayoutWindow(379, windowPos, OnWindow, "Minimum Ambient Lighting");
            }
        }


        float SlideValue
        {
            get
            {
                if (HighLogic.CurrentGame == null)
                    return 0;
                float sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().defaultValue;
                switch (HighLogic.LoadedScene)
                {
                    case GameScenes.EDITOR:
                        sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().editorMin;
                        break;
                    case GameScenes.FLIGHT:
                        if (MapView.MapIsEnabled)
                            sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().mapMin;
                        else
                            sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().flightMin;
                        break;
                    case GameScenes.SPACECENTER:
                        sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().spaceCenterMin;
                        break;
                    case GameScenes.TRACKSTATION:
                        sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().trackingStationMin;
                        break;
                }
                return sv;
            }
            set
            {
                switch (HighLogic.LoadedScene)
                {
                    case GameScenes.EDITOR:
                        HighLogic.CurrentGame.Parameters.CustomParams<MAL>().editorMin = value;
                        break;
                    case GameScenes.FLIGHT:
                        if (MapView.MapIsEnabled)
                            HighLogic.CurrentGame.Parameters.CustomParams<MAL>().mapMin = value;
                        else
                            HighLogic.CurrentGame.Parameters.CustomParams<MAL>().flightMin = value;
                        break;
                    case GameScenes.SPACECENTER:
                        HighLogic.CurrentGame.Parameters.CustomParams<MAL>().spaceCenterMin = value;
                        break;
                    case GameScenes.TRACKSTATION:
                        HighLogic.CurrentGame.Parameters.CustomParams<MAL>().trackingStationMin = value;
                        break;
                }

            }
        }

        private void OnWindow(int windowID)
        {

            GUILayout.BeginHorizontal();

            float newSlideValue = GUILayout.HorizontalSlider(SlideValue, 0f, 100f);
            if (newSlideValue != SlideValue)
            {
                lastTimeAccessed = Time.realtimeSinceStartup;
                SlideValue = newSlideValue;
                minAmbient = SlideValue / 100f;
            }
            if (GUILayout.Button("Reset to Default", GUILayout.ExpandWidth(false), GUILayout.Height(20f)))
            {
                SlideValue = HighLogic.CurrentGame.Parameters.CustomParams<MAL>().defaultValue;
                lastTimeAccessed = Time.realtimeSinceStartup;
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            var b = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<MAL>().useAltSkin, "Use alternate skin");
            if (b != HighLogic.CurrentGame.Parameters.CustomParams<MAL>().useAltSkin)
                lastTimeAccessed = Time.realtimeSinceStartup;
            HighLogic.CurrentGame.Parameters.CustomParams<MAL>().useAltSkin = b;
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save as Default", GUILayout.Height(20f)))
            {
                HighLogic.CurrentGame.Parameters.CustomParams<MAL>().defaultValue = SlideValue;
                lastTimeAccessed = Time.realtimeSinceStartup;
            }

            GUILayout.EndHorizontal();
            GUI.DragWindow();
        }

        private void ResizeWindow()
        {
            windowPos = new Rect((float)Screen.width - 400f - 41f, 0f, 400f, 56 + 56 / 2);
        }

        private void LateUpdate()
        {
            if (HighLogic.LoadedSceneIsFlight || HighLogic.LoadedScene == GameScenes.SPACECENTER || HighLogic.LoadedSceneIsEditor || HighLogic.LoadedSceneHasPlanetarium)
            {
                Color ambientLight = RenderSettings.ambientLight;
                if (HighLogic.LoadedScene == GameScenes.SPACECENTER)
                {
                    ambientLight = centerAmbient;
                }
                float num = 1f;
                minAmbient = SlideValue / 100f;
                num = ((ambientLight.r < minAmbient && ambientLight.r < num) ? ambientLight.r : num);
                num = ((ambientLight.g < minAmbient && ambientLight.g < num) ? ambientLight.g : num);
                num = ((ambientLight.b < minAmbient && ambientLight.b < num) ? ambientLight.b : num);
                if (num < 1f)
                {
                    num = minAmbient - num;
                    ambientLight.r += num;
                    ambientLight.g += num;
                    ambientLight.b += num;
                    RenderSettings.ambientLight = ambientLight;
                }
            }
            if (showSlider && HighLogic.CurrentGame.Parameters.CustomParams<MAL>().autoClose)
            {
                if (Time.realtimeSinceStartup - lastTimeAccessed > HighLogic.CurrentGame.Parameters.CustomParams<MAL>().autoCloseTimeout)
                {
                    showSlider = false;
                    toolbarControl.buttonActive = false;
                }
            }
        }

        private void OnDestroy()
        {
            if (toolbarControl != null)
            {
                toolbarControl.OnDestroy();
                Destroy(toolbarControl);
            }
        }
    }
}