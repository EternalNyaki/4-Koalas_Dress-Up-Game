using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOneShotTrigger : MonoBehaviour
{
    public AudioClip audioClip;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
