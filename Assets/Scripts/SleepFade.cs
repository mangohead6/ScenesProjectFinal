using UnityEngine;
using TMPro; 
using System.Collections; 

public class SleepFadeOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;   // Reference to the CanvasGroup (used for fading)
    public float fadeDuration = 3f;   // Duration of the fade-out
    public TextMeshProUGUI text;      // Reference to the TextMeshProUGUI text component

    private void Start()
    {
        // Start the fade-out process after a delay
        Invoke("StartFadeOut", 3f); // Adjust the delay to your preference
    }

    private void StartFadeOut()
    {
        StartCoroutine(FadeOut());  // Start the coroutine to fade out
    }

    private IEnumerator FadeOut()
    {
        float startAlpha = canvasGroup.alpha;
        float timeElapsed = 0f;

        // Fade the panel and text out to transparent
        while (timeElapsed < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 0f, timeElapsed / fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;  // Ensure Unity continues the coroutine over multiple frames
        }

        
        canvasGroup.alpha = 0f;
    }
}
