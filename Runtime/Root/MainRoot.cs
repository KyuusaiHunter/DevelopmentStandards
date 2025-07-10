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
        public BuildSetting ��Ŀ����;
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
        public Model �����汾 = Model.None;
        [ShowIf("�����汾", Model.Debug), ShowInInspector]
        private string ˮӡ���� = "���԰汾";
        [ShowIf("�����汾", Model.Release), ShowInInspector]
        private string �汾�� = "1.0.0";
        [HideInInspector]
        public string Version
        {
            get
            {
                return �汾��;
            }
        }
        private GameObject Player;
        [ShowInInspector]
        public GameObject �������
        {
            get
            {
                return Player;
            }
        }
        //������MonoSingleton��Awake
        protected override void OnStart()
        {

        }
        public void Start()
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            if (�����汾== Model.Debug)
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
        /// ���԰�ˮӡ
        /// </summary>
        public void DebugModelWaterMark()
        {
            // ���� Canvas
            GameObject canvasGO = new GameObject("DynamicCanvas");
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
            // ���� Text
            GameObject textGO = new GameObject("TopRightText");
            textGO.transform.SetParent(canvasGO.transform);
            Text text = textGO.AddComponent<Text>();
            text.text = ˮӡ����;
            text.fontSize = 24;
            text.color = Color.white;
            text.alignment = TextAnchor.UpperRight;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            // ���� RectTransform
            RectTransform rectTransform = text.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1, 1);
            rectTransform.anchorMax = new Vector2(1, 1);
            rectTransform.pivot = new Vector2(1, 1);
            rectTransform.anchoredPosition = new Vector2(-20, -20);
            rectTransform.sizeDelta = new Vector2(300, 60);
            //�����Ŀ��ת����������Ϊ������
            if (��Ŀ����.�Ƿ���Ҫ��ת����)
            {
                DontDestroyOnLoad(canvasGO);
            }
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
    public class BuildSetting
    {
        public bool �Ƿ���Ҫ��ת���� = false;
        public bool �������Ƿ����� = false;
        
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
