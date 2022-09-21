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

    private void Start()
    {
        backBtn.onClick.AddListener(OnClickBack);
        shopBtn.onClick.AddListener(OnClickShop);

        // pop up init
        shopPopUp.SetActive(false);
    }

    private void OnClickBack()
    {
        AudioManager.Instance.PlaySFX("Click2");
        SceneHandler.Instance.LoadScene("MainMenu");
    }

    private void OnClickShop()
    {
        AudioManager.Instance.PlaySFX("Click2");
        shopPopUp.SetActive(true);
    }
}
