using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }


    [Header("Source")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Source -- Alarm")]
    [SerializeField] private AudioSource sfxAlarmSource;

    [Header("BGM")]
    public AudioClip storyBGM;
    public AudioClip mainMenuBGM;
    public AudioClip gameplayBGM;
    public AudioClip lostBGM;

    [Header("SFX")]
    public AudioClip alarm;
    public AudioClip earthquacke;
    public AudioClip tsunami;
    public AudioClip buttonClick;
    public AudioClip switchTV;
    public AudioClip upgrade;
    public AudioClip volcanoEruption;

    public void PlayBGM(AudioClip clip)
    {
        if(clip == null || bgmSource == null) return;

        bgmSource.Stop();
        bgmSource.clip = clip;
        bgmSource.loop = true;
        bgmSource.Play();

    }
    public void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;

        sfxSource.PlayOneShot(clip);

    }
    public void PlayAlarmSFX(bool x)
    {
        if (sfxAlarmSource == null) return;
        sfxAlarmSource.mute = x;

    }
}
