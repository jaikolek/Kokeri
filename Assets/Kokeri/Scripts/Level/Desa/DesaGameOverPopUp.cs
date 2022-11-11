using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DesaGameOverPopUp : MonoBehaviour
{
    [SerializeField] private Button submitBtn;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI errorText;

    private TMP_InputField nameInputField;

    private void Start()
    {
        submitBtn.onClick.AddListener(OnSubmit);

        nameInputField = GetComponentInChildren<TMP_InputField>();
    }

    public void ShowResult(int _score, int _coin)
    {
        scoreText.text = _score.ToString();
        coinText.text = _coin.ToString();
    }

    public void OnSubmit()
    {
        if (nameInputField.text == "")
        {
            errorText.gameObject.SetActive(true);
            errorText.text = "Ayo tulis nama kamu!";
        }
        else
        {
            DesaEventManager.Instance.UserSubmit(nameInputField.text, int.Parse(scoreText.text));
            gameObject.SetActive(false);
        }
    }
}

