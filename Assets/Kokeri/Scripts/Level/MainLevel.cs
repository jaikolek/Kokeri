using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainLevel : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button backBtn;
    [SerializeField] private Button shopBtn;
    [SerializeField] private Button desaBtn;
    [SerializeField] private Button lautBtn;
    [SerializeField] private Button hutanBtn;

    [Header("Pop Ups")]
    [SerializeField] private GameObject shopPopUp;

    [Header("Level")]
    [SerializeField] private GameObject desaHTP;
    
    [Header("Level Laut")]
    [SerializeField] private GameObject lautHTP;

    private void Start()
    {
        backBtn.onClick.AddListener(OnClickBack);
        shopBtn.onClick.AddListener(OnClickShop);

        desaBtn.onClick.AddListener(OnClickDesa);
        lautBtn.onClick.AddListener(OnClickLaut);
        hutanBtn.onClick.AddListener(OnClickHutan);

        shopPopUp.SetActive(false);
    }

    private void OnClickBack()
    {
        AudioManager.Instance.PlaySFX1("Click2");

        SceneHandler.Instance.LoadScene("MainMenu");
    }

    private void OnClickShop()
    {
        AudioManager.Instance.PlaySFX1("Click2");

        shopPopUp.SetActive(true);
    }

    private void OnClickDesa()
    {
        AudioManager.Instance.PlaySFX1("Click2");

        desaHTP.SetActive(true);
    }

    private void OnClickLaut()
    {
        AudioManager.Instance.PlaySFX1("Click2");
        lautHTP.SetActive(true);
        // SceneHandler.Instance.LoadScene("LevelLaut");
    }

    private void OnClickHutan()
    {
        AudioManager.Instance.PlaySFX1("Click2");

        SceneHandler.Instance.LoadScene("LevelHutan");
    }
}
