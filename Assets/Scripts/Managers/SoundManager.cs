using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource fxSource;
    [SerializeField] public List<AudioClip> sfxClips;
    public enum SfxClipType { UIButton, Jump, Hurt, Die};
    public enum Musics { Menu, Game};

    public SfxClipType sfxClip;    
    public Musics musicClip;  
    
    [SerializeField] public List<AudioClip> musicClips;

    [Range(0f, 0.15f)] public float musicVolume = 0.15f;
    [Range(0f, 1f)] public float fxVolume = 1f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        musicSource.volume = musicVolume;
        fxSource.volume = fxVolume;
    }

    public void PlayMusic(AudioClip musicClip)
    {
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void PlayFX(AudioClip fxClip)
    {
        fxSource.PlayOneShot(fxClip);
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = (volume) / (5f - 1f) * 0.15f;
        PlayFX(sfxClips[(int)SfxClipType.UIButton]);
    } 



    public void SetFXVolume(int volume)
    {
        fxVolume = (volume) / (5f - 1f);
        PlayFX(sfxClips[(int)SfxClipType.UIButton]);
    }

}