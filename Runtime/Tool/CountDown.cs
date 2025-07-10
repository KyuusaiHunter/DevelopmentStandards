using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class CountDown : MonoBehaviour
{
    [Header("�ȴ���ʱ��")]
    public float Time;
    [Header("ִ�еĲ���")]
    public bool �Ƿ�ִ�з��� = false;
    public bool �Ƿ�ر����� = false;
    public bool �Ƿ������� = false;
    [ShowIf("�Ƿ�ִ�з���",true)]
    public UnityEvent ִ�еķ���;
    [ShowIf("�Ƿ�ر�����", true)]
    public GameObject[] �رյ�����;
    [ShowIf("�Ƿ�������", true)]
    public GameObject[] �򿪵�����;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(CountDownFunction());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void Function()
    {
        if (�Ƿ�������)
        {
            foreach (GameObject item in �򿪵�����)
            {
                item.SetActive(true);
            }
        }
        if (�Ƿ�ִ�з���)
        {
            ִ�еķ���?.Invoke();
        }
        if (�Ƿ�ر�����)
        {
            foreach(GameObject item in �رյ�����)
            {
                item.SetActive(false);
            }
        }
    }

    IEnumerator CountDownFunction()
    {
        yield return new WaitForSeconds(Time);
        Function();
    }
}
