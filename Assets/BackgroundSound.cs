using UnityEngine;

public class BackgroundSound : MonoBehaviour
{
    public AudioClip backgroundClip;
    private AudioSource audioSource;

    void Awake()
    {
        // Persist across scenes
        DontDestroyOnLoad(gameObject);

        // Ensure an AudioSource is attached
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.clip = backgroundClip;
        audioSource.loop = true;
    }

    void Start()
    {
        if (backgroundClip)
            audioSource.Play();
    }
}
