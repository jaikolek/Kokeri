using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ScoreBoardPopUp : MonoBehaviour
{

    [SerializeField] private GameObject scoreboardContainer;
    [SerializeField] private GameObject boardItemPrefab;

    [SerializeField] private Button restartBtn;
    [SerializeField] private Button mapBtn;
    [SerializeField] private Button menuBtn;

    void Start()
    {
        HutanEventManager.Instance.OnUserSubmit += HutanEventManager_OnUserSubmit;

        restartBtn.onClick.AddListener(OnClickRestart);
        mapBtn.onClick.AddListener(OnClickMap);
        menuBtn.onClick.AddListener(OnClickMenu);
    }

    private void HutanEventManager_OnUserSubmit(string name, int _score)
    {
        gameObject.SetActive(true);

        GameObject boardItem = Instantiate(boardItemPrefab, scoreboardContainer.transform);

        // TODO: get rank
        boardItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "16. " + name;
        boardItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _score.ToString();
    }

    public void OnClickRestart()
    {
        if (GameManagerDesa.Instance != null)
        {
            // GameManagerDesa.Instance.RestartGame();
        }
        else if (HutanGameManager.Instance != null)
        {
            HutanGameManager.Instance.RestartGame();
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


}
