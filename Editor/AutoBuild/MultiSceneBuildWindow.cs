// File: Assets/Editor/MultiSceneBuildWindow.cs
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor.Build.Reporting;

public class MultiSceneBuildWindow : EditorWindow
{
    private MultiSceneBuildConfig config;
    private Vector2 scrollPos;

    [MenuItem("Tools/多场景APK批量打包")]
    public static void ShowWindow()
    {
        var window = GetWindow<MultiSceneBuildWindow>("批量APK打包");
        window.LoadOrCreateConfig();
    }

    private void LoadOrCreateConfig()
    {
        string configPath = "Assets/Editor/MultiSceneBuildConfig.asset";
        config = AssetDatabase.LoadAssetAtPath<MultiSceneBuildConfig>(configPath);

        if (config == null)
        {
            config = ScriptableObject.CreateInstance<MultiSceneBuildConfig>();
            AssetDatabase.CreateAsset(config, configPath);
            AssetDatabase.SaveAssets();
        }
    }

    private void OnGUI()
    {
        if (config == null)
        {
            LoadOrCreateConfig();
            return;
        }

        if (GUILayout.Button("添加构建条目"))
        {
            config.entries.Add(new SceneBuildEntry());
        }

        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);

        for (int i = 0; i < config.entries.Count; i++)
        {
            var entry = config.entries[i];
            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.LabelField($"构建条目 {i + 1}", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
            entry.scenePath = EditorGUILayout.TextField("场景路径", entry.scenePath);
            if (GUILayout.Button("选择", GUILayout.Width(50)))
            {
                string scenePath = EditorUtility.OpenFilePanel("选择场景", "Assets", "unity");
                if (!string.IsNullOrEmpty(scenePath))
                    entry.scenePath = "Assets" + scenePath.Replace(Application.dataPath, "");
            }
            EditorGUILayout.EndHorizontal();

            entry.packageName = EditorGUILayout.TextField("包名", entry.packageName);
            entry.appName = EditorGUILayout.TextField("应用名", entry.appName);
            entry.apkOutputName = EditorGUILayout.TextField("输出APK名", entry.apkOutputName);

            if (GUILayout.Button("删除该条目"))
            {
                config.entries.RemoveAt(i);
                break;
            }

            EditorGUILayout.EndVertical();
        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("开始批量打包"))
        {
            BuildAll();
        }
    }

    private void BuildAll()
    {
        string root = EditorUtility.SaveFolderPanel("选择输出目录", "BuildOutput/Android", "");
        if (string.IsNullOrEmpty(root))
            return;

        foreach (var entry in config.entries)
        {
            if (!File.Exists(entry.scenePath))
            {
                Debug.LogError($"场景路径无效: {entry.scenePath}");
                continue;
            }

            string apkFileName = entry.apkOutputName.EndsWith(".apk") ? entry.apkOutputName : entry.apkOutputName + ".apk";
            string apkPath = Path.Combine(root, apkFileName);
            string dir = Path.GetDirectoryName(apkPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);

            PlayerSettings.applicationIdentifier = entry.packageName;
            PlayerSettings.productName = entry.appName;

            var buildOptions = new BuildPlayerOptions
            {
                scenes = new[] { entry.scenePath },
                target = BuildTarget.Android,
                locationPathName = apkPath,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildOptions);
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"成功构建: {apkPath}");
            }
            else
            {
                Debug.LogError($"构建失败: {apkPath}");
            }
        }

        AssetDatabase.Refresh();
    }
}

[System.Serializable]
public class SceneBuildEntry
{
    public string scenePath;
    public string packageName;
    public string appName;
    public string apkOutputName;
}

public class MultiSceneBuildConfig : ScriptableObject
{
    public List<SceneBuildEntry> entries = new List<SceneBuildEntry>();
}
