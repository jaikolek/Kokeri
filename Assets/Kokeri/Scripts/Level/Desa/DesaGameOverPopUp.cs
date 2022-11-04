using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DesaGameOverPopUp : MonoBehaviour
{
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button mapBtn;
    [SerializeField] private Button menuBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(OnClickRestart);
        mapBtn.onClick.AddListener(OnClickMap);
        menuBtn.onClick.AddListener(OnClickMenu);
    }

    public void OnClickRestart()
    {
        SceneHandler.Instance.ReloadScene();
    }

    public void OnClickMap()
    {
        SceneHandler.Instance.LoadScene("MainLevel");
    }

    public void OnClickMenu()
    {
        SceneHandler.Instance.LoadScene("MainMenu");
    }
}

