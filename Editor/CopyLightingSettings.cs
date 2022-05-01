//
// Copy Lighting Settings Utility. Copyright (c) 2017-2022 Peter Schraut (www.console-dev.de). See LICENSE.md
// https://github.com/pschraut/UnityCopyLightingSettings
//
#pragma warning disable IDE0018 // Visual Studio: Inline variable declaration
#pragma warning disable IDE0051 // Visual Studio: Remove unused private members

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.Reflection;

namespace Oddworm.EditorFramework
{
    static class CopyLightingSettings
    {
        static SerializedObject s_SourceLightmapSettings = default;
        static SerializedObject s_SourceRenderSettings = default;
        static string s_SourceSunName = "";

#if UNITY_2018_2_OR_NEWER
        const string k_CopySettingsMenuPath = "Window/Rendering/Copy Lighting Settings";
        const string k_PasteSettingsMenuPath = "Window/Rendering/Paste Lighting Settings";
        const string k_PasteSettingsAllMenuPath = "Window/Rendering/Paste Lighting Settings in open Scenes";
#else
        const string k_CopySettingsMenuPath = "Window/Lighting/Copy Settings";
        const string k_PasteSettingsMenuPath = "Window/Lighting/Paste Settings";
        const string k_PasteSettingsAllMenuPath = "Window/Lighting/Paste Settings in open Scenes";
#endif

        [MenuItem(k_CopySettingsMenuPath, priority = 200)]
        static void CopySettings()
        {
            UnityEngine.Object lightmapSettings;
            if (!TryGetSettings(typeof(LightmapEditorSettings), "GetLightmapSettings", out lightmapSettings))
                return;

            UnityEngine.Object renderSettings;
            if (!TryGetSettings(typeof(RenderSettings), "GetRenderSettings", out renderSettings))
                return;

            s_SourceLightmapSettings = new SerializedObject(lightmapSettings);
            s_SourceRenderSettings = new SerializedObject(renderSettings);

            // Get the sun name
            s_SourceSunName = "";
            var sunProperty = s_SourceRenderSettings.FindProperty("m_Sun");
            if (sunProperty != null && sunProperty.objectReferenceValue != null)
                s_SourceSunName = sunProperty.objectReferenceValue.name;
        }

        [MenuItem(k_PasteSettingsMenuPath, priority = 201)]
        static void PasteSettings()
        {
            UnityEngine.Object lightmapSettings;
            if (!TryGetSettings(typeof(LightmapEditorSettings), "GetLightmapSettings", out lightmapSettings))
                return;

            UnityEngine.Object renderSettings;
            if (!TryGetSettings(typeof(RenderSettings), "GetRenderSettings", out renderSettings))
                return;

            CopyInternal(s_SourceLightmapSettings, new SerializedObject(lightmapSettings));

            var targetRenderSettings = new SerializedObject(renderSettings);
            CopyInternal(s_SourceRenderSettings, targetRenderSettings);

            var sunProperty = targetRenderSettings.FindProperty("m_Sun");
            TryConnectSunSource(sunProperty, s_SourceSunName);

            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
        }

        static void TryConnectSunSource(SerializedProperty sunProperty, string sunName)
        {
            if (sunProperty == null)
                return;

            if (sunProperty.objectReferenceValue != null)
                return; // don't set sun if it's assigned already

            if (string.IsNullOrEmpty(sunName))
                return;

            var activeScene = EditorSceneManager.GetActiveScene();
            Light sunLight = null;

            // Try to find an active sun first
            foreach (var light in Resources.FindObjectsOfTypeAll<Light>())
            {
                if (!light.enabled)
                    continue;

                if (!string.Equals(light.name, sunName, System.StringComparison.OrdinalIgnoreCase))
                    continue;

                if (light.gameObject.scene != activeScene)
                    continue;

                sunLight = light;
                break;
            }

            // If no active sun was found, consider inactive as well
            if (sunLight == null)
            {
                foreach (var light in Resources.FindObjectsOfTypeAll<Light>())
                {
                    if (!string.Equals(light.name, s_SourceSunName, System.StringComparison.OrdinalIgnoreCase))
                        continue;

                    if (light.gameObject.scene != activeScene)
                        continue;

                    sunLight = light;
                    break;
                }
            }

            if (sunLight != null)
            {
                sunProperty.objectReferenceValue = sunLight;
                sunProperty.serializedObject.ApplyModifiedProperties();
            }
        }

        [MenuItem(k_PasteSettingsAllMenuPath, priority = 202)]
        static void PasteSettingsAll()
        {
            var activeScene = EditorSceneManager.GetActiveScene();
            try
            {
                for (var n = 0; n < EditorSceneManager.sceneCount; ++n)
                {
                    var scene = EditorSceneManager.GetSceneAt(n);
                    if (!scene.IsValid() || !scene.isLoaded)
                        continue;

                    EditorSceneManager.SetActiveScene(scene);

                    PasteSettings();
                }
            }
            finally
            {
                EditorSceneManager.SetActiveScene(activeScene);
            }
        }

        [MenuItem(k_PasteSettingsAllMenuPath, validate = true)]
        [MenuItem(k_PasteSettingsMenuPath, validate = true)]
        static bool PasteValidate()
        {
            return s_SourceLightmapSettings != null && s_SourceRenderSettings != null;
        }

        static void CopyInternal(SerializedObject source, SerializedObject dest)
        {
            var prop = source.GetIterator();
            while (prop.Next(true))
            {
                var copyProperty = true;
                foreach (var propertyName in new[] { "m_Sun", "m_FileID", "m_PathID", "m_ObjectHideFlags" })
                {
                    if (string.Equals(prop.name, propertyName, System.StringComparison.Ordinal))
                    {
                        copyProperty = false;
                        break;
                    }
                }

                if (copyProperty)
                    dest.CopyFromSerializedProperty(prop);
            }

            dest.ApplyModifiedProperties();
        }

        static bool TryGetSettings(System.Type type, string methodName, out UnityEngine.Object settings)
        {
            settings = null;

            var method = type.GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic);
            if (method == null)
            {
                Debug.LogErrorFormat("CopyLightingSettings: Could not find {0}.{1}", type.Name, methodName);
                return false;
            }

            var value = method.Invoke(null, null) as UnityEngine.Object;
            if (value == null)
            {
                Debug.LogErrorFormat("CopyLightingSettings: Could get data from {0}.{1}", type.Name, methodName);
                return false;
            }

            settings = value;
            return true;
        }
    }
}
#endif
