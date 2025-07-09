using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Sirenix.OdinInspector;
namespace StandardizedProcess
{
    [DefaultExecutionOrder(-1000)]
    public class MainRoot : MonoSingleton<MainRoot>
    {
        public bool LoadProfileSuccess = false;
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
        public Model model = Model.None;
        private void Awake()
        {


        }
        private void Start()
        {

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
