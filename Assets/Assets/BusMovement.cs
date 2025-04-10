using UnityEngine;

public class BusMovement : MonoBehaviour
{
    // 拉伸程度
    public float stretchAmount = 0.1f;
    // 拉伸频率
    public float stretchFrequency = 2f;
    // 移动速度
    public float moveSpeed = 2f;
    // 巴士的默认缩放比例
    private float defaultScale = 8.1422f;

    private Vector3[] waypoints = new Vector3[]
    {
        new Vector3(4.022341f, 9.11f, 283f),
        new Vector3(11.3f, 9.11f, 165f),
        new Vector3(11.3f, 9.11f, -24.7f),
        new Vector3(-11.6f, 9.11f, -152.7f),
        new Vector3(-11.6f, 9.11f, -278.1f),
        new Vector3(18.9f, 9.11f, -378.5f),
              new Vector3(-11.6f, 9.11f, -278.1f),        new Vector3(-11.6f, 9.11f, -152.7f),        new Vector3(11.3f, 9.11f, -24.7f),        new Vector3(11.3f, 9.11f, 165f),
            new Vector3(4.022341f, 9.11f, 283f),
    };

    private int currentWaypointIndex = 0;
    private bool movingForward = true;
    private bool rotating = false;
    private float rotationStartTime;
    private Quaternion startRotation;

    void Update()
    {
        // 模拟拉伸效果，基于默认缩放比例
        float stretch = Mathf.Sin(Time.time * stretchFrequency) * stretchAmount;
        transform.localScale = new Vector3(defaultScale + stretch, defaultScale + stretch, defaultScale);

        if (rotating)
        {
            float rotationProgress = (Time.time - rotationStartTime) / (1 / moveSpeed);
            // 旋转 180 度而不是 360 度
            transform.rotation = Quaternion.Slerp(startRotation, Quaternion.Euler(0, 180, 0), rotationProgress);
            if (rotationProgress >= 1)
            {
                rotating = false;
                movingForward = false;
                currentWaypointIndex = waypoints.Length - 1;
            }
        }
        else
        {
            // 移动逻辑
            if (currentWaypointIndex < waypoints.Length && currentWaypointIndex >= 0)
            {
                Vector3 target = waypoints[currentWaypointIndex];
                transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

                // 让巴士朝向目标点，旋转 180 度
                if (transform.position != target)
                {
                    Vector3 direction = target - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    // 调整朝向旋转 180 度
                    lookRotation *= Quaternion.Euler(0, 180, 0);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * moveSpeed);
                }

                if (transform.position == target)
                {
                    if (movingForward)
                    {
                        if (currentWaypointIndex == waypoints.Length - 1)
                        {
                            rotating = true;
                            rotationStartTime = Time.time;
                            startRotation = transform.rotation;
                        }
                        else
                        {
                            currentWaypointIndex++;
                        }
                    }
                    else
                    {
                        if (currentWaypointIndex == 0)
                        {
                            movingForward = true;
                        }
                        else
                        {
                            currentWaypointIndex--;
                        }
                    }
                }
            }
        }
    }
}