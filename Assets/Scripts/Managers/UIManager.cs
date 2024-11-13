using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SoundManager;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator _UIAnimator;

    public event Action OnScreenDark;


    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void EndGameUIActivate()
    {
        _UIAnimator.SetBool("EndGameUI", true);
        ShowUI();
    }

    public void EndGameUIDeactivate()
    {
      
        _UIAnimator.SetBool("EndGameUI", false);
    }
    public void TakeOffUI()
    {
        SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.UIButton]);
        _UIAnimator.SetBool("DisplayUI", false);
    }
    public void ShowUI()
    {

      
        _UIAnimator.SetBool("DisplayUI", true);
    }
    public void ScreenIsDarkening()
    {
        SoundManager.Instance.PlayMusic(SoundManager.Instance.musicClips[(int)Musics.Menu]);
        OnScreenDark?.Invoke();
    }
    public void ShowSettings()
    {
        SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.UIButton]);
        _UIAnimator.SetBool("Settings", true);
    }

    public void TurnOffSettings()
    {
        SoundManager.Instance.PlayFX(SoundManager.Instance.sfxClips[(int)SfxClipType.UIButton]);
        _UIAnimator.SetBool("Settings", false);
    }
}
