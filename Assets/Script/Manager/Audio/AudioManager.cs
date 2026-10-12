using Unity.AppUI.UI;
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

    [Header("Source -- Outpost")]
    [SerializeField] private AudioSource outpost_1_Source;
    [SerializeField] private AudioSource outpost_2_Source;
    [SerializeField] private AudioSource outpost_3_Source;

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

    public void PlayDisasterSound(int outpostIndex, AudioClip clip)
    {
        switch (outpostIndex)
        {
            case 1:
                outpost_1_Source.Stop();
                outpost_1_Source.clip = clip;
                outpost_1_Source.Play();
                break;
            case 2:
                outpost_2_Source.Stop();
                outpost_2_Source.clip = clip;
                outpost_2_Source.Play();
                break;
            case 3:
                outpost_3_Source.Stop();
                outpost_3_Source.clip = clip;
                outpost_3_Source.Play();
                break;
        }
    }

    public void StopDisasterSound(int outpostIndex)
    {
        switch (outpostIndex)
        {
            case 1:
                outpost_1_Source.Stop();
                outpost_1_Source.clip = null;
                break;
            case 2:
                outpost_2_Source.Stop();
                outpost_2_Source.clip = null;
                break;
            case 3:
                outpost_3_Source.Stop();
                outpost_3_Source.clip = null;
                break;
        }
    }
    public void MuteOtherDisasterOutpost(int outpostIndex)
    {
        switch (outpostIndex)
        {
            case 1:
                outpost_1_Source.mute = false;
                outpost_2_Source.mute = true;
                outpost_3_Source.mute = true;
                break;
            case 2:
                outpost_1_Source.mute = true;
                outpost_2_Source.mute = false;
                outpost_3_Source.mute = true;
                break;
            case 3:
                outpost_1_Source.mute = true;
                outpost_2_Source.mute = true;
                outpost_3_Source.mute = false;
                break;
        }
    }
}
