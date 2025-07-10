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
            if (GameObject.FindObjectOfType<MainRoot>().�����汾 == MainRoot.Model.Release)
            {
                string versionPrefix = GameObject.FindObjectOfType<MainRoot>().Version;
                int buildNumber = (int)(System.DateTime.Now - new System.DateTime(2024, 1, 1)).TotalMinutes;

                // ���ð汾��
                PlayerSettings.bundleVersion = versionPrefix + buildNumber;

                // Android ����
#if UNITY_ANDROID
                PlayerSettings.Android.bundleVersionCode = buildNumber;
#endif

                // iOS ����
#if UNITY_IOS
        PlayerSettings.iOS.buildNumber = buildNumber.ToString();
#endif

                Debug.Log($" �Զ����ð汾��: {PlayerSettings.bundleVersion}");
            }
            
        }
    }

}
