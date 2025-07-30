using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    [Header("AudioSource")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("AudioClips")]
    public AudioClip[] musicClips;
    public AudioClip[] sfxClips;
    private const string MUSIC_MUTE_KEY = "MusicMute";
    private const string Sfx_MUTE_KEY = "SfxMute";

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        musicSource.mute = GetMusicState();
        sfxSource.mute = GetSfxState();
    }

    public void PlayMusic(int id)
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
        }
        musicSource.loop = true;
        musicSource.clip = musicClips[id];
        musicSource.Play();
    }

    public void PlaySfx(int id)
    {
        if (id >= 0 && id < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[id]);
        }
    }

    public void ToggleMusic(bool isMuted)
    {
        musicSource.mute = isMuted;
        PlayerPrefs.SetInt(MUSIC_MUTE_KEY, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ToggleSfx(bool isMuted)
    {
        sfxSource.mute = isMuted;
        PlayerPrefs.SetInt(Sfx_MUTE_KEY, isMuted ? 1 : 0);
        PlayerPrefs.Save();
    }

    public static bool GetMusicState() => PlayerPrefs.GetInt(MUSIC_MUTE_KEY, 0) == 1;
    public static bool GetSfxState() => PlayerPrefs.GetInt(Sfx_MUTE_KEY, 0) == 1;

}
