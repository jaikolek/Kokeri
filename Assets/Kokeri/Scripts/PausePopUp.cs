using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PausePopUp : BasePopUp
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
        if (DesaGameManager.Instance != null)
        {
            DesaGameManager.Instance.HandleResume();
        }
        else if (HutanEventManager.Instance != null)
        {
            HutanEventManager.Instance.GameResumed();
        }
    }

    public void OnClickMap()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void OnClickMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
