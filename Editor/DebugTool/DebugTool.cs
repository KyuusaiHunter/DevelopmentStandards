using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DebugTool
{
    [MenuItem("Tools/DebugTool/设置为开发者模式")]
    public static void DebugModel()
    {
        try
        {
            MainRoot.Instance.model = MainRoot.Model.Debug;
            UnityEngine.Debug.Log("已切换为开发者模式");
        }
        catch
        {
            UnityEngine.Debug.LogError("切换开发者模式失败，请检查设置！");
        }
    }
    [MenuItem("Tools/DebugTool/设置为发布模式")]
    public static void ReleaseModel()
    {
        try
        {
            MainRoot.Instance.model = MainRoot.Model.Release;
            UnityEngine.Debug.Log("已切换为发布模式");
        }
        catch
        {
            UnityEngine.Debug.LogError("切换发布模式失败，请检查设置！");
        }
    }
}
