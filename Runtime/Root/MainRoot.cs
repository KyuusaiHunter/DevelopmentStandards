using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using Sirenix.OdinInspector;
namespace StandardizedProcess
{
    [DefaultExecutionOrder(-1000)]
    public class MainRoot : MonoSingleton<MainRoot>
    {
        public bool LoadProfileSuccess = false;
        public BuildSetting 项目设置;
        public ProjectSetting Setting;
        private static readonly Dictionary<Language, string> LanguageMap = new Dictionary<Language, string>
    {
        { Language.English, "en" },
        { Language.简体中文, "zh-Hans" },
        { Language.日本語, "ja" },
        //{ Language.Korean, "ko" },
        //{ Language.German, "de" },
        //{ Language.French, "fr" }
    };
        public enum Model
        {
            None,
            Debug,
            Release,
            展会模式
        }
        [EnumPaging]
        public Model 发布版本 = Model.None;
        [ShowIf("发布版本", Model.Debug), ShowInInspector]
        private string 水印文字 = "测试版本";
        [ShowIf("发布版本", Model.Release), ShowInInspector]
        private string 版本号 = "1.0.0";
        [HideInInspector]
        public string Version
        {
            get
            {
                return 版本号;
            }
        }
        private GameObject Player;
        [ShowInInspector]
        public GameObject 玩家物体
        {
            get
            {
                return Player;
            }
        }
        //基础自MonoSingleton的Awake
        protected override void OnStart()
        {

        }
        public void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (发布版本== Model.Debug)
            {
                DebugModelWaterMark();
            }
        }
        public void PlayerDontDestory()
        {
            if(Player == null)
                Player = GameObject.FindGameObjectWithTag("Player");
            DontDestroyOnLoad(Player);
        }
        /// <summary>
        /// 测试版水印
        /// </summary>
        public void DebugModelWaterMark()
        {
            // 创建 Canvas
            GameObject canvasGO = new GameObject("DynamicCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            // 创建 Text
            GameObject textGO = new GameObject("TopRightText");
            textGO.transform.SetParent(canvasGO.transform);
            Text text = textGO.AddComponent<Text>();
            text.text = 水印文字;
            text.fontSize = 24;
            text.color = Color.white;
            text.alignment = TextAnchor.UpperRight;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            // 设置 RectTransform
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -20);
            rectTransform.sizeDelta = new Vector2(300, 60);
            //如果项目跳转场景则设置为不销毁
            if (项目设置.是否需要跳转场景)
            {
                DontDestroyOnLoad(canvasGO);
            }
        }
        public void SetLanguage(Language selectedLanguage)
        {
            if (!LanguageMap.TryGetValue(selectedLanguage, out string localeCode))
            {
                Debug.LogWarning("未映射该语言：" + selectedLanguage);
                return;
            }

            var locale = LocalizationSettings.AvailableLocales.Locales
                .Find(l => l.Identifier.Code == localeCode);

            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($" 语言切换为：{selectedLanguage} ({localeCode})");
            }
            else
            {
                Debug.LogWarning("未找到对应 Locale：" + localeCode);
            }
        }
        public void AwakeSetLanguage()
        {
            StartCoroutine(InitializeAndApplySavedLanguage());
        }
        public static IEnumerator InitializeAndApplySavedLanguage()
        {
            yield return LocalizationSettings.InitializationOperation;
            Instance.SetLanguage(Instance.Setting.language);
        }
    }
    [Serializable]
    public class BuildSetting
    {
        public bool 是否需要跳转场景 = false;
        public bool 玩家组件是否销毁 = false;
        
    }
    [Serializable]
    public class ProjectSetting
    {
        //语言
        [ReadOnly, EnumPaging]
        public Language language = Language.简体中文;
    }
    public enum Language
    {
        简体中文,
        English,
        日本語
    }
}
