using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

namespace StandardizedProcess
{
    public class UIRoot : MonoSingleton<UIRoot>
    {
        
        public void OpeneUI(GameObject UIObj)
        {
            UIObj.SetActive(true);
        }
        public void OpeneUI(Transform UIObj)
        {
            UIObj.gameObject.SetActive(true);
        }
        public void OpeneUI(CanvasGroup UIObj)
        {
            UIObj.gameObject.SetActive(true);
        }
        public void ShowUI(GameObject UIObj)
        {
            UIObj.GetComponent<CanvasGroup>().alpha = 1;
            UIObj.GetComponent<CanvasGroup>().interactable = true;
            UIObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        public void ShowUI(Transform UIObj)
        {
            UIObj.GetComponent<CanvasGroup>().alpha = 1;
            UIObj.GetComponent<CanvasGroup>().interactable = true;
            UIObj.GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
        public void ShowUI(CanvasGroup UIObj)
        {
            UIObj.alpha = 1;
            UIObj.interactable = true;
            UIObj.blocksRaycasts = true;
        }
        public void CloseUI(GameObject UIObj)
        {
            UIObj.GetComponent<CanvasGroup>().alpha = 0;
            UIObj.GetComponent<CanvasGroup>().interactable = false;
            UIObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        public void CloseUI(Transform UIObj)
        {
            UIObj.GetComponent<CanvasGroup>().alpha = 0;
            UIObj.GetComponent<CanvasGroup>().interactable = false;
            UIObj.GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
        public void CloseUI(CanvasGroup UIObj)
        {
            UIObj.alpha = 0;
            UIObj.interactable = false;
            UIObj.blocksRaycasts = false;
        }
        public void FullCloseUI(GameObject UIObj)
        {
            UIObj.SetActive(false);
        }
        public void FullCloseUI(Transform UIObj)
        {
            UIObj.gameObject.SetActive(false);
        }
        public void FullCloseUI(CanvasGroup UIObj)
        {
            UIObj.gameObject.SetActive(false);
        }

    }
}

