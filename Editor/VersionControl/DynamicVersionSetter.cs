using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace StandardizedProcess.Editor
{
    public class DynamicVersionSetter : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            if (GameObject.FindObjectOfType<MainRoot>().发布版本 == MainRoot.Model.Release)
            {
                string versionPrefix = GameObject.FindObjectOfType<MainRoot>().Version;
                int buildNumber = (int)(System.DateTime.Now - new System.DateTime(2024, 1, 1)).TotalMinutes;

                // 设置版本号
                PlayerSettings.bundleVersion = versionPrefix + buildNumber;

                // Android 特有
#if UNITY_ANDROID
                PlayerSettings.Android.bundleVersionCode = buildNumber;
#endif

                // iOS 特有
#if UNITY_IOS
        PlayerSettings.iOS.buildNumber = buildNumber.ToString();
#endif

                Debug.Log($" 自动设置版本号: {PlayerSettings.bundleVersion}");
            }
            
        }
    }

}
