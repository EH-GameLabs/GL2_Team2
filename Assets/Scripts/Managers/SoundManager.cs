using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;

    [Header("Wav")]
    public AudioClip playerStep;
    public AudioClip grunt;
    public AudioClip trainBell;
    public AudioClip skin;

    private static SoundManager Instance;
    public static SoundManager instance { get { return Instance; } }

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ApplyVolumeSettings(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}

