using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookAt : MonoBehaviour
{
    public Transform Target;
    [Header("������ֵ����")]
    public float thresholdAngleDeg = 0f;
    public float timeWindow = 0.5f;
    private Quaternion lastRotation;
    private float accumulatedAngle = 0f;
    private float windowTimer = 0f;
    [Header("�¼�")]
    public UnityEvent onAngleExceeded;
    // Start is called before the first frame update
    void Start()
    {
        lastRotation = transform.rotation;
        windowTimer = 0f;
        accumulatedAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtOnlyAxis(transform, Target, Vector3.up);
        
    }
    /// <summary>
    /// ʹ��ǰ�������ָ���ᣨ��Y�ᣩ����Ŀ��
    /// </summary>
    /// <param name="self">Ҫ��ת������Transform</param>
    /// <param name="target">Ŀ��Transform</param>
    /// <param name="axis">Լ����ת���ᣨ���� Vector3.up ΪY�ᣩ</param>
    public  void LookAtOnlyAxis(Transform self, Transform target, Vector3 axis)
    {
        // 1. ����Ŀ�귽������
        Vector3 direction = target.position - self.position;

        // 2. �������Ҫ��ת�ķ���������ֻ��Y����ת�������Y�����Ա���ˮƽ��
        if (axis == Vector3.up)
            direction.y = 0;
        else if (axis == Vector3.right)
            direction.x = 0;
        else if (axis == Vector3.forward)
            direction.z = 0;

        // 3. ������Ϊ�գ�ֱ�ӷ���
        if (direction.sqrMagnitude < 0.001f)
            return;

        // 4. ����Ŀ����ת
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, axis);

        // ���Ƕ��Ƿ񳬹���ֵ
        Quaternion current = transform.rotation;
        float deltaAngle = Quaternion.Angle(lastRotation, current);
        // �ۼƽǶ� & ʱ��
        accumulatedAngle += deltaAngle;
        windowTimer += Time.deltaTime;
        lastRotation = current;

        if (accumulatedAngle >= thresholdAngleDeg)
        {
            onAngleExceeded?.Invoke();
            ResetWindow();  // �����ۼƣ�Ҳ�ɲ����ã����裩
        }
        if (windowTimer >= timeWindow)
        {
            ResetWindow();
        }
        // 5. Ӧ����ת��������ָ���ᣩ
        self.rotation = Quaternion.RotateTowards(self.rotation, targetRotation, 999f);
    }
    private void ResetWindow()
    {
        windowTimer = 0f;
        accumulatedAngle = 0f;
        lastRotation = transform.rotation;
    }
}
