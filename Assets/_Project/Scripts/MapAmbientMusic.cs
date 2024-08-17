using System;
using UnityEngine;

public class MapAmbientMusic : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // TODO Ayarlardaki Volume e gore ayarla
        _audioSource.volume = 0.5f;
    }
}
