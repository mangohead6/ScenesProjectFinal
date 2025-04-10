using UnityEngine;

public class HiGrass : MonoBehaviour
{
    // 控制整体“风感”的大小
    public float windIntensity = 1f;

    // 每轴的摆动幅度范围
    public Vector3 rotationRange = new Vector3(10f, 10f, 10f);
    public Vector3 scaleRange = new Vector3(0.05f, 0.05f, 0.05f); // 缩放弹性范围

    private Vector3 baseScale;
    private Vector3 noiseOffset; // 保证每棵草动得不一样

    void Start()
    {
        baseScale = transform.localScale;

        // 给每棵草一个不同的偏移，避免同步抖
        noiseOffset = new Vector3(
            Random.Range(0f, 100f),
            Random.Range(0f, 100f),
            Random.Range(0f, 100f)
        );
    }

    void Update()
    {
        float time = Time.time * windIntensity;

        // 🎐 每轴根据 Perlin Noise 随时间波动
        float rotX = (Mathf.PerlinNoise(time + noiseOffset.x, 0f) - 0.5f) * 2f * rotationRange.x;
        float rotY = (Mathf.PerlinNoise(time + noiseOffset.y, 10f) - 0.5f) * 2f * rotationRange.y;
        float rotZ = (Mathf.PerlinNoise(time + noiseOffset.z, 20f) - 0.5f) * 2f * rotationRange.z;

        transform.localRotation = Quaternion.Euler(rotX, rotY, rotZ);

        // 🌱 模拟被风压缩的样子（scale 随时间抖动）
        float scaleX = baseScale.x + (Mathf.PerlinNoise(time + noiseOffset.x, 30f) - 0.5f) * 2f * scaleRange.x;
        float scaleY = baseScale.y + (Mathf.PerlinNoise(time + noiseOffset.y, 40f) - 0.5f) * 2f * scaleRange.y;
        float scaleZ = baseScale.z + (Mathf.PerlinNoise(time + noiseOffset.z, 50f) - 0.5f) * 2f * scaleRange.z;

        transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }
}
