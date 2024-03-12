using Unity.VisualScripting;
using UnityEngine;

public class CollisionSound : MonoBehaviour
{

    public AudioClip clickSoundClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        CollisionManager.OnTouched += FallenIntoLava;
    }

    void OnDisable()
    {
        CollisionManager.OnTouched -= FallenIntoLava;
    }

    private void FallenIntoLava()
    {
        audioSource.clip = clickSoundClip;
        if (!audioSource.isPlaying || audioSource.clip != clickSoundClip)
        {
            audioSource.Play();
        }
    }
}
