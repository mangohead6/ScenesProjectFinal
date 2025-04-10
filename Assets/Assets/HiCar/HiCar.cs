using UnityEngine;

public class HiCar : MonoBehaviour
{
    public float moveSpeed = 5f;         // 向前移动速度
    public float turnSpeed = 60f;        // 每秒旋转角度，控制漂移弧度
    public float driftIntensity = 0.3f;  // 漂移偏移程度

    private Vector3 driftOffset;         // 用于漂移方向偏移

    void Update()
    {
        // 漂移偏移 = 右方向偏移一点点（模拟后轮漂移感）
        driftOffset = transform.right * driftIntensity;

        // 向前+偏移的方向推进
        transform.position += (transform.forward + driftOffset).normalized * moveSpeed * Time.deltaTime;

        // 转弯（向左/右旋转）
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }
}
