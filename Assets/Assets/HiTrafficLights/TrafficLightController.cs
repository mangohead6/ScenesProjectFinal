using System.Collections;
using UnityEngine;

public class TrafficLightController : MonoBehaviour
{
    public Renderer redLight;
    public Renderer yellowLight;
    public Renderer greenLight;

    public Material redMat;
    public Material yellowMat;
    public Material greenMat;
    public Material offMat;

    void Start()
    {
        StartCoroutine(ControlTrafficLights());
    }

    IEnumerator ControlTrafficLights()
    {
        while (true)
        {
            // 🔴 Red light on
            SetLights(redMat, offMat, offMat);
            yield return new WaitForSeconds(8f);

            // 🟢 Green light on
            SetLights(offMat, offMat, greenMat);
            yield return new WaitForSeconds(8f);

            // 🟡 Yellow light on
            SetLights(offMat, yellowMat, offMat);
            yield return new WaitForSeconds(2f);
        }
    }

    void SetLights(Material red, Material yellow, Material green)
    {
        redLight.material = red;
        yellowLight.material = yellow;
        greenLight.material = green;
    }
}
