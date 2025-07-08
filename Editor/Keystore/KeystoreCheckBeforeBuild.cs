using UnityEditor;
using UnityEngine;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using System.IO;

public class KeystoreCheckBeforeBuild : IPreprocessBuildWithReport
{
    public int callbackOrder => 0; // 优先级，越小越早执行

    public void OnPreprocessBuild(BuildReport report)
    {
        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
        {
            return; // 非 Android 平台无需检查
        }

        if (!PlayerSettings.Android.useCustomKeystore)
        {
            throw new BuildFailedException("! Android 打包签名未启用（useCustomKeystore 为 false）");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keystoreName) ||
            !File.Exists(PlayerSettings.Android.keystoreName))
        {
            throw new BuildFailedException("! 未设置有效的 Keystore 文件路径或文件不存在");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keystorePass))
        {
            throw new BuildFailedException("! Keystore 密码未设置");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keyaliasName))
        {
            throw new BuildFailedException("! Key Alias（别名）未设置");
        }

        if (string.IsNullOrEmpty(PlayerSettings.Android.keyaliasPass))
        {
            throw new BuildFailedException("! Key Alias 密码未设置");
        }

        Debug.Log(" [签名检查通过] Android 打包签名配置完备");
    }
}
