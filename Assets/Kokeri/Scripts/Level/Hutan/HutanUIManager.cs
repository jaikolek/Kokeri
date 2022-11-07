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
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.parent = GameObject.Find("ManagerContainer").transform;
                    obj.name = typeof(HutanUIManager).Name;
                    instance = obj.AddComponent<HutanUIManager>();
                }
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
    [SerializeField] private GameObject pausePopUp;
    [SerializeField] private GameObject gameOverPopUp;
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

    private void Start()
    {
        HutanEventManager.Instance.OnCharacterChanged += HutanEventManager_OnCharacterChanged;
        HutanEventManager.Instance.OnCollectRange += HutanEventManager_OnCollectRange;

        HutanEventManager.Instance.OnGamePaused += HutanEventManager_OnGamePaused;
        HutanEventManager.Instance.OnGameResumed += HutanEventManager_OnGameResumed;
        HutanEventManager.Instance.OnGameOver += HutanEventManager_OnGameOver;

        pauseBtn.onClick.AddListener(() => HutanEventManager.Instance.GamePaused());

        upBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Jump());

        downBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Crouch());
        downBtn.GetComponent<ButtonPointerUpListener>().onPointerUp.AddListener(() => HutanEventManager.Instance.Stand());

        catchBtn.GetComponent<ButtonPointerDownListener>().onPointerDown.AddListener(() => HutanEventManager.Instance.Catch());

        catchBtn.interactable = false;

        gameUI.SetActive(false);
        chooseCharacterPopUp.SetActive(true);
    }

    private void HutanEventManager_OnCharacterChanged(Character _character)
    {
        gameUI.SetActive(true);
        chooseCharacterPopUp.SetActive(false);

        StartCoroutine(CountDown());

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
    }

    private IEnumerator CountDown()
    {
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
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

        if (_character == Character.Chiko)
        {
            healthContainer.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (_character == Character.Ketti)
        {
            healthContainer.transform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            healthContainer.transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
