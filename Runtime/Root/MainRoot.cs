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
        { Language.��������, "zh-Hans" },
        { Language.�ձ��Z, "ja" },
        //{ Language.Korean, "ko" },
        //{ Language.German, "de" },
        //{ Language.French, "fr" }
    };
        public enum Model
        {
            None,
            Debug,
            Release,
            չ��ģʽ
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
                Debug.LogWarning("δӳ������ԣ�" + selectedLanguage);
                return;
            }

            var locale = LocalizationSettings.AvailableLocales.Locales
                .Find(l => l.Identifier.Code == localeCode);

            if (locale != null)
            {
                LocalizationSettings.SelectedLocale = locale;
                Debug.Log($" �����л�Ϊ��{selectedLanguage} ({localeCode})");
            }
            else
            {
                Debug.LogWarning("δ�ҵ���Ӧ Locale��" + localeCode);
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
        //����
        [ReadOnly, EnumPaging]
        public Language language = Language.��������;
    }
    public enum Language
    {
        ��������,
        English,
        �ձ��Z
    }
}
