using UnityEngine;

public class HiPlane : MonoBehaviour
{
    [Header("飞行轨迹设置（单位：世界坐标）")]
    public float radiusX = 20f;       // 椭圆的X半轴（左右范围）
    public float radiusZ = 10f;       // 椭圆的Z半轴（前后范围）

    [Header("飞行速度设置")]
    public float rotationSpeed = 1f;  // 每秒转多少圈的角度速度（弧度）

    private Vector3 centerPos;        // 飞行圆心
    private float angle;              // 当前旋转角度（弧度）

    void Start()
    {
        // 设置当前 GameObject 的初始中心为旋转中心
        centerPos = transform.position;
    }

    void Update()
    {
        angle += rotationSpeed * Time.deltaTime;

        // 椭圆轨道上的位置计算
        float x = Mathf.Cos(angle) * radiusX;
        float z = Mathf.Sin(angle) * radiusZ;

        // 设置飞机位置
        transform.position = centerPos + new Vector3(x, 0f, z);

        // 朝飞行方向旋转（始终朝向运动轨迹的切线方向）
        Vector3 forwardDir = new Vector3(-Mathf.Sin(angle) * radiusX, 0f, Mathf.Cos(angle) * radiusZ).normalized;
        transform.rotation = Quaternion.LookRotation(forwardDir);
    }
}
