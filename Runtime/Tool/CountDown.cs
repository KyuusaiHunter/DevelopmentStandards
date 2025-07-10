using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

public class CountDown : MonoBehaviour
{
    [Header("等待的时间")]
    public float Time;
    [Header("执行的操作")]
    public bool 是否执行方法 = false;
    public bool 是否关闭物体 = false;
    public bool 是否开启物体 = false;
    [ShowIf("是否执行方法",true)]
    public UnityEvent 执行的方法;
    [ShowIf("是否关闭物体", true)]
    public GameObject[] 关闭的物体;
    [ShowIf("是否开启物体", true)]
    public GameObject[] 打开的物体;
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
        if (是否开启物体)
        {
            foreach (GameObject item in 打开的物体)
            {
                item.SetActive(true);
            }
        }
        if (是否执行方法)
        {
            执行的方法?.Invoke();
        }
        if (是否关闭物体)
        {
            foreach(GameObject item in 关闭的物体)
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
