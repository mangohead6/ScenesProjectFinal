using UnityEngine;
using TMPro;

public class ArtifactCollector : MonoBehaviour
{
    public TextMeshProUGUI artifactCounterText;
    public AudioSource audioSource;  // AudioSource component to play the sound
    public AudioClip yaySound;      // The "yay" sound clip to play

    private int artifactsCollected = 0;
    private int totalArtifacts = 5;

    void Start()
    {
        UpdateUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Artifact"))
        {
            artifactsCollected++;
            UpdateUI();
            Destroy(other.gameObject); // Remove the artifact from the scene

            // Play the "yay" sound
            audioSource.PlayOneShot(yaySound);

            if (artifactsCollected >= totalArtifacts)
            {
                // Optional: Trigger something, like loading a new scene or opening a portal
                Debug.Log("All artifacts collected! Ready for the next dimension!");
            }
        }
    }

    void UpdateUI()
    {
        artifactCounterText.text = $"Collect artifacts to move to next dimension: {artifactsCollected}/{totalArtifacts}";
    }
}
