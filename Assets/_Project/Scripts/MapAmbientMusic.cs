using System;
using UnityEngine;

public class MapAmbientMusic : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        // TODO Ayarlardaki Volume e gore ayarla
        _audioSource.volume = 0.5f;
    }
}