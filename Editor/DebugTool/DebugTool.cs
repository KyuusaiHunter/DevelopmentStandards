using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using StandardizedProcess;

namespace StandardizedProcess.Editor
{
    public class DebugTool
    {
        [MenuItem("Tools/DebugTool/����Ϊ������ģʽ")]
        public static void DebugModel()
        {
            try
            {
                MainRoot.Instance.�����汾 = MainRoot.Model.Debug;
                UnityEngine.Debug.Log("���л�Ϊ������ģʽ");
            }
            catch
            {
                UnityEngine.Debug.LogError("�л�������ģʽʧ�ܣ��������ã�");
            }
        }
        [MenuItem("Tools/DebugTool/����Ϊ����ģʽ")]
        public static void ReleaseModel()
        {
            try
            {
                MainRoot.Instance.�����汾 = MainRoot.Model.Release;
                UnityEngine.Debug.Log("���л�Ϊ����ģʽ");
            }
            catch
            {
                UnityEngine.Debug.LogError("�л�����ģʽʧ�ܣ��������ã�");
            }
        }
    }
}

