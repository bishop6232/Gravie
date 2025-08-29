using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip background;
    public AudioClip coinSFX;
    public AudioClip impactSFX;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }
    private void Start()
    {
        backgroundMusicSource.clip = background;
        backgroundMusicSource.Play();
        backgroundMusicSource.loop = true;
    }
    public void PauseBackgroundMusic()
    {
        backgroundMusicSource.Pause();
    }
    public void PlaySound(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
    public void ResumeBackgroundMusic()
    {
        backgroundMusicSource.Play();

    }
    public void StopBackgroundMusic()
    {
        backgroundMusicSource.Stop();
    }
  
  

}
