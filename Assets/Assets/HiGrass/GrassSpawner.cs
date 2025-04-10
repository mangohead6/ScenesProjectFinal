using UnityEngine;

public class GrassSpawner : MonoBehaviour
{
    [Header("草的基础设置")]
    public GameObject grassPrefab;   // 草的 prefab
    public int count = 300;          // 草的数量
    public Vector2 areaSize = new Vector2(20f, 20f);  // 生成区域 XZ

    [Header("草的缩放范围")]
    public Vector3 minScale = new Vector3(0.5f, 0.5f, 0.5f); // 最小草尺寸
    public Vector3 maxScale = new Vector3(1.5f, 2f, 1.5f);   // 最大草尺寸（可用来模拟玉米地）

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
            float z = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
            Vector3 pos = new Vector3(x, 0f, z);

            GameObject grass = Instantiate(grassPrefab, pos, Quaternion.Euler(0, Random.Range(0f, 360f), 0), this.transform);

            // 随机缩放（模拟自然高矮、粗细）
            Vector3 scale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z)
            );

            grass.transform.localScale = scale;
        }
    }
}
