using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HutanGameOverPopUp : MonoBehaviour
{
    [SerializeField] private Button restartBtn;

    private void Start()
    {
        restartBtn.onClick.AddListener(OnClickRestart);
    }

    public void OnClickRestart()
    {
        HutanGameManager.Instance.RestartGame();
    }
}
