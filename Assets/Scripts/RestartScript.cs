using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartScript : MonoBehaviour
{
    public AudioClip clickSoundClip;

    private AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = clickSoundClip;
    }

    void OnEnable()
    {
        EventManager.OnClicked += Restart;
    }

    void OnDisable()
    {
        EventManager.OnClicked -= Restart;
    }

    void Restart()
    {
        audioSource.clip = clickSoundClip;
        transform.position = BallController.initialPosition;
        if (!audioSource.isPlaying || audioSource.clip != clickSoundClip)
        {
            audioSource.Play();
        }
    }
}
