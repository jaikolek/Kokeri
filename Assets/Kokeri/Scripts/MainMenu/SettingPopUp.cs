using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPopUp : BasePopUp
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    new private void Start()
    {
        base.Start();

        SetBGMVolume(AudioManager.Instance.GetBGMVolume());
        SetSFXVolume(AudioManager.Instance.GetSFXVolume());
    }

    private void Update()
    {
        OnChangeBGMVolume();
        OnChangeSFXVolume();
    }

    public void OnChangeBGMVolume()
    {
        AudioManager.Instance.SetBGMVolume(GetBGMVolume());
    }

    public void OnChangeSFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(GetSFXVolume());
    }

    public void SetBGMVolume(float _volume)
    {
        bgmSlider.value = _volume;
    }

    public void SetSFXVolume(float _volume)
    {
        sfxSlider.value = _volume;
    }

    public float GetBGMVolume()
    {
        return bgmSlider.value;
    }

    public float GetSFXVolume()
    {
        return sfxSlider.value;
    }
}
