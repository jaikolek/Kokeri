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

    public void ShowResultDesa(string _name, int _score)
    {
        GameObject boardItem = Instantiate(boardItemPrefab, scoreboardContainer.transform);

        // TODO: get rank
        boardItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "01. " + _name;
        boardItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _score.ToString();
    }

    public void ShowResultHutan(string _name, int _score)
    {
        GameObject boardItem = Instantiate(boardItemPrefab, scoreboardContainer.transform);

        // TODO: get rank
        boardItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "01. " + _name;
        boardItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _score.ToString();
    }

    public void ShowResultLaut(string _name, int _score)
    {
        GameObject boardItem = Instantiate(boardItemPrefab, scoreboardContainer.transform);

        // TODO: get rank
        boardItem.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "01. " + _name;
        boardItem.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = _score.ToString();
    }

    public void OnClickRestart()
    {
        AudioManager.Instance.PlaySFX("Click2");

        SceneHandler.Instance.ReloadScene();
    }

    public void OnClickMap()
    {
        AudioManager.Instance.PlaySFX("Click2");

        SceneHandler.Instance.LoadScene("MainLevel");
    }

    public void OnClickMenu()
    {
        AudioManager.Instance.PlaySFX("Click2");

        SceneHandler.Instance.LoadScene("MainMenu");
    }
}
