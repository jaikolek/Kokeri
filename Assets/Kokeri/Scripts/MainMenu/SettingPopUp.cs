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

        bgmSlider.value = AudioManager.Instance.GetBGMVolume();
        sfxSlider.value = AudioManager.Instance.GetSFXVolume();
    }

    private void Update()
    {
        OnChangeBGMVolume();
        OnChangeSFXVolume();
    }

    public void OnChangeBGMVolume()
    {
        AudioManager.Instance.SetBGMVolume(GetBgmVolume());
    }

    public void OnChangeSFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(GetSfxVolume());
    }

    public float GetBgmVolume()
    {
        return bgmSlider.value;
    }

    public float GetSfxVolume()
    {
        return sfxSlider.value;
    }
}
