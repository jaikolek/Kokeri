using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesaUI : MonoBehaviour
{
    [Header("Pause")]
    [SerializeField] private Button pauseBtn;
    [SerializeField] private GameObject pausePopUp;

    [Header("Control Button")]
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button rightBtn;
    [SerializeField] private Button leftBtn;

    private void Start()
    {
        pauseBtn.onClick.AddListener(OnClickPause);
        upBtn.onClick.AddListener(OnClickUp);
        downBtn.onClick.AddListener(OnClickDown);
        leftBtn.onClick.AddListener(OnClickLeft);
        rightBtn.onClick.AddListener(OnClickRight);
    }

    private void OnClickPause()
    {
        AudioManager.Instance.PlaySFX("Click2");
        GameManagerDesa.Instance.HandlePause();
        pausePopUp.SetActive(true);
    }

    private void OnClickUp()
    {
        AudioManager.Instance.PlaySFX("Click1");
        GameManagerDesa.Instance.HandleUpInput();
    }

    private void OnClickDown()
    {
        AudioManager.Instance.PlaySFX("Click1");
        GameManagerDesa.Instance.HandleDownInput();
    }

    private void OnClickLeft()
    {
        AudioManager.Instance.PlaySFX("Click1");
        GameManagerDesa.Instance.HandleLeftInput();
    }

    private void OnClickRight()
    {
        AudioManager.Instance.PlaySFX("Click1");
        GameManagerDesa.Instance.HandleRightInput();
    }
}
