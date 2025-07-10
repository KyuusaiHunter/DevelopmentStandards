using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitingContinue : MonoBehaviour
{
    public float[] �ȴ�ʱ��;
    public UnityEvent[] ʱ�䵽��ִ�еĲ���;
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
        while (currentStep < �ȴ�ʱ��.Length)
        {
            // �ȴ�ָ��ʱ��
            yield return new WaitForSeconds(�ȴ�ʱ��[currentStep]);

            // ִ�ж�Ӧ����
            if (ʱ�䵽��ִ�еĲ��� != null &&
                currentStep < ʱ�䵽��ִ�еĲ���.Length &&
                ʱ�䵽��ִ�еĲ���[currentStep] != null)
            {
                ʱ�䵽��ִ�еĲ���[currentStep].Invoke();
            }

            // �ȴ��ⲿ���� Continue
            yield return new WaitUntil(() => canContinue);
            canContinue = false;
            currentStep++;
        }

        Debug.Log("ȫ���������");
    }

}
