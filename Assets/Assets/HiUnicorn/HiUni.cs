using UnityEngine;
using System.Collections;

public class UnicornAnimation : MonoBehaviour
{
    public float bounceSpeed = 2f;
    public float bounceHeight = 0.5f;
    public float rotationSpeed = 60f;
    public string materialName = "Light1";

    public float emissionIntensity = 1f;
    public float colorChangeInterval = 1f;

    private float startY;
    private Material targetMaterial;

    void Start()
    {
        startY = transform.position.y;

        // 查找使用指定材质的Renderer组件
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                if (material.name.Contains(materialName))
                {
                    targetMaterial = material;
                    break;
                }
            }
            if (targetMaterial != null)
            {
                break;
            }
        }

        // 开始颜色更换协程
        StartCoroutine(ChangeColorPeriodically());
    }

    void Update()
    {
        // 弹跳效果
        float newY = startY + Mathf.Sin(Time.time * bounceSpeed) * bounceHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // 顺时针旋转效果
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
    }

    IEnumerator ChangeColorPeriodically()
    {
        while (true)
        {
            if (targetMaterial != null)
            {
                Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
                targetMaterial.SetColor("_EmissionColor", randomColor * emissionIntensity);
                targetMaterial.EnableKeyword("_EMISSION");
            }

            // 等待指定的时间间隔
            yield return new WaitForSeconds(colorChangeInterval);
        }
    }
}    