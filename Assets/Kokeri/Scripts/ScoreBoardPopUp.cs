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
        restartBtn.onClick.AddListener(OnClickRestart);
        mapBtn.onClick.AddListener(OnClickMap);
        menuBtn.onClick.AddListener(OnClickMenu);
    }

    public void ShowResultHutan(string _name, int _score)
    {
        gameObject.SetActive(true);

        GameObject boardItem = Instantiate(boardItemPrefab, scoreboardContainer.transform);

        // TODO: get rank
        boardItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "16. " + _name;
        boardItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _score.ToString();
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
