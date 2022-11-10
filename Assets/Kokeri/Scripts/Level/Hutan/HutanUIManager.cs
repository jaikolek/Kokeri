using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HutanUIManager : MonoBehaviour
{
    #region singleton
    private static HutanUIManager instance;
    public static HutanUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<HutanUIManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion singleton
    // ====================================================================================================


    // ====================================================================================================
    [Header("UI")]
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject chooseCharacterPopUp;
    [SerializeField] private GameObject gameOverPopUp;
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private GameObject scoreBoardPopUp;
    [SerializeField] private TextMeshProUGUI countDownText;

    [Header("Input")]
    [SerializeField] private Button upBtn;
    [SerializeField] private Button downBtn;
    [SerializeField] private Button catchBtn;
    [SerializeField] private Button pauseBtn;

    [Header("Bug")]
    [SerializeField] private TextMeshProUGUI bugText;

    [Header("Health")]
    [SerializeField] private GameObject healthContainer;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header("State")]
    [SerializeField] private TextMeshProUGUI hitText;
    [SerializeField] private TextMeshProUGUI catchText;

    private void Start()
    {
        HutanEventManager.Instance.OnGameStarted += HutanEventManager_OnGameStarted;
        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;
        HutanEventManager.Instance.OnCollectRange += HutanEventManager_OnCollectRange;
        HutanEventManager.Instance.OnGamePaused += HutanEventManager_OnGamePaused;
        HutanEventManager.Instance.OnGameResumed += HutanEventManager_OnGameResumed;
        HutanEventManager.Instance.OnGameOver += HutanEventManager_OnGameOver;

        HutanEventManager.Instance.OnUserSubmit += HutanEventManager_OnUserSubmit;

        pauseBtn.onClick.AddListener(() =>
        {
            AudioManager.Instance.PlaySFX("Click2");
            HutanEventManager.Instance.GamePaused();
        });

        upBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Jump());

        downBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Crouch());
        downBtn.GetComponent<ButtonPointerUpListener>().onPointerUp.AddListener(() => HutanEventManager.Instance.Stand());

        catchBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Catch());

        catchBtn.interactable = false;

        gameUI.SetActive(false);
        chooseCharacterPopUp.SetActive(true);
    }

    private void HutanEventManager_OnGameStarted()
    {
        upBtn.interactable = true;
        downBtn.interactable = true;

        HutanEventManager.Instance.OnGameStarted -= HutanEventManager_OnGameStarted;
    }

    private void HutanEventManager_OnCharacterChanged(Character _character)
    {
        chooseCharacterPopUp.SetActive(false);
        gameUI.SetActive(true);

        upBtn.interactable = false;
        downBtn.interactable = false;

        StartCoroutine(Countdown());

        HutanEventManager.Instance.OnCharacterChanged -= HutanEventManager_OnCharacterChanged;
    }

    private void HutanEventManager_OnCollectRange(bool _state)
    {
        if (_state)
        {
            catchBtn.interactable = true;
        }
        else
        {
            catchBtn.interactable = false;
        }
    }

    private void HutanEventManager_OnGamePaused()
    {
        pausePopUp.SetActive(true);
    }

    private void HutanEventManager_OnGameResumed()
    {
        pausePopUp.SetActive(false);
    }

    private void HutanEventManager_OnGameOver(int _score, int _coin, int _bug)
    {
        gameOverPopUp.SetActive(true);
        gameOverPopUp.GetComponent<HutanGameOverPopUp>().ShowResult(_score, _coin, _bug);

        HutanEventManager.Instance.OnGamePaused -= HutanEventManager_OnGamePaused;
        HutanEventManager.Instance.OnGameResumed -= HutanEventManager_OnGameResumed;
    }

    private void HutanEventManager_OnUserSubmit(string _name, int _score)
    {
        scoreBoardPopUp.SetActive(true);
        scoreBoardPopUp.GetComponent<ScoreBoardPopUp>().ShowResultHutan(_name, _score);
    }

    private IEnumerator Countdown()
    {
        countDownText.gameObject.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countDownText.text = Convert.ToString(i);
            yield return new WaitForSeconds(1);
        }
        countDownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countDownText.gameObject.SetActive(false);

        HutanGameManager.Instance.IsGameReady = true;
        HutanEventManager.Instance.GameStarted();
    }

    public void UpdateBug(int _bug)
    {
        this.bugText.text = _bug.ToString();
    }

    public void UpdateHealth(int _health, Character _character)
    {
        healthText.text = _health.ToString() + "X";

        if (_character == Character.CHIKO)
        {
            healthContainer.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (_character == Character.KETTI)
        {
            healthContainer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            healthContainer.transform.GetChild(2).gameObject.SetActive(false);
        }
    }

    public IEnumerator ShowState(string _state)
    {
        // TODO: refactor this later
        if (_state == "hit")
        {
            hitText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            hitText.gameObject.SetActive(false);
        }
        else if (_state == "catch")
        {
            catchText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            catchText.gameObject.SetActive(false);
        }
    }
}
