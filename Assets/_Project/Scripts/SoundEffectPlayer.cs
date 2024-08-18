using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;

    public static SoundEffectPlayer Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void PlaySoundEffect(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}