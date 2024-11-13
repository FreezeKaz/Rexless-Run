using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Gauge VolumeGauge;
    [SerializeField] private Gauge FXGauge;
    void Start()
    {
        VolumeGauge.OnValueChanged += ChangeMusicVolume;
        FXGauge.OnValueChanged += ChangeFXVolume;
    }

    public void ChangeMusicVolume(int value)
    {
        SoundManager.Instance.SetMusicVolume(value);
    }
    public void ChangeFXVolume(int value)
    {
        SoundManager.Instance.SetFXVolume(value);
    }

    private void OnDestroy()
    {
        VolumeGauge.OnValueChanged -= ChangeMusicVolume;
        FXGauge.OnValueChanged -= ChangeFXVolume;
    }
}
