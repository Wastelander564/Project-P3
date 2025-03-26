using UnityEngine;

public class DetectionAudio : MonoBehaviour
{
    public AudioSource detectionAudio;  // Drag your AudioSource in here (with your detection sound)
    public Detector detector;           // Reference to the Detector script on the player or target
    public float maxPitch = 2.5f;       // Max pitch when fully detected
    public float minPitch = 1.0f;       // Min pitch at no detection
    public float maxVolume = 1.0f;      // Max volume when detected
    public float minVolume = 0.1f;      // Min volume at no detection
    public bool playOnStart = true;     // Option to play sound as soon as detection starts

    private bool isPlaying = false;

    void Start()
    {
        // Optionally start the sound on start if you prefer
        if (playOnStart)
        {
            detectionAudio.Play();
            isPlaying = true;
        }
    }

    void Update()
    {
        // Ensure sound is playing when detection value is non-zero
        if (detector.detectionValue > 0 && !detectionAudio.isPlaying)
        {
            detectionAudio.Play();
            isPlaying = true;
        }
        
        if (detector.detectionValue > 0)
        {
            // Update pitch and volume based on the detection value
            float detectionFactor = detector.detectionValue / 100f;
            detectionAudio.pitch = Mathf.Lerp(minPitch, maxPitch, detectionFactor);
            detectionAudio.volume = Mathf.Lerp(minVolume, maxVolume, detectionFactor);
        }
        else
        {
            // Stop sound if no detection
            if (detectionAudio.isPlaying)
            {
                detectionAudio.Stop();
                isPlaying = false;
            }
        }
    }
}
