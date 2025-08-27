using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip background;
    public AudioClip coinSFX;
    public AudioClip impactSFX;


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
}
