using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HutanGameOverPopUp : MonoBehaviour
{
    [SerializeField] private Button submitBtn;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI bugText;

    private TMP_InputField nameInputField;

    private void Start()
    {
        HutanEventManager.Instance.OnGameOver += HutanEventManager_OnGameOver;
        HutanEventManager.Instance.OnUserSubmit += HutanEventManager_OnUserSubmit;

        submitBtn.onClick.AddListener(OnSubmit);

        nameInputField = GetComponentInChildren<TMP_InputField>();
    }

    private void HutanEventManager_OnGameOver(int _score, int _coin, int _bug)
    {
        gameObject.SetActive(true);

        scoreText.text = _score.ToString();
        coinText.text = _coin.ToString();
        bugText.text = _bug.ToString();
    }

    private void HutanEventManager_OnUserSubmit(string name, int _score)
    {
        gameObject.SetActive(false);
    }

    public void OnSubmit()
    {
        HutanEventManager.Instance.UserSubmit(nameInputField.text, int.Parse(scoreText.text));
    }
}
