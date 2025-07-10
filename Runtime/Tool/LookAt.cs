using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookAt : MonoBehaviour
{
    public Transform Target;
    [Header("触发阈值设置")]
    public float thresholdAngleDeg = 0f;
    public float timeWindow = 0.5f;
    private Quaternion lastRotation;
    private float accumulatedAngle = 0f;
    private float windowTimer = 0f;
    [Header("事件")]
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
    /// 使当前物体仅绕指定轴（如Y轴）朝向目标
    /// </summary>
    /// <param name="self">要旋转的物体Transform</param>
    /// <param name="target">目标Transform</param>
    /// <param name="axis">约束旋转的轴（例如 Vector3.up 为Y轴）</param>
    public  void LookAtOnlyAxis(Transform self, Transform target, Vector3 axis)
    {
        // 1. 计算目标方向向量
        Vector3 direction = target.position - self.position;

        // 2. 清除不需要旋转的分量（例如只绕Y轴旋转，就清除Y分量以保持水平）
        if (axis == Vector3.up)
            direction.y = 0;
        else if (axis == Vector3.right)
            direction.x = 0;
        else if (axis == Vector3.forward)
            direction.z = 0;

        // 3. 若方向为空，直接返回
        if (direction.sqrMagnitude < 0.001f)
            return;

        // 4. 计算目标旋转
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized, axis);

        // 检测角度是否超过阈值
        Quaternion current = transform.rotation;
        float deltaAngle = Quaternion.Angle(lastRotation, current);
        // 累计角度 & 时间
        accumulatedAngle += deltaAngle;
        windowTimer += Time.deltaTime;
        lastRotation = current;

        if (accumulatedAngle >= thresholdAngleDeg)
        {
            onAngleExceeded?.Invoke();
            ResetWindow();  // 重置累计（也可不重置，按需）
        }
        if (windowTimer >= timeWindow)
        {
            ResetWindow();
        }
        // 5. 应用旋转（仅限绕指定轴）
        self.rotation = Quaternion.RotateTowards(self.rotation, targetRotation, 999f);
    }
    private void ResetWindow()
    {
        windowTimer = 0f;
        accumulatedAngle = 0f;
        lastRotation = transform.rotation;
    }
}
