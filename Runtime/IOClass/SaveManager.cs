using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace StandardizedProcess.Runtime
{
    public class SaveManager : MonoSingleton<SaveManager>
    {
        private SaveData CurrentData;
        private string savePath;

        protected override void OnStart()
        {
            savePath = Path.Combine(Application.persistentDataPath, "save.json");
            Load();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Save()
        {
            CurrentData.Setting = MainRoot.Instance.Setting;
            string json = JsonUtility.ToJson(CurrentData, true);
            File.WriteAllText(savePath, json);
        }
        public void Load()
        {
            if (File.Exists(savePath))
            {
                string json = File.ReadAllText(savePath);
                CurrentData = JsonUtility.FromJson<SaveData>(json);
            }
            else
            {
                CurrentData = new SaveData(); // Ĭ�ϳ�ʼֵ
            }
        }
        public void SetData()
        {
            MainRoot.Instance.Setting = CurrentData.Setting;
            MainRoot.Instance.AwakeSetLanguage();
        }
        private void OnApplicationQuit()
        {
            Save();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause) Save();
        }
    }
    [Serializable]
    public class SaveData
    {
        public string ExtraInfo = "";
        [SerializeField]
        public ProjectSetting Setting = new ProjectSetting();

    }
}
