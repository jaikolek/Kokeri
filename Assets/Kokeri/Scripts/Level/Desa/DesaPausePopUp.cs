using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesaPausePopUp : BasePopUp
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private Button mapBtn;
    [SerializeField] private Button menuBtn;

    new private void Start()
    {
        base.Start();

        mapBtn.onClick.AddListener(OnClickMap);
        menuBtn.onClick.AddListener(OnClickMenu);

        SetBGMVolume(AudioManager.Instance.GetBGMVolume());
        SetSFXVolume(AudioManager.Instance.GetSFXVolume());
    }

    private void Update()
    {
        OnChangeBGMVolume();
        OnChangeSFXVolume();
    }

    public override void OnClickClose()
    {
        base.OnClickClose();
        GameManagerDesa.Instance.HandleResume();
    }

    public void OnClickMap()
    {
        SceneHandler.Instance.LoadScene("MainLevel");
    }

    public void OnClickMenu()
    {
        SceneHandler.Instance.LoadScene("MainMenu");
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
