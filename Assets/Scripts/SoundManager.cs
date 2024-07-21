using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SFX
{
    UIClick, BlockExplode, GameOver, GameStart, Trap, Collectable, CountDown, SensorBlast
}
[System.Serializable]
public class Sounds
{
    public SFX sfx;
    public AudioClip[] clips;
    public Vector2 pitchRange = new(1, 1);
    public float volumeMultiplier = 1;
}

public class SoundManager : Singleton<SoundManager>
{
    [Header("Mute")]
    [SerializeField]
    private bool muted;
    public bool Muted { get { return muted; } }
    public bool SetMute { set { muted = value; ToggleAudio(muted); } }

    [Header("BGM")]
    [SerializeField]
    private AudioClip normalBGM;
    [SerializeField]
    private AudioClip explosionBGM;
    [SerializeField]
    private AudioSource m_musicSource;
    [SerializeField, Range(0f, 1f)]
    private float bgmVolume;
    public float MusicVolume { get { return bgmVolume; } set { bgmVolume = m_musicSource.volume = value; } }

    [Header("SFX")]
    [SerializeField]
    private AudioSource m_sfxSource;
    [SerializeField, Range(0f, 1f)]
    private float sfxVolume;
    public float EffectsVolume { get { return sfxVolume; } set { sfxVolume = m_sfxSource.volume = value; } }

    [Header("Clips")]
    [SerializeField]
    private List<Sounds> m_sounds;

    private void OnEnable()
    {
        if (m_sfxSource == null)
            m_sfxSource = GetComponent<AudioSource>();
        TimeBasedTrap.SwitchToSpike += SwitchToExplosion;
      
    }
    private void OnLevelWasLoaded()
    {
        PlayMusic();
    }
    private void OnDisable()
    {
        TimeBasedTrap.SwitchToSpike += SwitchToExplosion;
    }
    public void PlaySoundEffect(SFX sfx)
    {
        if (muted) return;

        foreach (var sound in m_sounds)
        {
            if (sound.sfx == sfx)
            {
                int r = Random.Range(0, sound.clips.Length);
                if (!m_sfxSource.isPlaying)
                {
                    m_sfxSource.clip = sound.clips[r];
                    m_sfxSource.volume = sfxVolume * sound.volumeMultiplier;
                    m_sfxSource.pitch = Eerp(sound.pitchRange.x, sound.pitchRange.y);
                    m_sfxSource.Play();
                }
                else
                {
                    AudioSource tempSource = Instantiate(m_sfxSource);
                    //tempAudioSource.transform.parent = transform;
                    //AudioSource tempSource = tempAudioSource.AddComponent<AudioSource>();
                    tempSource.playOnAwake = false;
                    tempSource.clip = sound.clips[r];
                    tempSource.pitch = Eerp(sound.pitchRange.x, sound.pitchRange.y);
                    tempSource.volume = sfxVolume * sound.volumeMultiplier;
                    tempSource.Play();
                    StartCoroutine(WaitForSFX(tempSource.clip.length, tempSource));
                }
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        PlayMusic();

    }

    public void PlayTest()
    {
        PlaySoundEffect(SFX.UIClick);
    }

    IEnumerator WaitForSFX(float delay, AudioSource source)
    {
        yield return new WaitForSeconds(delay);
        if (source != null)
        {
            Destroy(source.gameObject);
        }
    }
    void ToggleAudio(bool mute)
    {
        if (mute)
        {
            m_musicSource.Pause();
        }
        else
        {
            m_musicSource.Play();
        }
    }
    private void SwitchToExplosion()
    {
        m_musicSource.clip = explosionBGM;
        m_musicSource.Play();
    }
    public void PlayMusic()
    {
        m_musicSource.clip = normalBGM;
        m_musicSource.loop = true;
        m_musicSource.playOnAwake = true;
        m_musicSource.volume = bgmVolume;
        m_musicSource.Play();
    }
    // interpolates in log scale (multiplicatively linear)
    static float Eerp(float a, float b)
    {
        float t = Random.Range(0, 1);
        return a * System.MathF.Exp(t * System.MathF.Log(b / a));
    }
}