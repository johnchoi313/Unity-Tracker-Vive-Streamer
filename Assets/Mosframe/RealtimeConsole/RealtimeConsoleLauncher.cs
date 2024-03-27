/*
 * RealtimeConsoleLauncher.cs
 * 
 * @author mosframe / https://github.com/mosframe
 * 
 */
 namespace Mosframe {

    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;

    /// <summary>
    /// Realtime Console Launcher
    /// </summary>
    public static class RealtimeConsoleLauncher {

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Run() {

            var go = new GameObject( "Console Canvas" ) {
                hideFlags = HideFlags.HideInHierarchy
            };
            UnityEngine.Object.DontDestroyOnLoad( go );
            var canvas = go.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 1000;
            canvas.pixelPerfect = true;

            var canvasScaler = go.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(Screen.width,Screen.height);

            go.AddComponent<GraphicRaycaster>();

            var console = new GameObject( "Console", typeof(RectTransform) );
            console.transform.SetParent( canvas.transform, false );
            console.AddComponent<RealtimeConsole>();

            var eventSystem = UnityEngine.Object.FindObjectOfType<EventSystem>();
            if( eventSystem == null ) {

                UnityEngine.Object.DontDestroyOnLoad( new GameObject( "EventSystem", typeof(EventSystem), typeof(StandaloneInputModule) ) );
            }
        }
    }
}
