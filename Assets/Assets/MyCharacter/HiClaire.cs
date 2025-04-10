using UnityEngine;

public class HiClaire : MonoBehaviour
{
    public float moveDistance = 1f;             // 每步移动的距离
    public float moveInterval = 1.433f;         // 与动画长度保持一致（秒）

    void Start()
    {
        // 每 moveInterval 秒执行一次 MoveForward 方法
        InvokeRepeating(nameof(MoveForward), 0f, moveInterval);
    }

    void MoveForward()
    {
        // 向角色面朝方向前进 moveDistance
        transform.position += transform.forward * moveDistance;
    }
}
