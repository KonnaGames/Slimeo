using System;
using UnityEngine;

public class MapAmbientMusic : MonoBehaviour
{
    public static MapAmbientMusic Instance;
    [SerializeField] private AudioSource _audioSource;

    private void Start()
    {
        Instance = this;
        // TODO Ayarlardaki Volume e gore ayarla
        _audioSource.volume = 0.5f;
    }

    public void ChangeMusic(AudioClip clip)
    {
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}