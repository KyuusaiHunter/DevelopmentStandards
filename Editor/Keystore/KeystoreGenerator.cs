using UnityEditor;
using UnityEngine;
using System.Diagnostics;
using System.IO;

public class KeystoreGenerator
{
    private static string keystorePath = Path.Combine(Application.dataPath, "Editor", "ZKZP_Release.keystore");
    private static string keystorePass = "DP9925";
    private static string keyAlias = "zkzp_alias";
    private static string keyAliasPass = "DP9925";

    [MenuItem("Tools/签名/ 设置 Keystore 配置")]
    public static void ConfigureKeystore()
    {
        if (!File.Exists(keystorePath))
        {
            UnityEngine.Debug.LogError(" Keystore 文件不存在，请先运行 generate_keystore.bat 生成！");
            return;
        }

        PlayerSettings.Android.useCustomKeystore = true;
        PlayerSettings.Android.keystoreName = keystorePath;
        PlayerSettings.Android.keystorePass = keystorePass;
        PlayerSettings.Android.keyaliasName = keyAlias;
        PlayerSettings.Android.keyaliasPass = keyAliasPass;

        UnityEngine.Debug.Log($" Keystore 配置完成: {keystorePath}");
    }

}
