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
        submitBtn.onClick.AddListener(OnSubmit);

        nameInputField = GetComponentInChildren<TMP_InputField>();
    }

    public void ShowResult(int _score, int _coin, int _bug)
    {
        scoreText.text = _score.ToString();
        coinText.text = _coin.ToString();
        bugText.text = _bug.ToString();
    }

    public void OnSubmit()
    {
        HutanEventManager.Instance.UserSubmit(nameInputField.text, int.Parse(scoreText.text));
        gameObject.SetActive(false);
    }
}
