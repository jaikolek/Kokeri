using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playBtn;
    [SerializeField] private Button settingBtn;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button rankingBtn;

    [Header("Pop Ups")]
    [SerializeField] private GameObject settingPopUp;
    [SerializeField] private GameObject shopPopUp;
    [SerializeField] private GameObject rankingPopUp;

    private void Start()
    {
        playBtn.onClick.AddListener(OnClickPlay);
        settingBtn.onClick.AddListener(OnClickSetting);
        shopBtn.onClick.AddListener(OnClickShop);
        rankingBtn.onClick.AddListener(OnClickRanking);

        // pop up init
        settingPopUp.SetActive(false);
        shopPopUp.SetActive(false);
        rankingPopUp.SetActive(false);

        AudioManager.Instance.PlayBGM("MainMenu");
    }

    private void OnClickPlay()
    {
        AudioManager.Instance.PlaySFX("Click1");
        SceneHandler.Instance.LoadScene("MainLevel");
    }

    private void OnClickSetting()
    {
        AudioManager.Instance.PlaySFX("Click2");
        settingPopUp.SetActive(true);
    }

    private void OnClickShop()
    {
        AudioManager.Instance.PlaySFX("Click2");
        shopPopUp.SetActive(true);
    }

    private void OnClickRanking()
    {
        AudioManager.Instance.PlaySFX("Click2");
        rankingPopUp.SetActive(true);
    }
}
