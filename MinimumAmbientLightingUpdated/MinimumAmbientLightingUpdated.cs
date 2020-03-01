using KSP.UI.Screens;
using UnityEngine;
using ToolbarControl_NS;
using ClickThroughFix;


namespace MinimumAmbientLightingUpdated
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class MinimumAmbientLightingUpdated : MonoBehaviour
    {
        private static bool streaming = false;
        private static bool day = true;
        private static bool night = false;
        enum LightMode { streaming = 0, day = 1, night = 2};
        private static LightMode mode = 0;

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
                if (!HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().useAltSkin)
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
                float sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().defaultValue;

                switch (HighLogic.LoadedScene)
                {
                    case GameScenes.EDITOR:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().editorMinStream; break;
                            case LightMode.day:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().editorMinDay; break;
                            case LightMode.night:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().editorMinNight; break;
                        }
                        break;

                    case GameScenes.FLIGHT:
                        if (MapView.MapIsEnabled)
                        {
                            switch (mode)
                            {
                                case LightMode.streaming:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().mapMinStream; break;
                                case LightMode.day:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().mapMinDay; break;
                                case LightMode.night:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().mapMinNight; break;
                            }

                        }
                        else
                        {
                            switch (mode)
                            {
                                case LightMode.streaming:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().flightMinStream; break;
                                case LightMode.day:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().flightMinDay; break;
                                case LightMode.night:
                                    sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().flightMinNight; break;
                            }
                        }
                        break;

                        break;
                    case GameScenes.SPACECENTER:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().spaceCenterMinStream; break;
                            case LightMode.day:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().spaceCenterMinDay; break;
                            case LightMode.night:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().spaceCenterMinNight; break;
                        }

                        break;
                    case GameScenes.TRACKSTATION:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().trackingStationMinStream; break;
                            case LightMode.day:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().trackingStationMinDay; break;
                            case LightMode.night:
                                sv = HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().trackingStationMinNight; break;
                        }

                        break;
                }
                return sv;
            }


            set
            {
                switch (HighLogic.LoadedScene)
                {
                    case GameScenes.EDITOR:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().editorMinStream = value; ; break;
                            case LightMode.day:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().editorMinDay = value; break;
                            case LightMode.night:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().editorMinNight = value; break;
                        }
                        break;
                    case GameScenes.FLIGHT:
                        if (MapView.MapIsEnabled)
                        {
                            switch (mode)
                            {
                                case LightMode.streaming:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().mapMinStream = value; ; break;
                                case LightMode.day:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().mapMinDay = value; break;
                                case LightMode.night:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().mapMinNight = value; break;
                            }
                        }
                        else
                        {
                            switch (mode)
                            {
                                case LightMode.streaming:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().flightMinStream = value; ; break;
                                case LightMode.day:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().flightMinDay = value; break;
                                case LightMode.night:
                                    HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().flightMinNight = value; break;
                            }
                        }
                        break;
                    case GameScenes.SPACECENTER:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().spaceCenterMinStream = value; ; break;
                            case LightMode.day:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().spaceCenterMinDay = value; break;
                            case LightMode.night:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().spaceCenterMinNight = value; break;
                        }
                        break;
                    case GameScenes.TRACKSTATION:
                        switch (mode)
                        {
                            case LightMode.streaming:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL3>().trackingStationMinStream = value; ; break;
                            case LightMode.day:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().trackingStationMinDay = value; break;
                            case LightMode.night:
                                HighLogic.CurrentGame.Parameters.CustomParams<MAL2>().trackingStationMinNight = value; break;
                        }
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
                SlideValue = HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().defaultValue;
                lastTimeAccessed = Time.realtimeSinceStartup;
            }

            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            var b = GUILayout.Toggle(HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().useAltSkin, "Use alternate skin");
            if (b != HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().useAltSkin)
                lastTimeAccessed = Time.realtimeSinceStartup;
            HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().useAltSkin = b;
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Save as Default", GUILayout.Height(20f)))
            {
                HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().defaultValue = SlideValue;
                lastTimeAccessed = Time.realtimeSinceStartup;
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (night || streaming)
                day = false;
            day = GUILayout.Toggle(day, "Day");
            GUILayout.FlexibleSpace();
            if (day || streaming)
                night = false;
            night = GUILayout.Toggle(night, "Night");
            GUILayout.FlexibleSpace();
            if (day || night) streaming = false;
            streaming = GUILayout.Toggle(streaming, "Streaming mode");
            GUILayout.FlexibleSpace();
            if (!HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().useAltSkin)
                GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUI.DragWindow();
            mode = (LightMode)(streaming ? 0 : 0) + (day ? 1 : 0) + (night ? 2 : 0); 
        }

        private void ResizeWindow()
        {
            windowPos = new Rect((float)Screen.width - 400f - 41f, 0f, 400f, 56 + 56 / 2);
        }

        private void LateUpdate()
        {
            if (HighLogic.CurrentGame == null)
                return;
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
            if (showSlider && HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().autoClose)
            {
                if (Time.realtimeSinceStartup - lastTimeAccessed > HighLogic.CurrentGame.Parameters.CustomParams<MAL1>().autoCloseTimeout)
                {
                    showSlider = false;
                    //toolbarControl.buttonActive = false;
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