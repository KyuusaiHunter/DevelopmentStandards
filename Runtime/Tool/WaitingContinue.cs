using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingContinue : MonoBehaviour
{
    public float[] 等待时间;
    public UnityEvent[] 时间到后执行的操作;
    private int currentStep = 0;
    private bool canContinue = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ProcessSteps());
    }

    public void Continue()
    {
        canContinue = true;
    }
    IEnumerator ProcessSteps()
    {
        while (currentStep < 等待时间.Length)
        {
            // 等待指定时间
            yield return new WaitForSeconds(等待时间[currentStep]);

            // 执行对应操作
            if (时间到后执行的操作 != null &&
                currentStep < 时间到后执行的操作.Length &&
                时间到后执行的操作[currentStep] != null)
            {
                时间到后执行的操作[currentStep].Invoke();
            }

            // 等待外部调用 Continue
            yield return new WaitUntil(() => canContinue);
            canContinue = false;
            currentStep++;
        }

        Debug.Log("全部任务完成");
    }

}
